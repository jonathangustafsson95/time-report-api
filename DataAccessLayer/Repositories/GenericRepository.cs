using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
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
        //public void GetAllById(int id)
        //{
            
        //}
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
        public void Save()
        {
            _context.SaveChanges();
        }
        public bool Exists(object id)
        {
            T model = _table.Find(id);
            return model != null;
        }
        public List<T> Search<T>(/*List<T> list,*/ Func<T, string> getKey, string searchString)
        {
            IEnumerable<T> list = (IEnumerable<T>)GetAll();
            return list.Where(x => getKey(x).Contains(searchString)).ToList();
        }
        public List<T> Search<T>(/*List<T> list,*/ Func<T, int> getKey, int searchId)
        {
            IEnumerable<T> list = (IEnumerable<T>)GetAll();
            return list.Where(x => getKey(x).Equals(searchId)).ToList();
        }
        public List<T> Search<T>(/*List<T> list,*/ Func<T, DateTime> getKey, DateTime searchDate)
        {
            IEnumerable<T> list = (IEnumerable<T>)GetAll();
            return list.Where(x => getKey(x).Equals(searchDate)).ToList();
        }



    }
}
