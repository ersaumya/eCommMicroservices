using System.Collections.Generic;
using System.Threading.Tasks;
using Order.Core.Entities;
using Order.Core.Repositories.Base;

namespace Order.Core.Repositories
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserName(string userName);
    }
}
