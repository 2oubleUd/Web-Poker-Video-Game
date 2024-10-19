using MediatR;

namespace PokerVideoGame.Api.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        // to do: implement delete request in WebApi
        public int Id { get; set; }
    }
}
