using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        public static IMetricsBuilder CountHitsTotal(this IMetricsBuilder subject)
        {
            MetricsHandler handler = delegate (Endpoint endpoint, ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                endpoint.Record<Counting>("overall.total", 1);
                return next();
            };

            subject.UseMetricsMiddleware(handler);

            return subject;
        }
    }
}