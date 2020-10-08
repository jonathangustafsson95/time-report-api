using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.IReppositories;
using DataAccessLayer.IRepositories;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class MissionRepository : GenericRepository<Mission>, IMissionRepository
    {
        public MissionRepository(BulbasaurDevContext context)
            : base(context)
        {

        }
        public List<Mission> GetAllByUserId(int id)
        {
            IEnumerable<Mission> all = GetAll();
            IEnumerable<Mission> allByUserById = from a in all
                                                       where a.UserId == id
                                                       select a;
            return allByUserById != null ? allByUserById.ToList() : new List<Mission>();

        }
        public List<Mission> GetAllByMissionId(int id)
        {
            IEnumerable<Mission> all = GetAll();
            IEnumerable<Mission> allByMissionById = from a in all
                                                          where a.MissionId == id
                                                          select a;
            return allByMissionById != null ? allByMissionById.ToList() : new List<Mission>();

        }

        public List<Mission> GetAllByCustomerId(int id)
        {
            IEnumerable<Mission> all = GetAll();
            IEnumerable<Mission> allByCustomerId = from a in all
                                                    where a.CustomerId == id
                                                    select a;
            return allByCustomerId != null ? allByCustomerId.ToList() : new List<Mission>();

        }

    }
}
