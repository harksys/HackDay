using RestSharp;
using System;
using System.Threading.Tasks;

namespace Application.Core
{
    public sealed class ControlPanelHttpClient : IControlPanel
    {
        private readonly RestClient _restClient;

        public ControlPanelHttpClient(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException(nameof(baseUrl));
            }

            _restClient = new RestClient(baseUrl);
        }

        public async Task SetMetricAsync(string id, decimal value)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            var request = new RestRequest("metric", Method.POST)
                .AddJsonBody(new { id, value });

            await _restClient.ExecuteTaskAsync(request);
        }

        public async Task<Metrics> GetMetricsAsync()
        {
            var request = new RestRequest("metric", Method.GET);
            var response = await _restClient.ExecuteTaskAsync<StateResponse<Metrics>>(request);

            return response.Data.State;
        }

        public async Task SetDeviceStateAsync(string key, bool active)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            var request = new RestRequest($"devices/{key}/state", Method.POST)
                .AddJsonBody(new { turnOn = active });

            await _restClient.ExecuteTaskAsync(request);
        }
        
        public async Task<bool> GetDeviceStateAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            var request = new RestRequest($"devices/{key}/state", Method.GET);
            var response = await _restClient.ExecuteTaskAsync<DeviceState>(request);

            return response.Data.IsOn;
        }

        public async Task<DeviceStates> GetDeviceStatesAsync()
        {
            var request = new RestRequest("devices/state", Method.GET);
            var response = await _restClient.ExecuteTaskAsync<StateResponse<DeviceStates>>(request);

            return response.Data.State;
        }

        public async Task SetMotorSpeedAsync(int speed)
        {
            if (speed < 0 || speed > 4000)
            {
                throw new ArgumentOutOfRangeException("Motor speed must be between 0 and 4000");
            }

            var request = new RestRequest("devices/motor", Method.POST)
                .AddJsonBody(new { speed });

            await _restClient.ExecuteTaskAsync(request);
        }

        public async Task<MotorStatus> GetMotorStatusAsync()
        {
            var request = new RestRequest("devices/motor", Method.GET);
            var response = await _restClient.ExecuteTaskAsync<MotorStatus>(request);

            return response.Data;
        }
    }
}
