using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        public static IMetricsBuilder CountHitsTotal(this IMetricsBuilder subject)
        {
            MetricsHandler handler = MetricHandlers.Counter("overall.total");

            subject.UseMetricsMiddleware(handler);

            return subject;
        }
    }
}