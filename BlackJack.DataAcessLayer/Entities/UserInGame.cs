using BlackJack.DataAcessLayer;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using UI.Data;

namespace UI.Entities
{
    [Table("UserInGame")]
    public class UserInGame : BasedEntity
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Id")]
        public string UserId{ get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ID")]
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public bool IsDealer { get; set; }
        public bool IsFinished { get; set; }
        public int Points { get; set; }
        public string GamerStatus { get; set; }
        [Computed]
        public virtual User ApplicatonUser { get; set; }
        [Computed]
        public virtual Game Game { get; set; }
        [Computed]
        public virtual List<GameRound> Cards { get; set; }

    }
}
