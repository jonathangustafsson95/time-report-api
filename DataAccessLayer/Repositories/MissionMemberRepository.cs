using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.IReppositories;
using DataAccessLayer.IRepositories;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class MissionMemberRepository : GenericRepository<MissionMember>, IMissionMember
    {
        public MissionMemberRepository(BulbasaurDevContext context)
            :base(context)
        {

        }
        public List<MissionMember> GetAllByUserId(int id)
        {
            IEnumerable<MissionMember> all = GetAll();
            IEnumerable<MissionMember> allByUserById = from a in all
                                                      where a.UserId == id
                                                      select a;
            return allByUserById != null ? allByUserById.ToList() : new List<MissionMember>();

        }
        public List<MissionMember> GetAllByMissionId(int id)
        {
            IEnumerable<MissionMember> all = GetAll();
            IEnumerable<MissionMember> allByMissionById = from a in all
                                                       where a.MissionId == id
                                                       select a;
            return allByMissionById != null ? allByMissionById.ToList() : new List<MissionMember>();

        }

    }
}
