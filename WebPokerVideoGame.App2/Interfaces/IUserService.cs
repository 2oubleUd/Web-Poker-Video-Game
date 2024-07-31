using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using System.Security.Claims;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(int userId);

        Task UpdateUsersAccountBalance(List<Claim> claims, int newValue);
    }
}
