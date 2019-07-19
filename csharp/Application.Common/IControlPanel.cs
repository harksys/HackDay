// <copyright file="IControlPanel.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Common
{
    using System.Threading.Tasks;

    using Application.Common.Enums;
    using Application.Common.Models;

    /// <summary>
    /// The control panel api.
    /// </summary>
    public interface IControlPanel
    {
        /// <summary>
        /// Sets the value of the given metric.
        /// </summary>
        /// <param name="metricType">The metric type.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetMetricAsync(MetricType metricType, decimal value);

        /// <summary>
        /// Turns an IO device ON or OFF.
        /// </summary>
        /// <param name="deviceType">The device type.</param>
        /// <param name="active">True to turn ON, false to turn OFF.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetDeviceStateAsync(DeviceType deviceType, bool active);

        /// <summary>
        /// Gets the state of a device.
        /// </summary>
        /// <param name="deviceType">The device type.</param>
        /// <returns>True if the device is ON, false is the device is OFF.</returns>
        Task<bool> GetDeviceStateAsync(DeviceType deviceType);

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
