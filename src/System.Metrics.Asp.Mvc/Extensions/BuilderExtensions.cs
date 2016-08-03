
using Microsoft.Extensions.DependencyInjection;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsExtensions
    {
        public static void AddMetrics(this IServiceCollection services, Action<IMetricsBuilder> metricsSetup, Action<Endpoint> endpointSetup)
        {
            var metricsFilter = new MetricsFilter();
            metricsSetup(metricsFilter);

            services.AddMvcCore(x => x.Filters.Add(metricsFilter));

            services.AddScoped<Endpoint>(x =>
            {
                var service = new StandardEndpoint();
                endpointSetup(service);
                return service;
            });
        }
    }
}