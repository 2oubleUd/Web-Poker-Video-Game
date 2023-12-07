using PokerVideoGame.Models;

namespace Web_Poker_Video_Game.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> UpdatePlayerAsync(Player player);


    }
}
