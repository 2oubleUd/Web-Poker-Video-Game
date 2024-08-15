using MediatR;
using PokerVideoGame.Api.Commands;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Settings;

namespace PokerVideoGame.Api.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, (bool IsLoginSuccess, JwtTokenResponseDto tokenResponse)>
    {
        private readonly IUserRepository _userRepository;

        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool IsLoginSuccess, JwtTokenResponseDto tokenResponse)> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var loginUser = new LoginDto()
            {
                Email = command.Email,
                Password = command.Password
            };

            return await _userRepository.LoginAsync(loginUser);
        }
    }
}
