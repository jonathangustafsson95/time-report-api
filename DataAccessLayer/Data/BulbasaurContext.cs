﻿using CommonLibrary.Model;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class BulbasaurContext: DbContext
    {
        public BulbasaurContext(DbContextOptions<BulbasaurContext>options):base(options)
        {

        }
        DbSet<Customer> customers { get; set; }
        DbSet<FavoriteMission> favoriteMissions { get; set; }
        DbSet<Mission> missions { get; set; }
        DbSet<MissionMember> missionMembers { get; set; }
        DbSet<Registry> registries { get; set; }
        DbSet<Task> tasks { get; set; }
        DbSet<User> users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteMission>().HasKey(f => new { f.userId, f.missionId });
            modelBuilder.Entity<Task>().HasKey(t => new { t.taskId, t.userId });
            modelBuilder.Entity<MissionMember>().HasKey(m => new { m.userId, m.missionId });
            modelBuilder.Seed();
        }
    }
}
