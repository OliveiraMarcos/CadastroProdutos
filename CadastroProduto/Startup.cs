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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using Repository.Class;
using Repository.Interface;

namespace CadastroProduto
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
                .AddFluentValidation(x => {});
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
