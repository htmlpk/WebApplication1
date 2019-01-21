using BlackJack.DataAcessLayer.BaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Entities;

namespace BlackJack.DataAcessLayer.Repository
{
    public interface IGameRepository : IBaseGuidRepository<Game>
    {
        Task<IEnumerable<Game>> GetAll(string userName);
        Task<Game> GetLastGame(string userName);
    }
}