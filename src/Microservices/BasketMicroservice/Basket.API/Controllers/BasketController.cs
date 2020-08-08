using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.DomainModels;
using Basket.API.Repositories.Abstraction;
using EventBusRabbitMQ.Constant;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly EventBusProducer _eventBusProducer;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, EventBusProducer eventBusProducer, IMapper mapper)
        {
            _repository = repository;
            _eventBusProducer = eventBusProducer;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new BasketCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basketCart)
        {
            var basket = await _repository.UpdateBasket(basketCart);
            return Ok(basket);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var basket = await _repository.DeleteBasket(userName);
            return Ok(basket);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody]BasketCheckout basketCheckout)
        {
            //get total price of basket
            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if(basket == null)
            {
                return BadRequest();
            }
            //remove the basket
            var basketremoved = await _repository.DeleteBasket(basketCheckout.UserName);
            if(!basketremoved)
            {
                return BadRequest();
            }
            //send checkout event to rabbitmq
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;
            //adding queue item to rabbitmq
            try
            {
                _eventBusProducer.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {

                throw;
            }
            return Accepted();
        }
    }
}
