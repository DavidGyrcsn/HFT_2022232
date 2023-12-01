//using Castle.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RP7XMC_HFT_2022232.Endpoint.Services;

namespace RP7XMC_HFT_2022232.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CarDbContext>();

            services.AddTransient<IRepository<Brand>, BrandRepository>();
            services.AddTransient<IRepository<Car>, CarRepository>();
            services.AddTransient<IRepository<Service>, ServiceRepository>();

            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IServiceLogic, ServiceLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RP7XMC_HFT_2022232.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RP7XMC_HFT_2022232.Endpoint v1"));
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}