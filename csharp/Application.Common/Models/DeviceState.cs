// <copyright file="DeviceState.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Common.Models
{
    /// <summary>
    /// Represents the state of an IO device.
    /// </summary>
    public sealed class DeviceState
    {
        /// <summary>
        /// Gets or sets a value indicating whether the device is ON or OFF.
        /// </summary>
        public bool IsOn { get; set; }
    }
}
