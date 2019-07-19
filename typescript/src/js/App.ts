import { controls, metrics } from './Core';
import { DeviceType, MetricType } from './Core';

const hot: number = 20;
const cold: number = 15;

// turn on the red/blue lamp according to the temperature
(async (): Promise<void> => {
  await metrics.connect();

  console.log('Successfully connected to metric stream');
  
  // Log all incoming metrics to the console
  await metrics.handleAll(async (metric): Promise<void> =>
    console.log(metric));

  await metrics.handle(MetricType.EpcTemperature, async (temperature): Promise<void> => {
    console.log(`Temperature: ${temperature.value}`);

    await controls.setDeviceState(DeviceType.RedLamp, (temperature.value >= hot));
    await controls.setDeviceState(DeviceType.BlueLamp, (temperature.value <= cold));
  });

  await metrics.handle(MetricType.EpcCo2, async (co2): Promise<void> =>
    console.log(`CO2: ${co2.value}`));
})();
