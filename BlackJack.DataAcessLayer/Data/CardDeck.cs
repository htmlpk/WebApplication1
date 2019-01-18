using BlackJack.DataAcessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BlackJack.DataAcessLayer.Data
{
    public class CardDeck
    {
        protected List<Card> _cards;
        public IEnumerable<Card> Cards { get { return _cards; } }

        public CardDeck()
        {
            _cards = new List<Card>();
        }

        public CardDeck(bool shuffled) : this()
        {
            var value = Enum.GetValues(typeof(Card.CardValue)).Cast<Card.CardValue>().ToList();
            var suits = Enum.GetValues(typeof(Card.CardSuit)).Cast<Card.CardSuit>().ToList();
            var points = Enum.GetValues(typeof(Card.CardPoints)).Cast<Card.CardPoints>().ToList();

            suits.ForEach(suit =>
                value.ForEach(rank =>
                _cards.Add(new Card(
                    Enum.GetName(typeof(Card.CardValue), rank),
                    Enum.GetName(typeof(Card.CardSuit), suit)
                   )
                ))
                );

            if (shuffled)
                Shuffle();
        }

        public void Shuffle()
        {
            _cards = _cards.OrderBy(o => Guid.NewGuid()).ToList();
        }

        public Card DealCard(ref List<Card> usedCards)
        {
            if (!_cards.Any())
                throw new InvalidOperationException("Deck is out of cards");

            List<Card> unuserCards = _cards.Except(usedCards).ToList();

            var card = unuserCards.ElementAt(0);
            usedCards.Add(card);
           
            return card;
        }


        
    }
}