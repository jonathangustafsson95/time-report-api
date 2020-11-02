using CommonLibrary.Model;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Data.Model
{
    public static class Seed3
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
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
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
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 1,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
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
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 1,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 2,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 3,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 4,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 5,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 6,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 7,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 7,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 7,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 7,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId =7,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 8,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 8,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 8,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 8,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 8,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 9,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 9,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 9,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 9,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 9,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 10,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Meeting with customer",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Meeting with customer",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 10,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Analysis",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Analysis",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 10,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Design",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Design",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
                        },
                        new Task
                        {
                            UserId = 2,
                            MissionId = 10,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 11, 5),
                            Description = "Implementation",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.NotInvoicable,
                            Name = "Implementation",
                            Start = new DateTime(2020, 12, 6),
                            Finished = new DateTime(2020, 12, 7)
                        },
                        new Task
                        {
                            UserId = 1,
                            MissionId = 10,
                            Status = 0,
                            ActualHours = null,
                            Created = new DateTime(2020, 10, 5),
                            Description = "Support",
                            EstimatedHour = 8.30,
                            Invoice = InvoiceType.Invoicable,
                            Name = "Support",
                            Start = new DateTime(2020, 10, 6),
                            Finished = null
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
