using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeReportApi.Models.ViewModel;

namespace TimeReportApi.Models
{
    public class MissionViewModel
    {
        public int MissionId { get; set; }
        public int? UserId { get; set; }
        public int? CustomerId { get; set; }
        public string MissionCustomerName { get; set; }
        public string MissionName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finished { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public List<TaskViewModel> Tasks { get; set; }

        public  MissionViewModel ConvertToViewModel( Mission a, Customer c,List<TaskViewModel> tasksList)
        {
           return new MissionViewModel { MissionId = a.MissionId, Description = a.Description,MissionCustomerName=c.Name, Created = a.Created, CustomerId = a.CustomerId, Finished = a.Finished, MissionName = a.MissionName, Start = a.Start, UserId = a.UserId, Status = a.Status ,Tasks=tasksList,Color=a.Color};
        }
    }
}
