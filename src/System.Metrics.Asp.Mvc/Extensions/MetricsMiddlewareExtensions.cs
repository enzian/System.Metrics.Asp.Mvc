using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        public static IMetricsBuilder CountHitsTotal(this IMetricsBuilder subject)
        {
            MetricsHandler handler = async (Endpoint a, ResourceExecutingContext b, ResourceExecutionDelegate c) =>
            {
                a.Record<Counting>("overall.total", 1);
                await c();
            };

            subject.UseMetricsMiddleware(handler);

            return subject;
        }
    }
}