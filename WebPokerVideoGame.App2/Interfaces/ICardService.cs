using PokerVideoGame.Models;

namespace WebPokerVideoGame.App.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> PrepareDeck();
        Task<List<Card>> ShuffleCards(List<Card> Cards);
        Task<Card[]> PrepareTable(List<Card> PreparedDeck);

        Task<IEnumerable<Card>> GetDeckOfCardsAsync();
    }
}
