using BlackJack.DataAccessLayer.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private string _connectionString;
        private IDbConnection _connection;
        protected readonly string _tableName;

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
