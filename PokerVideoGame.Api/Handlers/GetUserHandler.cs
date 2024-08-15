using MediatR;
using PokerVideoGame.Api.Queries;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserAsync(request.Id);
        }
    }
}
