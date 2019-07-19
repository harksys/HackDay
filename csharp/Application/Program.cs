// <copyright file="Program.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application
{
    using System;
    using System.Threading.Tasks;
    using Application.Common.Enums;
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
        private const int ColdTemperature = 8;

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

            metrics.Handle(metric =>
                Console.WriteLine(metric.ToString()));

            metrics.Handle(MetricType.IfmTemperature, async metric =>
            {
                Console.WriteLine($"Temperature: {metric.Value}");

                await controls.SetDeviceStateAsync(DeviceType.RedLamp, metric.Value >= HotTemperature);
                await controls.SetDeviceStateAsync(DeviceType.BlueLamp, metric.Value <= ColdTemperature);
            });

            metrics.Handle(MetricType.EpcCo2, metric =>
                Console.WriteLine($"CO2: {metric.Value}"));

            while (true)
            {
                // wait for exit
            }
        }
    }
}
