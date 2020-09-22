using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    class Customer
    {
        [Key]
        public int customerId { get; set; }
        public int custNo { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }


    }
}
