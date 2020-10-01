using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class Statistic
    {
        //public float internalTime { get; set; }
        //public float customerTime { get; set; }
        public float internalTimePerc { get; set; }
        public float customerTimePerc { get; set; }
        public float totalTime { get; set; }


        //public float internalTaskPerc { get; set; }
        //public float customerTaskPerc { get; set; }
        public float internalTaskCount { get; set; }
        public float customerTaskCount { get; set; }
        public float totalTask { get; set; }

    }
}
