using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace BlackJack.DataAccessLayer.Entities
{
    public class UserInGame : BaseEntity
    {
        public string UserId{ get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public bool IsDealer { get; set; }
        public bool IsFinished { get; set; }
        public int Points { get; set; }
        public string GamerStatus { get; set; }
        [Computed]
        public virtual User ApplicationUser { get; set; }
        [Computed]
        public virtual Game Game { get; set; }
        [Computed]
        public virtual List<GameRound> Rounds { get; set; }
    }
}
