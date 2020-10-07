using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeReportApi.Models
{
    public class MissionViewModel
    {
        public int missionId { get; set; }
        public int? userId { get; set; }
        public int? customerId { get; set; }
        public string missionCustomerName { get; set; }
        public string missionName { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime? finished { get; set; }
        public DateTime created { get; set; }
        public int status { get; set; }

        public  MissionViewModel ConvertToViewModel( Mission a, Customer c)
        {
           return new MissionViewModel { missionId = a.MissionId, description = a.Description,missionCustomerName=c.Name, created = a.Created, customerId = a.CustomerId, finished = a.Finished, missionName = a.MissionName, start = a.Start, userId = a.UserId, status = a.Status };
        }
    }
}
