using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;

namespace Data.Data
{
    interface IRegistryRepository :IGenericRepository<Registry>
    {
        List<Registry> GetRegistriesLastWeek();
    }
}
