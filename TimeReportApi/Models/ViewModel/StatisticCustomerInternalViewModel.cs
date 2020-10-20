using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class StatisticCustomerInternalViewModel
    {
        public string Month { get; set; }
        public double CustomerTime{ get; set; }
        public double InternalTime { get; set; }
    }
}
