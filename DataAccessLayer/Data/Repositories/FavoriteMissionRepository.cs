using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Data.IRepositories;
using CommonLibrary.Model;

namespace DataAccessLayer.Data.Repositories
{
    public class FavoriteMissionRepository : GenericRepository<FavoriteMission>, IFavoriteMissionRepository
    {
        public FavoriteMissionRepository(BulbasaurContext context) : base(context)
        {
        }

        public List<FavoriteMission> GetFavoriteMissionsById(int id)
        {
            IEnumerable<FavoriteMission> allFavoriteMissions = GetAll().Where(i => i.userId == id);
            return allFavoriteMissions.ToList();

        }
    }
}
