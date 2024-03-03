using Microsoft.AspNetCore.Identity;
using PokerVideoGame.Models.Data.Dtos;
using PokerVideoGame.Models.Data.Entites;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
    
        public async Task UpdateUsersAccountBalance(List<Claim> claims, int newValue)
        {
            try
            {
                UpdateUserMoneyDto user = new UpdateUserMoneyDto();
                user.AmountOfMoney = newValue;
                user.UserId = Convert.ToInt32(claims.Where(x => x.Type == "Sub").Select(x => x.Value).FirstOrDefault() ?? "0");

                var updatedUser = JsonSerializer.Serialize(user);
                var requestUpdatedUser = new StringContent(updatedUser, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/user/account", requestUpdatedUser);
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Error with updating user's bank account", ex);
            }
        }
    }
}
