using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using SollarsFinalApp.Models;

namespace SollarsFinalApp
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
            //CORS Policy
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("p1", confPolicy =>
                {
                    confPolicy.AllowAnyOrigin().WithOrigins(new string[] { "http://localhost:4200" }).WithMethods("GET", "DELETE", "POST").AllowAnyHeader();
                });
            });

            var conn = Configuration.GetConnectionString("EmployeeDb");

            //Connect to Microsoft DB Server 
            services.AddDbContext<CustomerContext>(optionsBuilder =>
                            optionsBuilder.UseSqlServer(conn));

            //Adds TodoList to available services
            //services.AddDbContext<TodoContext>(optionsBuilder =>
            //   optionsBuilder.UseInMemoryDatabase("TodoList"));
            ////Add Customer to available services
            //services.AddDbContext<CustomerContext>(optionsBuilder =>
            //   optionsBuilder.UseInMemoryDatabase("Customer"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("p1");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
