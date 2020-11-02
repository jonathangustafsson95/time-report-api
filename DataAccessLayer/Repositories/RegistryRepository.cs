using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// This method gets all registries ever made by a given user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all registries for the given user. </returns>

        public List<Registry> GetAllByUserId(int id)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allByRegistryById = from a in all
                                                      where a.UserId == id
                                                      select a;
            return allByRegistryById != null ? allByRegistryById.ToList() : new List<Registry>();
        }

        /// <summary>
        /// This method gets a number of registries for a given user, within specified dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userId"></param>
        /// <returns> A list of registries for a user, witihn the specified dates. </returns>
        public List<Registry> GetRegistriesByDate(DateTime startDate, DateTime endDate, int userId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allRegistriesBetweenDates = from a in all
                                                              where a.UserId == userId
                                                              && (a.Date <= endDate && a.Date >= startDate)
                                                              select a;
            return allRegistriesBetweenDates != null ? allRegistriesBetweenDates.ToList() : new List<Registry>();
        }


        /// <summary>
        /// This method gets a number of registries for a given user, for example the ten latest registries.
        /// It is used to display to the user it's recent history for suggestions when time reporting.
        /// </summary>
        /// <param name="nrOfRegs"></param>
        /// <param name="userId"></param>
        /// <returns> A list of registries for the given task. </returns>
        public List<Registry> GetLatestRegistries(int nrOfRegs, int userId)
        {
            List<Registry> registries = GetAllByUserId(userId);
            var enumerable = registries.ToList();
            return enumerable.OrderByDescending(d => d.RegistryId).Take(nrOfRegs).ToList();
        }

        /// <summary>
        /// This method gets all the registries for a task. Input is startDate and endDate, startDate being
        /// the date that the task was started and endDate being the task's end date or present time if the 
        /// task is not yet ended.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="taskId"></param>
        /// <returns> A list of registries for the given task. </returns>
        public List<Registry> GetRegistriesByTask(DateTime startDate, DateTime endDate, int taskId)
        {
            IEnumerable<Registry> all = GetAll();
            IEnumerable<Registry> allRegistriesBetweenDates = from a in all
                                                              where a.TaskId == taskId
                                                              && (a.Date <= endDate && a.Date >= startDate)
                                                              select a;
            return allRegistriesBetweenDates != null ? allRegistriesBetweenDates.ToList() : new List<Registry>();
        }
    }  
}
