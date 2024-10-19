using Microsoft.JSInterop;
using PokerVideoGame.Models;
using System.Net.Http.Json;
using WebPokerVideoGame.App.Interfaces;

public class CardService : ICardService
{
    private IHttpClientFactory _httpClientFactory;

    public CardService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Card>> GetDeckOfCardsAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("Dot7Api");
            return await httpClient.GetFromJsonAsync<Card[]>("api/card/get-deck");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching deck of cards: {ex.Message} ");
            throw;
        }
    }

    public async Task<List<Card>> PrepareDeck()
    {
        List<Card> deckOfCards = (await GetDeckOfCardsAsync()).ToList();
        List<Card> ShuffledDeck = await ShuffleCards(deckOfCards);
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

    public async Task<Card[]> PrepareTable(List<Card> preparedDeck)
    {
        Card[] table = new Card[5];

        for (int i = 0; i < table.Length; i++)
        {
            table[i] = preparedDeck[preparedDeck.Count - 1];
            preparedDeck.RemoveAt(preparedDeck.Count - 1);
        }

        return table;
    }
}
