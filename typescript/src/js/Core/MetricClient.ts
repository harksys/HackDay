import * as mqtt from 'async-mqtt';

import { MetricType } from './Enums';
import { default as config } from './Config';

type Metric = {
  id: string;
  receivedUtc: string;
  value: number;
};

type CallbackHandleMetric = (metric: Metric) => Promise<void>;

export interface IMetricClient {
  connect(): Promise<void>;

  handle(type: MetricType, callback: CallbackHandleMetric): void;
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

  public handleAll(callback: CallbackHandleMetric): void {
    for (const type in MetricType) {
      this.handle(Number(type), callback);
    }
  }

  public handle(type: MetricType, callback: CallbackHandleMetric): void {
    const topic = `metrics/${type}`;
    const callbacks = this.callbacks[topic] || [];

    callbacks.push(callback);

    this.callbacks[topic] = callbacks;
  }
}

export default new MqttMetricClient(config.mqttBrokerUrl);
