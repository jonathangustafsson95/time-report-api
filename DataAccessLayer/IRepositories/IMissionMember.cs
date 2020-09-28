using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;


namespace DataAccessLayer.IRepositories
{
    public interface IMissionMember: IGenericRepository<MissionMember>
    {
        List<MissionMember> GetAllByUserId(int id);
        List<MissionMember> GetAllByMissionId(int id);
    }
}
