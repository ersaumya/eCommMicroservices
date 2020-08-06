using Basket.API.Core;
using Basket.API.DomainModels;
using Basket.API.Repositories.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Implementation
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context;
        }

       
        public async Task<BasketCart> GetBasket(string userName)
        {
            var basket = await _context.Redis.StringGetAsync(userName);
            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            var updated = await _context.Redis.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            if (!updated)
            {
                return null;
            }
            return await GetBasket(basket.UserName);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            return await _context.Redis.KeyDeleteAsync(userName);
        }

    }
}
