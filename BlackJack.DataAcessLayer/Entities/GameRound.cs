using Dapper.Contrib.Extensions;
using System;

namespace BlackJack.DataAccessLayer.Entities
{
    [Table("GameRound")]
    public class GameRound : BasedEntity
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Id")]
        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Id")]
        public Guid UserInGameId { get; set; }
        public int RaundNumber { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public int Points { get; set; }
        [Computed]
        public virtual UserInGame Users { get; set; }
        public GameRound()
        {
        }
    }
}
