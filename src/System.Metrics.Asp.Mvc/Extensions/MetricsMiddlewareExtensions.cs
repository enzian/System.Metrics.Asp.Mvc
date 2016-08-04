namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        public static IMetricsBuilder CountHitsTotal(this IMetricsBuilder subject, string metric = "overall.total")
        {
            MetricsHandler handler = MetricHandlers.Counter(metric);

            subject.UseMetricsMiddleware(handler);

            return subject;
        }

        public static IMetricsBuilder TimeTotal(this IMetricsBuilder subject, string metric = "overall.total.duration")
        {
            MetricsHandler handler = MetricHandlers.Timer(metric);

            subject.UseMetricsMiddleware(handler);

            return subject;
        }

        public static IMetricsBuilder GaugeParallelRequests(this IMetricsBuilder subject, string metric = "overall.total_active")
        {
            MetricsHandler handler = MetricHandlers.Gauge(metric);

            subject.UseMetricsMiddleware(handler);

            return subject;
        }
    }
}