using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetLoggedUserId()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
   
            return Ok(new {Id = id}); // to do: it returns always 0, not the correct Id
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

        // to do: implement method for adding money for user
        [HttpPut("balance")]
        public async Task<ActionResult<User>> PutUserAsync(User user)
        {
            try
            {
                var userResult = await _userRepository.GetUserAsync(user);

                if(userResult == null)
                {
                    return NotFound($"User with Id: {user.Id} not found");
                }

                return await _userRepository.UpdateUserAsync(user);
            }

            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
