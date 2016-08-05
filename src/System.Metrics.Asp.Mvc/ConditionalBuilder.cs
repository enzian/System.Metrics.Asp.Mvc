using Microsoft.AspNetCore.Mvc.Filters;

namespace System.Metrics.Asp.Mvc.Extensions
{
    internal class ConditionalBuilder : IConditionalBuilder
    {
        public MetricsCondition Condition { get; set; }

        public MetricsHandler Handler
        {
            get
            {
                throw new NotImplementedException("Cannot receive handler from Conditional Metrics");
            }
        }

        public IMetricsBuilder Predecessor { get; set;}

        public MetricsHandler UseMetricsMiddleware(MetricsHandler handler)
        {
            MetricsHandler unconditionalHandler = Predecessor.Handler;

            MetricsHandler conditionalHandler = async (Endpoint endpoint, ResourceExecutingContext context, ResourceExecutionDelegate next) =>
            {
                var shouldHandle = Condition(context);
                
                if(shouldHandle){
                     return await handler(endpoint, context, next);
                }
                
                return await next();
            };

            Predecessor.UseMetricsMiddleware(conditionalHandler);

            return conditionalHandler;
        }
    }
}