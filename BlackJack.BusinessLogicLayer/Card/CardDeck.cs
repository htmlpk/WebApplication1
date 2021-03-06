﻿using BlackJack.BusinessLogicLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogicLayer.CardData
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
            var values = Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList();
            var suits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>().ToList();
            suits.ForEach(suit =>
            {
                values.ForEach(value =>
                {
                    if (!Enum.GetName(typeof(CardValue), value).Equals("None")
                    && !Enum.GetName(typeof(CardSuit), suit).Equals("None"))
                    {
                        _cards.Add(new Card(
                        Enum.GetName(typeof(CardValue), value),
                        Enum.GetName(typeof(CardSuit), suit)
                       )
                         );
                    }
                }
                );
            }
                );
            if (shuffled)
            {
                Shuffle();
            }
        }

        public void Shuffle()
        {
            _cards = _cards.OrderBy(o => Guid.NewGuid()).ToList();
        }

        public Card DealCard(ref List<Card> usedCards)
        {
            if (!_cards.Any())
            {
                throw new InvalidOperationException("Deck is out of cards");
            }
            List<Card> unUsedCards = _cards.Except(usedCards).ToList();
            Card card = unUsedCards.ElementAt(0);
            usedCards.Add(card);
            return card;
        }
    }
}