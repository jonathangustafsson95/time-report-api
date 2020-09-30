using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class RegistryViewModel
    {
        public int registryId { get; set; }
        public string missionName { get; set; }
        public string taskName { get; set; }
        public int? taskId { get; set; }
        public DayOfWeek day { get; set; }
        public double hours { get; set; }
        public InvoiceType invoice { get; set; }
    }
}
