using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public abstract class BaseEFRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _database;

        public BaseEFRepository(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task Add(T entity)
        {
            await _database.Set<T>().AddAsync(entity);
            await _database.SaveChangesAsync();
        }

        public async Task Add(List<T> entities)
        {
            await _database.Set<T>().AddRangeAsync(entities);
            await _database.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _database.Set<T>().Update(entity);
            await _database.SaveChangesAsync();
        }

        public async Task Update(List<T> entities)
        {
            _database.Set<T>().UpdateRange(entities);
            await _database.SaveChangesAsync();
        }
       
    }
}
