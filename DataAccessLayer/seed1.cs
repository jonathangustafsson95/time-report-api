using CommonLibrary.Model;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Data.Model
{
    public static class Seed1
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BulbasaurDevContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BulbasaurDevContext>>()))
            {

                // Look for any users.
                if (context.User.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.User.AddRange(
                        new User
                        {
                            UserName = "Bengt",
                            Password = "bengt123",
                            EMail = "Bengt@bengt.se",
                        },
                        new User
                        {
                            UserName = "Inger",
                            Password = "inger123",
                            EMail = "Inger@inger.se",
                        },
                        new User
                        {
                            UserName = "Rolf",
                            Password = "rolf123",
                            EMail = "Rolf@rolf.se",
                        },
                        new User
                        {
                            UserName = "Anki",
                            Password = "anki123",
                            EMail = "Anki@anki.se",
                        },
                        new User
                        {
                            UserName = "Rudolf",
                            Password = "rudolf123",
                            EMail = "Rudolf@rudolf.se",
                        }
                    );
                };

                // Look for any customers.
                if (context.Customer.Any())
                {
                    // DB has been seeded
                }
                else
                {
                    context.Customer.AddRange(
                       new Customer
                       {
                           Name = "DHL",
                           Created = new DateTime(2020, 8, 5)
                       },
                       new Customer
                       {
                           Name = "Nike",
                           Created = new DateTime(2020, 8, 5)
                       },
                       new Customer
                       {
                           Name = "Ikea",
                           Created = new DateTime(2020, 8, 5)
                       },
                       new Customer
                       {
                           Name = "ICA",
                           Created = new DateTime(2020, 8, 5)
                       }
                   );
                }
                context.SaveChanges();
            }
        }
    }
}
