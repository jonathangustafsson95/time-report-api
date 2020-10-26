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
