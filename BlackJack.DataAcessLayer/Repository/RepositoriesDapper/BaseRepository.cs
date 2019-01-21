
using BlackJack.DataAcessLayer.BaseRepository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.Repository
{
    public abstract class BaseRepository<T> : IBaseGuidRepository<T> where T : BasedEntity
    {
        private string _connectionString;
        protected readonly string _tableName;
        private IDbConnection _connection;

        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public BaseRepository(string tableName, string connectionString)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        public virtual async Task Add(T item)
        {
            DapperHack();
            await Connection.InsertAsync(item);
        }

        public virtual async Task Add(List<T> items)
        {
            DapperHack();
            await Connection.InsertAsync(items);
        }

        public virtual async Task Update(List<T> items)
        {
            await Connection.UpdateAsync(items);
        }

        public virtual async Task Update(T item)
        {
            await Connection.UpdateAsync(item);
        }

        public async Task Remove(List<string> idToDelete)
        {
            if (idToDelete == null || idToDelete.Count < 1)
            {
                return;
            }

            var IdsForDelete = new List<T>();

            foreach (var id in idToDelete)
            {
                var itemForDelete = (T)Activator.CreateInstance(typeof(T), new object[] { });
                itemForDelete.Id = Guid.Parse(id);
                IdsForDelete.Add(itemForDelete);
            }
            await Connection.DeleteAsync(IdsForDelete);
        }

        public virtual async Task Remove(T item)
        {
            await Connection.ExecuteAsync("DELETE FROM " + _tableName + " WHERE Id=@Id", new { Id = item.Id });
        }

        public virtual async Task RemoveById(string id)
        {
            await Connection.ExecuteAsync("DELETE FROM " + _tableName + " WHERE Id=@Id", new { Id = id });
        }

        public virtual async Task<T> FindById(string id)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<T>("SELECT TOP(1) * FROM " + _tableName + " WHERE Id=@Id", new { Id = id });
            return result;
        }

        private static void DapperHack()
        {
            var cache = typeof(SqlMapperExtensions).GetField("KeyProperties", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null)
                as ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>;
            cache?.Clear();
        }
    }
}
