using PokerVideoGame.Models;
using System.ComponentModel;

namespace Web_Poker_Video_Game.Interfaces
{
    public interface IGameHistoryService
    {
        public List<GameHistory> GetAllGamesHistory();
        public void AddGameHistory(Card[] Table, int Winnings);

        public void RemoveGameHistory();
    }
}
