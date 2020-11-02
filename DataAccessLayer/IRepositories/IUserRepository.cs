using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetByName(string userName);
       
    }
}
