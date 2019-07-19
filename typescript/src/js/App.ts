import { controls, metrics } from './Core';
import { DeviceType, MetricType } from './Core';

const hotTemperature: number = 20;
const coldTemperature: number = 15;

// turn on the red/blue lamp according to the temperature
(async (): Promise<void> => {
  await metrics.connect();

  console.log('Successfully connected to metric stream');

  // log all incoming metrics to the console
  await metrics.handleAll(async (metric): Promise<void> =>
    console.log(metric));

  await metrics.handle(MetricType.EpcTemperature, async (temperature): Promise<void> => {
    console.log(`Temperature: ${temperature.value}`);

    await controls.setDeviceState(DeviceType.RedLamp, (temperature.value >= hotTemperature));
    await controls.setDeviceState(DeviceType.BlueLamp, (temperature.value <= coldTemperature));
  });

  await metrics.handle(MetricType.EpcCo2, async (co2): Promise<void> =>
    console.log(`CO2: ${co2.value}`));
})();
