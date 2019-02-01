using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class MatchViewModel
    {
        public GameViewModel Game { get; set; }
        public List<UserInGameViewModel> Gamers { get; set; }
        public List<RoundViewModel> Rounds { get; set; }
    }
}
