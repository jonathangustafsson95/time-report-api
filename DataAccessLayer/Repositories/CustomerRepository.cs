using CommonLibrary.Model;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

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
