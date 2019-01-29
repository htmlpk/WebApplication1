﻿using BlackJack.DataAccessLayer.Entities;
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

        public async Task<IEnumerable<string>> GetBotsIds()
        {
            var botsIds = "Select Id from AspNetUsers where Email LIKE 'Bot%' Order By Email;";
            var ids = await Connection.QueryAsync<string>(botsIds);
            return ids;
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var usersByGame = await Connection.QueryAsync<UserInGame>($"SELECT * FROM {_tableName} WHERE GameId= @id", new { id = id.ToString() });
            return usersByGame;
        }
    }
}
