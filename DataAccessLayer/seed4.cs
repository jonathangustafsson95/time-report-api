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
    public static class seed4
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BulbasaurDevContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BulbasaurDevContext>>()))
            {
                // Look for any registries.
                if (context.Registry.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.Registry.AddRange(
                        new Registry
                        {
                            TaskId = 1,
                            UserId = 1,
                            Hours = 7,
                            Created = new DateTime(2020, 12, 8),
                            Date = new DateTime(2020, 12, 8),
                            Invoice = InvoiceType.NotInvoicable
                        }
                    );
                }
            }
        }
    }
}
