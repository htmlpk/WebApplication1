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
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(string connectionString)
            : base("Game", connectionString)
        {

        }

        public async Task<List<Game>> GetAll(string userName)
        {
            var getAllGames = "Select * from Game";

            var allGames = await Connection.QueryAsync<Game>(getAllGames);

            return allGames.OrderByDescending(item => item.Data).ToList();
        }

        


        

        public async Task<Game> GetLastGame(string userName)
        {
            var sqllastGame = $"Select Game.* from Game,UserInGame,AspNetUsers where UserInGame.GameId = Game.ID and AspNetUsers.Id = UserInGame.UserId and AspNetUsers.Email = '{userName}' and Game.Data = (Select Max(Data) from Game) ";

            var lastGame = await Connection.QueryFirstAsync<Game>(sqllastGame);

            return lastGame;



        }


    }
}
