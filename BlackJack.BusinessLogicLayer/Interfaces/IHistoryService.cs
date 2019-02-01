using BlackJack.DataAccessLayer.Entities;
using BlackJack.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer
{
    public interface IHistoryService
    {
        Task<MatchViewModel> GetMatchById(Guid id);
        Task<IEnumerable<Game>> GetAll(string userName);
    }
}
