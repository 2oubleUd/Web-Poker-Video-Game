using Microsoft.AspNetCore.Mvc;
using PokerVideoGame.Api.Models;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Controllers
{
    [Route("api/GameHistory")]
    [ApiController]
    public class GameHistoryController : ControllerBase
    {
        public readonly IGameHistoryRepository _gameHistoryRepository;

        public GameHistoryController(IGameHistoryRepository gameHistoryRepository)
        {
            _gameHistoryRepository = gameHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllHistoryGamesAsync()
        {
            try
            {
                var result = Ok(await _gameHistoryRepository.GetAllGameHistoriesAsync());

                if(result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    }
}
