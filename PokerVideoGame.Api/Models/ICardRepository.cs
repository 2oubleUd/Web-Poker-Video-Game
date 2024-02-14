using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public interface ICardRepository
    {
        Task SeedCardsAsync();
        Task<List<byte[]>> InitListOfPicturesAsync();
        Task<List<Card>> SetUpDeckAsync();

        Task<IEnumerable<Card>> GetDeckOfCardsAsync();
    }
}
