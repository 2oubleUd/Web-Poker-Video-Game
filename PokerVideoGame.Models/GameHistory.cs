using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models
{
    public class GameHistory
    {
        public int Id { get; set; }
        public int Prize { get; set; }

        // to do: idea of saving image paths
        public string[] ImagePath = new string[5];

        //public Card[] PokerHandsHistory { get; set; }  // using this line provoked PUT to do not work
        public Card[] PokerHandsHistory = new Card[5];
    }
}
