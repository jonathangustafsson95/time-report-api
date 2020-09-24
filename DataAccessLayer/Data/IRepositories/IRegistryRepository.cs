using System.Collections.Generic;
using CommonLibrary.Model;

namespace DataAccessLayer.Data.IRepositories
{
    interface IRegistryRepository : IGenericRepository<Registry>
    {
        List<Registry> GetRegistriesLastWeek();
    }
}
