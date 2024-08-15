using MediatR;
using PokerVideoGame.Api.Commands;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using System.Net.Sockets;

namespace PokerVideoGame.Api.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(command.Id);

            if (user == null)
                return default;

            //user.AccountBalance += command.AmountOfMoney;

            var updateUserMoney = new UpdateUserMoneyDto()
            {
                UserId = command.Id,
                AmountOfMoney = command.AmountOfMoney
            };

            return await _userRepository.UpdateUserAsync(updateUserMoney);
        }
    }
}
