// <copyright file="ControlPanelHttpClient.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    using System;
    using System.Threading.Tasks;

    using RestSharp;

    /// <inheritdoc />
    public sealed class ControlPanelHttpClient : IControlPanel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlPanelHttpClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The device control api base url.</param>
        public ControlPanelHttpClient(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException(nameof(baseUrl));
            }

            this.RestClient = new RestClient(baseUrl);
        }

        private RestClient RestClient { get; }

        /// <inheritdoc />
        public async Task SetMetricAsync(string id, decimal value)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            var request = new RestRequest("metric", Method.POST)
                .AddJsonBody(new { id, value });

            await this.RestClient.ExecuteTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task SetDeviceStateAsync(string key, bool active)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            var request = new RestRequest($"devices/{key}/state", Method.POST)
                .AddJsonBody(new { turnOn = active });

            await this.RestClient.ExecuteTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task<bool> GetDeviceStateAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            var request = new RestRequest($"devices/{key}/state", Method.GET);
            var response = await this.RestClient.ExecuteTaskAsync<DeviceState>(request);

            return response.Data.IsOn;
        }

        /// <inheritdoc />
        public async Task<DeviceStates> GetDeviceStatesAsync()
        {
            var request = new RestRequest("devices/state", Method.GET);
            var response = await this.RestClient.ExecuteTaskAsync<StateResponse<DeviceStates>>(request);

            return response.Data.State;
        }

        /// <inheritdoc />
        public async Task SetMotorSpeedAsync(int speed)
        {
            if (speed < 0 || speed > 4000)
            {
                throw new ArgumentOutOfRangeException("Motor speed must be between 0 and 4000");
            }

            var request = new RestRequest("devices/motor", Method.POST)
                .AddJsonBody(new { speed });

            await this.RestClient.ExecuteTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task<MotorStatus> GetMotorStatusAsync()
        {
            var request = new RestRequest("devices/motor", Method.GET);
            var response = await this.RestClient.ExecuteTaskAsync<MotorStatus>(request);

            return response.Data;
        }
    }
}
