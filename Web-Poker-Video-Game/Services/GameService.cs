using PokerVideoGame.Models;
using Web_Poker_Video_Game.Interfaces;

namespace Web_Poker_Video_Game.Services
{
    public class GameService : IGameService
    {
        public string CurrentPokerHand = "";

        public bool Pair(Card[] table)
        {
            // see if exacly 2 cards on the table have the same rank
            if (table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1)
            {
                CurrentPokerHand = ($"Pair of {table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1}");
                return table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1;
            }
            else
            {
                return false;
            }
        }

        public bool TwoPairs(Card[] table)
        {
            // see if there are 2 lots of exacly 2 cards with the same rank
            if (table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 2)
            {
                CurrentPokerHand = ($"Two pairs of {table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 2}");
                return table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 2;
            }
            else
            {
                return false;
            }
        }

        public bool ThreeOfKind(Card[] table)
        {
            if (table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3))
            {
                CurrentPokerHand = ($"Three of kind of {table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3)}");
                return table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3);
            }
            else
            {
                return false;
            }
        }

        public bool FourOfKind(Card[] table)
        {
            if (table.GroupBy(c => c.CardValue).Any(g => g.Count() == 4))
            {
                CurrentPokerHand = ($"Four of kind of {table.GroupBy(c => c.CardValue).Any(g => g.Count() == 4)}");
                return table.GroupBy(c => c.CardValue).Any(g => g.Count() == 4);
            }
            else
            {
                return false;
            }
        }

        public bool Flush(Card[] table)
        {
            if (table.GroupBy(c => c.CardSuit).Count(g => g.Count() >= 5) == 1)
            {
                CurrentPokerHand = ($"Flush of {table.GroupBy(c => c.CardSuit).Count(g => g.Count() >= 5) == 1}");
                return table.GroupBy(c => c.CardSuit).Count(g => g.Count() >= 5) == 1;
            }
            else
            {
                return false;
            }
        }

        public bool FullHouse(Card[] table)
        {
            if (table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1
                && table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3))
            {
                CurrentPokerHand = ($"Full House of {table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1
                    && table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3)}");
                
                return table.GroupBy(c => c.CardValue).Count(g => g.Count() == 2) == 1 &&
                    table.GroupBy(c => c.CardValue).Any(g => g.Count() == 3);
            }
            else
            {
                return false;
            }
        }

        // Straight
        public bool Straight(Card[] table)
        {
            var ordered = table.OrderByDescending(a => a.CardValue).ToList();
            for (int i = 0; i < ordered.Count - 5; i++)
            {
                var skipped = ordered.Skip(i);
                var possibleStraight = skipped.Take(5);
                if (IsStraight(possibleStraight))
                {
                    
                    return true;
                }
            }
            return false;
        }

        public bool IsStraight(IEnumerable<Card> table)
        {
            if (table.GroupBy(c => c.CardValue).Count() == table.Count()
                && table.Max(c => (int)c.CardValue) - table.Min(c => (int)c.CardValue) == 4)
            {
                CurrentPokerHand = ($"Straight of {table.GroupBy(c => c.CardValue).Count() == table.Count()
                        && table.Max(c => (int)c.CardValue) - table.Min(c => (int)c.CardValue) == 4}");
                
                return table.GroupBy(c => c.CardValue).Count() == table.Count()
                && table.Max(c => (int)c.CardValue) - table.Min(c => (int)c.CardValue) == 4;
            }
            else
            {
                return false;
            }
        }

        // Stright Flash    
        public bool StraightFlush(Card[] table)
        {
            if (Straight(table) && Flush(table))
            {
                CurrentPokerHand = ($"Straight Flush of {table.GroupBy(c => c.CardValue).Count() == table.Count()
                && table.Max(c => (int)c.CardValue) - table.Min(c => (int)c.CardValue) == 4 && 
                table.GroupBy(c => c.CardSuit).Count(g => g.Count() >= 5) == 1}");
                return Straight(table) && Flush(table);
            }
            else
            {
                return false;
            }
        }

    }
}
