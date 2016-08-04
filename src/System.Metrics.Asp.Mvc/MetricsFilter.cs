
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc
{
    public delegate Task<ResourceExecutedContext> MetricsHandler(Endpoint metricsEndpoint, ResourceExecutingContext context, ResourceExecutionDelegate next);

    public delegate bool MetricsCondition(ResourceExecutingContext context);

    public class MetricsFilter : IAsyncResourceFilter, IMetricsBuilder
    {
        public MetricsFilter()
        {
           Handler = delegate(Endpoint metricsEndpoint, ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                return next();
            };
        }

        public MetricsHandler Handler { get; internal set; }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            return Task.Run(() =>
            {
                var service = context?.HttpContext?.RequestServices?.GetService(typeof(Endpoint)) as Endpoint;
                return Handler(service, context, next);
            });
        }

        public MetricsHandler UseMetricsMiddleware(MetricsHandler handler)
        {
            var oldHandler = Handler;
            MetricsHandler h = delegate(Endpoint metricsEndpoint, ResourceExecutingContext b, ResourceExecutionDelegate c)
            {
                ResourceExecutionDelegate next = async () => {
                    return await oldHandler(metricsEndpoint, b, c);
                };

                return handler(metricsEndpoint, b, next);
            };

            Handler = h;

            return h;
        }
    }
}