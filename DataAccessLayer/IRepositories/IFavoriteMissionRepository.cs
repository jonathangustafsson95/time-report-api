using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.IRepositories
{
    public interface IFavoriteMissionRepository: IGenericRepository<FavoriteMission>
    {
        List<FavoriteMission> GetFavoriteMissionsById(int id);
    }
}
