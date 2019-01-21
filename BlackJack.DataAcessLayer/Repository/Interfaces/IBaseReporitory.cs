
using System;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.DataAcessLayer.BaseRepository
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
