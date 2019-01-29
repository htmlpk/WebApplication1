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
            var sqlGetCardswByGameId = $"Select r.* from {_tableName} r,UserInGames u,Games g where u.Id = r.UserInGameId and g.Id = u.GameId and g.Id = @id";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId, new { id = id.ToString() });
            return cards;
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            var sqlGetCardswByGameId = $"Select r.* from {_tableName} r Where UserInGameId = @id";
            var cards = await Connection.QueryAsync<GameRound>(sqlGetCardswByGameId, new { id = id.ToString() });
            return cards;
        }
    }
}
