using System;
using System.Threading.Tasks;

namespace Application.Core
{
    public interface IMetricClient
    {
        Task ConnectAsync();

        Task SubscribeAsync(string metricId, Action<Metric> handler);

        Task SubscribeAsync(string metricId, Func<Metric, Task> handler);
    }
}
