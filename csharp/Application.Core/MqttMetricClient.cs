using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core
{
    public sealed class MqttMetricClient : IMetricClient
    {
        private readonly IDictionary<string, List<Func<Metric, Task>>> _handlers = new Dictionary<string, List<Func<Metric, Task>>>();

        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;

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

            _mqttClient = factory.CreateMqttClient();
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(address, port)
                .Build();
        }

        public async Task ConnectAsync()
        {
            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                if (!_handlers.TryGetValue(e.ApplicationMessage.Topic, out var handlers))
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

            await _mqttClient.ConnectAsync(_mqttClientOptions);

            var filter = new TopicFilterBuilder()
                .WithTopic("#")
                .Build();

            await _mqttClient.SubscribeAsync(filter);
        }

        public void Handle(string metricId, Action<Metric> handler)
        {
            if (string.IsNullOrWhiteSpace(metricId))
            {
                throw new ArgumentException(nameof(metricId));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscribe(metricId, async metric =>
                await Task.Run(() => handler(metric)));
        }

        public void Handle(string metricId, Func<Metric, Task> handler)
        {
            if (string.IsNullOrWhiteSpace(metricId))
            {
                throw new ArgumentException(nameof(metricId));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var topic = $"metrics/{metricId}";

            if (!_handlers.TryGetValue(topic, out var handlers))
            {
                handlers = new List<Func<Metric, Task>>();

                _handlers.Add(topic, handlers);
            }

            handlers.Add(handler);
        }
    }
}
