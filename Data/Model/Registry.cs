using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Model
{
    public class Registry
    {
        [Key]
        public int reqistryId { get; set; }
        [ForeignKey("TaskId")]
        public int taskId { get; set; }
        [ForeignKey("userId")]
        public int userId { get; set; }
        public string? description { get; set; }
        public double hours { get; set; }
        public DateTime created { get; set; }
        public DateTime date { get; set; }
        public InvoiceType invoice { get; set; }

    }
}
