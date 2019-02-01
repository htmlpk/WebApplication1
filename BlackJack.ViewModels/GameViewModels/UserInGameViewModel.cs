using BlackJack.DataAcсessLayer.Enums;
using System;

namespace BlackJack.ViewModels
{
    public class UserInGameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDealer { get; set; }
        public bool IsFinished { get; set; }
        public int Points { get; set; }
        public GamerStatus GamerStatus { get; set; }
    }
}
