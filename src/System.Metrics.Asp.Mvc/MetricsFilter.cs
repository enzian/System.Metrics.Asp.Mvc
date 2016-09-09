
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc
{

    public class MetricsFilter : IAsyncResourceFilter
    {
        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // Enrich the transient instance of IMetrics Serivce with the controller name and ActionDescriptor
            var service = context.HttpContext.RequestServices.GetService(typeof(ITransientMetricsEndpoint)) as ITransientMetricsEndpoint;
            if(service != null)
            {
                var controllerAction = context.ActionDescriptor as ControllerActionDescriptor;
                if(controllerAction != null)
                {
                    service.Suffix = $"{controllerAction.ControllerName}.{controllerAction.ActionName}";
                }
            }

            return next();
        }
    }
}