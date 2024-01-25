using PokerVideoGame.Models;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface ICardService
    {
        List<Card> SetUpDeck();
        List<Card> ShuffleCards(List<Card> cards);
        List<string> ImageNameToCardNumber();
        List<string> InitListOfPictures();
        Card[] PrepareTable(List<Card> PreparedDeck);

    }
}
