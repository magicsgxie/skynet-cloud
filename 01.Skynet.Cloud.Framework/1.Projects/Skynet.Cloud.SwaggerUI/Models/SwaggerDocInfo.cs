// <copyright file="SwaggerDocInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UWay.Skynet.Cloud.SwaggerUI.Models
{
    /// <summary>
    /// API文档信息.
    /// </summary>
    public class SwaggerDocInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 是否启用.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets 文档标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets 分组名称.
        /// </summary>
        public string GroupName { get; set; } = "v1";

        /// <summary>
        /// Gets or sets 版本.
        /// </summary>
        public string Version { get; set; } = "v1";

        /// <summary>
        /// Gets or sets 描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets 联系信息.
        /// </summary>
        public ContactInfo Contact { get; set; }

        /// <summary>
        /// Gets or sets 分组Url前缀.
        /// </summary>
        public string GroupUrlPrefix { get; set; }

        /// <summary>
        /// Gets or sets 隐藏API配置.
        /// </summary>
        public HiddenApiConfigInfo HiddenApi { get; set; }
    }
}