using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ValueBlue.MovieSearch.Api.HealthChecks
{
    public class LivenessHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken()) =>
            Task.FromResult(HealthCheckResult.Healthy());
    }
}