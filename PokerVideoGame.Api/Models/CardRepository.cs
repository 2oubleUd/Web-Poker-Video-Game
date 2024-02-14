using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Models;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PokerVideoGame.Api.Models
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
                // Handle or log the situation
                throw new DirectoryNotFoundException($"Directory not found: {pathToImages}");
            }
        }

        public async Task SeedCardsAsync()
        {
            List<Card> cards = await SetUpDeckAsync();

            for (int i = 1; i < cards.Count; i++)
            {
                _appDbContext.Deck.Add(new Card
                {
                    Id = i + 1,
                    CardSuit = cards[i].CardSuit,
                    CardValue = cards[i].CardValue,
                    ImageData = cards[i].ImageData
                });

                _appDbContext.SaveChanges();

            }

            // Save changes to the database.
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<byte[]>> InitListOfPicturesAsync()
        {
            byte[] imgData = System.IO.File.ReadAllBytes(pathToImages);
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            List<byte[]> imageData = new List<byte[]>();


            List<string> listOfImages = new List<string>(Directory.GetFiles(pathToImages, "*.*", SearchOption.AllDirectories));

            foreach (string imagePath in listOfImages)
            {
                if (ext.Any(e => imagePath.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        imageData.Add(System.IO.File.ReadAllBytes(imagePath));
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception.
                    }
                }
            }

            return imageData;
        }

        public List<byte[]> InitListOfPictures()
        {
            List<string> pathToImages = new List<string>();
            List<byte[]> imgData = new List<byte[]>();

            for (int i = 1; i < 53; i++)
            {
                pathToImages.Add(Path.Combine(_env.WebRootPath, $"PNG-cards-1.3\\{1}.png"));
                imgData.Add(System.IO.File.ReadAllBytes(pathToImages[i - 1]));
            }

            return imgData;
        }

        public async Task<List<Card>> SetUpDeckAsync()
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

            return cards;
        }

        public async Task<IEnumerable<Card>> GetDeckOfCardsAsync()
        {
            return await _appDbContext.Deck.ToListAsync();
        }


    }
}
