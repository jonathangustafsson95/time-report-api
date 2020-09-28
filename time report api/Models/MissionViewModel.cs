using CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace time_report_api.Models
{
    public class MissionViewModel
    {
        public int missionId { get; set; }
        public int? userId { get; set; }
        public int? customerId { get; set; }
        public string missionName { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime? finished { get; set; }
        public DateTime created { get; set; }
        public int status { get; set; }

        public  MissionViewModel ConvertToViewModel( Mission a)
        {
           return new MissionViewModel { missionId = a.missionId, description = a.description, created = a.created, customerId = a.customerId, finished = a.finished, missionName = a.missionName, start = a.start, userId = a.userId, status = a.status };
        }
    }
}
