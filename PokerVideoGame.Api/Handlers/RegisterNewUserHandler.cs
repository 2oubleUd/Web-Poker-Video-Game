using MediatR;
using PokerVideoGame.Api.Commands;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Handlers
{
    public class RegisterNewUserHandler : IRequestHandler<RegisterNewUserCommand, (bool IsUserRegistered, string Message)>
    {
        private readonly IUserRepository _userRepository;

        public RegisterNewUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool IsUserRegistered, string Message)> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            var newUser = new UserRegistrationDto()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password,
                ConfirmPassword = command.ConfirmPassword
            };

            return await _userRepository.RegisterNewUserAsync(newUser);
        }
    }
}
