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

        //public async Task LogoutUserAsync()
        //{
        //    try
        //    {
        //        return await _httpClient.PostAsJsonAsync<User>("api/user/logout",);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error logging out: {ex.Message}");
        //        throw;
        //    }
        //}

        //public async Task<User> GetUserById(int id)
        //{
        //    try
        //    {
        //        _httpClient.GetFromJsonAsync<User>()
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
