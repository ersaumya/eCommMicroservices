using Microsoft.AspNetCore.Builder;
using Order.API.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Order.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusConsumer Listener { get; set; }
        public static IApplicationBuilder UseRabbitMqListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }
        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
