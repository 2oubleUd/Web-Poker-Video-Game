using PokerVideoGame.Models;
using WebPokerVideoGame.App.Interfaces;

namespace WebPokerVideoGame.App.Services
{
    public class GameService 
    {
        public enum HandRank
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfKind,
            Straight,
            Flush,
            FullHouse,
            FourOfKind,
            StraightFlush,
            RoyalFlush
        }

        public HandRank EvaluateHand(Card[] table)
        {
            if (IsRoyalFlush(table)) { Console.WriteLine("It royal flush"); return HandRank.RoyalFlush; }
            if (IsStraightFlush(table)) { Console.WriteLine("It royal flush"); return HandRank.StraightFlush; }
            if (IsFourOfKind(table)) { Console.WriteLine("It royal flush"); return HandRank.FourOfKind; }
            if (IsFullHouse(table)) { Console.WriteLine("It royal flush"); return HandRank.FullHouse; }
            if (IsFlush(table)) { Console.WriteLine("It's flush"); return HandRank.Flush; }
            if (IsStraight(table)) { Console.WriteLine("It's straight"); return HandRank.Straight; }
            if (IsThreeOfKind(table)) { Console.WriteLine("It's three of kind"); return HandRank.ThreeOfKind; }
            if (IsTwoPair(table)) { Console.WriteLine("It's two pair"); return HandRank.TwoPair; }
            if (IsOnePair(table)) { Console.WriteLine("It's one pair"); return HandRank.OnePair; }
            return HandRank.HighCard;
        }

        private bool IsRoyalFlush(Card[] table)
        {
            return IsStraightFlush(table) && table.All(card => card.CardValue >= ValueOfCard.Ten);
        }

        private bool IsStraightFlush(Card[] table)
        {
            return IsStraight(table) && IsFlush(table);
        }

        private bool IsFourOfKind(Card[] table)
        {
            var groupValue = table.GroupBy(c => c.CardValue);
            return groupValue.Any(g => g.Count() == 4);
        }

        private bool IsFullHouse(Card[] table)
        {
            var groupValue = table.GroupBy(card => card.CardValue);
            return groupValue.Any(g => g.Count() == 2) && groupValue.Any(g => g.Count() == 3);
        }
        private bool IsFlush(Card[] table)
        {
            var groupSuites = table.GroupBy(c => c.CardSuit);
            return groupSuites.Any(g => g.Count() == 5);

            // alternative
            //return table.GroupBy(c => c.suiteOfCard).Count() == 1;
        }
        private bool IsStraight(Card[] table)
        {
            // select cards from table and parse their values to integers, then sort them and
            // save in array
            var sortedRanks = table.Select(card => (int)card.CardValue).OrderBy(rank => rank).ToList();
            if (sortedRanks.Last() == (int)ValueOfCard.Ace && sortedRanks.First() == (int)ValueOfCard.Two)
            {
                // Handle A-2-3-4-5 as a valid straight (wheel)
                sortedRanks.Remove(sortedRanks.Last());
                sortedRanks.Insert(0, 1);
            }
            for (int i = 1; i < sortedRanks.Count; i++)
            {
                if (sortedRanks[i] != sortedRanks[i - 1] + 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsThreeOfKind(Card[] table)
        {
            var groupValues = table.GroupBy(c => c.CardValue);
            return groupValues.Any(g => g.Count() == 3);
        }

        private bool IsTwoPair(Card[] table)
        {
            var groupValues = table.GroupBy(card => card.CardValue);
            return groupValues.Count(g => g.Count() == 2) == 2;
        }

        private bool IsOnePair(Card[] table)
        {
            var groupValues = table.GroupBy(c => c.CardValue);
            return groupValues.Any(g => g.Count() == 2);
        }

    }
}
