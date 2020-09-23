using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IReppositories;

namespace DataAccessLayer.Data.IRepositories
{
    interface IRegistryRepository : IGenericRepository<Registry>
    {
        List<Registry> GetRegistriesLastWeek();
    }
}
