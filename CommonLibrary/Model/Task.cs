using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommonLibrary.Model
{
   
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [ForeignKey("Mission")]
        public int? MissionId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double EstimatedHour { get; set; }
        public double? ActualHours { get; set; }
        public InvoiceType Invoice { get; set; }
        public DateTime Created { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finished { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; }
        public virtual Mission Mission { get; set; }
    }
}
