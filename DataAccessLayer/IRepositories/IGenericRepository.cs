using System;
using System.Collections.Generic;

namespace DataAccessLayer.IReppositories
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T GetById(object firstId, object secondId);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Delete(object firstId, object secondId);
        void Save();
        bool Exists(object id);
        bool Exists(object firstId, object secondId);
        List<T> Search<T>(/*List<T> list,*/ Func<T, string> getKey, string searchString);
        List<T> Search<T>(/*List<T> list,*/ Func<T, int> getKey, int searchId);
        List<T> Search<T>(/*List<T> list,*/ Func<T, DateTime> getKey, DateTime searchDate); 



    }
}
