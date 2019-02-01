using System;

namespace BlackJack.ViewModels
{
    public class RoundViewModel
    {
        public Guid UserInGameId { get; set; }
        public int RoundNumber { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public int Points { get; set; }
    }
}
