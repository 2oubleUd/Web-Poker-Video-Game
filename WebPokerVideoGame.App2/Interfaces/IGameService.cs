using PokerVideoGame.Models;
using static WebPokerVideoGame.App.Services.GameService;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface IGameService
    {
        public HandRank EvaluateHand(Card[] table);
        bool IsRoyalFlush(Card[] table);
        bool IsStraightFlush(Card[] table);
        bool IsFourOfKind(Card[] table);
        bool IsFullHouse(Card[] table);
        bool IsFlush(Card[] table);
        bool IsStraight(Card[] table);
        bool IsThreeOfKind(Card[] table);
        bool IsTwoPair(Card[] table);
        bool IsOnePair(Card[] table);
    }
}
