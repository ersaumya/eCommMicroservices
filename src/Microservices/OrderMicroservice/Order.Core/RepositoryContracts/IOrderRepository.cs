using System.Collections.Generic;
using System.Threading.Tasks;
using Order.Core.Entities;
using Order.Core.RepositoryContracts.Base;

namespace Order.Core.RepositoryContracts
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserName(string userName);
    }
}
