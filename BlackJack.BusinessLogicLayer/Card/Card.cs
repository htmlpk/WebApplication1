using System.ComponentModel.DataAnnotations;

namespace BlackJack.BusinessLogicLayer.CardData
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
            if (value.Equals("One"))
            {
                Points = 1;
                return;
            }
            if (value.Equals("Two"))
            {
                Points = 2;
                return;
            }
            if (value.Equals("Three"))
            {
                Points = 3;
                return;
            }
            if (value.Equals("Four"))
            {
                Points = 4;
                return;
            }
            if (value.Equals("Five"))
            {
                Points = 5;
                return;
            }
            if (value.Equals("Six"))
            {
                Points = 6;
                return;
            }
            if (value.Equals("Seven"))
            {
                Points = 7;
                return;
            }
            if (value.Equals("Eight"))
            {
                Points = 8;
                return;
            }
            if (value.Equals("Nine"))
            {
                Points = 9;
                return;
            }
            if (value.Equals("Ace"))
            {
                Points = 11;
                return;
            }
            Points = 10;
        }

        public override int GetHashCode() =>
            this.Suit.GetHashCode() ^
            this.Value.GetHashCode() ^
            this.Points.GetHashCode();

        public override bool Equals(object obj)
        {
            if ((object)this == obj)
            {
                return true;
            }
            var other = obj as Card;
            if ((object)other == null)
            {
                return false;
            }
            return EqualsHelper(this, other);
        }
        protected static bool EqualsHelper(Card first, Card second) =>
            first.Suit == second.Suit &&
            first.Value == second.Value &&
            first.Points == second.Points;
    }
}
