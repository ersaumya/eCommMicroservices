using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // INFO: Run this if using a real database. Used to automaticly migrate docker image of sql server db.
                orderContext.Database.Migrate();
                //orderContext.Database.EnsureCreated();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 5)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(orderContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Core.Entities.Order> GetPreconfiguredOrders()
        {
            return new List<Core.Entities.Order>()
            {
                new Core.Entities.Order() { UserName = "soumya", FirstName = "Soumya", LastName = "Rout", EmailAddress = "soumya@gmail.com", AddressLine = "Bangalore", TotalPrice = 5239 },
                new Core.Entities.Order() { UserName = "mahaprasad", FirstName = "Mahaprasad", LastName = "Rout", EmailAddress ="mprasad@gmail.com", AddressLine = "Cuttack", TotalPrice = 3486 }
            };
        }
    }
}
