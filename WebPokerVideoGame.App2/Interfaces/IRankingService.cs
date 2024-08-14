using PokerVideoGame.Models;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface IRankingService
    {
        public int Ranking(Card[] table, int wage);
    }
}
