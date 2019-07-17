import * as mqtt from 'async-mqtt';

import { default as config } from './Config';

type Metric = {
    id: string,
    receivedUtc: string,
    value: number
};

type MetricCallback = (metric: Metric) => Promise<void>;
type Unsubscribe = () => void;

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
    subscribe(metricTopic: MetricTopic, callback: MetricCallback): Promise<Unsubscribe>;
}

export class MqttMetricClient implements IMetricClient {
    private readonly callbacks: { [topic: string]: MetricCallback[] } = { };
    private mqttClient: mqtt.AsyncMqttClient;

    constructor(private brokerUrl: string) {
    }

    public connect(): Promise<void> {
        this.mqttClient = mqtt.connect(this.brokerUrl);

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
        
        return new Promise<void>(resolve => {
            this.mqttClient.on('connect', resolve);
        });
    }

    public async subscribe(metricTopic: MetricTopic, callback: MetricCallback): Promise<Unsubscribe> {
        const topic = `metrics/${metricTopic}`;

        const callbacks = this.callbacks[topic] || [];
        if (callbacks.length === 0)
            await this.mqttClient.subscribe(topic);

        const index = callbacks.push(callback) - 1;

        this.callbacks[topic] = callbacks;

        return async () => {
            callbacks.splice(index, 1);

            if (callbacks.length === 0)
                await this.mqttClient.unsubscribe(topic);
        };
    }
}

export default new MqttMetricClient(config.mqttBrokerUrl);
