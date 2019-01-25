using BlackJack.DataAcessLayer;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace UI.Entities
{
    [Table("Game")]
    public class Game : BasedEntity
    {
        public DateTime Data { get; set; }
        public bool IsFinished { get; set; }
        public int CountOfRounds { get; set; }
        [Computed]
        public virtual List<UserInGame> UserInGame { get; set; }
    }
}
