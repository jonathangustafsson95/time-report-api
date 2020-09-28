using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommonLibrary.Model
{
   
    public class Task
    {
        [Key]
        public int taskId { get; set; }

        [ForeignKey("Mission")]
        public int missionId { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double estimatedHour { get; set; }
        public double? actualHours { get; set; }
        public InvoiceType invoice { get; set; }
        public DateTime created { get; set; }
        public DateTime start { get; set; }
        public DateTime? finished { get; set; }
        public int status { get; set; }

        public User User { get; set; }
        public Mission Mission { get; set; }
    }
}
