using Order.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
