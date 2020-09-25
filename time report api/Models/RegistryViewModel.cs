﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace time_report_api.Models
{
    public class RegistryViewModel
    {
        public int RegistryId { get; set; }
        public string MissionName { get; set; }
        public string TaskName { get; set; }
        public int Day { get; set; }
        public double Hours { get; set; }
    }
}