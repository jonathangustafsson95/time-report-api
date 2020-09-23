﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommonLibrary.Model
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }
    }
}
