using System;
using System.Collections.Generic;
using System.Text;
using UI.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UI.Data;

namespace BlackJack.DataAcessLayer.Repository
{
    public class CardEFRepository : BaseEFRepository<Raund>, ICardRepository
    {
        public CardEFRepository(ApplicationDbContext database)
            :base(database)
        {
            
        }
        public async Task Add(Raund item)
        {
            await _database.Raund.AddAsync(item);
            await _database.SaveChangesAsync();
        }
        public virtual async Task Add(List<Raund> item)
        {
            await _database.Raund.AddRangeAsync(item);
            await _database.SaveChangesAsync();
        }

        public virtual async Task Update(Raund item)
        {

        }
        public virtual async Task Update(List<Raund> items)
        {
            _database.Raund.UpdateRange(items);
            await _database.SaveChangesAsync();
        }

       

        public async Task Remove(List<string> idToDelete)
        {
            if (idToDelete == null || idToDelete.Count < 1)
            {
                return;
            }

            var IdsForDelete = new List<Raund>();

            foreach (var id in idToDelete)
            {
                var itemForDelete = (Raund)Activator.CreateInstance(typeof(Raund), new object[] { });
                itemForDelete.ID = Guid.Parse(id);
                IdsForDelete.Add(itemForDelete);
            }
            _database.RemoveRange(IdsForDelete);
        }

        public virtual async Task Remove(Raund item)
        {

        }

        public virtual async Task RemoveById(string id)
        {

        }

        public virtual async Task<Raund> FindById(string id)
        {
            return _database.Raund.Where(item=>item.ID == Guid.Parse(id)).First();
        }
        public async Task<List<Raund>> FindByGameId(Guid id)
        {
            return _database.Raund.Where(item=>item.GameId == id).ToList();
        }

        public async Task<List<Raund>> FindByUserId(Guid id)
        {
            return _database.Raund.Where(item => item.UserInGameId == id).ToList();
        }
    }
}
