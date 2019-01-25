using BlackJack.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface IGameRepository : IBaseGuidRepository<Game>
    {
        Task<IEnumerable<Game>> GetAll(string userName);
        Task<Game> GetLastGame(string userName);
    }
}