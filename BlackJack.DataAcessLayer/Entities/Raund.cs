
using BlackJack.DataAcessLayer;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace UI.Entities
{
    [Table("Raund")]
    public class Raund : BasedEntity
    {

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ID")]
        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ID")]
        public Guid UserInGameId { get; set; }
        
        public int RaundNumber { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public int Points { get; set; }

        [Computed]
        public virtual UserInGame Users { get; set; }


        public Raund()
        {
            
        }
        public Raund(Guid id_Game, string value, string suit, int points)
        {
           
            
            this.GameId = GameId;
            this.Value = value;
            this.Suit = suit;
            this.Points = points;

        }

       

    }
}
