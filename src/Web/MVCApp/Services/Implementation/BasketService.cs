using MVCApp.Infrastructure;
using MVCApp.Models;
using MVCApp.Services.Abstraction;
using MVCApp.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCApp.Services.Implementation
{
    public class BasketService : BaseHttpClientWithFactory, IBasketService
    {
        private readonly IApiSettings _settings;

        public BasketService(IHttpClientFactory factory, IApiSettings settings): base(factory)
        {
            _settings = settings;
        }

        public async Task<Basket> GetBasket(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.BasketPath)
                               .AddQueryString("username", userName)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<Basket>(message);
        }

        public async Task<Basket> UpdateBasket(Basket model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.BasketPath)
                                .HttpMethod(HttpMethod.Post)
                                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<Basket>(message);
        }

        public async Task CheckoutBasket(BasketCheckout model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.BasketPath)
                                .AddToPath("Checkout")
                                .HttpMethod(HttpMethod.Post)
                                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            await SendRequest<BasketCheckout>(message);
        }
    }
}
