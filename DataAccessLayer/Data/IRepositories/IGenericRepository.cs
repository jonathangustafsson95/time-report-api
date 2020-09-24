﻿using System;
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
        List<T> Search<T>(/*List<T> list,*/ Func<T, string> getKey, string searchString);
        List<T> Search<T>(/*List<T> list,*/ Func<T, int> getKey, int searchId);
        List<T> Search<T>(/*List<T> list,*/ Func<T, DateTime> getKey, DateTime searchDate); 



    }
}
