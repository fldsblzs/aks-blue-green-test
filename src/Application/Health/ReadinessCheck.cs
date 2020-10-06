using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Health
{
    public class ReadinessCheck : IHealthCheck
    {
        private readonly ILogger<ReadinessCheck> _logger;

        public ReadinessCheck(ILogger<ReadinessCheck> logger)
        {
            _logger = logger;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = new CancellationToken())
        {
            Task.Delay(5000);
            
            _logger.LogInformation("Readiness health check executed.");

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}