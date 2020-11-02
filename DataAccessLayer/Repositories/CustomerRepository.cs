using CommonLibrary.Model;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BulbasaurDevContext context)
            : base(context)
        {

        }
    }
}
