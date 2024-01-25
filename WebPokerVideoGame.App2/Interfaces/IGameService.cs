using PokerVideoGame.Models;

namespace WebPokerVideoGame.App2.Interfaces
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
