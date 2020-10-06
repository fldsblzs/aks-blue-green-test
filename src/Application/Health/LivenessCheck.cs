using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Health
{
    public class LivenessCheck : IHealthCheck
    {
        private readonly ILogger<LivenessCheck> _logger;

        public LivenessCheck(ILogger<LivenessCheck> logger)
        {
            _logger = logger;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation("LivenessHealthCheck executed.");

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}