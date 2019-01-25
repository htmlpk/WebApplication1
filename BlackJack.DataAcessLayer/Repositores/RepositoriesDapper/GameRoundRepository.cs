using BlackJack.DataAccessLayer.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class GameRoundRepository : BaseRepository<GameRound>, ICardRepository
    {
        public GameRoundRepository(string connectionString)
            : base("GameRounds", connectionString)
        {
        }

        public async Task<IEnumerable<GameRound>> FindByGameId(Guid id)
        {
            var sqlGetCardswByGameId = $"Select {_tableName}.* from {_tableName},UserInGames,Games where UserInGames.ID = {_tableName}.UserInGameId and Games.ID = UserInGames.GameId and Games.ID = @id";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId, new { id = id.ToString() });
            return cards;
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            var sqlGetCardswByGameId = $"Select {_tableName}.* from {_tableName} Where UserInGameId = @id";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId, new { id = id.ToString() });
            return cards;
        }
    }
}
