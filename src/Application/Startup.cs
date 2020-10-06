using Application.Health;
using Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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

            services
                .AddSingleton<LivenessCheck>()
                .AddSingleton<ReadinessCheck>();

            services.AddHealthChecks()
                .AddCheck<LivenessCheck>("live")
                .AddCheck<ReadinessCheck>("ready", tags: new[] {"readiness"});

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

            app.UseSerilogRequestLogging();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = hcr => hcr.Name == "live"
            });
            
            app.UseHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = hcr => hcr.Tags.Contains("readiness")
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}