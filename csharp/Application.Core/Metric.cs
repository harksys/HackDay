// <copyright file="Metric.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    using System;

    /// <summary>
    /// Represents a metric.
    /// </summary>
    public sealed class Metric
    {
        /// <summary>
        /// Gets or sets the metric type id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the UTC received time.
        /// </summary>
        public DateTimeOffset ReceivedUtc { get; set; }

        /// <summary>
        /// Gets or sets the metric value.
        /// </summary>
        public decimal Value { get; set; }
    }
}
