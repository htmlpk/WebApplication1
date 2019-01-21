using BlackJack.DataAcessLayer.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Entities;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface ICardRepository : IBaseGuidRepository<GameRound>
    {
        Task<IEnumerable<GameRound>> FindByGameId(Guid Id);
        Task<IEnumerable<GameRound>> FindByUserId(Guid Id);
    }
}