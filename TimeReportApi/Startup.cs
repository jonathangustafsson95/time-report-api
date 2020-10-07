using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using TimeReportApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TimeReportApi
{
    public class Startup
    {
        //const string connection = @"Data Source=193.10.247.98, 1433;Database=BulbasaurDev;User ID=sa;Password=Pa55w0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string BulbasaurPolicy = "bulbasaurPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // För att kalla på api med headers genom ajax
            services.AddCors(options =>
                options.AddPolicy(name: BulbasaurPolicy,
                     builder =>
                     {
                         builder
                         .AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                     }
                )
            );
            services.AddControllers();
            services.AddDbContext<BulbasaurDevContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BulbasaurDevContext"), b => b.MigrationsAssembly("TimeReportApi")));
            //services.AddSwaggerGen();
            services.AddScoped<UnitOfWork>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

                services.AddAuthorization(config =>
                {
                    config.AddPolicy(Policies.projectLeader, Policies.ProjectLeaderPolicy());
                    config.AddPolicy(Policies.user, Policies.UserPolicy());
                });
            });
            services.AddHttpContextAccessor();
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
            app.UseCors(BulbasaurPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
