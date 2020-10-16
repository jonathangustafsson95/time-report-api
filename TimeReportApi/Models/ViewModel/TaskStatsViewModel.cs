using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models.ViewModel
{
    public class TaskStatsViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public double? EstimatedHours { get; set; }
        public double? ActualHours { get; set; }
    }
}
