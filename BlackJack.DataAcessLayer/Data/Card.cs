using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Data
{
    public class Card
    {
        [Key]
        public string Suit { get; set; }
        public string Value { get; set; }
        public int Points { get; set; }

        public Card(string value, string suit)
        {
            this.Value = value;
            this.Suit = suit;
            this.Points = (int) (Enum.Parse(typeof(CardPoints), value));
           

        }

        public override int GetHashCode() =>
            this.Suit.GetHashCode() ^
            this.Value.GetHashCode() ^
            this.Points.GetHashCode();

        protected static bool EqualsHelper(Card first, Card second) =>
            first.Suit == second.Suit &&
            first.Value == second.Value &&
            first.Points == second.Points;

        public override bool Equals(object obj)
        {
            if ((object)this == obj)
                return true;

            var other = obj as Card;

            if ((object)other == null)
                return false;

            return EqualsHelper(this, other);
        }

        public enum CardValue
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }

        public enum CardSuit
        {
            Peak = 1,
            Diamond = 2,
            Heart = 3,
            Cross = 4
        }

        public enum CardPoints
        {
            Ace = 11,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 10,
            Queen = 10,
            King = 10
        }

    }
}
