using BlackJack.DataAcessLayer;
using Dapper.Contrib.Extensions;
using System;

namespace UI.Entities
{
    [Table("GameRound")]
    public class GameRound : BasedEntity
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ID")]
        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ID")]
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
        public GameRound(Guid id_Game, string value, string suit, int points)
        {
            this.GameId = GameId;
            this.Value = value;
            this.Suit = suit;
            this.Points = points;
        }
    }
}
