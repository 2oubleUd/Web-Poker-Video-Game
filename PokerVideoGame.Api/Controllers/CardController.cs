using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Api.Models;
using PokerVideoGame.Models;

namespace PokerVideoGame.Api.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpPut("create-deck")]
        public async Task<IActionResult> CreateDeck()
        {
            var deck = _cardRepository.GetDeckOfCardsAsync();

            if(deck != null)
            {
                try
                {
                    await _cardRepository.SeedCardsAsync();
                    return Ok("Deck created successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during deck creating: {ex}");
                    // Log or handle the exception appropriately.
                    return StatusCode(500, "Internal Server Error");
                }
            }

            return BadRequest("Deck is already created!");
        }

        [HttpGet("get-deck")]
        public async Task<IActionResult> GetDeckAsync()
        {
            var result = await _cardRepository.GetDeckOfCardsAsync();

            if (!result.IsNullOrEmpty())
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving deck of cards");
        }
    }
}
