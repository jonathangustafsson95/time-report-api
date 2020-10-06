using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.Repositories
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
                                               where a.UserId == id
                                               select a;
            return allByRegistryById != null ? allByRegistryById.ToList() : new List<Registry>();
        }
        // GetByID får ju bara ut en reg? 
        public List<Registry> GetRegistriesByNumberOfDays(int days, int userId)
        {
            List<Registry> registries = GetAllByUserId(userId);
            var enumerable = registries.ToList();
            enumerable.OrderBy(d => d.Date);
            return enumerable.Take(days).ToList();
        }

        public List<Registry> GetAllUserRegistries(int userId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allByUserId = from a in all
                                                      where a.UserId == userId
                                                      select a;
            return allByUserId != null ? allByUserId.ToList() : new List<Registry>();
        }
        public List<Registry> GetRegistriesByDate(DateTime startDate, DateTime endDate, int userId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allRegistriesBetweenDates = from a in all
                                                              where a.UserId == userId
                                                              && (a.Date <= endDate && a.Date >= startDate)
                                                              select a;
            return allRegistriesBetweenDates != null ? allRegistriesBetweenDates.ToList() : new List<Registry>();
        }

        public List<Registry> GetLatestRegistries(int nrOfRegs, int userId)
        {
            List<Registry> registries = GetAllByUserId(userId);
            var enumerable = registries.ToList();
            enumerable.OrderBy(d => d.RegistryId);
            return enumerable.Take(nrOfRegs).ToList();
        }

    }
    
}
