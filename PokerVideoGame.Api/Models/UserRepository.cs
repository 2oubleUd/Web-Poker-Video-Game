using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using PokerVideoGame.Models.Data.Settings;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PokerVideoGame.Api.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenSettings _tokenSettings;
        public UserRepository(AppDbContext appDbContext, IOptions<TokenSettings> tokenSettings)
        {
            _appDbContext = appDbContext;
            _tokenSettings = tokenSettings.Value;

        }

        private User FromUserRegistrationModelToUserModel(UserRegistrationDto userRegistration)
        {
            return new User
            {
                Email = userRegistration.Email,
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName,
                Password = userRegistration.Password,
                AccountBalance = 0

            };
        }

        private string HashPassword(string plainPassoword)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var rfcPassword = new Rfc2898DeriveBytes(plainPassoword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassword.GetBytes(20);

            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);

            return Convert.ToBase64String(passwordHash);
        }

        public async Task<(bool IsUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration)
        {
            var isUserExist = _appDbContext.User.Any(x => x.Email.ToLower() == userRegistration.Email.ToLower());

            if (isUserExist)
            {
                return (false, "Email address already registered");
            }

            var newUser = FromUserRegistrationModelToUserModel(userRegistration);
            newUser.Password = HashPassword(newUser.Password);

            _appDbContext.User.Add(newUser);
            await _appDbContext.SaveChangesAsync();
            return (true, "New user registered successfuly");
        }

        public bool CheckUserUniqueEmail(string email)
        {
            var userAlreadyExists = _appDbContext.User.Any(x => x.Email.ToLower() == email.ToLower());

            return !userAlreadyExists;
        }

        private string GenerateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("Sub", user.Id.ToString()));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));
            claims.Add(new Claim("Email", user.Email));

            var securityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private async Task<string> GenerateRefreshToken(int userId)
        {
            byte[] bytesOfToken = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytesOfToken);
            }
            var token = Convert.ToBase64String(bytesOfToken);

            var refreshToken = new UserRefreshToken
            {
                ExpirationDate = DateTime.UtcNow.AddMinutes(1),
                Token = token,
                UserId = userId
            };

            _appDbContext.userRefreshToken.Add(refreshToken);

            await _appDbContext.SaveChangesAsync();

            return token;

        }
        public async Task<(bool IsLoginSuccess, JwtTokenResponseDto tokenResponse)> LoginAsync(LoginDto loginPayload)
        {
            if (string.IsNullOrEmpty(loginPayload.Email) || string.IsNullOrEmpty(loginPayload.Password))
            {
                return (false, null);
            }

            var user = await _appDbContext.User.Where(x => x.Email.ToLower() == loginPayload.Email.ToLower()).FirstOrDefaultAsync();

            if (user == null)
            {
                return (false, null);
            }

            bool validPassword = PasswordVerification(loginPayload.Password, user.Password);

            if (!validPassword)
            {
                return (false, null);
            }

            string jwtAccessToken = GenerateJwtToken(user);

            string refreshToken = await GenerateRefreshToken(user.Id);

            var result = new JwtTokenResponseDto
            {
                AccessToken = jwtAccessToken,
                RefreshToken = refreshToken
            };

            return (true, result);

        }

        private bool PasswordVerification(string plainPassword, string dbPassword)
        {
            byte[] dbPasswordHash = Convert.FromBase64String(dbPassword);

            byte[] salt = new byte[16];
            Array.Copy(dbPasswordHash, 0, salt, 0, 16);

            var rfcPassword = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassword.GetBytes(20);

            for (int i = 0; i < rfcPasswordHash.Length; i++)
            {
                if (dbPasswordHash[i + 16] != rfcPasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<User> UpdateUserAsync(UpdateUserMoneyDto user)
        {
            var result = await GetUserAsync(user.UserId);

            if (result != null)
            {
                result.AccountBalance = result.AccountBalance + user.AmountOfMoney;

                _appDbContext.SaveChanges();

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetUsersListAsync()
        {
            return await _appDbContext.User.ToListAsync();
        }


        public async Task<(string ErrorMessage, JwtTokenResponseDto jwtTokenRespone)> RenewTokenAsync(RenewTokenRequestDto renewTokenRequest)
        {
            var existingRefreshToken = await _appDbContext.userRefreshToken
                .Where(x => x.UserId == renewTokenRequest.UserId 
                && x.Token == renewTokenRequest.RefreshToken && 
                x.ExpirationDate > DateTime.Now).FirstOrDefaultAsync();

            if (existingRefreshToken == null)
            {
                return ("Invalid Refresh Token", null);
            }

            _appDbContext.Remove(existingRefreshToken);
            await _appDbContext.SaveChangesAsync();

            var user = await _appDbContext.User.Where(x => x.Id == renewTokenRequest.UserId).FirstOrDefaultAsync();
            
            string jwtAccessToken = GenerateJwtToken(user);
            string refreshToken = await GenerateRefreshToken(user.Id);
            var result = new JwtTokenResponseDto
            {
                AccessToken = jwtAccessToken,
                RefreshToken = refreshToken,
            };

            return ("", result);

        }

        public async Task<User> GetUserAsync(int userId)
        {

            var result = await _appDbContext.User.Where(u => u.Id == userId).FirstOrDefaultAsync();


            return result;
        }

        public async Task LogoutUserAsync(LogoutRequestDto logoutRequest)
        {
            var tokenToDelete = await _appDbContext.userRefreshToken.Where(x => x.UserId == logoutRequest.UserId
            && x.Token == logoutRequest.RefreshToken).FirstOrDefaultAsync();

            if(tokenToDelete != null)
            {
                _appDbContext.Remove(tokenToDelete);
                await _appDbContext.SaveChangesAsync();
            }

        }
    }
}
