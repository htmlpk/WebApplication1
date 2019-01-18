using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UI.Entities;

namespace BlackJack.DataAcessLayer.Data
{
    public class Gamer
    {
        [Key]
        public string Name {get;set; }

        public UserInGame UserInGame { get; set; }

        public List<Card> Cards {get;set; }

      

        
    }
}
