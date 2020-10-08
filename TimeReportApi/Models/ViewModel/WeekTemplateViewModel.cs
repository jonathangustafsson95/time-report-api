using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models.ViewModel
{
    public class WeekTemplateViewModel
    {
        public int WeekNr { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<RegistryViewModel> Week { get; set; }
    }
}
