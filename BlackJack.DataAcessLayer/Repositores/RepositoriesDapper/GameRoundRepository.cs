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
            var roundsByGame = $"Select r.* from {_tableName} r,UserInGames u,Games g where u.Id = r.UserInGameId and g.Id = u.GameId and g.Id = @id";
            var rounds = await Connection.QueryAsync<GameRound>(roundsByGame, new { id = id.ToString() });
            return rounds;
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            var roundsByUser = $"Select r.* from {_tableName} r Where UserInGameId = @id";
            var rounds = await Connection.QueryAsync<GameRound>(roundsByUser, new { id = id.ToString() });
            return rounds;
        }
    }
}
