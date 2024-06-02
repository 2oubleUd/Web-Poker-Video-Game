using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Models;
using PokerVideoGame.Models.Data.Entites;
using PokerVideoGame.Models.Data.Settings;
using ServiceStack.Text;
using Web_Poker_Video_Game.Services;

namespace PokerVideoGame.Api.Models
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly IWebHostEnvironment _env;
        private List<Card> cards;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //cards = SetUpDeck();
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }
        public DbSet<Card> Deck { get; set; }
        
        // it's for seeding DbSet by data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Player>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 1, Name = "Mikolaj", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 2, Name = "Bartosz", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 3, Name = "Kamil", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 4, Name = "Korneliusz", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 5, Name = "Amadeusz", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 6, Name = "Mateusz", AccountBalance = 1000 });

            //modelBuilder.Entity<Player>().HasData(
            //    new Player { Id = 7, Name = "Szymon", AccountBalance = 1000 });

            //modelBuilder.Entity<GameHistory>()
            //    .HasKey(g => g.Id); // Assuming Id is the primary key 

            //modelBuilder.Entity<Card>()
            //    .HasKey(c => c.Id);


            //for (int i = 0; i < 52; i++)
            //{
            //    modelBuilder.Entity<Card>().HasData(
            //        new Card
            //        {
            //            Id = i + 1,
            //            CardSuit = cards[i].CardSuit,
            //            CardValue = cards[i].CardValue,
            //            ImageData = cards[i].ImageData
            //        });
            //}

            //modelBuilder.Entity<Card>().HasData(
            //    new Card
            //    {
            //        Id = 1,
            //        CardSuit = cards[0].CardSuit,
            //        CardValue = cards[0].CardValue,
            //        ImageData = cards[0].ImageData
            //    });

        }

        //public List<byte[]> InitListOfPictures()
        //{
        //    string pathToImages = Path.Combine(_env.WebRootPath, @"wwwroot\PNG-cards-1.3");
        //    byte[] imgData = System.IO.File.ReadAllBytes(pathToImages);
        //    var ext = new List<string> { ".jpg", ".gif", ".png" };
        //    List<byte[]> imageData = new List<byte[]>();
        //    List<string> listOfImages = new List<string>(Directory.GetFiles(pathToImages));

        //    foreach (string imagePath in listOfImages)
        //    {
        //        if (ext.Any(e => imagePath.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
        //        {
        //            try
        //            {
        //                imageData.Add(System.IO.File.ReadAllBytes(imagePath));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Didn't work out");
        //                // Handle or log the exception.
        //            }
        //        }
        //    }

        //    return imageData;
        //}

        //public List<Card> SetUpDeck()
        //{
        //    int cardNumber = 0;
        //    List<byte[]> cardImage = InitListOfPictures();
        //    List<Card> cards = new List<Card>();

        //    foreach (ValueOfCard v in Enum.GetValues(typeof(ValueOfCard)))
        //    {
        //        foreach (SuitOfCard s in Enum.GetValues(typeof(SuitOfCard)))
        //        {
        //            cards.Add(new Card { CardSuit = s, CardValue = v, ImageData = cardImage[cardNumber] });
        //            cardNumber++;
        //        }
        //    }

        //    return cards;
        //}

    }
}
