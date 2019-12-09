using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// 连接和Button类型.
    /// </summary>
    public class UrlAndButtonType
    {
        /// <summary>
        /// 地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 类型.
        /// </summary>
        public byte ButtonType { get; set; }

        /// <summary>
        /// 是否为页面.
        /// </summary>
        public bool IsPage { get; set; }
    }
}
