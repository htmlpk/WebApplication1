using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class GameEFRepository : BaseEFRepository<Game>, IGameRepository
    {
        public GameEFRepository(ApplicationDbContext database)
            : base(database)
        {
        }

        public async Task<Game> FindById(string id)
        {
            var gamesById = await _database.Games.Where(item => item.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            return gamesById;
        }
        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            var allUsersGames = await _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null)
                .OrderByDescending(t => t.Date).ToListAsync();
            return allUsersGames;
        }
       
        public async Task<Game> GetLastGame(string userName)
        {
            var lastUsersGame = await _database.Games.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicationUser.Email == userName) != null)
                .OrderByDescending(t => t.Date).FirstOrDefaultAsync();
            return lastUsersGame;
        }
    }
}
