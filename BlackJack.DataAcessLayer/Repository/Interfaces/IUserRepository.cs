using BlackJack.DataAcessLayer.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Entities;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface IUserRepository : IBaseGuidRepository<UserInGame>
    {
        Task<List<string>> GetBotsIds();
        Task<string> GetUserId(string userName);
        Task<IEnumerable<UserInGame>> FindByGameId(Guid id);
    }
}