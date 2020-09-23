using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Data.Repositories
{
    class RegistryRepository:GenericRepository<Registry>,IRegistryRepository
    {
        
        public RegistryRepository(BulbasaurContext context)
        :base(context)
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
