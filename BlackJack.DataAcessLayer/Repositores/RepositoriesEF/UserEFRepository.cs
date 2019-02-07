using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class UserEFRepository : BaseEFRepository<UserInGame>, IUserRepository
    {
        public UserEFRepository(ApplicationDbContext database)
            : base(database)
        {
        }
       
        public async Task<UserInGame> FindById(string id)
        {
            return await _database.UserInGames.Where(item=>item.Id == Guid.Parse(id)).FirstOrDefaultAsync();
        }

        public async Task<string> GetUserId(string userName)
        {
            var userIds = await _database.Users.Where(item => item.Email == userName).Select(item=>item.Id).FirstOrDefaultAsync();
            return userIds;
        }

        public async Task<List<User>> GetBots()
        {
            var botsIds = await _database.Users.Where(x => x.Email.Contains("Bot")).OrderBy(y=>y.Email).ToListAsync();
            return botsIds;
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var gameById = await _database.UserInGames.Where(item => item.GameId == id).ToListAsync();
            return gameById;
        }
    }
}
