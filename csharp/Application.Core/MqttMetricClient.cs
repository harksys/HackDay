﻿// <copyright file="MqttMetricClient.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Application.Common;
    using Application.Common.Enums;
    using Application.Common.Models;

    using MQTTnet;
    using MQTTnet.Client;
    using MQTTnet.Client.Options;

    using Newtonsoft.Json;

    /// <inheritdoc />
    public sealed class MqttMetricClient : IMetricClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqttMetricClient"/> class.
        /// </summary>
        /// <param name="address">The mqtt address.</param>
        /// <param name="port">The mqtt port.</param>
        public MqttMetricClient(string address, int port)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }

            if (port < 0 || port > 65535)
            {
                throw new ArgumentOutOfRangeException($"{nameof(port)} must be between 0 and 65535");
            }

            var factory = new MqttFactory();

            this.MqttClient = factory.CreateMqttClient();

            this.MqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(address, port)
                .Build();
        }

        private IDictionary<string, List<Func<Metric, Task>>> Handlers { get; }
            = new Dictionary<string, List<Func<Metric, Task>>>();

        private IMqttClient MqttClient { get; }

        private IMqttClientOptions MqttClientOptions { get; }

        /// <inheritdoc />
        public async Task ConnectAsync()
        {
            this.MqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                if (!this.Handlers.TryGetValue(e.ApplicationMessage.Topic, out var handlers))
                {
                    return;
                }

                var message = e.ApplicationMessage.ConvertPayloadToString();

                var metric = JsonConvert.DeserializeObject<Metric>(message);
                if (metric == null)
                {
                    return;
                }

                foreach (var handler in handlers)
                {
                    await handler(metric);
                }
            });

            await this.MqttClient.ConnectAsync(this.MqttClientOptions);

            var filter = new TopicFilterBuilder()
                .WithTopic("#")
                .Build();

            await this.MqttClient.SubscribeAsync(filter);
        }

        /// <inheritdoc />
        public void Handle(Action<Metric> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.Handle(async metric =>
                await Task.Run(() => handler(metric)));
        }

        /// <inheritdoc />
        public void Handle(Func<Metric, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            foreach (MetricType metricType in Enum.GetValues(typeof(MetricType)))
            {
                this.Handle(metricType, handler);
            }
        }

        /// <inheritdoc />
        public void Handle(MetricType metricType, Action<Metric> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.Handle(metricType, async metric =>
                await Task.Run(() => handler(metric)));
        }

        /// <inheritdoc />
        public void Handle(MetricType metricType, Func<Metric, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var topic = $"metrics/{(int)metricType}";

            if (!this.Handlers.TryGetValue(topic, out var handlers))
            {
                handlers = new List<Func<Metric, Task>>();

                this.Handlers.Add(topic, handlers);
            }

            handlers.Add(handler);
        }
    }
}
