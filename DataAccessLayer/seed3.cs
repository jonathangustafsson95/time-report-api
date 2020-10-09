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
    public static class seed3
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BulbasaurDevContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BulbasaurDevContext>>()))
            {

                // Look for any tasks.
                if (context.Task.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.Task.AddRange(
                        new Task
                        {
                            UserId = 1,
                            MissionId = 1,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "DHL Project 1 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Task1 DHL Project1",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 1,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "DHL Project 1 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task2 DHL Project1",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 3,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "DHL Project 2 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task1 DHL Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        },
                        new Task
                        {
                            UserId = 3,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "DHL Project 2 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task2 DHL Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Ikea Project 1 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Task1 Ikea Project1",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Ikea Project 1 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Ikea Ikea Project1",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 3,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "Ikea Project 2 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task1 Ikea Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        },
                        new Task
                        {
                            UserId = 3,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "Ikea Project 2 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task2 Ikea Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        },
                        new Task
                        {
                            UserId = 5,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Ica Project 1 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Task1 Ica Project1",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 5,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Ica Project 1 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task2 Ica Project1",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 4,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "Ica Project 2 Task1",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task1 Ica Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        },
                        new Task
                        {
                            UserId = 4,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 12, 8),
                            Description = "Ica Project 2 Task2",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Task2 Ica Project2",
                            Start = new DateTime(2020, 12, 9),
                            Finished = new DateTime(2020, 12, 10)
                        }
                    );
                }

                // Look for any missionmembers.
                if (context.MissionMember.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.MissionMember.AddRange(
                        new MissionMember
                        {
                            UserId = 1,
                            MissionId = 1
                        },
                        new MissionMember
                        {
                            UserId = 1,
                            MissionId = 2
                        },
                        new MissionMember
                        {
                            UserId = 1,
                            MissionId = 3
                        },
                        new MissionMember
                        {
                            UserId = 1,
                            MissionId = 4
                        }
                    );
                }

                // Look for any favoritemissions.
                if (context.FavoriteMission.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.FavoriteMission.AddRange(
                        new FavoriteMission
                        {
                            UserId = 1,
                            MissionId = 1
                        },
                        new FavoriteMission
                        {
                            UserId = 1,
                            MissionId = 3
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
