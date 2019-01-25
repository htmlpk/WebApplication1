using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
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
       
        public override async Task<UserInGame> FindById(string id)
        {
            return _database.UserInGame.Where(item=>item.Id == Guid.Parse(id)).First();
        }

        public async Task<string> GetUserId(string userName)
        {
            var lastgame = _database.Users.Where(item => item.Email == userName).Select(item=>item.Id).First();
            return lastgame;
        }

        public async Task<IEnumerable<string>> GetBotsIds()
        {
            var lastgame = _database.Users.Where(item => item.Email.Contains("Bot")).OrderBy(item2=>item2.Email).Select(item => item.Id);
            return lastgame;
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var lastgame = _database.UserInGame.Where(item => item.GameId == id);
            return lastgame;
        }
    }
}
