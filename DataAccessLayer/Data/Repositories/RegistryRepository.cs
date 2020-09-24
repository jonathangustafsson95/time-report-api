using System;
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

        public List<Registry> GetRegistriesLastWeek()
        {
            var registries = GetAll();
            var enumerable = registries.ToList();
            enumerable.OrderBy(d => d.date);
            return (List<Registry>)enumerable.Take(7);
        }
    }
    
    }
