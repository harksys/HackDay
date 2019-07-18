import { controls, metrics } from './Core';
import { MetricId } from './Core/MetricClient';

const hot: number = 20;
const cold: number = 15;

const metricMap: { [key: string]: { id: MetricId; topic: string; source: string } } = {
  'ifm-temperature': { id: 1, topic: 'temperature', source: 'IFM' },
  'ifm-vibration': { id: 2, topic: 'vibration', source: 'IFM' },
  'motor-rpm': { id: 3, topic: 'motor', source: 'MOTOR' },
  'motor-frequency': { id: 4, topic: 'motor', source: 'MOTOR' },
  'mototr-amps': { id: 5, topic: 'motor', source: 'MOTOR' },
  'motor-watts': { id: 6, topic: 'motor', source: 'MOTOR' },
  'mototr-volts': { id: 7, topic: 'motor', source: 'MOTOR' },
  'epc-co2': { id: 8, topic: 'co2', source: 'EPC' },
  'epc-temperature': { id: 9, topic: 'temperature', source: 'EPC' },
  'epc-humidity': { id: 10, topic: 'humidity', source: 'EPC' },
  'meter-amps': { id: 11, topic: 'energy', source: 'METER' },
  'meter-volts': { id: 12, topic: 'energy', source: 'METER' },
  'meter-kw': { id: 13, topic: 'energy', source: 'METER' },
  'meter-kwh': { id: 14, topic: 'energy', source: 'METER' }
};

// turn on the red/blue lamp according to the temperature
(async (): Promise<void> => {
  await metrics.connect();

  console.log('Successfully connected to metric stream');

  await metrics.handle(metricMap['epc-temperature'].id, async (temperature): Promise<void> => {
    console.log(`Temperature: ${temperature.value}`);

    await controls.setDeviceState('redLamp', (temperature.value >= hot));
    await controls.setDeviceState('blueLamp', (temperature.value <= cold));
  });

  await metrics.handle(metricMap['epc-co2'].id, async (co2): Promise<void> =>
    console.log(`CO2: ${co2.value}`));
})();
