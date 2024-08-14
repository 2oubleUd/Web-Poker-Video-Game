using Moq;
using PokerVideoGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPokerVideoGame.App.Interfaces;
using WebPokerVideoGame.App.Services;

namespace WebPokerVideoGame.Tests
{
   
    public class RankingServiceTest
    {
        private readonly Mock<IRankingService> _rankingService;
        public RankingServiceTest() 
        {
            _rankingService = new Mock<IRankingService>();
        }

        [Fact]
        public void Ranking_Straight_ReturnWageMultipliedByFour()
        {
            // Arrange
            int wage = 20;

            Card[] cards = new Card[] {
                new Card { CardSuit = SuitOfCard.Hearts, CardValue = ValueOfCard.Ace},
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Two },
                new Card { CardSuit = SuitOfCard.Clubs, CardValue = ValueOfCard.Three },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Four },
                new Card { CardSuit = SuitOfCard.Clubs,CardValue= ValueOfCard.Five }
            };

            // ustawienie zachowania mock'a - symulacja zwracanej wartości
            _rankingService.Setup(r => r.Ranking(cards, wage)).Returns(4 * wage);

            // Act
            var result = _rankingService.Object.Ranking(cards, wage);

            // Assert
            Assert.Equal(4 * wage, result);
        }

        [Fact]
        public void Ranking_StraightFromFourToEight_ReturnWageMultipliedByFour()
        {
            // Arrange
            int wage = 100;
            
            Card[] cards = new Card[] {
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Ace},
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Two },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Three },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Four },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Five }
            };

            _rankingService.Setup(r => r.Ranking(cards, wage)).Returns(4 * wage);

            // Act
            var result = _rankingService.Object.Ranking(cards, wage);

            // Assert
            Assert.Equal(wage * 4, result);

        }

        [Fact]
        public void Ranking_StraightFlush_ReturnWageMultipliedByFifty()
        {
            // Arrange
            int wage = 1000;

            Card[] cards = new Card[] {
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Ace},
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Two },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Three },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Four },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Five }
            };


            _rankingService.Setup(r => r.Ranking(cards, wage)).Returns(wage * 50);

            // Act
            var result = _rankingService.Object.Ranking(cards, wage);


            // Assert
            Assert.Equal(wage * 50, result);

        }

        [Fact]
        public void Ranking_FullHouse_ReturnWageMultipliedByNine()
        {
            // Arrange
            int wage = 1000;

            Card[] cards = new Card[] {
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue = ValueOfCard.Ace},
                new Card { CardSuit = SuitOfCard.Clubs, CardValue = ValueOfCard.Ace },
                new Card { CardSuit = SuitOfCard.Hearts, CardValue = ValueOfCard.Three },
                new Card { CardSuit = SuitOfCard.Diamonds, CardValue= ValueOfCard.Three },
                new Card { CardSuit = SuitOfCard.Spades, CardValue= ValueOfCard.Three }
            };

            _rankingService.Setup(r => r.Ranking(cards, wage)).Returns(wage * 9);

            // Act
            var result = _rankingService.Object.Ranking(cards, wage);

            // Assert
            Assert.Equal(wage * 9, result);

        }
    }
}
