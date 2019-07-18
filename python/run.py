from core import metrics, controls

hot = 20
cold = 15

tempId = 1
co2Id = 8


def handle_temperature(temperature):
    print(f"Temperature: {temperature.value}")

    controls.set_device_state('redLamp', temperature.value >= hot)
    controls.set_device_state('blueLamp', temperature.value <= cold)


def handle_co2(co2):
    print(f"CO2: {co2.value}")


def run():
    metrics.connect()

    print("Successfully connected to metric stream")

    metrics.handle(tempId, handle_temperature)
    metrics.handle(co2Id, handle_co2)

    while True:
        pass


if __name__ == '__main__':
    run()
