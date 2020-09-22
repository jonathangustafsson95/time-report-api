﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer { customerId=0, created=new DateTime(2020,9,21), custNo=0, name="Bobby"});
            modelBuilder.Entity<Mission>().HasData(new Mission { created=new DateTime(2020,8,5),  });
            
        }
    }
}
