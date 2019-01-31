using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer
{
    public interface IHistoryService
    {
        Task<Match> GetMatchById(Guid id);
        Task<IEnumerable<Game>> GetAll(string userName);
    }
}
