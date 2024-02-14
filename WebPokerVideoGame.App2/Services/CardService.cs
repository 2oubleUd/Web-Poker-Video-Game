using Microsoft.JSInterop;
using PokerVideoGame.Models;
using System.Net.Http.Json;
using WebPokerVideoGame.App2.Interfaces;

public class CardService : ICardService
{
    private readonly HttpClient _httpClient;

    public List<Card> deckOfCards { get; set; }
    public CardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Card>> GetDeckOfCardsAsync()
    {
        try
        { 
            return await _httpClient.GetFromJsonAsync<Card[]>("api/user/get-deck"); ;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching deck of cards: {ex.Message} ");
            throw;
        }

    }

    public async Task<List<Card>> PrepareDeck()
    {
        deckOfCards = (await GetDeckOfCardsAsync()).ToList();
        List<Card> ShuffledDeck = await ShuffleCards(deckOfCards);// to do: here get deck from DB);
        return ShuffledDeck;
    }

    public async Task<List<Card>> ShuffleCards(List<Card> Cards)
    {
        Random random = new Random();
        for (int i = 0; i < 1000; i++)
        {
            int FirstCard = random.Next(0, 52);
            int SecondCard = random.Next(0, 52);
            if (FirstCard != SecondCard)
            {
                var temp = Cards[FirstCard];
                Cards[FirstCard] = Cards[SecondCard];
                Cards[SecondCard] = temp;
                FirstCard = SecondCard;
            }
        }

        return Cards;
    }

    public async Task<Card[]> PrepareTable(List<Card> PreparedDeck)
    {
        Card[] Table = new Card[5];

        for (int i = 0; i < Table.Length; i++)
        {
            Table[i] = PreparedDeck[PreparedDeck.Count - 1];
            PreparedDeck.RemoveAt(PreparedDeck.Count - 1);
        }

        return Table;
    }
}
