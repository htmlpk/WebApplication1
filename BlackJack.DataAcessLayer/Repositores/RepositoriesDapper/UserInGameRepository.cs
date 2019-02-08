using BlackJack.DataAccessLayer.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class UserInGameRepository : BaseRepository<UserInGame>, IUserRepository
    {
        public UserInGameRepository(string connectionString)
            : base("UserInGames",connectionString)
        {
        }

        public async Task<string> GetUserId(string userName)
        {
            var userIds = $"Select Id from AspNetUsers where Email = @username;";
            var id = await Connection.QueryFirstOrDefaultAsync<string>(userIds, new { username = userName });
            return id;
        }

        public async Task<(List<User> bots,User dealer)> GetBotsAndDealer(int countOfBots)
        {
            var botssql = "Select TOP(@countofbots) * from AspNetUsers where Email LIKE 'Bot%' and Email NOT LIKE 'BotDealer' Order By Email ";
            var bots = await Connection.QueryAsync<User>(botssql, new { countofbots = countOfBots-1 });
            var dealersql = "Select * from AspNetUsers where Email LIKE 'BotDealer'";
            var dealer = await Connection.QueryFirstOrDefaultAsync<User>(dealersql);
            return (bots.AsList(),dealer);
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var usersByGame = await Connection.QueryAsync<UserInGame>($"SELECT * FROM {_tableName} WHERE GameId= @id", new { id = id.ToString() });
            return usersByGame;
        }
    }
}
