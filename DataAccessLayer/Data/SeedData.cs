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
            modelBuilder.Entity<Customer>().HasData(new Customer { customerId=1, created=new DateTime(2020,9,20), name="Bobby"});
            modelBuilder.Entity<Mission>().HasData(
                new Mission 
                { 
                    created=new DateTime(2020,8,5),
                    description="Make cool stuffs",
                    finished=null,
                    missionId=1,
                    missionName="Operation Cool Stuffs",
                    start=new DateTime(2020,8,6),
                    status=1, 
                    userId=1,
                    customerId=1
                },
                new Mission 
                {
                    created = new DateTime(2020, 9, 5), 
                    description = "Lorem Ipsum ", 
                    finished = new DateTime(2020,10,1),
                    missionId = 2,
                    missionName = "dolor sit amet", 
                    start = new DateTime(2020, 8, 6), 
                    status = 1,
                    userId = 1,
                    customerId=1
                });
            modelBuilder.Entity<User>().HasData(new User {userId=1, password="abc123", eMail="hej@lol.com", userName="John" });
            modelBuilder.Entity<Task>().HasData(
                new Task 
                {
                    taskId=1, 
                    userId=1, 
                    missionId=1, 
                    status=0, 
                    actualHours=null, 
                    created=new DateTime(2020,10,5), 
                    description="Make cool thing work", 
                    estimatedHour=8.30,  
                    invoice=InvoiceType.invoicable,
                    name="work", 
                    start=new DateTime(2020,10,6),
                    finished=null
                    

                },
                new Task
                {
                    taskId = 2,
                    userId = 1,
                    missionId = 1,
                    status = 0,
                    actualHours = null,
                    created = new DateTime(2020, 11, 5),
                    description = "PLACEHOLDER",
                    estimatedHour = 8.30,
                    invoice = InvoiceType.notInvoicable,
                    name = "PLACEHOLDER",
                    start = new DateTime(2020, 12, 6),
                    finished = new DateTime(2020, 12, 7)
                },
                new Task
                {
                    taskId = 3,
                    userId = 1,
                    missionId = 1,
                    status = 0,
                    actualHours = null,
                    created = new DateTime(2020, 12, 8),
                    description = "PLACEHOLDER",
                    estimatedHour = 8.30,
                    invoice = InvoiceType.notInvoicable,
                    name = "PLACEHOLDER",
                    start = new DateTime(2020, 12, 9),
                    finished = new DateTime(2020, 12, 10)
                },
                new Task
                {
                    taskId = 4,
                    userId = 1,
                    missionId = 1,
                    status = 0,
                    actualHours = null,
                    created = new DateTime(2020, 12, 11),
                    description = "PLACEHOLDER",
                    estimatedHour = 8.30,
                    invoice = InvoiceType.notInvoicable,
                    name = "PLACEHOLDER",
                    start = new DateTime(2020, 12, 12),
                    finished = new DateTime(2020, 12, 13)
                }
                );
            modelBuilder.Entity<MissionMember>().HasData(new MissionMember { missionId=1, userId=1});
            modelBuilder.Entity<Registry>().HasData(
                new Registry { created=new DateTime(2021, 1,1), date=new DateTime(2020,11,5), hours=8, registryId=1, taskId=1, userId=1, invoice=InvoiceType.invoicable},
                 new Registry { created = new DateTime(2021, 2, 1), date = new DateTime(2020, 11, 5), hours = 8, registryId = 2, taskId = 1, userId = 1, invoice = InvoiceType.invoicable },
                  new Registry { created = new DateTime(2021, 3, 2), date = new DateTime(2020, 11, 6), hours = 8, registryId = 3, taskId = 1, userId = 1, invoice = InvoiceType.invoicable },
                 new Registry { created = new DateTime(2021, 4, 5), date = new DateTime(2020, 11, 7), hours = 8, registryId = 4, taskId = 1, userId = 1, invoice = InvoiceType.invoicable });
            modelBuilder.Entity<FavoriteMission>().HasData(new FavoriteMission { userId=1, missionId=1});


        }
    }
}
