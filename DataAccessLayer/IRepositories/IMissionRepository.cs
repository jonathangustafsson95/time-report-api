using CommonLibrary.Model;
using DataAccessLayer.IReppositories;
using System.Collections.Generic;

namespace DataAccessLayer.IRepositories
{
    public interface IMissionRepository: IGenericRepository<Mission>
    {
        List<Mission> GetAllByCustomerId(int id);
    }
}

