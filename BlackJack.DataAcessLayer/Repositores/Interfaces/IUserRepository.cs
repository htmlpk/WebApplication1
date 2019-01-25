using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface IUserRepository : IBaseRepository<UserInGame>
    {
        Task<IEnumerable<string>> GetBotsIds();
        Task<string> GetUserId(string userName);
        Task<IEnumerable<UserInGame>> FindByGameId(Guid id);
    }
}