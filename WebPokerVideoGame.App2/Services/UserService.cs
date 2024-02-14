using Microsoft.AspNetCore.Identity;
using PokerVideoGame.Models.Data.Entites;
using System.Net.Http;
using System.Net.Http.Json;
using WebPokerVideoGame.App2.Interfaces;

namespace WebPokerVideoGame.App2.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        
        public UserService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            
            try
            {
                return await _httpClient.GetFromJsonAsync<User[]>("api/user/rankings");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching players: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the calling code
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User>($"api/user/current/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with Id: {userId}");
                throw;
            }

        }
    }
}
