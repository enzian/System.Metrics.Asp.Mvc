
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static System.Metrics.Asp.Mvc.MetricHandlers;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsExtensions
    {
        public static void AddMetrics(this IServiceCollection services, Action<IMetricsBuilder> metricsSetup, Action<IMetricsEndpoint> endpointSetup)
        {
            var metricsFilter = new MetricsFilter();
            // metricsSetup(metricsFilter);

            services.AddMvcCore(x => x.Filters.Add(metricsFilter));

            services.AddSingleton<IMetricsEndpoint>(x =>
            {
                var service = new StandardEndpoint();
                endpointSetup(service);
                return service;
            });

            services.AddScoped<ITransientMetricsEndpoint>(x => new TransientMetricsEndpoint(x.GetRequiredService<IMetricsEndpoint>()));
        }

        public static void UseMetrics(this IApplicationBuilder subject, Func<MetricsMiddleware, MetricsMiddleware> setup)
        {

            subject.Use(
                async (ctx, next) => {
                    MetricsMiddleware finalizer = async (context, ep, n) => { await n(); };

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