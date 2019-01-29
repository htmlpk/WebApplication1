using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Game> FindById(string id)
        {
            return await _database.Games.Where(item => item.Id == Guid.Parse(id)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            return await _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null).OrderByDescending(t => t.Date).ToListAsync();
        }
       
        public async Task<Game> GetLastGame(string userName)
        {
            return await _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null).OrderByDescending(t => t.Date).FirstOrDefaultAsync();
        }
    }
}
