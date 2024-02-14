using PokerVideoGame.Models;

// Create a PokerViewModel.cs file
namespace PokerVideoGame.ViewModels
{
    public class PokerViewModel
    {
        public int Money { get; set; }
        public Player Player { get; set; }
        //public List<GameHistory> ListOfGames { get; set; }
        public int[] Wages { get; set; } = new int[6] { 10, 20, 50, 100, 200, 500 };
        public int SelectedWage { get; set; }
        public int WageCounter { get; set; }
        public int Prize { get; set; }
        public List<Card> PreparedDeck { get; set; }
        public List<bool> CardsToChange { get; set; } = new List<bool> { true, true, true, true, true };
        public Card[] Table { get; set; }
        public Timer? DelayTimer { get; set; }
        public bool Dealing { get; set; }
        public string PokerHand { get; set; }
    }
}
