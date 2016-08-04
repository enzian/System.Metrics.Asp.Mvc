using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

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

        
        public static MetricsHandler Timer(string metric)
        {
            MetricsHandler handler = async (Endpoint endpoint, ResourceExecutingContext context, ResourceExecutionDelegate next) =>
            {
                var stopwatch = Stopwatch.StartNew();
                
                var t = await next();

                stopwatch.Stop();
                endpoint.Record<Timing>(metric, (int)stopwatch.ElapsedMilliseconds);

                return t;
            };

            return handler;
        }
    }
}