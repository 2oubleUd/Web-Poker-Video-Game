using Microsoft.AspNetCore.Mvc;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using PokerVideoGame.Models.Data.Settings;

namespace PokerVideoGame.Api.Models
{
    public interface IUserRepository
    {
        public Task<(bool IsUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration);

        bool CheckUserUniqueEmail(string email);

        Task<(bool IsLoginSuccess, JwtTokenResponseDto tokenResponse)> LoginAsync(LoginDto loginPayload);

        Task<(string ErrorMessage, JwtTokenResponseDto jwtTokenRespone)> RenewTokenAsync(RenewTokenRequestDto renewTokenRequest);
        
        Task<User> GetUserAsync(User user);
        Task<User> UpdateUserAsync(User user);

        Task<IEnumerable<User>> GetUsersListAsync();
    }
}
