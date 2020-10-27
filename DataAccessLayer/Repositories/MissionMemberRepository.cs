using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.IReppositories;
using DataAccessLayer.IRepositories;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class MissionMemberRepository : GenericRepository<MissionMember>, IMissionMemberRepository
    {
        public MissionMemberRepository(BulbasaurDevContext context)
            :base(context)
        {

        }

        /// <summary>
        /// This method gets all the missions that a given user is a member of.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all missionmember-rows for the given user. </returns>

        public List<MissionMember> GetAllByUserId(int id)
        {
            IEnumerable<MissionMember> all = GetAll();
            IEnumerable<MissionMember> allByUserById = from a in all
                                                      where a.UserId == id
                                                      select a;
            return allByUserById != null ? allByUserById.ToList() : new List<MissionMember>();

        }

        /// <summary>
        /// This method gets all members of a mission. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all missionmember-rows for the given mission. </returns>

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
