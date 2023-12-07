using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> SearchByNameAsync(string name);
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerAsync(int playerId);
        Task<Player> GetPlayerByEmailAsync(string email);
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task<Player> DeletePlayerAsync(int playerId);
    }
}
