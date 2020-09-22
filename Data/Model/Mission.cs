using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    class Mission
    {
        [Key]
        public int missionId { get; set; }

        [ForeignKey("UserId")]
        public int userId { get; set; }
        [ForeignKey("CustNo")]
        public int custNo { get; set; }
        public string missionName;
        public string description { get; set; }//ha kvar? enum/string
        [ForeignKey("Owner")]
        public string owner { get; set; }
        public DateTime start { get; set; }
        public DateTime? finished { get; set; }
        public DateTime created { get; set; }
        public int status { get; set; }
    }
}
