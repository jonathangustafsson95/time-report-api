using CommonLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                
                .HasData(new User { UserId = 1, Password = "abc123", EMail = "hej@lol.com", UserName = "John" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId=1, Created=new DateTime(2020,9,20), Name="Bobby"});
            modelBuilder.Entity<Mission>().HasData(
                new Mission 
                { 
                    Created=new DateTime(2020,8,5),
                    Description="Make cool stuffs",
                    Finished=null,
                    MissionId=1,
                    MissionName="Operation Cool Stuffs",
                    Start=new DateTime(2020,8,6),
                    Status=1, 
                    UserId=1,
                    CustomerId=1
                },
                new Mission
                {
                    Created = new DateTime(2020, 9, 5), 
                    Description = "Lorem Ipsum ", 
                    Finished = new DateTime(2020,10,1),
                    MissionId = 2,
                    MissionName = "dolor sit amet", 
                    Start = new DateTime(2020, 8, 6), 
                    Status = 1,
                    UserId = 1,
                    CustomerId=1
                });
            modelBuilder.Entity<Task>().HasData(
                new Task
                {
                    TaskId=1, 
                    UserId=1, 
                    MissionId=1, 
                    Status=0, 
                    ActualHours=null, 
                    Created=new DateTime(2020,10,5), 
                    Description="Make cool thing work", 
                    EstimatedHour=8.30,  
                    Invoice=InvoiceType.Invoicable,
                    Name="work", 
                    Start=new DateTime(2020,10,6),
                    Finished=null
                    

                },
                new Task
                {
                    TaskId = 2,
                    UserId = 1,
                    MissionId = 1,
                    Status = 0,
                    ActualHours = null,
                    Created = new DateTime(2020, 11, 5),
                    Description = "PLACEHOLDER",
                    EstimatedHour = 8.30,
                    Invoice = InvoiceType.NotInvoicable,
                    Name = "PLACEHOLDER",
                    Start = new DateTime(2020, 12, 6),
                    Finished = new DateTime(2020, 12, 7)
                },
                new Task
                {
                    TaskId = 3,
                    UserId = 1,
                    MissionId = 1,
                    Status = 0,
                    ActualHours = null,
                    Created = new DateTime(2020, 12, 8),
                    Description = "PLACEHOLDER",
                    EstimatedHour = 8.30,
                    Invoice = InvoiceType.NotInvoicable,
                    Name = "PLACEHOLDER",
                    Start = new DateTime(2020, 12, 9),
                    Finished = new DateTime(2020, 12, 10)
                },
                new Task
                {
                    TaskId = 4,
                    UserId = 1,
                    MissionId = 1,
                    Status = 0,
                    ActualHours = null,
                    Created = new DateTime(2020, 12, 11),
                    Description = "PLACEHOLDER",
                    EstimatedHour = 8.30,
                    Invoice = InvoiceType.NotInvoicable,
                    Name = "PLACEHOLDER",
                    Start = new DateTime(2020, 12, 12),
                    Finished = new DateTime(2020, 12, 13)
                }
                );
            modelBuilder.Entity<MissionMember>().HasData(new MissionMember { MissionId=1, UserId=1});
            modelBuilder.Entity<Registry>().HasData(
                new Registry { Created=new DateTime(2021, 1,1), Date=new DateTime(2020,11,5), Hours=8, RegistryId=1, TaskId=1, UserId=1, Invoice=InvoiceType.Invoicable},
                 new Registry { Created = new DateTime(2021, 2, 1), Date = new DateTime(2020, 11, 5), Hours = 8, RegistryId = 2, TaskId = 1, UserId = 1, Invoice = InvoiceType.Invoicable },
                  new Registry { Created = new DateTime(2021, 3, 2), Date = new DateTime(2020, 11, 6), Hours = 8, RegistryId = 3, TaskId = 1, UserId = 1, Invoice = InvoiceType.Invoicable },
                 new Registry { Created = new DateTime(2021, 4, 5), Date = new DateTime(2020, 11, 7), Hours = 8, RegistryId = 4, TaskId = 1, UserId = 1, Invoice = InvoiceType.Invoicable });
            modelBuilder.Entity<FavoriteMission>().HasData(new FavoriteMission { UserId=1, MissionId=1});


        }
    }
}
