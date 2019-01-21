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
            switch (value)
            {
                case "One":
                    this.Points = 1;
                    break;
                case "Two":
                    this.Points = 2;
                    break;
                case "Three":
                    this.Points = 3;
                    break;
                case "Four":
                    this.Points = 4;
                    break;
                case "Five":
                    this.Points = 5;
                    break;
                case "Six":
                    this.Points = 6;
                    break;
                case "Seven":
                    this.Points = 7;
                    break;
                case "Eight":
                    this.Points = 8;
                    break;
                case "Nine":
                    this.Points = 9;
                    break;
                case "Ace":
                    this.Points = 11;
                    break;

                default:
                    this.Points = 10;
                    break;
            }
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
    }
}
