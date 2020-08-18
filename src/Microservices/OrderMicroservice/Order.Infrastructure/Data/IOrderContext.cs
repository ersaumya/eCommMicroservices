using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Data
{
    public interface IOrderContext
    {
        DbSet<Core.Entities.Order> Orders { get; set; }
    }
}