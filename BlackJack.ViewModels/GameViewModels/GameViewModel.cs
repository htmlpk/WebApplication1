using BlackJack.DataAcсessLayer.Enums;
using System;

namespace BlackJack.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public GameStatus Status { get; set; }
        public int CountOfRounds { get; set; }
    }
}
