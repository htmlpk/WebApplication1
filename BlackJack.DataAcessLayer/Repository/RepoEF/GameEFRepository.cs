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
    public class GameEFRepository : BaseEFRepository<Game>, IGameRepository
    {
        public GameEFRepository(ApplicationDbContext database)
            : base(database)
        {

        }

        public async Task Add(Game item)
        {
            await _database.Game.AddAsync(item);
            await _database.SaveChangesAsync();
        }
        public virtual async Task Add(List<Game> item)
        {
            await _database.Game.AddRangeAsync(item);
            await _database.SaveChangesAsync();
        }

        public virtual async Task Update(Game item)
        {

        }
        public virtual async Task Update(List<Game> items)
        {
            _database.Game.UpdateRange(items);
            await _database.SaveChangesAsync();
        }
    

        public virtual async Task Remove(List<string> idToDelete)
        {
        }

        public virtual async Task RemoveById(string id)
        {
        }

        public virtual async Task<Game> FindById(string id)
        {
            return _database.Game.Where(item => item.ID == Guid.Parse(id)).First();
        }
        public async Task<List<Game>> GetAll(string userName)
        {
            
            var a = _database.Game.ToList();
                        
            return a;
        }
       
        public async Task<Game> GetLastGame(string userName)
        {

            var game = _database.Game.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicatonUser.Email == userName) != null).OrderByDescending(t => t.Data).FirstOrDefault();
            return game;
        }


    }
}
