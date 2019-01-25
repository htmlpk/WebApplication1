using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer
{
    public interface IGameService
    {
        Task StartGame(string username, int countogbots);
        Task<Match> NextRound(string userName, bool isUserNeedCard);
        Task<Match> GetLastMatch(string userName);
        Task<Match> GetMatchById(Guid id);
        Task<IEnumerable<Game>> GetAll(string userName);
    }
}
