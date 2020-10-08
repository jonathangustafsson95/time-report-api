using CommonLibrary.Model;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public static class seed2
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BulbasaurDevContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BulbasaurDevContext>>()))
            {
                // Look for any missions.
                if (context.Mission.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.Mission.AddRange(
                        new Mission
                        {
                            Created = new DateTime(2020, 8, 5),
                            Description = "Project1 for DHL",
                            Finished = null,
                            MissionName = "DHL Project1",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 1
                        },
                        new Mission
                        {
                            Created = new DateTime(2020, 9, 5),
                            Description = "Project2 for DHL",
                            Finished = new DateTime(2020, 10, 1),
                            MissionName = "DHL Project2",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 1
                        },
                        new Mission
                        {
                            Created = new DateTime(2020, 8, 5),
                            Description = "Project1 for Nike",
                            Finished = null,
                            MissionName = "Nike Project1",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 2
                        },
                        new Mission
                        {
                            Created = new DateTime(2020, 9, 5),
                            Description = "Project2 for Nike",
                            Finished = new DateTime(2020, 10, 1),
                            MissionName = "Nike Project2",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 2
                        },
                        new Mission
                        {
                            Created = new DateTime(2020, 8, 5),
                            Description = "Project1 for Ikea",
                            Finished = null,
                            MissionName = "Project1 Ikea",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 3
                        },
                        new Mission
                        {
                            Created = new DateTime(2020, 9, 5),
                            Description = "Project1 for ICA",
                            Finished = new DateTime(2020, 10, 1),
                            MissionName = "Project1 ICA",
                            Start = new DateTime(2020, 8, 6),
                            Status = 1,
                            UserId = 1,
                            CustomerId = 4
                        }
                    );
                    context.SaveChanges();
                };
            }
        }
    }
}