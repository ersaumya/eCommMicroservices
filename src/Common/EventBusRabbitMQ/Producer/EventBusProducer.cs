using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusProducer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusProducer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }
        public void PublishBasketCheckout(string queueName,BasketCheckoutEvent publishModel)
        {
            using (var channel=_connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true,basicProperties:properties,body:body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMQ");
                };
                channel.ConfirmSelect();

            }
        }
    }
}
