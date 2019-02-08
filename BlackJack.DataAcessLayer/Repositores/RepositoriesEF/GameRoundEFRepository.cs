using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class GameRoundEFRepository : BaseEFRepository<GameRound>, IRoundRepository
    {
        public GameRoundEFRepository(ApplicationDbContext database)
            :base(database)
        {
        }

        public async Task<GameRound> FindById(string id)
        {
            var roundsById = await _database.GameRounds.Where(item => item.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            return roundsById;
        }
        public async Task<IEnumerable<GameRound>> FindByGameId(Guid id)
        {
            var roundsByGame = await _database.GameRounds.Where(item => item.GameId == id).ToListAsync();
            return roundsByGame;
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            var roundsByUser = await _database.GameRounds.Where(item => item.UserInGameId == id).ToListAsync();
            return roundsByUser;
        }
    }
}
