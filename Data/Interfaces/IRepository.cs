using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IRepository<T>
    {
        void Save(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}