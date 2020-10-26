using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TimeReportApi.Models.ViewModel
{
    public class MissionTaskViewModel
    {
        public string MissionName { get; set; }
        public int MissionId { get; set; }
        public string MissionColor { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public bool IsMember { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
        public List<UserViewModel> Users { get; set; }

    }
}
