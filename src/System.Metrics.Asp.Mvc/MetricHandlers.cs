using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace System.Metrics.Asp.Mvc
{
    public delegate Task MetricsMiddleware(HttpContext context, IMetricsEndpoint endpoint, Func<Task> next);

    public static class MetricHandlers
    {
        public static MetricsMiddleware Counter(string metric, int increment = 1)
        {

            return async (context, ep, next) =>
            {
                await next();
                ep.Record<Counting>(metric, increment);
            };
        }

        public static MetricsMiddleware Timer(string metric)
        {
            return async (context, ep, next) =>
            {
                var stopwatch = Stopwatch.StartNew();

                await next();

                ep.Record<Timing>(metric, (int)stopwatch.ElapsedMilliseconds);
            };
        }


        public static MetricsMiddleware Gauge(string metric)
        {
            return async (context, ep, next) =>
            {
                ep.Record<Gauge>(metric, 1, true);

                await next();

                ep.Record<Gauge>(metric, -1, true);
            };
        }
    }
}