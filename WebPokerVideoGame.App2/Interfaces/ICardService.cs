﻿using PokerVideoGame.Models;

namespace WebPokerVideoGame.App2.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> PrepareDeck();
        Task<List<Card>> SetUpDeck();
        Task<List<string>> ImageNameToCardNumber();
        Task<List<string>> InitListOfPictures();
        Task<List<Card>> ShuffleCards(List<Card> Cards);
        Task<Card[]> PrepareTable(List<Card> PreparedDeck);

    }
}
