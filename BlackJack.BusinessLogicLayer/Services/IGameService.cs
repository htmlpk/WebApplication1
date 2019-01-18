
using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<List<Game>> GetAll(string userName);
    }
}
