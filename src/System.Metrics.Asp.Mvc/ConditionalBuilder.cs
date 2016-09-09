using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc.Extensions
{
    internal class ConditionalBuilder// : IConditionalBuilder
    {
        // public MetricsCondition Condition { get; set; }

        // public MetricsMiddleware Handler
        // {
        //     get
        //     {
        //         throw new NotImplementedException("Cannot receive handler from Conditional Metrics");
        //     }
        // }

        // public IMetricsBuilder Predecessor { get; set;}

        // public MetricsMiddleware UseMetricsMiddleware(MetricsMiddleware handler)
        // {
        //     MetricsMiddleware unconditionalHandler = Predecessor.Handler;

        //     MetricsMiddleware conditionalHandler = async (IMetricsEndpoint endpoint, ResourceExecutingContext context, ResourceExecutionDelegate next) =>
        //     {
        //         var shouldHandle = Condition(context);

        //         if(shouldHandle){
        //              return await handler(endpoint, context, next);
        //         }

        //         return await next();
        //     };

        //     Predecessor.UseMetricsMiddleware(conditionalHandler);

        //     return conditionalHandler;
        // }
    }
}