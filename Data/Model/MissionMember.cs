using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class MissionMember
    {
        [Key]
        public int missionMemberId { get; set; }// räcker det inte att ha userId och mission Id som compositekey
        [ForeignKey("UserId")]
        public int userId { get; set; }

        [ForeignKey("MissionId")]
        public int missionId;

    }
}
