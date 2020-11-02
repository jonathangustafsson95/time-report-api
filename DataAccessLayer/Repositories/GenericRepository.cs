using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly DbContext _context = null;
        private readonly DbSet<T> _table = null;
        public GenericRepository(BulbasaurDevContext context)
        {
            this._context = context;
            _table = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }
        public T GetById(object id)
        {
            return _table.Find(id);
        }
        public void Insert(T obj)
        {
            _table.Add(obj);
        }
        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Delete(object firstId, object secondId)
        {
           T existing = _table.Find(firstId, secondId);
            _table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public bool Exists(object id)
        {
            T model = _table.Find(id);
            return model != null;
        }
        /// <summary>
        /// This method takes a function getKey that returns a string which is used to set on which 
        /// class property the list is going to be filtered on based on the 
        /// searchKey parameter.
        /// </summary>
        /// <param name="getKey"></param>
        /// <param name="searchString"></param>
        /// <returns> A list of objects that matches the search string. </returns>
        public List<T> Search<T>(Func<T, string> getKey, string searchString)
        {
            IEnumerable<T> list = (IEnumerable<T>)GetAll();
            return list.Where(x => getKey(x).Contains(searchString)).ToList();
        }
    }
}
