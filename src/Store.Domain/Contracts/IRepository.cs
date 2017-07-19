using System.Collections.Generic;

namespace Store.Domain.Contracts
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
    }
}
