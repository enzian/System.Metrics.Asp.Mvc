
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsExtensions
    {
        public static IMvcBuilder AddMetrics(this IServiceCollection services)
        {
            var builder = services.AddMvcCore(x => x.Filters.Add(new MetricsFilter()));
            return new MvcBuilder(builder.Services, builder.PartManager);
        }
    }
}