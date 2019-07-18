import { controls, metrics } from './Core';

const hot = 20;
const cold = 15;

// Turn on the red/blue lamp according to the temperature
(async () => {
    await metrics.connect();

    console.log('Successfully connected to metric stream');

    await metrics.handle('temperature', async (temperature) => {
        console.log(`Temperature: ${temperature.value}`);

        await controls.setDeviceState('redLamp', (temperature.value >= hot));
        await controls.setDeviceState('blueLamp', (temperature.value <= cold));
    });
    
    await metrics.handle('co2', async (co2) =>
        console.log(`CO2: ${co2.value}`));
})();