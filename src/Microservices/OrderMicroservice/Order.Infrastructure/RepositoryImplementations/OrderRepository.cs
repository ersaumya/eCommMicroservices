using Microsoft.EntityFrameworkCore;
using Order.Core.RepositoryContracts;
using Order.Infrastructure.Data;
using Order.Infrastructure.RepositoryImplementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.RepositoryImplementations
{
    public class OrderRepository:Repository<Core.Entities.Order>,IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Core.Entities.Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.UserName == userName)
                      .ToListAsync();

            return orderList;
        }
    }
}
