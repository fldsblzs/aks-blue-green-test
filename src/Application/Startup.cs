using Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Application
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<VersionOptions>(_configuration.GetSection("VersionOptions"));

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck("ready", () =>
                {
                    Task.Delay(5000);
                    return HealthCheckResult.Healthy();
                }, new[] {"services"});
            
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

            app.UseAuthorization();

            app.UseHealthChecks("/self", new HealthCheckOptions
            {
                Predicate = hcr => hcr.Name.Contains("self")
            });
            app.UseHealthChecks("/ready", new HealthCheckOptions
            {
                Predicate = hcr => hcr.Tags.Contains("services")
            });
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}