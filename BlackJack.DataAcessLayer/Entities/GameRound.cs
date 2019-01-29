using Dapper.Contrib.Extensions;
using System;

namespace BlackJack.DataAccessLayer.Entities
{
    public class GameRound : BaseEntity
    {
        public Guid GameId { get; set; }
        public Guid UserInGameId { get; set; }
        public int RaundNumber { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public int Points { get; set; }
        [Computed]
        public virtual UserInGame Users { get; set; }
    }
}
