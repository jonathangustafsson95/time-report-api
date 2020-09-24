using System;
using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.Data.IReppositories;

namespace DataAccessLayer.Data.IRepositories
{
    public interface IRegistryRepository : IGenericRepository<Registry>
    {
        List<Registry> GetRegistriesByNumberOfDays(int days, int id);
        List<Registry> GetAllByUserId(int id);
        List<Registry> GetRegistriesByDate(DateTime startDate, DateTime endDate, int userId);
    }
}
