using MediatR;
using Order.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Queries
{
    public class GetOrderByUserNameQuery:IRequest<IEnumerable<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
