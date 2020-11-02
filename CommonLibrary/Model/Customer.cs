using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }


    }
}
