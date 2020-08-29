using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services.Abstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetOrdersByUserName(string userName);
    }
}
