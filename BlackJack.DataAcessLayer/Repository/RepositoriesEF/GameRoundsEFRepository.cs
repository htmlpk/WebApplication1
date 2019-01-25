using BlackJack.DataAcessLayer.Context;
using BlackJack.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public class GameRoundsEFRepository : BaseEFRepository<Game>, IGameRepository
    {
        public GameRoundsEFRepository(ApplicationDbContext database)
            : base(database)
        {
        }

        public override async Task<Game> FindById(string id)
        {
            return _database.Game.Where(item => item.Id == Guid.Parse(id)).First();
        }
        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            return _database.Game.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicatonUser.Email == userName) != null).OrderByDescending(t => t.Data);
        }
       
        public async Task<Game> GetLastGame(string userName)
        {
            return _database.Game.Where(item => item.UserInGame.FirstOrDefault(item2 => item2.ApplicatonUser.Email == userName) != null).OrderByDescending(t => t.Data).FirstOrDefault();
        }
    }
}
