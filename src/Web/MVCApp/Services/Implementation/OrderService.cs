using MVCApp.Infrastructure;
using MVCApp.Models;
using MVCApp.Services.Abstraction;
using MVCApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCApp.Services.Implementation
{
    public class OrderService: BaseHttpClientWithFactory,IOrderService
    {
        private readonly IApiSettings _settings;

        public OrderService(IHttpClientFactory factory, IApiSettings settings): base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                           .SetPath(_settings.OrderPath)
                           .AddQueryString("username", userName)
                           .HttpMethod(HttpMethod.Get)
                           .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponse>>(message);
        }
    }
}
