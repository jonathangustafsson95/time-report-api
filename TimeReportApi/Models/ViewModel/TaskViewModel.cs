using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models.ViewModel
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public int? MissionId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
