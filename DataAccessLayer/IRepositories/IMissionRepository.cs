using CommonLibrary.Model;
using DataAccessLayer.IReppositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.IRepositories
{
    public interface IMissionRepository: IGenericRepository<Mission>
    {
        List<Mission> GetAllByUserId(int id);
        List<Mission> GetAllByMissionId(int id);
        List<Mission> GetAllByCustomerId(int id);
    }
}

