﻿using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TimeReportApi.Models.ViewModel
{
    public class MissionTaskViewModel
    {
        public string MissionName { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public List<Task> Tasks { get; set; }
    }
}