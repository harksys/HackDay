// <copyright file="Program.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application
{
    using System;
    using System.Threading.Tasks;

    using Application.Core;

    /// <summary>
    /// The application.
    /// </summary>
    public class Program
    {
        // NOTE:
        // update the configuration with the ip address of the gateway
        // you have been given.
        private const string GatewayIp = "[YOUR_GATEWAY_ADDRESS_HERE]";

        private const int MqttBrokerPort = 1883;

        private const int HotTemperature = 20;
        private const int ColdTemperature = 15;

        private static string ControlPanelUrl => $"http://{GatewayIp}:3000/api";

        private static string MqttBrokerAddress => GatewayIp;

        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">The application arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            var metrics = new MqttMetricClient(MqttBrokerAddress, MqttBrokerPort);
            var controls = new ControlPanelHttpClient(ControlPanelUrl);

            await metrics.ConnectAsync();

            Console.WriteLine("Successfully connected to metric stream");

            metrics.Handle($"{MetricIds.Epc_Temperature}", async metric =>
            {
                Console.WriteLine($"Temperature: {metric.Value}");

                await controls.SetDeviceStateAsync("redLamp", metric.Value >= HotTemperature);
                await controls.SetDeviceStateAsync("blueLamp", metric.Value <= ColdTemperature);
            });

            metrics.Handle($"{MetricIds.Epc_Co2}", metric =>
                Console.WriteLine($"CO2: {metric.Value}"));

            while (true)
            {
                // wait for exit
            }
        }

        internal enum MetricIds
        {
            Ifm_Temperature = 1,
            Ifm_Vibration = 2,
            Motor_Rpm = 3,
            Motor_Frequency = 4,
            Mototr_Amps = 5,
            Motor_Watts = 6,
            Mototr_Volts = 7,
            Epc_Co2 = 8,
            Epc_Temperature = 9,
            Epc_Humidity = 10,
            Meter_Amps = 11,
            Meter_Volts = 12,
            Meter_Kw = 13,
            Meter_Kwh = 14,
        }
    }
}
