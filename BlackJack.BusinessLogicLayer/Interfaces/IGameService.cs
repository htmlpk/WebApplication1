using BlackJack.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Entities;

namespace UI.Data.GameRepository
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
