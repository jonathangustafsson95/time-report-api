using CommonLibrary.Model;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer
{
    public class BulbasaurDevContext: DbContext
    {
        public BulbasaurDevContext(DbContextOptions<BulbasaurDevContext>options):base(options)
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
            modelBuilder.Entity<FavoriteMission>()
                .HasKey(fm => new { fm.userId, fm.missionId });
            modelBuilder.Entity<FavoriteMission>()
                .HasOne(fm => fm.User)
                .WithMany(fm => fm.missionFavorites)
                .HasForeignKey(fm => fm.userId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FavoriteMission>()
                .HasOne(fm => fm.Mission)
                .WithMany(fm => fm.favoritedMission)
                .HasForeignKey(fm => fm.missionId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MissionMember>()
                .HasKey(mm => new { mm.userId, mm.missionId });
            modelBuilder.Entity<MissionMember>()
                .HasOne(mm => mm.User)
                .WithMany(mm => mm.missionMemberships)
                .HasForeignKey(mm => mm.userId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MissionMember>()
                .HasOne(mm => mm.Mission)
                .WithMany(mm => mm.missionMembers)
                .HasForeignKey(mm => mm.missionId)
                .OnDelete(DeleteBehavior.NoAction);

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
            modelBuilder.Seed();
        }
    }
}
