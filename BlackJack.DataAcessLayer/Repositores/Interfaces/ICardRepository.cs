using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface ICardRepository : IBaseRepository<GameRound>
    {
        Task<IEnumerable<GameRound>> FindByGameId(Guid id);
        Task<IEnumerable<GameRound>> FindByUserId(Guid id);
    }
}