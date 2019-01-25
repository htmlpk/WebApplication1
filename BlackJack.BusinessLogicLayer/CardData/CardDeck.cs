using System;
using System.Collections.Generic;
using System.Linq;

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
            var value = Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList();
            var suits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>().ToList();
            suits.ForEach(suit => {
                if (!Enum.GetName(typeof(CardSuit), suit).Equals("None"))
                    value.ForEach(rank =>
                    {
                        if (!Enum.GetName(typeof(CardValue), rank).Equals("None"))
                            _cards.Add(new Card(
                            Enum.GetName(typeof(CardValue), rank),
                            Enum.GetName(typeof(CardSuit), suit)
                           )
                        );
                    }
                    );
                }
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
            Card card = unuserCards.ElementAt(0);
            usedCards.Add(card);
            return card;
        }
    }
}