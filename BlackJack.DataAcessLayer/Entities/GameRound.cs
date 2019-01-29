using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackJack.DataAccessLayer.Entities
{
    public class GameRound : BaseEntity
    {
        [ForeignKey("Id")]
        public Guid GameId { get; set; }
        [ForeignKey("Id")]
        public Guid UserInGameId { get; set; }
        public int RaundNumber { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public int Points { get; set; }
        [Computed]
        public virtual UserInGame Users { get; set; }
    }
}
