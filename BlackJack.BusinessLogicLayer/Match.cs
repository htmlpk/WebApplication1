using System;
using System.Collections.Generic;
using System.Text;
using UI.Entities;

namespace BlackJack.BusinessLogicLayer
{
    public class Match
    {
        public Game Game { get; set; }
        public IEnumerable<UserInGame> Gamers { get; set; }
        public List<Raund> Cards { get; set; }
    }
}
