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

        public List<FavoriteMission> GetFavoriteMissionsById(int id)
        {
            IEnumerable<FavoriteMission> allFavoriteMissions = GetAll().Where(i => i.UserId == id);
            return allFavoriteMissions.ToList();

        }
    }
}
