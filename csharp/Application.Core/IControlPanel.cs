// <copyright file="IControlPanel.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// The control panel api.
    /// </summary>
    public interface IControlPanel
    {
        /// <summary>
        /// Sets the value of the given metric.
        /// </summary>
        /// <param name="id">The metric id.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetMetricAsync(string id, decimal value);

        /// <summary>
        /// Turns an IO device ON or OFF.
        /// </summary>
        /// <param name="key">The device key.</param>
        /// <param name="active">True to turn ON, false to turn OFF.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetDeviceStateAsync(string key, bool active);

        /// <summary>
        /// Gets the state of a device.
        /// </summary>
        /// <param name="key">The device key.</param>
        /// <returns>True if the device is ON, false is the device is OFF.</returns>
        Task<bool> GetDeviceStateAsync(string key);

        /// <summary>
        /// Gets the state of all IO devices.
        /// </summary>
        /// <returns>The state of all IO devices.</returns>
        Task<DeviceStates> GetDeviceStatesAsync();

        /// <summary>
        /// Sets the speed of the motor.
        /// </summary>
        /// <param name="speed">The required speed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetMotorSpeedAsync(int speed);

        /// <summary>
        /// Gets the status of the motor.
        /// </summary>
        /// <returns>The motor status.</returns>
        Task<MotorStatus> GetMotorStatusAsync();
    }
}
