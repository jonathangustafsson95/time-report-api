using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class RegistriesDelete
    { 
        public List<int> RegistriesToDelete { get; set; }  
    }

}

