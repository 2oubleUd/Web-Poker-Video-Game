using System.ComponentModel.DataAnnotations.Schema;

namespace PokerVideoGame.Models
{
    public class Card
    { 
        public int Id { get; set; }
        public ValueOfCard CardValue { get; set; }
        public SuitOfCard CardSuit { get; set; }
        public string? ImagePath { get; set; }

        public byte[] ImageData { get; set; }

    }
}