using PokerVideoGame.Models;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface IRankingService
    {
        int Ranking(Card[] table, int wage);
    }
}
