using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Models;
using PokerVideoGame.Models.Data.Entites;
using PokerVideoGame.Models.Data.Settings;
using ServiceStack.Text;
using Web_Poker_Video_Game.Services;

namespace PokerVideoGame.Api.Data
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly IWebHostEnvironment _env;
        private List<Card> cards;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }
        public DbSet<Card> Deck { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Card>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
