using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.IRepositories;
using CommonLibrary.Model;

namespace DataAccessLayer.Repositories
{
    public class FavoriteMissionRepository : GenericRepository<FavoriteMission>, IFavoriteMissionRepository
    {
        public FavoriteMissionRepository(BulbasaurDevContext context) : base(context)
        {
        }

        /// <summary>
        /// This method gets all favorite missions for a given user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all favorite missions for the given user. </returns>
        public List<FavoriteMission> GetFavoriteMissionsById(int id)
        {
            IEnumerable<FavoriteMission> allFavoriteMissions = GetAll().Where(i => i.UserId == id);
            return allFavoriteMissions.ToList();

        }
    }
}
