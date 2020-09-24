using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Data.IReppositories;

namespace DataAccessLayer.Data.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        private readonly DbContext _context = null;
        private readonly DbSet<T> _table = null;
        public GenericRepository(BulbasaurContext context)
        {
            this._context = context;
            _table = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }
        //public IEnumerable<T>GetAllById(int id)
        //{
        //    var All = GetAll();
        //    T test= _table.
        //    List<T> _list = LikeSearch<T>(All,x=>x.);
        //    var list = user.Roles.Select(r => r.RoleId);
        //    var roles = db.Roles.Where(r => listOfRoleId.Contains(r.RoleId));
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
        public List<T> Search<T>(List<T> list, Func<T, string> getKey, string searchString)
        {
            return list.Where(x => getKey(x).Contains(searchString)).ToList();
        }
        public List<T> Search<T>(List<T> list, Func<T, int> getKey, int searchId)
        {
            return list.Where(x => getKey(x).Equals(searchId)).ToList();
        }
        public List<T> Search<T>(List<T> list, Func<T, DateTime> getKey, DateTime searchDate)
        {
            return list.Where(x => getKey(x).Equals(searchDate)).ToList();
        }



    }
}
