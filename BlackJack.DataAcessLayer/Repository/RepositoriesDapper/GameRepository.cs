using BlackJack.DataAcessLayer.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(string connectionString)
            : base("Game", connectionString)
        {
        }

        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            var getAllGames = $@"Select {_tableName}.* from {_tableName} where UserInGame.GameId = Game.Id and UsernInGame.Name = {userName}";
            var allGames = await Connection.QueryAsync<Game>(getAllGames);
            return allGames.OrderByDescending(item => item.Data);
        }

        public async Task<Game> GetLastGame(string userName)
        {
            var sqllastGame = $"Select {_tableName}.* from {_tableName},UserInGame,AspNetUsers where UserInGame.GameId = {_tableName}.ID and AspNetUsers.Id = UserInGame.UserId and AspNetUsers.Email = '{userName}' and {_tableName}.Data = (Select Max(Data) from {_tableName}) ";
            var lastGame = await Connection.QueryFirstAsync<Game>(sqllastGame);
            return lastGame;
        }
    }
}
