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
            var getAllGames = $@"Select g.* from {_tableName} g, UserInGames u where u.GameId = g.Id and u.Name = @username";
            var allGames = await Connection.QueryAsync<Game>(getAllGames, new { username = userName});
            return allGames.OrderByDescending(item => item.Date);
        }

        public async Task<Game> GetLastGame(string userName)
        {
            var sqllastGame = $"Select g.* from {_tableName} g,UserInGames u,AspNetUsers a where u.GameId = g.Id and a.Id = u.UserId and a.Email = @username and g.Date = (Select Max(Date) from Games) ";
            var lastGame = await Connection.QueryFirstAsync<Game>(sqllastGame,new { username = userName});
            return lastGame;
        }
    }
}
