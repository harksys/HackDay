from enum import Enum

class DeviceType(Enum):    
    HEATER = 'heater',
    FAN = 'fan',
    KETTLE = 'kettle',
    GREEN_LAMP = 'greenLamp',
    BLUE_LAMP = 'blueLamp',
    RED_LAMP = 'redLamp'
    
    def __str__(self):
        return '%s' % self.value

class MetricType(Enum):    
    IFM_TEMPERATURE = 1,
    IFM_VIBRATION = 2,
    MOTOR_RMP = 3,
    MOTOR_FREQUENCY = 4,
    MOTOR_AMPS = 5,
    MOTOR_WATTS = 6,
    MOTOR_VOLTS = 7,
    EPC_CO2 = 8,
    EPC_TEMPERATURE = 9,
    EPC_HUMIDITY = 10,
    METER_AMPS = 11,
    METER_VOLTS = 12,
    METER_KW = 13,
    METER_KWH = 14

    def __str__(self):
        return '%s' % self.value
