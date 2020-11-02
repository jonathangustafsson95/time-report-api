using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Model
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string MissionName { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finished { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<MissionMember> MissionMembers { get; set; }
        public virtual ICollection<FavoriteMission> FavoritedMission { get; set; }
    }
}
 