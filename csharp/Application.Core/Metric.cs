using System;

namespace Application.Core
{
    public sealed class Metric
    {
        public string Id { get; set; }

        public DateTimeOffset ReceivedUtc { get; set; }

        public decimal Value { get; set; }
    }
}
