using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Database_UnitTest
{

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<FavoriteMission> favoriteMissions { get; set; }
        public DbSet<Mission> missions { get; set; }
        public DbSet<MissionMember> missionMembers { get; set; }
        public DbSet<Registry> registries { get; set; }
        public DbSet<Task> tasks { get; set; }
        public DbSet<User> users { get; set; }
    }
 


}
