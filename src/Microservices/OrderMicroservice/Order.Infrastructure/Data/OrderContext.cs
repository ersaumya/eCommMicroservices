using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Data
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options)
        {

        }
        public DbSet<Core.Entities.Order> Orders { get; set; }
    }
}
