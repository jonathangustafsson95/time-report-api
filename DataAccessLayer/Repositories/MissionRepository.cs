using CommonLibrary.Model;
using System.Collections.Generic;
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

        /// <summary>
        /// This method gets all missions linked to a given customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all missions linked to a given customer. </returns>

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
