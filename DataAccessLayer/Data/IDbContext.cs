using CommonLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Data
{
    public interface IDbContext
    {
        DbSet<Customer> customers { get; set; }
        DbSet<FavoriteMission> favoriteMissions { get; set; }
        DbSet<Mission> missions { get; set; }
        DbSet<MissionMember> missionMembers { get; set; }
        DbSet<Registry> registries { get; set; }
        DbSet<Task> tasks { get; set; }
        DbSet<User> users { get; set; }
    }
}
