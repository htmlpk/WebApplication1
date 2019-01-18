using System;
using System.Collections.Generic;
using System.Text;
using UI.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public class CardRepository : BaseRepository<Raund>, ICardRepository
    {
        public CardRepository(string connectionString)
            : base("CardOfUserInGame",connectionString)
        {
            
        }

        public async Task<List<Raund>> FindByGameId(Guid Id)
        {
            var sqlGetCardswByGameId = $"Select Raund.* from Raund,UserInGame,Game where UserInGame.ID = Raund.UserInGameId and Game.ID = UserInGame.GameId and Game.ID = '{Id}'";
            var cards = await Connection.QueryAsync<Raund>(sqlGetCardswByGameId);
            return cards.ToList();
        }

        public async Task<List<Raund>> FindByUserId(Guid Id)
        {
            var sqlGetCardswByGameId = $"Select Raund.* from Raund Where UserInGameId = '{Id}'";
            var cards = await Connection.QueryAsync<Raund>(sqlGetCardswByGameId);
            return cards.ToList();
        }
    }
}
