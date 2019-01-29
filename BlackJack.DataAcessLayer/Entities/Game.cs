using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace BlackJack.DataAccessLayer.Entities
{
    public class Game : BaseEntity
    {
        public DateTime Date { get; set; }
        public bool IsFinished { get; set; }
        public int CountOfRounds { get; set; }
        [Computed]
        public virtual List<UserInGame> UserInGame { get; set; }
    }
}
