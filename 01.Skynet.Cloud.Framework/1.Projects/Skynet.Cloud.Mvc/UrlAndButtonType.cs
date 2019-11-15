using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// 连接和Button类型
    /// </summary>
    public class UrlAndButtonType
    {
        public string Url { get; set; }

        public byte ButtonType { get; set; }

        public bool IsPage { get; set; }
    }
}
