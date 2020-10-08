using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class Statistic
    {
        [JsonExtensionData]
        public Dictionary<string, float> StatisticDictionary {get;set;}

        //public float TotalTask { get; set; }
        //public float InternalTaskCount { get; set; }
        //public float CustomerTaskCount { get; set; }
        //public float CustomerTime { get; set; }
        //public float TotalTime { get; set; }
        //public float InternalTimePerc { get; set; }
        //public float CustomerTimePerc { get; set; }
        public Statistic()=> StatisticDictionary = new Dictionary<string, float>();

        public Dictionary<string, float> StatisticsDictionary { get; set; }

    }
}
