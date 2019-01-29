using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class GameRoundsEFRepository : BaseEFRepository<Game>, IGameRepository
    {
        public GameRoundsEFRepository(ApplicationDbContext database)
            : base(database)
        {
        }

        public override async Task<Game> FindById(string id)
        {
            return _database.Games.Where(item => item.Id == Guid.Parse(id)).First();
        }
        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            return _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null).OrderByDescending(t => t.Data);
        }
       
        public async Task<Game> GetLastGame(string userName)
        {
            return _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null).OrderByDescending(t => t.Data).FirstOrDefault();
        }
    }
}
