using PokerVideoGame.Models;
using System.ComponentModel;

namespace Web_Poker_Video_Game.Interfaces
{
    public interface IGameHistoryService
    {
        Task<GameHistory> AddGameHistory(Card[] Table, int Winnings);

        public void RemoveGameHistory();
    }
}
