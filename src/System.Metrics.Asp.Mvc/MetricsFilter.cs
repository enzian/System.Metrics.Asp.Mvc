
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc
{

    public class MetricsFilter : IAsyncResourceFilter
    {
        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            return Task.Run(() =>
            {
            });
        }
    }
}