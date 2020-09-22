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
            modelBuilder.Entity<Customer>().HasData(new Customer { customerId=0, created=new DateTime(2020,9,21), custNo=0, name="Bobby"});
            modelBuilder.Entity<Mission>().HasData(new Mission { created=new DateTime(2020,8,5), custNo=0, description="Make cool stuffs", finished=null, missionId=0, missionName="Operation Cool Stuffz", start=new DateTime(2020,8,6), status=0, userId=0});
            modelBuilder.Entity<User>().HasData(new User {userId=0, password="abc123", eMail="hej@lol.com", userName="Erik" });
            modelBuilder.Entity<Task>().HasData(new Task {taskId=0, userId=0, missionId=0, status=0, actualHours=null, created=new DateTime(2020,10,5), description="Make cool thing work", estimatedHour=8.30,  invoice=InvoiceType.invoicable, name="workpls", start=new DateTime(2020,10,6)});
            modelBuilder.Entity<MissionMember>().HasData(new MissionMember { missionId=0, userId=0 , missionMemberId=0});
            modelBuilder.Entity<Registry>().HasData(new Registry { created=new DateTime(2021, 1,1), date=new DateTime(2020,12,5), description=null, hours=8, invoice=InvoiceType.notInvoicable, reqistryId=0, taskId=0, userId=0});
            modelBuilder.Entity<FavoriteMission>().HasData(new FavoriteMission { userId=0, missionId=0});


        }
    }
}
