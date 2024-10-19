using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using System.Security.Claims;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<User> GetUserAsync(int userId);

        public Task UpdateUsersAccountBalance(List<Claim> claims, int newValue);
    }
}
