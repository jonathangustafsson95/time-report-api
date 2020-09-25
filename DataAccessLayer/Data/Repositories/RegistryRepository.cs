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
        public RegistryRepository(BulbasaurDevContext context)
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
        // GetByID får ju bara ut en reg? 
        public List<Registry> GetRegistriesByNumberOfDays(int days, int id)
        {
            List<Registry> registries = GetAllByUserId(id);
            var enumerable = registries.ToList();
            enumerable.OrderBy(d => d.date);
            return (List<Registry>)enumerable.Take(days);
        }

        public List<Registry> GetAllUserRegistries(int userId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allByUserId = from a in all
                                                      where a.userId == userId
                                                      select a;
            return allByUserId != null ? allByUserId.ToList() : new List<Registry>();
        }
        public List<Registry> GetRegistriesByDate(DateTime startDate, DateTime endDate, int userId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allRegistriesBetweenDates = from a in all
                                                              where a.userId == userId
                                                              && (a.date <= endDate && a.date >= startDate)
                                                              select a;
            return allRegistriesBetweenDates != null ? allRegistriesBetweenDates.ToList() : new List<Registry>();
        }

    }
    
    }
