using Microsoft.JSInterop;
using PokerVideoGame.Models;
using System.Net.Http.Json;
using WebPokerVideoGame.App.Interfaces;

public class CardService : ICardService
{
    private readonly HttpClient _httpClient;

    public CardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Card>> GetDeckOfCardsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Card[]>("api/card/get-deck");
        }
        catch (Exception ex) // to do: write handling Error 404
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
