using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommonLibrary.Model
{
    public class Registry
    {
        [Key]
        public int registryId { get; set; }
        [ForeignKey("Task")]
        public int? taskId { get; set; }
        [ForeignKey("User")]
        public int? userId { get; set; }
        public double hours { get; set; }
        public DateTime created { get; set; }
        public DateTime date { get; set; }
        public InvoiceType invoice { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }

    }
}
