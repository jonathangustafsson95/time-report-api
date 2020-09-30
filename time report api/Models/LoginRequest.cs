using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace time_report_api.Models
{
    public class LoginRequest
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string passWord { get; set; }
    }
}
