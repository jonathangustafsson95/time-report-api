using System;
using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.IRepositories
{
    public interface IRegistryRepository : IGenericRepository<Registry>
    {
        List<Registry> GetAllByUserId(int id);
        List<Registry> GetRegistriesByDate(DateTime startDate, DateTime endDate, int userId);
        List<Registry> GetLatestRegistries(int nrOfRegs, int userId);
        List<Registry> GetRegistriesByTask(DateTime startDate, DateTime endDate, int taskId);
    }
}
