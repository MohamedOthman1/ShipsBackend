using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShipsBackEnd.Models;
using ShipsBackEnd.Repositories;
using ShipsBackEnd.Utils;

namespace ShipsBackEnd
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

            var connString = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<DatabaseContext>(
                options => options.UseMySql(
                    connString,
                    new MySqlServerVersion(new Version(5, 7, 22)),
                    mySqlOptionsAction => mySqlOptionsAction.EnableRetryOnFailure()
                )); 

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IBaseRepo, BaseRepo>();
            services.AddTransient<IPortsRepo, PortsRepo>();
            services.AddTransient<IShipsRepo, ShipsRepo>();
            services.AddLogging(configure => configure.AddLog4Net());
            services.AddTransient<ILogHelper, LogHelper>();

             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ships API",
                    Version = "v1"
                });
            });
             services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            var context = serviceProvider.GetService<DatabaseContext>();

            context.Database.Migrate();
            app.UseHttpsRedirection();

           

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}