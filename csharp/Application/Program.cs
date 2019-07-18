using Application.Core;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class Program
    {
        private const string ControlPanelUrl = "http://10.0.0.45:3000/api";
        private const string MqttBrokerAddress = "10.0.0.45";
        private const int MqttBrokerPort = 1883;

        private const int HotTemperature = 20;
        private const int ColdTemperature = 15;

        public static async Task Main(string[] args)
        {
            var metrics = new MqttMetricClient(MqttBrokerAddress, MqttBrokerPort);
            var controls = new ControlPanelHttpClient(ControlPanelUrl);

            await metrics.ConnectAsync();

            Console.WriteLine("Successfully connected to metric stream");

            metrics.Handle("temperature", async metric =>
            {
                await controls.SetDeviceStateAsync("redLamp", metric.Value >= HotTemperature);
                await controls.SetDeviceStateAsync("blueLamp", metric.Value <= ColdTemperature);
            });

            while (true) ;
        }
    }
}
