using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BulbasaurDevContext context)
            : base(context)
        {

        }
        public List<User> GetByName(string userName)
        {
            IEnumerable<User> all = GetAll();
            IEnumerable<User> allByName = from a in all
                                                      where a.userName == userName
                                                      select a;
            return allByName != null ? allByName.ToList() : new List<User>();
        }
    }
}
