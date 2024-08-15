using MediatR;
using PokerVideoGame.Models.Data.Settings;

namespace PokerVideoGame.Api.Commands
{
    public class LoginCommand : IRequest<(bool IsLoginSuccess, JwtTokenResponseDto tokenResponse)>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
