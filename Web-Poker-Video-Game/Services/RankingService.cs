using PokerVideoGame.Models;
using PokerVideoGame.ViewModels;
using Web_Poker_Video_Game.Interfaces;
using Web_Poker_Video_Game.Pages;

namespace Web_Poker_Video_Game.Services
{
    public class RankingService : IRankingService
    {
        // ta klasa to taki pomysl zgodny z Single Responsiblity Principle (Litera 'S' w SOLID)
        
        private Web_Poker_Video_Game.Services.GameService _gameService;
        private PokerViewModel _pokerViewModel;

        public RankingService(GameService gameService, PokerViewModel pokerCards) 
        {
            _gameService = gameService;
            _pokerViewModel = pokerCards;
        }

        public int Ranking(Card[] table, int wage)
        {

            int prize = 0;

            if (_gameService.Pair(table))
            {
                prize = wage;   
            }

            if (_gameService.TwoPairs(table))
            {
                prize = 2 * wage;
                
            }

            if (_gameService.ThreeOfKind(table))
            {
                prize = 3 * wage;
                
            }

            if (_gameService.FourOfKind(table))
            {
                prize = 25 * wage;
                
            }

            if (_gameService.Flush(table))
            {
                prize = 6 * wage;
                
            }

            if (_gameService.FullHouse(table))
            {
                prize = 9 * wage;
                
            }

            if (_gameService.Straight(table))
            {
                prize = 4 * wage;
                
            }

            if (_gameService.StraightFlush(table))
            {
                prize = 50 * wage;
            }

            _pokerViewModel.Money -= wage;

            _pokerViewModel.Money += prize;

            return _pokerViewModel.Money;
        }
    }
}
