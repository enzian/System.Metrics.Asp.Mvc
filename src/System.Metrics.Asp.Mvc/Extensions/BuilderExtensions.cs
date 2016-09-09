
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsExtensions
    {
        /// <Summary>
        /// Extends the service collection with all services necessary to run metrics
        /// </summar>
        public static void AddMetrics(this IServiceCollection services, Action<IMetricsEndpoint> endpointSetup)
        {
            var metricsFilter = new MetricsFilter();
            services.AddMvcCore(x => x.Filters.Add(metricsFilter));

            services.AddSingleton<IMetricsEndpoint>(x =>
            {
                var service = new StandardEndpoint();
                endpointSetup(service);
                return service;
            });

            services.AddScoped<ITransientMetricsEndpoint>(x => new TransientMetricsEndpoint(x.GetRequiredService<IMetricsEndpoint>()));
        }

        
        /// <Summary>
        /// Attach a metrics handler into the request pipe
        /// </summar>
        public static void UseMetrics(this IApplicationBuilder subject, Func<MetricsMiddleware, MetricsMiddleware> setup)
        {

            subject.Use(
                async (ctx, next) => {
                    MetricsMiddleware finalizer = async (context, ep, _) =>
                    {
                        await next();
                    };

                    var d = setup(finalizer);

                    var service = ctx.RequestServices.GetService(typeof(ITransientMetricsEndpoint)) as ITransientMetricsEndpoint;
                    if(service != null)
                    {
                        await d(ctx, service, next);
                        return;
                    }

                    await next();
                });
        }
    }
}