using BlackJack.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface ICardRepository : IBaseGuidRepository<GameRound>
    {
        Task<IEnumerable<GameRound>> FindByGameId(Guid Id);
        Task<IEnumerable<GameRound>> FindByUserId(Guid Id);
    }
}