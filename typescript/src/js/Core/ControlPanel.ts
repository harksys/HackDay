import * as request from 'request-promise';

import { DeviceType, MetricType } from './Enums';
import { default as config } from './Config';

type DeviceStates = {
  [key in MetricType]: boolean;
};

type MotorStatus = {
  rpm: number;
  frequency: number;
  amps: number;
  volts: number;
  watts: number;
  speed: number;
  running: boolean;
};

export interface IControlPanel {
  setMetric(metricType: MetricType, value: number): Promise<void>;

  setDeviceState(deviceType: DeviceType, active: boolean): Promise<void>;

  getDeviceStates(): Promise<DeviceStates>;

  getDeviceState(deviceType: DeviceType): Promise<boolean>;

  setMotorSpeed(speed: number): Promise<void>;

  getMotorStatus(): Promise<MotorStatus>;
}

export class ControlPanelHttpClient implements IControlPanel {
  constructor(private baseUrl: string) { }

  public async setMetric(metricType: MetricType, value: number): Promise<void> {
    await request.post(`${this.baseUrl}/metric`, { json: { id: `${metricType}`, value } });
  }

  public async setDeviceState(deviceType: DeviceType, active: boolean): Promise<void> {
    await request.post(`${this.baseUrl}/devices/${deviceType}/state`, { json: { turnOn: active } });
  }

  public async getDeviceStates(): Promise<DeviceStates> {
    const { state } = await request.get(`${this.baseUrl}/devices/state`, { json: true });

    return state as DeviceStates;
  }

  public async getDeviceState(deviceType: DeviceType): Promise<boolean> {
    const { isOn } = await request.get(`${this.baseUrl}/devices/${deviceType}/state`, { json: true });

    return isOn;
  }

  public async setMotorSpeed(speed: number): Promise<void> {
    if (speed < 0 || speed > 4000) {
      throw new RangeError('Motor speed must be between 0 and 4000');
    }

    await request.post(`${this.baseUrl}/devices/motor`, { json: { speed } });
  }

  public async getMotorStatus(): Promise<MotorStatus> {
    return await request.get(`${this.baseUrl}/devices/motor`, { json: true }) as MotorStatus;
  }
}

export default new ControlPanelHttpClient(config.controlPanelBaseUrl);
