import core.config as config

from core.metrics import MqttMetricClient
from core.controls import ControlPanelHttpClient

metrics = MqttMetricClient(config.mqtt_broker_address, config.mqtt_broker_port)
controls = ControlPanelHttpClient(config.control_panel_url)