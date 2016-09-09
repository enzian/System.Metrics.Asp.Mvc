using static System.Metrics.Asp.Mvc.MetricHandlers;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        // public static IMetricsBuilder CountHitsTotal(this IMetricsBuilder subject, string metric = "overall.total")
        // {
        //     MetricsMiddleware handler = MetricHandlers.Counter(metric);

        //     subject.UseMetricsMiddleware(handler);

        //     return subject;
        // }

        // public static IMetricsBuilder TimeTotal(this IMetricsBuilder subject, string metric = "overall.total.duration")
        // {
        //     MetricsMiddleware handler = MetricHandlers.Timer(metric);

        //     subject.UseMetricsMiddleware(handler);

        //     return subject;
        // }

        // public static IMetricsBuilder GaugeParallelRequests(this IMetricsBuilder subject, string metric = "overall.total_active")
        // {
        //     MetricsMiddleware handler = MetricHandlers.Gauge(metric);

        //     subject.UseMetricsMiddleware(handler);

        //     return subject;
        // }

        // public static IConditionalBuilder If(this IMetricsBuilder subject, MetricsCondition condition)
        // {
        //     return new ConditionalBuilder
        //     {
        //         Predecessor = subject,
        //         Condition = condition
        //     };
        // }
    }
}