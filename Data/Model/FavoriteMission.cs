using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    class FavoriteMission
    {
        [Key]
        [ForeignKey("userId")]
        public int userId { get; set; }

        [Key]
        [ForeignKey("MissionId")]
        public int missionId { get; set; }
    }
}
