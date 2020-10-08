using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class RegistryViewModel
    {
        public int? RegistryId { get; set; }
        public string MissionName { get; set; }
        public string TaskName { get; set; }
        public int? TaskId { get; set; }
        public DayOfWeek Day { get; set; }
        public double Hours { get; set; }
        public DateTime Created { get; set; }
        public DateTime Date { get; set; }
        public InvoiceType Invoice { get; set; }
    }
}
