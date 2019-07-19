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
  setMetric(type: MetricType, value: number): Promise<void>;

  setDeviceState(type: DeviceType, active: boolean): Promise<void>;

  getDeviceStates(): Promise<DeviceStates>;

  getDeviceState(type: DeviceType): Promise<boolean>;

  setMotorSpeed(speed: number): Promise<void>;

  getMotorStatus(): Promise<MotorStatus>;
}

export class ControlPanelHttpClient implements IControlPanel {
  constructor(private baseUrl: string) { }

  public async setMetric(type: MetricType, value: number): Promise<void> {
    await request.post(`${this.baseUrl}/metric`, { json: { id: type, value } });
  }

  public async setDeviceState(type: DeviceType, active: boolean): Promise<void> {
    await request.post(`${this.baseUrl}/devices/${type}/state`, { json: { turnOn: active } });
  }

  public async getDeviceStates(): Promise<DeviceStates> {
    const response = await request.get(`${this.baseUrl}/devices/state`, { json: true });

    return response.state as DeviceStates;
  }

  public async getDeviceState(type: DeviceType): Promise<boolean> {
    const response = await request.get(`${this.baseUrl}/devices/${type}/state`, { json: true });

    return response.isOn;
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
