using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace time_report_api
{
    public class Startup
    {
        //const string connection = @"Data Source=193.10.247.98, 1433;Database=BulbasaurDev;User ID=sa;Password=Pa55w0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // För att kalla på api med headers genom ajax
            services.AddCors(options =>
                options.AddPolicy("MyPolicy",
                     builder =>
                     {
                         builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                     }
                )
            );
            services.AddControllers();
            services.AddDbContext<BulbasaurDevContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BulbasaurDevContext"), b => b.MigrationsAssembly("time report api")));
            //services.AddSwaggerGen();
            services.AddScoped<UnitOfWork>();
        }
        //options.UseSqlServer(connection, b => b.MigrationsAssembly("time report api")));*/
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // API anrop genom headers med ajax
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
