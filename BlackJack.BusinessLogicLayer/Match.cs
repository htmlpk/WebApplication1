using BlackJack.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BlackJack.BusinessLogicLayer
{
    public class Match
    {
        public Game Game { get; set; }
        public IEnumerable<UserInGame> Gamers { get; set; }
        public IEnumerable<GameRound> Cards { get; set; }
    }
}
