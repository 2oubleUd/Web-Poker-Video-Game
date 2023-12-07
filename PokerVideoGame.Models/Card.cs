namespace PokerVideoGame.Models
{
    public class Card
    { 
        public int Id { get; set; }
        // to do: adding cards to database as game is starting
        public Card(string imagePath)
        {
            ImagePath = imagePath;
        }

        public ValueOfCard CardValue { get; set; }
        public SuitOfCard CardSuit { get; set; }
        public string ImagePath { get; set; }

    }
}