using System.Collections.Generic;

namespace DataAccessLayer.Data.IReppositories
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        bool Exists(object id);

    }
}
