using PokerVideoGame.Models.Data.Dtos;
using MediatR;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Commands
{
    public class UpdateUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public int AmountOfMoney { get; set; }

        public UpdateUserCommand(int id, int accountBalance)
        {
            Id = id;
            AmountOfMoney = accountBalance;
        }
    }
}
