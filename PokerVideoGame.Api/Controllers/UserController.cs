using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Api.Models;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using ServiceStack;
using System.Security.Claims;

namespace PokerVideoGame.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;

        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,
            ICardRepository cardRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _httpContextAccessor = httpContextAccessor;

            _configuration = configuration;

            Console.WriteLine("User Id: " +
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine("Username: " +
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));

        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DBConnection"))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var canConnect = context.Database.CanConnect();
                return Ok($"Can connect to database: {canConnect}");
            }
        }

        [HttpGet("get-user-id")]
        public IActionResult GetUserId()
        {
            var result = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration(UserRegistrationDto userRegistration)
        {
            var result = await _userRepository.RegisterNewUserAsync(userRegistration);
            if (result.IsUserRegistered)
            {
                return Ok(result.Message);
            }
            ModelState.AddModelError("Email", result.Message);
            return BadRequest(ModelState);
        }

        [HttpGet("unique-user-email")]
        public IActionResult CheckUserUniqueEmail(string email)
        {
            var result =_userRepository.CheckUserUniqueEmail(email);

            return Ok(result);
        }

        [HttpPost("renew-tokens")]
        public async Task<IActionResult> RenewTokenAsync(RenewTokenRequestDto renewTokenRequest)
        {
            var result = await _userRepository.RenewTokenAsync(renewTokenRequest);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.jwtTokenRespone);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto payload)
        {
            var result = await _userRepository.LoginAsync(payload);
            if(result.IsLoginSuccess)
            {
                return Ok(result.tokenResponse);
            }
            ModelState.AddModelError("LoginError", "Invalid Credentials");

            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync(LogoutRequestDto logoutRequestDto)
        {
            await _userRepository.LogoutUserAsync(logoutRequestDto);

            return Ok();
        }

        [HttpGet("current/{userId:int}")]
        public async Task<IActionResult> GetCurrentUser(int userId)
        {
            User user = await _userRepository.GetUserAsync(userId);
            if(user != null)
            {
                return Ok(await _userRepository.GetUserAsync(userId));
            }

            return StatusCode(StatusCodes.Status404NotFound,
                $"User with Id: {userId} not found");
        }

        [HttpGet("rankings")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userRepository.GetUsersListAsync();
            if(!result.IsNullOrEmpty())
            {
                return Ok(await _userRepository.GetUsersListAsync());
            }

            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Error retrieving list of users");
        }

        [HttpPut("account")]
        public async Task<ActionResult<User>> PutUserAsync(UpdateUserMoneyDto updateRequest)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(updateRequest.UserId);

                if (user == null)
                {
                    return NotFound($"User with Id: {updateRequest.UserId} not found");
                }

                return await _userRepository.UpdateUserAsync(updateRequest);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
