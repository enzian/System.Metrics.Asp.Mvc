
namespace System.Metrics.Asp.Mvc
{
    public interface IMetricsBuilder
    {
        MetricsHandler UseMetricsMiddleware(MetricsHandler handler);
    }
}