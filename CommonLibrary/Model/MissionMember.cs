using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Model
{
    public class MissionMember
    {
        [Key]
        [ForeignKey("UserId")]
        public int userId { get; set; }
        [Key]
        [ForeignKey("MissionId")]
        public int missionId { get; set; }

    }
}
