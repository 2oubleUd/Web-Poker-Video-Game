using PokerVideoGame.Models;
using Web_Poker_Video_Game.Interfaces;

namespace Web_Poker_Video_Game.Services
{
    public class CardService : ICardService
    {

        public string PathToImages = @"C:\Users\Mikolaj\source\repos\WebPokerVideoGame-main\Web-Poker-Video-Game\wwwroot\PNG-cards-1.3";

        public List<Card> PrepareDeck()
        {
            List<Card> ShuffledDeck = ShuffleCards(SetUpDeck());
            return ShuffledDeck;
        }

        public List<Card> SetUpDeck()
        {
            int CardNumber = 0;

            List<string> CardImage = ImageNameToCardNumber();
            List<Card> Cards = new List<Card>();

            foreach (ValueOfCard v in Enum.GetValues(typeof(ValueOfCard)))
            {
                foreach (SuitOfCard s in Enum.GetValues(typeof(SuitOfCard)))
                {
                    Cards.Add(new Card(CardImage[CardNumber]) { CardSuit = s, CardValue = v });
                    CardNumber++;
                }
            }

            return Cards;
        }

        public List<Card> ShuffleCards(List<Card> Cards)
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int FirstCard = random.Next(0, 52); // 0 do 52 bo 52 sie nie wlicza do tego zakresu
                                                    // numer karty, ktory chce potasowac i indeks karty Z ktora chce potasowac
                int SecondCard = random.Next(0, 52);
                if (FirstCard != SecondCard) // zeby nie tasowac tej samej karty ze soba
                {
                    var temp = Cards[FirstCard];
                    Cards[FirstCard] = Cards[SecondCard];
                    Cards[SecondCard] = temp;
                    FirstCard = SecondCard;
                }
            }

            return Cards;
        }

        public List<string> ImageNameToCardNumber()
        {
            List<string> ListOfImages = InitListOfPictures();
            List<string> CardsNumber = new List<string>();

            for (int i = 0; i < ListOfImages.Count; i++)
            {
                CardsNumber.Add(Path.GetFileName(ListOfImages[i]));
            }

            var ordered = CardsNumber.Select(s => new { Str = s, Split = s.Split('.') })
                .OrderBy(x => int.Parse(x.Split[0]))
                .ThenBy(x => x.Split[1])
                .Select(x => x.Str)
                .ToList();

            return ordered;
        }

        public List<string> InitListOfPictures()
        {
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            List<string> ListOfImages;

            ListOfImages = new List<string>
                    (Directory.GetFiles(PathToImages, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => ext.Any(e => s.EndsWith(e))));

            return ListOfImages;
        }

        public Card[] PrepareTable(List<Card> PreparedDeck)
        {
            Card[] Table = new Card[5];

            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = PreparedDeck[PreparedDeck.Count - 1];
                PreparedDeck.RemoveAt(PreparedDeck.Count - 1);
            }

            return Table;
        }

        
    }
}
