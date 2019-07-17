using System.Threading.Tasks;

namespace Application.Core
{
    public interface IControlPanel
    {
        Task SetMetricAsync(string id, decimal value);

        Task<Metrics> GetMetricsAsync();

        Task SetDeviceStateAsync(string key, bool active);

        Task<bool> GetDeviceStateAsync(string key);

        Task<DeviceStates> GetDeviceStatesAsync();
        
        Task SetMotorSpeedAsync(int speed);

        Task<MotorStatus> GetMotorStatusAsync();
    }
}
