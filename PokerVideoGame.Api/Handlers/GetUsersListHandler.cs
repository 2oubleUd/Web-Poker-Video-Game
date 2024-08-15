using MediatR;
using NuGet.Protocol.Plugins;
using PokerVideoGame.Api.Queries;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Handlers
{
    public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersListHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsersListAsync();
        }
    }
}
