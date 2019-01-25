using BlackJack.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface IUserRepository : IBaseGuidRepository<UserInGame>
    {
        Task<IEnumerable<string>> GetBotsIds();
        Task<string> GetUserId(string userName);
        Task<IEnumerable<UserInGame>> FindByGameId(Guid id);
    }
}