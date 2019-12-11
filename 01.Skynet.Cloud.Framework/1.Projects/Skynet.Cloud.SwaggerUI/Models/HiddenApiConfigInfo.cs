// <copyright file="HiddenApiConfigInfo.cs" company="PlaceholderCompany">
// Copyright (c) Uway. All rights reserved.
// </copyright>

namespace UWay.Skynet.Cloud.SwaggerUI.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// API隐藏配置.
    /// </summary>
    public class HiddenApiConfigInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether isEnabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets urls.
        /// </summary>
        public List<ApiConfigInfo> Urls { get; set; }
    }
}