import * as mqtt from 'async-mqtt';

import { default as config } from './Config';

export type MetricId =
  1
  | 2
  | 3
  | 4
  | 5
  | 6
  | 7
  | 8
  | 9
  | 10
  | 11
  | 12
  | 13
  | 14;

type Metric = {
  id: string;
  receivedUtc: string;
  value: number;
};

type CallbackHandleMetric = (metric: Metric) => Promise<void>;

export interface IMetricClient {
  connect(): Promise<void>;

  handle(metricTopic: MetricId, calId: CallbackHandleMetric): void;
}

export class MqttMetricClient implements IMetricClient {
  constructor(private brokerUrl: string) { }

  private readonly callbacks: { [topic: string]: CallbackHandleMetric[] } = {};

  private mqttClient: mqtt.AsyncMqttClient;

  public async connect(): Promise<void> {
    this.mqttClient = mqtt.connect(this.brokerUrl);

    await this.mqttClient.subscribe('#');

    this.mqttClient.on('message', async (topic: string, message: string) => {
      const callbacks = this.callbacks[topic];
      if (!callbacks) {
        return;
      }

      const metric = JSON.parse(message.toString()) as Metric;
      if (!metric) {
        return;
      }

      for (const callback of callbacks) {
        await callback(metric);
      }
    });
  }

  public handle(metricTopic: MetricId, callback: CallbackHandleMetric): void {
    const topic = `metrics/${metricTopic}`;
    const callbacks = this.callbacks[topic] || [];

    callbacks.push(callback) - 1;

    this.callbacks[topic] = callbacks;
  }
}

export default new MqttMetricClient(config.mqttBrokerUrl);
