using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Api.Data;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using MediatR;
using ServiceStack;
using System.Security.Claims;
using PokerVideoGame.Api.Queries;
using PokerVideoGame.Api.Handlers;
using PokerVideoGame.Api.Commands;

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
        private readonly IMediator _mediator;

        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,
            ICardRepository cardRepository, IConfiguration configuration, IMediator mediator)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _httpContextAccessor = httpContextAccessor;

            _configuration = configuration;

            Console.WriteLine("User Id: " +
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine("Username: " +
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));
            
            _mediator = mediator;
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

        [HttpGet("unique-user-email")]
        public IActionResult CheckUserUniqueEmail(string email)
        {
            var result = _userRepository.CheckUserUniqueEmail(email);

            return Ok(result);
        }

        [HttpGet("get-user-id")]
        public IActionResult GetUserId()
        {
            var result = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(result);
        }

        [HttpGet("current/{userId:int}")]
        public async Task<IActionResult> GetCurrentUser(int userId)
        {
            var user = await _mediator.Send(new GetUserQuery() { Id = userId });
                
            if (user != null)
            {
                return Ok(await _userRepository.GetUserAsync(userId));
            }

            return StatusCode(StatusCodes.Status404NotFound,
                $"User with Id: {userId} not found");
        }

        [HttpGet("rankings")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _mediator.Send(new GetUsersListQuery());

            if (!result.IsNullOrEmpty())
            {
                return Ok(await _mediator.Send(new GetUsersListQuery()) /*await _userRepository.GetUsersListAsync()*/);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving list of users");
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration(UserRegistrationDto userRegistration)
        {
            var result = await _mediator.Send(new RegisterNewUserCommand(userRegistration.FirstName,
                 userRegistration.LastName, userRegistration.Email, userRegistration.Password, 
                 userRegistration.ConfirmPassword));

            if (result.IsUserRegistered)
            {
                return Ok(result.Message);
            }
            ModelState.AddModelError("Email", result.Message);

            return BadRequest(ModelState);
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
            var result = await _mediator.Send(new LoginCommand(payload.Email, payload.Password));

            //var result = await _userRepository.LoginAsync(payload);
            
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

        [HttpPut("account")]
        public async Task<ActionResult<User>> PutUserAsync(UpdateUserMoneyDto updateRequest)
        {
            try
            {
                var user = await _mediator.Send(new GetUserQuery() { Id = updateRequest.UserId });

                if (user == null)
                {
                    return NotFound($"User with Id: {updateRequest.UserId} not found");
                }

                return await _mediator.Send(new UpdateUserCommand(updateRequest.UserId, 
                    updateRequest.AmountOfMoney));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}