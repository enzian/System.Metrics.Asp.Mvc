using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc
{
    public static class MetricHandlers
    {
        public static MetricsHandler Counter(string metric, int increment = 1)
        {
            MetricsHandler handler = delegate (Endpoint endpoint, ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                endpoint.Record<Counting>(metric, increment);
                return next();
            };

            return handler;
        }
    }
}