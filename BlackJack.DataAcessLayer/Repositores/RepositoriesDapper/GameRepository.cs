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
            var getAllGames = $@"Select {_tableName}.* from {_tableName},UserInGames where UserInGames.GameId = Games.Id and UserInGames.Name = @username";
            var allGames = await Connection.QueryAsync<Game>(getAllGames, new { username = userName});
            return allGames.OrderByDescending(item => item.Data);
        }

        public async Task<Game> GetLastGame(string userName)
        {
            var sqllastGame = $"Select {_tableName}.* from {_tableName},UserInGames,AspNetUsers where UserInGames.GameId = {_tableName}.ID and AspNetUsers.Id = UserInGames.UserId and AspNetUsers.Email = @username and {_tableName}.Data = (Select Max(Data) from {_tableName}) ";
            var lastGame = await Connection.QueryFirstAsync<Game>(sqllastGame,new { username = userName});
            return lastGame;
        }
    }
}
