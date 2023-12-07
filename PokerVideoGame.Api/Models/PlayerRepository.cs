using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Models
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _appDbContext;
        public PlayerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Player>> SearchByNameAsync(string name)
        {
            // it's a whole query to send to api, it's only for not including every single 
            // variable in implementation of Search() in PlayerController.cs 
            IQueryable<Player> Query = _appDbContext.Players;

            if (!string.IsNullOrEmpty(name)) 
            {
                Query = Query.Where(p => p.Name.Contains(name)
                         || p.Surname.Contains(name));
            }

            return await Query.ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _appDbContext.Players.ToListAsync();
        }

        public async Task<Player> GetPlayerAsync(int playerId)
        {
            // '.Include()' to add reference to GameHistory Table
            return await _appDbContext.Players
                .Include(p => p.PlayerGameHistory)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }
        

        public async Task<Player> GetPlayerByEmailAsync(string email)
        {
            return _appDbContext.Players.FirstOrDefault(p => p.Email == email);
        }

        
        public async Task<Player> AddPlayerAsync(Player player)
        {
            var result = await _appDbContext.Players.AddAsync(player);
            await _appDbContext.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<Player> DeletePlayerAsync(int playerId)
        {
            var result = await _appDbContext.Players.
                FirstOrDefaultAsync(p => p.Id == playerId);
            if(result != null) 
            {
                _appDbContext.Players.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            var result = await _appDbContext.Players.FirstOrDefaultAsync(p => p.Id == player.Id);

            if (result != null)
            {
                result.Name = player.Name;
                result.Surname = player.Surname;
                result.AccountBalance = player.AccountBalance;
                result.Email = player.Email;
                result.PlayerGameHistory = player.PlayerGameHistory;

                _appDbContext.SaveChanges();

                return result;

            }

            return null;
        }
    }
}
