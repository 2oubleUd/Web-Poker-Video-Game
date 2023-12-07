using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public class GameHistoryRepository : IGameHistoryRepository
    {
        private readonly AppDbContext _appDbContext;

        // to do: implement automatic updating bank account and game history

        public GameHistoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<GameHistory> AddGameHistoryAsync(GameHistory history)
        {
            var result = await _appDbContext.GameHistories.AddAsync(history);
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<GameHistory>> GetAllGameHistoriesAsync()
        {
            return await _appDbContext.GameHistories.ToListAsync();
        }

        public async Task<GameHistory> GetGameHistoryAsync(int id)
        {
            return await _appDbContext.GameHistories.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
