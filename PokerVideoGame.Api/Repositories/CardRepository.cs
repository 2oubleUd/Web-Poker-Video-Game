using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PokerVideoGame.Api.Data;
using PokerVideoGame.Models;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PokerVideoGame.Api.Repositories
{
    public class CardRepository : ICardRepository
    {
        public string pathToImages = "PNG-cards-1.3";
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public CardRepository(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

            pathToImages = Path.Combine(_env.WebRootPath, pathToImages);

            if (!Directory.Exists(pathToImages))
            {
                throw new DirectoryNotFoundException($"Directory not found: {pathToImages}");
            }
        }

        public async Task SeedCardsAsync()
        {
            List<Card> cards = (await SetUpDeckAsync()).ToList();

            for (int i = 0; i < cards.Count; i++)
            {
                _appDbContext.Deck.Add(new Card
                {
                    CardSuit = cards[i].CardSuit,
                    CardValue = cards[i].CardValue,
                    ImageData = cards[i].ImageData
                });
                _appDbContext.SaveChanges();
            }
        }

        public List<byte[]> InitListOfPictures()
        {
            List<string> pathToImages = new List<string>();
            List<byte[]> imgData = new List<byte[]>();

            for (int i = 1; i <= 53; i++)
            {
                pathToImages.Add(Path.Combine(_env.WebRootPath, $"PNG-cards-1.3\\{i}.png"));
                imgData.Add(File.ReadAllBytes(pathToImages[i - 1]));
            }

            return imgData;
        }

        public async Task<IEnumerable<Card>> SetUpDeckAsync()
        {
            int cardNumber = 0;
            List<byte[]> cardImage = InitListOfPictures();
            List<Card> cards = new List<Card>();

            foreach (ValueOfCard v in Enum.GetValues(typeof(ValueOfCard)))
            {
                foreach (SuitOfCard s in Enum.GetValues(typeof(SuitOfCard)))
                {
                    cards.Add(new Card { CardSuit = s, CardValue = v, ImageData = cardImage[cardNumber] });
                    cardNumber++;
                }
            }

            cards.Add(new Card { ImageData = cardImage[52] }); // Add blank card at the end
            return await Task.FromResult(cards.AsEnumerable());
        }

        public async Task<IEnumerable<Card>> GetDeckOfCardsAsync()
        {
            return await _appDbContext.Deck.ToListAsync();
        }
    }
}
