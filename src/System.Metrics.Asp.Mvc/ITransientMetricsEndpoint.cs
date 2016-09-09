namespace System.Metrics.Asp.Mvc
{
    public interface ITransientMetricsEndpoint : IMetricsEndpoint
    {
        string Suffix { get; set; }
    }
}