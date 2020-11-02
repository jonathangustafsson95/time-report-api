using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;


namespace DataAccessLayer.IRepositories
{
    public interface IMissionMemberRepository: IGenericRepository<MissionMember>
    {
        List<MissionMember> GetAllByUserId(int id);
        List<MissionMember> GetAllByMissionId(int id);
    }
}
