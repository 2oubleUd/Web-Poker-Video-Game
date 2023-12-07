using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public interface IGameHistoryRepository
    {
        Task<GameHistory> AddGameHistoryAsync(GameHistory history);
        Task<IEnumerable<GameHistory>> GetAllGameHistoriesAsync();
        Task<GameHistory> GetGameHistoryAsync(int id);
    }
}
