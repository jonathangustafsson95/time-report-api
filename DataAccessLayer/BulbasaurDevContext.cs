using CommonLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BulbasaurDevContext: DbContext
    { 
        public BulbasaurDevContext(DbContextOptions<BulbasaurDevContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<FavoriteMission> FavoriteMission { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<MissionMember> MissionMember { get; set; }
        public DbSet<Registry> Registry { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteMission>()
                .HasKey(fm => new { fm.UserId, fm.MissionId });
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
                .HasKey(mm => new { mm.UserId, mm.MissionId });
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
        }
    }
}
