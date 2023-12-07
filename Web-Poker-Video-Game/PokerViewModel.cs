using PokerVideoGame.Models;

namespace Web_Poker_Video_Game
{
    public class PokerViewModel
    {
        // to do: maybe I can store all variables from PokerCards.razor here? Or even whole c# part of that file?
        public int Money = 1000;
        public int[] Wages = new int[6] { 10, 20, 50, 100, 200, 500 };
        public int SelectedWage;
        public int WageCounter = 0;
        public int Prize = 0;

        public List<Card> PreparedDeck = new List<Card>();
        public List<bool> CardsToChange = new List<bool> { true, true, true, true, true };
        Card[] Table = new Card[5];

        private Timer? delayTimer;

        private bool dealing = false;

        private string PokerHand = "";
    }
}
