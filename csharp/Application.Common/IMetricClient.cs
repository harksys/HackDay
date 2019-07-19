// <copyright file="IMetricClient.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Common
{
    using System;
    using System.Threading.Tasks;

    using Application.Common.Enums;
    using Application.Common.Models;

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
        /// Sets a message handler for all metric types.
        /// </summary>
        /// <param name="handler">The handler function.</param>
        void Handle(Action<Metric> handler);

        /// <summary>
        /// Sets a message handler for all metric types.
        /// </summary>
        /// <param name="handler">The handler function.</param>
        void Handle(Func<Metric, Task> handler);

        /// <summary>
        /// Sets a message handler for the given metric type.
        /// </summary>
        /// <param name="metricType">The metric type.</param>
        /// <param name="handler">The handler function.</param>
        void Handle(MetricType metricType, Action<Metric> handler);

        /// <summary>
        /// Sets a message handler for the given metric type.
        /// </summary>
        /// <param name="metricType">The metric type.</param>
        /// <param name="handler">The handler function.</param>
        void Handle(MetricType metricType, Func<Metric, Task> handler);
    }
}
