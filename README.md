# Welcome to the Hark Hack Day!
\#harkhack

## The Brief

Showcase your talents and build something that interacts with physical devices on display based on different inputs.

For example:
- Temperature control system that turns on the fan under certain conditions.
- A payment gateway that allows you to turn on the kettle and bills for the total amount of electricity
- A twitter bot that tweets sensor information or can be used to control lights.

#### Judging Criteria
- Ability to work to the brief
- Technical accuracy and innovation
- Real world application


## What Have You Got to Play With?

### Devices & Data

Various sensors and devices are available to use. The sensor data includes temperature, CO2, humidity, vibration and kWh, and will be streamed and made available in real-time via an MQTT feed.

There are also a set of devices that can be turned on and off, including a kettle, heater, fan, motor and red, green and blue lamps. Changes to the state of these devices (ie: whether they have been turned on or off) will also be available via the same MQTT feed.

#### Units of Measure (UoM)
| Device | UoM |
|-|-|
| Temperature | °C |
| CO2 | PPM (parts per million) |
| Humidity | Percentage |


### Dell Gateway 3001/3002/3003

Each team will be given a Dell Edge Gateway.

The Dell Edge Gateway, with powerful dual-core Intel® Atom™ processors, connects varied wired and wireless devices and systems, aggregates and analyzes the input, and sends it on.

The Edge Gateway is made to withstand harsh conditions. It works out in the field, down on the factory floor, and stuck on an HVAC system, because it has industrial-grade form factors. Some models offer an operating temperature range of -30°C to 70°C.

### Emulation Control Panel
Your gateway will be running in `Development Mode` which will include access to a control panel with emulated devices and data.

It allows you to simulate turning devices on and off, as well as setting the temperature, CO2 level, humidity and motor speed. You can also generate a temporary small, medium or large vibration. This data will be sent to an MQTT feed running on the same gateway.

##### Control Panel Access
- {GATEWAY_IP_ADDRESS}:3000

##### MQTT Feed
- mqtt://{GATEWAY_IP_ADDRESS}:1883
- ws://{GATEWAY_IP_ADDRESS}:1884

#### Production Mode

The control panel is designed to emulate the real set of devices and sensor data.

Be sure to factor in a way to easily change these details as at the end of the day, you will need to connect to the real data feed and devices to showcase your invention.

## Device API

The devices are controlled via a simple api.

### IO Devices

#### Get the status of all IO devices

`GET /api/devices/state`

**Response**:

```json
{
  "state": {
    "fan": false,
    "greenLamp": false,
    "heater": false,
    "kettle": false,
    "redLamp": false,
    "blueLamp": false
  }
}
```
Where `true` means the device is ON, and `false` means the device is OFF.

#### Get the state of a single device

`GET /api/devices/{device}/state`

Where `{device}` is one of the following:
- `fan`
- `greenLamp`
- `heater`
- `kettle`
- `redLamp`
- `blueLamp`

**Response**:

```json
{
  "isOn": false
}
```

**`isOn`** will be either `true` or `false`.

#### Turn a device ON or OFF

`POST /api/devices/{device}/state`

Where `{device}` is one of the following:
- `fan`
- `greenLamp`
- `heater`
- `kettle`
- `redLamp`
- `blueLamp`

**Body**:

```json
{
  "turnOn": true
}
```

**`turnOn`** excepts either `true` or `false`.

**Response**: `200 OK` (no body)

### Motor

#### Get motor status:

`GET /api/devices/motor`

**Response**:

```json
{
  "amps": 0,
  "frequency": 0,
  "rpm": 0,
  "volts": 0,
  "watts": 0,
  "speed": 0,
  "running": false
}
```

#### Set motor speed:

`POST /api/devices/motor`

**Body**:

```json
{
  "speed": 100
}
```

The allowed values for **`speed`** are `0 - 4000` in steps of `100`.

**Response**: `200 OK` (no body)


## Metrics

Metrics are streamed to an MQTT feed every second. This includes values from all the sensors, IO devices and motor.

### Sensor Metrics

Sensor metrics are identified by the following ids:

| Id | Type | Source | Available in Emulator |
|-|-|-|-|
|  1 | temperature | IFM   | YES |
|  2 | vibration   | IFM   | YES |
|  3 | rpm         | MOTOR | YES |
|  4 | frequency   | MOTOR | YES |
|  5 | amps        | MOTOR | YES |
|  6 | watts       | MOTOR | YES |
|  7 | volts       | MOTOR | YES |
|  8 | co2         | EPC   | YES |
|  9 | temperature | EPC   | NO  |
| 10 | humidity    | EPC   | YES |
| 11 | amps        | METER | NO  |
| 12 | volts       | METER | NO  |
| 13 | kw          | METER | NO  |
| 14 | kwh         | METER | YES |

The topic will be `metrics/{id}` - ie: `metrics/1` for the IFM temperature data.

The payload includes the id, the value and the UTC received time.

##### Example:

```json
{"id":"1","receivedUtc":"2019-07-15T11:49:18Z","value":20}
{"id":"2","receivedUtc":"2019-07-15T11:49:18Z","value":0}
{"id":"8","receivedUtc":"2019-07-15T11:49:18Z","value":60
{"id":"10","receivedUtc":"2019-07-15T11:49:18Z","value":10}
```

### IO Devices & Motor Speed Metrics

Events are raised when the state of an IO device changes, or the speed of the motor changes. The topic is `devices` and `motor` respectively.

##### Example:
```json
{ "key":"redLamp", "isOn": false }
{ "key":"redLamp", "isOn": true }
{ "speed": 1000 }
```

## Example Code

Why not check out the sample code in this repo. There are examples of using the devices api and subscribing to the mqtt feed in C#, Typescript and Python.

## Links

* [Hark Hack Day Website](https://harksys.com/events/hack-day)
* [Hark Hack Day Eventbrite Page](https://www.eventbrite.co.uk/e/hark-hack-day-tickets-63980204514)
