using PokerVideoGame.Models.Data.Entites;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
