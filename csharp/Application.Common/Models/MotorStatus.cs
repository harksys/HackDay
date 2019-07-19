// <copyright file="MotorStatus.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Common.Models
{
    /// <summary>
    /// Represents the status of the motor.
    /// </summary>
    public sealed class MotorStatus
    {
        /// <summary>
        /// Gets or sets the amps.
        /// </summary>
        public decimal Amps { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// Gets or sets the rpm.
        /// </summary>
        public decimal RPM { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the volts.
        /// </summary>
        public decimal Volts { get; set; }

        /// <summary>
        /// Gets or sets the watts.
        /// </summary>
        public decimal Watts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the motor is running or not.
        /// </summary>
        public bool Running { get; set; }
    }
}
