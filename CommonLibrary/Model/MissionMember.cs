using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Model
{
    public class MissionMember
    {
        public int userId { get; set; }
        public int missionId { get; set; }
        public virtual User User { get; set; }
        public virtual Mission Mission { get; set; }
    }
}
