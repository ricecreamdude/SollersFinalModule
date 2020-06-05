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

            //Allows the angular application to access the API
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("generalPolicy", confPolicy =>
                {
                    confPolicy.AllowAnyOrigin().WithOrigins(new string[] { "http://localhost:4200" }).WithMethods("GET", "DELETE", "POST", "PUT").AllowAnyHeader();

                });
            });

            var conn = Configuration.GetConnectionString("finalDB");

            //Connect to Microsoft DB Server and serve Customer
            services.AddDbContext<CustomerContext>(optionsBuilder =>
                            optionsBuilder.UseSqlServer(conn));

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

            app.UseCors("generalPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
