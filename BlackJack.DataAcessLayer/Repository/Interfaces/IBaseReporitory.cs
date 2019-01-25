﻿using BlackJack.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public interface IBaseGuidRepository<T> where T : BasedEntity
    {
        Task Add(List<T> item);
        Task Add(T item);
        Task<T> FindById(string id);
        Task Update(List<T> items);
        Task Update(T item);
    }
}
