using PokerVideoGame.Models.Data.Entites;
using MediatR;

namespace PokerVideoGame.Api.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
