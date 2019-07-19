// <copyright file="StringExtensions.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for strings.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Convert a string to camel casing.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The camel cased string.</returns>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            return char.ToLowerInvariant(value.First()) + value.Substring(1);
        }
    }
}
