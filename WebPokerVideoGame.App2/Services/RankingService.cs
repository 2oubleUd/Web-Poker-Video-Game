using PokerVideoGame.Models;
using WebPokerVideoGame.App.Interfaces;
using WebPokerVideoGame.App.ViewModels;

namespace WebPokerVideoGame.App.Services
{
    public class RankingService : IRankingService
    {
        private GameService _gameService;
        private PokerViewModel _pokerViewModel;

        public RankingService(GameService gameService, PokerViewModel pokerCards)
        {
            _gameService = gameService;
            _pokerViewModel = pokerCards;
        }

        public int Ranking(Card[] table, int wage)
        {
            if (_gameService.EvaluateHand(table) == GameService.HandRank.OnePair)
            {
                return 0;
            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.TwoPair)
            {
                return wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.ThreeOfKind)
            {
                return 3 * wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.FourOfKind)
            {
                return 25 * wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.Flush)
            {
                return 6 * wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.FullHouse)
            {
                return 9 * wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.Straight)
            {
                return 4 * wage;

            }

            if (_gameService.EvaluateHand(table) == GameService.HandRank.StraightFlush)
            {
                return 50 * wage;
            }

            return (-1*wage);
        }
    }
}
