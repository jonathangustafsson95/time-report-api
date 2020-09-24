﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.Data.IRepositories;

namespace DataAccessLayer.Data.Repositories
{
    public class RegistryRepository : GenericRepository<Registry>, IRegistryRepository
    {

        public RegistryRepository(BulbasaurContext context)
            : base(context)
        {

        }
        public List<Registry> GetAllByUserId(int id)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allByRegistryById= from a in all
                                               where a.userId == id
                                               select a;
            return allByRegistryById != null ? allByRegistryById.ToList() : new List<Registry>();


        }
        public List<Registry> GetRegistriesByNumberOfDays(int days, int id)
        {
            List<Registry> registries = GetAllByUserId(id);
            var enumerable = registries.ToList();
            enumerable.OrderBy(d => d.date);
            return (List<Registry>)enumerable.Take(days);
        }
    }
    
    }
