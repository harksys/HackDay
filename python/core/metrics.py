import json
import paho.mqtt.client as mqtt

from collections import namedtuple

class MqttMetricClient:
    def __init__(self, address, port):   
        self.address = address
        self.port = port

        self.client = mqtt.Client()
        self.callbacks = { }

    def connect(self):
        self.client.connect(self.address, self.port)
        self.client.on_message = self.__on_message

        self.client.subscribe('#')
        self.client.loop_start()

    def handle(self, id, callback):
        topic = f"metrics/{id}"

        callbacks = self.callbacks.get(topic)
        if not callbacks:
            self.callbacks[topic] = []

        self.callbacks[topic].append(callback)

    def __on_message(self, client, userdata, message):
        callbacks = self.callbacks.get(message.topic)
        if not callbacks:
            return

        payload = message.payload.decode('utf-8')
        if not payload:
            return

        d = json.loads(payload)
        metric = namedtuple("Metric", d.keys())(*d.values())

        for callback in callbacks:
            callback(metric)