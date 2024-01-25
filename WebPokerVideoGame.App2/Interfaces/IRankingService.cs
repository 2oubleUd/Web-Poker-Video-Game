using PokerVideoGame.Models;

namespace Web_Poker_Video_Game.Interfaces
{
    public interface IRankingService
    {
        int Ranking(Card[] table, int wage);
    }
}
