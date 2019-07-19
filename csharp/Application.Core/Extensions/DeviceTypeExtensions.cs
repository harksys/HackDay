// <copyright file="DeviceTypeExtensions.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core.Extensions
{
    using Application.Common.Enums;

    /// <summary>
    /// Extensions for <see cref="DeviceType"/>
    /// </summary>
    internal static class DeviceTypeExtensions
    {
        /// <summary>
        /// Convert a <see cref="DeviceType"/> to a camel cased string.
        /// </summary>
        /// <param name="deviceType">The device type to convert.</param>
        /// <returns>The camel cased string.</returns>
        public static string ToCamelCaseString(this DeviceType deviceType)
        {
            return deviceType
                .ToString()
                .ToCamelCase();
        }
    }
}
