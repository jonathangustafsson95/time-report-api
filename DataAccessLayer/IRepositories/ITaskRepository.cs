using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.IRepositories
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        List<Task> GetAllByMissionId(int id);

    }
}
