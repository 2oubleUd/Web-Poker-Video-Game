using PokerVideoGame.Models;
using System.Net.Http;
using System.Net.Http.Json;
using WebPokerVideoGame.App.Interfaces;

namespace WebPokerVideoGame.App.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync()
        {
            try
            {
                // "api/Players" it has to be the same Route as Route in PlayerController.cs in Api project
                // '[Route("api/Players")]'
                return await _httpClient.GetFromJsonAsync<Player[]>("api/Players");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching players: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the calling code
            }
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Player>($"api/Players/{id}");
        }

        // to do: write function for adding and taking money from player's account
        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            var result = await _httpClient.PutAsJsonAsync("api/Players", player);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<Player>();
            }
            else
            {
                return null;
            }

        }




    }
}
