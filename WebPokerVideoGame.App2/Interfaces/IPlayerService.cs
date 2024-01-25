using PokerVideoGame.Models;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> UpdatePlayerAsync(Player player);


    }
}
