using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Order.API.Extensions;
using Order.API.RabbitMq;
using Order.Application.Handlers;
using Order.Core.RepositoryContracts;
using Order.Core.RepositoryContracts.Base;
using Order.Infrastructure.Data;
using Order.Infrastructure.RepositoryImplementations;
using Order.Infrastructure.RepositoryImplementations.Base;
using RabbitMQ.Client;

namespace Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<OrderContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Singleton);

            #region Di order should be implemented as follows

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(CheckoutOrderHandler).GetTypeInfo().Assembly);

            #endregion

            //swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
            });

            //RabbitMQ connection setting
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }
                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }
                return new RabbitMQConnection(factory);
            });
            services.AddSingleton<EventBusConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Initilize Rabbit Listener in ApplicationBuilderExtentions
            app.UseRabbitMqListener();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
            });
        }
    }
}
