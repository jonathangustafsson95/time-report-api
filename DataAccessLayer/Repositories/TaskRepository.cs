using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(BulbasaurDevContext context)
            : base(context)
        {

        }
        public List<Task> GetAllByMissionId(int id)
        {
            IEnumerable<Task> all = GetAll();
            IEnumerable<Task> allByRegistryById = from a in all
                                                      where a.MissionId == id
                                                      select a;
            return allByRegistryById != null ? allByRegistryById.ToList() : new List<Task>();
        }
    }

}
