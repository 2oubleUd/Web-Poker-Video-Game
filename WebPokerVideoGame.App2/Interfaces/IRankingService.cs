using PokerVideoGame.Models;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface IRankingService
    {
        int Ranking(Card[] table, int wage);
    }
}
