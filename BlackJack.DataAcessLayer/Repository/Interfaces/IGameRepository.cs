using BlackJack.DataAcessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface IGameRepository : IBaseGuidRepository<Game>
    {
        Task<IEnumerable<Game>> GetAll(string userName);
        Task<Game> GetLastGame(string userName);
    }
}