using System;
using System.Collections.Generic;

namespace DataAccessLayer.IReppositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Delete(object firstId, object secondId);
        void Save();
        bool Exists(object id);
        List<T> Search<T>(Func<T, string> getKey, string searchString);
    }
}
