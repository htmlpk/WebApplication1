
using BlackJack.DataAcessLayer.BaseRepository;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UI.Data;

namespace BlackJack.DataAcessLayer.Repository
{
    public abstract class BaseEFRepository<T> where T : BasedEntity
    {
        protected ApplicationDbContext _database;

        public BaseEFRepository(ApplicationDbContext database)
        {
            _database = database;
        }
               
       
    }
}
