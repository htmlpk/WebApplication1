using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface ICardRepository : IBaseRepository<GameRound>
    {
        Task<IEnumerable<GameRound>> FindByGameId(Guid Id);
        Task<IEnumerable<GameRound>> FindByUserId(Guid Id);
    }
}