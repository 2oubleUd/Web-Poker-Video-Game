using PokerVideoGame.Models;

namespace Web_Poker_Video_Game.Interfaces
{
    public interface IGameService
    {
        bool Pair(Card[] table);
        bool TwoPairs(Card[] table);
        bool ThreeOfKind(Card[] table);
        bool FourOfKind(Card[] table);
        bool Flush(Card[] table);
        bool FullHouse(Card[] table);
        bool Straight(Card[] table);
        bool StraightFlush(Card[] table);
    }
}
