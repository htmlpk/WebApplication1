using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class CardEFRepository : BaseEFRepository<GameRound>, ICardRepository
    {
        public CardEFRepository(ApplicationDbContext database)
            :base(database)
        {
        }

        public async Task<GameRound> FindById(string id)
        {
            return await _database.GameRounds.Where(item => item.Id == Guid.Parse(id)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<GameRound>> FindByGameId(Guid id)
        {
            return await _database.GameRounds.Where(item=>item.GameId == id).ToListAsync();
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            return await _database.GameRounds.Where(item => item.UserInGameId == id).ToListAsync();
        }
    }
}
