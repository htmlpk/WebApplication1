using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface IUserRepository : IBaseRepository<UserInGame>
    {
        Task<(List<User> bots, User dealer)> GetBotsAndDealer(int countOfBots);
        Task<string> GetUserId(string userName);
        Task<IEnumerable<UserInGame>> FindByGameId(Guid id);
    }
}