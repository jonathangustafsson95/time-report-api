using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLibrary.Model
{
    public class Mission
    {
        [Key]
        public int missionId { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }
        [ForeignKey("Customer")]
        public int? customerId { get; set; }
        public string missionName { get; set; }
        public string description { get; set; }//ha kvar? enum/string
        public DateTime start { get; set; }
        public DateTime? finished { get; set; }
        public DateTime created { get; set; }
        public int status { get; set; }
        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<MissionMember> missionMembers { get; set; }
        public virtual ICollection<FavoriteMission> favoritedMission { get; set; }
    }
}
 