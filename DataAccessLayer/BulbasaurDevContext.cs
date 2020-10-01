using CommonLibrary.Model;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer
{
    public class BulbasaurDevContext: DbContext
    { 
        public BulbasaurDevContext(DbContextOptions<BulbasaurDevContext> options) : base(options)
        {
        }
        DbSet<Customer> Customers { get; set; }
        DbSet<FavoriteMission> FavoriteMissions { get; set; }
        DbSet<Mission> Missions { get; set; }
        DbSet<MissionMember> MissionMembers { get; set; }
        DbSet<Registry> Registries { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteMission>()
                .HasKey(fm => new { userId = fm.UserId, missionId = fm.MissionId });
            modelBuilder.Entity<FavoriteMission>()
                .HasOne(fm => fm.User)
                .WithMany(fm => fm.MissionFavorites)
                .HasForeignKey(fm => fm.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FavoriteMission>()
                .HasOne(fm => fm.Mission)
                .WithMany(fm => fm.FavoritedMission)
                .HasForeignKey(fm => fm.MissionId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MissionMember>()
                .HasKey(mm => new { mm.UserId, missionId = mm.MissionId });
            modelBuilder.Entity<MissionMember>()
                .HasOne(mm => mm.User)
                .WithMany(mm => mm.MissionMemberships)
                .HasForeignKey(mm => mm.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MissionMember>()
                .HasOne(mm => mm.Mission)
                .WithMany(mm => mm.MissionMembers)
                .HasForeignKey(mm => mm.MissionId)
                .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Mission)
            //    .WithOne(m => m.User)
            //    .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.missionMemberships)
            //    .WithOne(m => m.User)
            //    .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.missionFavorites)
            //    .WithOne(m => m.User)
            //    .OnDelete(DeleteBehavior.NoAction);
         

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
            modelBuilder.Seed();
        }
    }
}
