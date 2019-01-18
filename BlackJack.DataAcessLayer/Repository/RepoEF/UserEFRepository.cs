using System;
using System.Collections.Generic;
using System.Text;
using UI.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace BlackJack.DataAcessLayer.Repository
{
    public class UserEFRepository : BaseEFRepository<UserInGame>, IUserRepository
    {
        public UserEFRepository(ApplicationDbContext database)
            : base(database)
        {
            
        }
        public async Task Add(UserInGame item)
        {
            await _database.UserInGame.AddAsync(item);
            await _database.SaveChangesAsync();
        }
        public virtual async Task Add(List<UserInGame> item)
        {
            await _database.UserInGame.AddRangeAsync(item);
            await _database.SaveChangesAsync();
        }

        public virtual async Task Update(UserInGame item)
        {

        }
        public virtual async Task Update(List<UserInGame> items)
        {
            _database.UserInGame.UpdateRange(items);
            await _database.SaveChangesAsync();
        }

       

        public virtual async Task<UserInGame> FindById(string id)
        {
            return _database.UserInGame.Where(item=>item.ID == Guid.Parse(id)).First();
        }

        public async Task<string> GetUserId(string userName)
        {
            var lastgame = _database.Users.Where(item => item.Email == userName).Select(item=>item.Id).First();
            return lastgame;

        }

        public async Task<List<string>> GetBotsIds()
        {
            var lastgame = _database.Users.Where(item => item.Email.Contains("Bot")).OrderBy(item2=>item2.Email).Select(item => item.Id);
            return lastgame.ToList();
        }

        public virtual async Task<IEnumerable<UserInGame>> FindByGameId(Guid id)
        {
            var lastgame = _database.UserInGame.Where(item => item.GameId == id);
            return lastgame;
        }




    }
}
