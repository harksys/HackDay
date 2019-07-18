// <copyright file="StateResponse{T}.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace Application.Core
{
    /// <summary>
    /// Represents a api state response.
    /// </summary>
    /// <typeparam name="T">The state response type.</typeparam>
    internal sealed class StateResponse<T>
    {
        /// <summary>
        /// Gets or sets the state response.
        /// </summary>
        public T State { get; set; }
    }
}
