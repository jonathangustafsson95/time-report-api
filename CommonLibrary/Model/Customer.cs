﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Model
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }


    }
}