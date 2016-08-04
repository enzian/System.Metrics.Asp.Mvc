
namespace System.Metrics.Asp.Mvc
{
    public interface IMetricsBuilder
    {
        MetricsHandler Handler { get; }

        MetricsHandler UseMetricsMiddleware(MetricsHandler handler);
    }
}