using MediatR;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Queries
{
    public class GetUsersListQuery : IRequest<IEnumerable<User>>
    {
    }
}
