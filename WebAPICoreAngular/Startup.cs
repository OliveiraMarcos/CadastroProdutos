using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entity;
using Domain.ValidatorViewModel;
using Domain.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Class;
using Repository.Interface;

namespace WebAPICoreAngular
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
            services.AddMvc()
                .AddFluentValidation(x => { });
            services.AddAutoMapper();
            services.AddMediatR();
            string connectionString = Configuration.GetSection("ConnectionStrings")
                                                   .GetValue<string>("Default");
            services.AddDbContext<Contexto>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IGenericRepository<Product>, ProductRepository>();
            services.AddTransient<IGenericRepository<Customer>, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IGenericRepository<OrderItem>, OrderItemRepository>();

            services.AddTransient<IValidator<OrderViewModel>, OrderValidator>();
            services.AddTransient<IValidator<OrderItemViewModel>, OrderItemValidator>();
            services.AddTransient<IValidator<ProductViewModel>, ProductValidator>();
            services.AddTransient<IValidator<CustomerViewModel>, CustomerValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
