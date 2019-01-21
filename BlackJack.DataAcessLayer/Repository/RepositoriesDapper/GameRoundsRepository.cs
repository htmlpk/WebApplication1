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
    public class GameRoundsRepository : BaseRepository<GameRound>, ICardRepository
    {
        public GameRoundsRepository(string connectionString)
            : base("GameRound", connectionString)
        {
        }

        public async Task<IEnumerable<GameRound>> FindByGameId(Guid Id)
        {
            var sqlGetCardswByGameId = $"Select {_tableName}.* from {_tableName},UserInGame,Game where UserInGame.ID = {_tableName}.UserInGameId and Game.ID = UserInGame.GameId and Game.ID = '{Id}'";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId);
            return cards;
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid Id)
        {
            var sqlGetCardswByGameId = $"Select {_tableName}.* from {_tableName} Where UserInGameId = '{Id}'";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId);
            return cards;
        }
    }
}
