using BlackJack.DataAccessLayer.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(string connectionString)
            : base("Games", connectionString)
        {
        }

        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            var allGames = $@"Select g.* from {_tableName} g, UserInGames u where u.GameId = g.Id and u.Name = @username";
            var allUsersGames = await Connection.QueryAsync<Game>(allGames, new { username = userName});
            return allUsersGames.OrderByDescending(item => item.Date);
        }

        public async Task<Game> GetLastGame(string userName)
        {
            var lastGame = $"Select g.* from {_tableName} g,UserInGames u,AspNetUsers a " +
                $"where u.GameId = g.Id and a.Id = u.UserId and a.Email = @username and g.Date = (Select Max(Date) from Games) ";
            var lastUsersGame = await Connection.QueryFirstOrDefaultAsync<Game>(lastGame, new { username = userName});
            return lastUsersGame;
        }
    }
}
