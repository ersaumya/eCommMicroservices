using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.DomainModels
{
    public class BasketCart
    {
        public BasketCart()
        {

        }
        public BasketCart(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public decimal TotalPrice {
            get
            {
                decimal totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
