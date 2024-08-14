using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Repositories
{
    public interface ICardRepository
    {
        Task SeedCardsAsync();
        Task<IEnumerable<Card>> GetDeckOfCardsAsync();
        Task<IEnumerable<Card>> SetUpDeckAsync();
    }
}
