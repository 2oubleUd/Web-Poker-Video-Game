using PokerVideoGame.Models;
using System.ComponentModel;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface IGameHistoryService
    {
        public List<GameHistory> GetAllGamesHistory();
        public void AddGameHistory(Card[] Table, int Winnings);

        public void RemoveGameHistory();
    }
}
