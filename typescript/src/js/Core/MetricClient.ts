import * as mqtt from 'async-mqtt';

import { default as config } from './Config';

type Metric = {
    id: string,
    receivedUtc: string,
    value: number
};

type CallbackHandleMetric = (metric: Metric) => Promise<void>;

type MetricTopic =
    'co2'
    | 'humidity'
    | 'temperature'
    | 'vibration'
    | 'motor-rmp'
    | 'motor-frequency'
    | 'motor-volts'
    | 'motor-amps'
    | 'motor-watts';

export interface IMetricClient {
    connect(): Promise<void>;
    handle(metricTopic: MetricTopic, callback: CallbackHandleMetric): void;
}

export class MqttMetricClient implements IMetricClient {
    private readonly callbacks: { [topic: string]: CallbackHandleMetric[] } = { };
    private mqttClient: mqtt.AsyncMqttClient;

    constructor(private brokerUrl: string) {
    }

    public async connect(): Promise<void> {
        this.mqttClient = mqtt.connect(this.brokerUrl);

        await this.mqttClient.subscribe('#');

        this.mqttClient.on('message', async (topic: string, message: string) => {
            const callbacks = this.callbacks[topic];
            if (!callbacks)
                return;

            const metric = JSON.parse(message.toString()) as Metric;
            if (!metric)
                return;

            for (const callback of callbacks)
                await callback(metric);
        });
    }

    public handle(metricTopic: MetricTopic, callback: CallbackHandleMetric): void {
        const topic = `metrics/${metricTopic}`;
        const callbacks = this.callbacks[topic] || [];

        callbacks.push(callback) - 1;

        this.callbacks[topic] = callbacks;
    }
}

export default new MqttMetricClient(config.mqttBrokerUrl);
