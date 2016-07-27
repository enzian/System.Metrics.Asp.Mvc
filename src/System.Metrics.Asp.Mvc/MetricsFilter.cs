
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc
{
    public delegate Task MetricsHandler(Endpoint metricsEndpoint, ResourceExecutingContext context, ResourceExecutionDelegate next);

    public class MetricsFilter : IAsyncResourceFilter, IMetricsBuilder
    {
        public MetricsFilter()
        {
           _handler = delegate(Endpoint metricsEndpoint, ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                return next();
            };
        }

        internal Func<Endpoint, ResourceExecutingContext, ResourceExecutionDelegate, Task<ResourceExecutedContext>> _handler { get; set; }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            return Task.Run(() =>
            {
                var service = context.HttpContext.RequestServices.GetService(typeof(Endpoint)) as Endpoint;
                return _handler(service, context, next);
            });
        }

        public MetricsHandler UseMetricsMiddleware(MetricsHandler handler)
        {
            MetricsHandler h = delegate(Endpoint metricsEndpoint, ResourceExecutingContext b, ResourceExecutionDelegate c)
            {
                ResourceExecutionDelegate next = async () => {
                    return await _handler(metricsEndpoint, b, c);
                };

                return handler(metricsEndpoint, b, next);
            };

            return h;
        }
    }
}