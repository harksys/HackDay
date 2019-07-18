import requests

from collections import namedtuple

class ControlPanelHttpClient:
    def __init__(self, base_url):
        self.base_url = base_url
    
    def set_metric(self, id, value):
        requests.post(f"{self.base_url}/metric", json={ "id": id, "value": value })

    def get_metrics(self):
        r = requests.get(f"{self.base_url}/metric")
        d = r.json()['state']
        
        return namedtuple("Metrics", d.keys())(*d.values())

    def set_device_state(self, key, active):
        requests.post(f"{self.base_url}/devices/{key}/state", json={ "turnOn": active })

    def get_device_state(self, key):
        r = requests.get(f"{self.base_url}/devices/{key}/state")
        d = r.json()
        
        return namedtuple("DeviceState", d.keys())(*d.values())

    def get_device_states(self):
        r = requests.get(f"{self.base_url}/devices/state")
        d = r.json()['state']
        
        return namedtuple("DeviceStates", d.keys())(*d.values())

    def set_motor_speed(self, speed):
        if speed < 0 or speed > 4000:
            raise Exception('Motor speed must be between 0 and 4000')

        requests.post(f"{self.base_url}/devices/motor", json={ "speed": speed })

    def get_motor_status(self):
        r = requests.get(f"{self.base_url}/devices/motor")
        d = r.json()
        
        return namedtuple("MototStatus", d.keys())(*d.values())