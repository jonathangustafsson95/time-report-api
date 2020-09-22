using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Model
{
    public enum InvoiceType{
        invoicable,
        notInvoicable
    }
    class Task
    {
        [Key]
        public int taskId { get; set; }

        [ForeignKey("MissionId")]
        public int missionId { get; set; }

        [Key]
        [ForeignKey("UserId")]
        public int userId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public double estimatedHour { get; set; }
        public double? actualHours { get; set; }
        public InvoiceType invoice { get; set; }
        public DateTime created { get; set; }

        public DateTime? finished { get; set; }
        public DateTime start { get; set; } // sample data är int? vf?
        public int status { get; set; }
    }
}
