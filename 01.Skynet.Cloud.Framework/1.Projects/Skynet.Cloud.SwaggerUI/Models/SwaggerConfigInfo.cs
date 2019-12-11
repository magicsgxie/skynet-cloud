// <copyright file="SwaggerConfigInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UWay.Skynet.Cloud.SwaggerUI.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Swagger配置信息.
    /// </summary>
    public class SwaggerConfigInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 是否启用.
        /// </summary>
        public bool IsEnabled { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether 将枚举值以字符串显示.
        /// </summary>
        public bool DescribeAllEnumsAsStrings { get; set; }

        /// <summary>
        /// Gets or sets api文档列表.
        /// </summary>
        public List<SwaggerDocInfo> SwaggerDocInfos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 在界面上显示验证（Authorize）按钮，验证按钮处理逻辑基于 wwwroot/swagger/ui/index.html.
        /// </summary>
        public bool Authorize { get; set; }

        /// <summary>
        /// Gets or sets 隐藏API配置.
        /// </summary>
        public HiddenApiConfigInfo HiddenApi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否启用API全名.
        /// </summary>
        public bool UseFullNameForSchemaId { get; set; }

        /// <summary>
        /// Gets or sets 需要从嵌入资源中加载的程序集全称.
        /// </summary>
        public string ManifestResourceAssembly { get; set; }

        /// <summary>
        /// Gets or sets 嵌入资源路径.
        /// </summary>
        public string ManifestResourceUrl { get; set; }

    }
}
