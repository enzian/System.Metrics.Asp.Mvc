namespace System.Metrics.Asp.Mvc.Extensions
{
    public interface IConditionalBuilder : IMetricsBuilder
    {
        MetricsCondition Condition { get; set; }
    }
}