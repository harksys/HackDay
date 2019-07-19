// <copyright file="Metric.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Common.Models
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

        /// <summary>
        /// Convert the metric to a string.
        /// </summary>
        /// <returns>The string representation of the metric.</returns>
        public override string ToString() =>
            $"Id: {this.Id} ReceivedUtc: {this.ReceivedUtc} Value: {this.Value}";
    }
}
