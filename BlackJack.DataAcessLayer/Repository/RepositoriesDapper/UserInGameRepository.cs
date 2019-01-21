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
    public class UserInGameRepository : BaseRepository<UserInGame>, IUserRepository
    {
        public UserInGameRepository(string connectionString)
            : base("UserInGame",connectionString)
        {
        }

        public async Task<string> GetUserId(string userName)
        {
            var sqlIds = $"Select Id,Email from AspNetUsers where Email = '{userName}';";
            var id = await Connection.QueryFirstAsync<string>(sqlIds);
            return id;
        }

        public async Task<IEnumerable<string>> GetBotsIds()
        {
            var sqlIds = "Select Id,Email from AspNetUsers where Email LIKE 'Bot%' Order By Email;";
            var ids = await Connection.QueryAsync<string>(sqlIds);
            return ids;
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var result = await Connection.QueryAsync<UserInGame>($"SELECT * FROM {_tableName} WHERE GameId='{id}'");
            return result;
        }




    }
}
