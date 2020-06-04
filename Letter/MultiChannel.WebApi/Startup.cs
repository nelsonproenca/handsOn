using System;
using System.IO;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multichannel.Application.Letters.Consumers;
using Multichannel.Ioc;
using MultiChannel.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace MultiChannel.WebApi
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ErrorHandlerAttribute));
            })
           .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ErrorHandlerAttribute));
            });

            DependencyInjectionIoc.ServiceIoc(services, Configuration);

            IBusControl CreateBus(IServiceProvider serviceProvider) => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost");

                    cfg.ReceiveEndpoint("submit-order", ep =>
                    {
                        ep.PrefetchCount = 16;

                        ep.ConfigureConsumer<DelegatedLettersSendedConsumer>(serviceProvider);
                    });
                });

            services.AddMassTransit(cfg =>
            {
                cfg.AddBus(CreateBus);

                cfg.AddConsumer<DelegatedLettersSendedConsumer>();
            });

            services.AddHostedService<MassTransitConsoleHostedService>();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                // version
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Multi Channel API v1",
                    Description = "Multi Channel Web API - Module Letters Microservice.",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "White Star ", Email = "nelson.proenca@whitestar.pt", Url = "www.whitestar.pt" }
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MultiChannel.WebApi.xml"));
                options.DescribeAllEnumsAsStrings();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="env">env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multi Channel API v1");
            });
        }
    }
}
