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
        public int RegistryId { get; set; }
        [ForeignKey("Task")]
        public int? TaskId { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public double Hours { get; set; }
        public DateTime Created { get; set; }
        public DateTime Date { get; set; }
        public InvoiceType Invoice { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }

    }
}
