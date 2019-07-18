// <copyright file="DeviceStates.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    /// <summary>
    /// Represents the state of all IO devices.
    /// </summary>
    public sealed class DeviceStates
    {
        /// <summary>
        /// Gets or sets a value indicating whether the heater is ON or OFF.
        /// </summary>
        public bool Heater { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fan is ON or OFF.
        /// </summary>
        public bool Fan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the kettle is ON or OFF.
        /// </summary>
        public bool Kettle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the green lamp is ON or OFF.
        /// </summary>
        public bool GreenLamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blue lamp is ON or OFF.
        /// </summary>
        public bool BlueLamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the red lamp is ON or OFF.
        /// </summary>
        public bool RedLamp { get; set; }
    }
}
