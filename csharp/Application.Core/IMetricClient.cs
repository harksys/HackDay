using System;
using System.Threading.Tasks;

namespace Application.Core
{
    public interface IMetricClient
    {
        Task ConnectAsync();

        void Handle(string metricId, Action<Metric> handler);

        void Handle(string metricId, Func<Metric, Task> handler);
    }
}
