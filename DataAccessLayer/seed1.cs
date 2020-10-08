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
    public static class seed1
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
                            Role = "User"
                        },
                        new User
                        {
                            UserName = "Inger",
                            Password = "inger123",
                            EMail = "Inger@inger.se",
                            Role = "User"
                        },
                        new User
                        {
                            UserName = "Rolf",
                            Password = "rolf123",
                            EMail = "Rolf@rolf.se",
                            Role = "User"
                        },
                        new User
                        {
                            UserName = "Anki",
                            Password = "anki123",
                            EMail = "Anki@anki.se",
                            Role = "User"
                        },
                        new User
                        {
                            UserName = "Rudolf",
                            Password = "rudolf123",
                            EMail = "Rudolf@rudolf.se",
                            Role = "Projectleader"
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
