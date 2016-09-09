using System.Metrics;
using System.Metrics.Asp.Mvc;

namespace System.Metrics.Asp.Mvc
{

    public class TransientMetricsEndpoint : ITransientMetricsEndpoint
    {
        private IMetricsEndpoint _backend;

        public string Suffix { get; set; }

        public string Prefix
        {
            get
            {
                return _backend.Prefix;
            }
            set
            {
                _backend.Prefix = value;
            }
        }

        public TransientMetricsEndpoint(IMetricsEndpoint backend)
        {
            _backend = backend;
        }

        public void AddBackend(IMetricsSink sink)
        {
            _backend.AddBackend(sink);
        }

        public void Record<TMetric>(string metric, string value) where TMetric : IAllowsString
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value);
        }

        public void Record<TMetric>(string metric, double value) where TMetric : IAllowsDouble
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value);
        }

        public void Record<TMetric>(string metric, int value) where TMetric : IAllowsInteger
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value);
        }

        public void Record<TMetric>(string metric, double value, double sampleRate) where TMetric : IAllowsDouble, IAllowsSampleRate
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value, sampleRate);
        }

        public void Record<TMetric>(string metric, int value, double sampleRate) where TMetric : IAllowsInteger, IAllowsSampleRate
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value, sampleRate);
        }

        public void Record<TMetric>(string metric, double value, bool isDelta = false) where TMetric : IAllowsDouble, IAllowsDelta
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value, isDelta);
        }

        public void Record<TMetric>(string metric, int value, bool isDelta = false) where TMetric : IAllowsInteger, IAllowsDelta
        {
            _backend.Record<TMetric>($"{Suffix}.{metric}", value, isDelta);
        }
    }
}