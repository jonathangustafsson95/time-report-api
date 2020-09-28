using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Model
{
    public class FavoriteMission
    {
        public int userId { get; set; }
        public int missionId { get; set; }
        public User User { get; set; }
        public Mission Mission { get; set;  }
    }
}
