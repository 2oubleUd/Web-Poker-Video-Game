using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public interface ICardRepository
    {
        Task SeedCardsAsync();
        Task<IEnumerable<Card>> GetDeckOfCardsAsync();
    }
}
