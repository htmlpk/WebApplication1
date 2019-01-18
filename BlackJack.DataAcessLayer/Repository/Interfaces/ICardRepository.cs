using BlackJack.DataAcessLayer.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Entities;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface ICardRepository : IBaseGuidRepository<Raund>
    {
        Task<List<Raund>> FindByGameId(Guid Id);
        Task<List<Raund>> FindByUserId(Guid Id);
    }
}