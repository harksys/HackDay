from core import metrics, controls
from core.enums import DeviceType, MetricType

hot_temperature = 20
cold_temperature = 8

def handle_all(metric):
    print(metric)

def handle_temperature(temperature):
    print(f"Temperature: {temperature.value}")

    controls.set_device_state(DeviceType.RED_LAMP, temperature.value >= hot_temperature)
    controls.set_device_state(DeviceType.BLUE_LAMP, temperature.value <= cold_temperature)

def handle_co2(co2):
    print(f"CO2: {co2.value}")

def run():
    metrics.connect()

    print("Successfully connected to metric stream")

    metrics.handle_all(handle_all)
    metrics.handle(MetricType.IFM_TEMPERATURE, handle_temperature)
    metrics.handle(MetricType.EPC_CO2, handle_co2)

    # Wait for the program to exit
    while True:
        pass

if __name__ == '__main__':
    run()
