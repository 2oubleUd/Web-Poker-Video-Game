using Microsoft.JSInterop;
using PokerVideoGame.Models;
using WebPokerVideoGame.App2.Interfaces;

public class CardService : ICardService
{
    private readonly IJSRuntime _jsRuntime;

    public CardService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<List<Card>> PrepareDeck()
    {
        List<Card> ShuffledDeck = await ShuffleCards(await SetUpDeck());
        return ShuffledDeck;
    }

    public async Task<List<Card>> SetUpDeck()
    {
        int CardNumber = 0;

        List<string> CardImage = await ImageNameToCardNumber();
        List<Card> Cards = new List<Card>();

        foreach (ValueOfCard v in Enum.GetValues(typeof(ValueOfCard)))
        {
            foreach (SuitOfCard s in Enum.GetValues(typeof(SuitOfCard)))
            {
                //Cards.Add(new Card(CardImage[CardNumber]) { CardSuit = s, CardValue = v });
                Cards.Add(new Card() { CardSuit = s, CardValue = v, ImagePath = CardImage[CardNumber] });

                CardNumber++;
            }
        }

        return Cards;
    }

    public async Task<List<string>> ImageNameToCardNumber()
    {
        List<string> ListOfImages = await InitListOfPictures();
        List<string> CardsNumber = new List<string>();

        for (int i = 0; i < ListOfImages.Count; i++)
        {
            CardsNumber.Add(Path.GetFileName(ListOfImages[i]));
        }

        var ordered = CardsNumber.Select(s => new { Str = s, Split = s.Split('.') })
            .OrderBy(x => int.Parse(x.Split[0]))
            .ThenBy(x => x.Split[1])
            .Select(x => x.Str)
            .ToList();

        return ordered;
    }

    public async Task<List<string>> InitListOfPictures()
    {
        var baseUri = await _jsRuntime.InvokeAsync<string>("BlazorWebAssemblyApp.getBaseUri");
        var imagesFolder = Path.Combine(baseUri, "images", "PNG-cards-1.3");

        string relImageFolder = new Uri(imagesFolder).LocalPath;

        var ext = new List<string> { ".jpg", ".gif", ".png" };
        List<string> ListOfImages;

        ListOfImages = new List<string>(
            Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => ext.Any(e => s.EndsWith(e))));

        return ListOfImages;
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
