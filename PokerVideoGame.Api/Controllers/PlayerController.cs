using Microsoft.AspNetCore.Mvc;
using PokerVideoGame.Api.Models;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Controllers
{
    [Route("api/Players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        public readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Player>>> SearchAsync(string name, string? surname)
        {
            try
            {
                var result = await _playerRepository.SearchByNameAsync(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAllPlayersAsync()
        {
            try
            {
                return Ok(await _playerRepository.GetAllPlayersAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Player>> GetPlayerByIdAsync(int id)
        {
            try
            {
                var result = Ok(await _playerRepository.GetPlayerAsync(id));  

                if(result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayerAsync([FromBody]Player player)
        {
            try
            {
                if (player == null)
                {
                    return BadRequest();
                }

                // validation of provided email
                var emailRequest = await _playerRepository.GetPlayerByEmailAsync(player.Email);

                if(emailRequest != null)
                {
                    ModelState.AddModelError("email", "Provided email is already in use");
                    return BadRequest(ModelState);
                }
                
                var createdPlayer = await _playerRepository.AddPlayerAsync(player);

                return CreatedAtAction(nameof(CreatePlayerAsync),
                    new { id = createdPlayer.Id }, createdPlayer);
                
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new player record");
            }
        }

        // to do: write function for adding and taking money from player's account1

        [HttpPut]
        public async Task<ActionResult<Player>> PutPlayerAsync(Player player)
        {
            try
            { 
                var playerResult = await _playerRepository.GetPlayerAsync(player.Id);

                if (playerResult == null)
                {
                    return NotFound($"Player with Id = {player.Id} not found");
                }

                return await _playerRepository.UpdatePlayerAsync(player);   
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Player>> RemovePlayerAsync(int id)
        {
            try
            {
                var playerToDelete = await _playerRepository.GetPlayerAsync(id);

                if(playerToDelete == null)
                {
                    return NotFound($"Player with Id = {id} not found");
                }

                return await _playerRepository.DeletePlayerAsync(id);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    $"Player with Id = {id} not found");
            }
           
        }
    }
}
