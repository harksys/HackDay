// <copyright file="IMetricClient.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The metric client.
    /// </summary>
    public interface IMetricClient
    {
        /// <summary>
        /// Connects to the metric data feed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ConnectAsync();

        /// <summary>
        /// Sets a message handler for the given metric type.
        /// </summary>
        /// <param name="metricId">The metric type id.</param>
        /// <param name="handler">The handler function.</param>
        void Handle(string metricId, Action<Metric> handler);

        /// <summary>
        /// Sets a message handler for the given metric type.
        /// </summary>
        /// <param name="metricId">The metric type id.</param>
        /// <param name="handler">The handler function.</param>
        void Handle(string metricId, Func<Metric, Task> handler);
    }
}
