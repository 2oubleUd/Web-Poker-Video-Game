using PokerVideoGame.Models;
using Web_Poker_Video_Game.Interfaces;

namespace Web_Poker_Video_Game.Services
{
    public class GameHistoryService : IGameHistoryService
    {
        private readonly HttpClient _httpClient;

        public List<GameHistory> gameHistoryList = new List<GameHistory>();

        public GameHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameHistory> AddGameHistory(Card[] Table, int Winnings)
        {
            var _gameHistory = new GameHistory
            {
                PokerHandsHistory = Table,
                Prize = Winnings
            };

            gameHistoryList.Insert(0, _gameHistory);

            var result = await _httpClient.PostAsJsonAsync<GameHistory>("api/GameHistory", _gameHistory);

            if(result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<GameHistory>();
            }
            else
            {
                return null;
            }
        }

        public void RemoveGameHistory()
        {
            if (gameHistoryList.Count == 10)
                gameHistoryList.Clear();
        }
    }
}
