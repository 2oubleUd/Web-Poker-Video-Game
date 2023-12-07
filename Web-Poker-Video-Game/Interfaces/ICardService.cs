using PokerVideoGame.Models;

namespace Web_Poker_Video_Game.Interfaces
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
