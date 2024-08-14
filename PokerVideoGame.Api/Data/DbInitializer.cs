using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, ICardRepository cardRepository)
        {
            context.Database.EnsureCreated();

            if (context.Deck.AsNoTracking().Any())
            {
                return;
            }

            var deck = await cardRepository.SetUpDeckAsync();

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    context.Deck.AddRange(deck);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            context.Deck.AddRange(deck);

            await context.SaveChangesAsync();
        }
    }
}
