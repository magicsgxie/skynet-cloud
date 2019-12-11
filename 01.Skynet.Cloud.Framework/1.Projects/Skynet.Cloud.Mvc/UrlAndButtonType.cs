
namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// 连接和Button类型.
    /// </summary>
    public class UrlAndButtonType
    {
        /// <summary>
        /// Gets or sets 地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets 类型.
        /// </summary>
        public byte ButtonType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否为页面.
        /// </summary>
        public bool IsPage { get; set; }
    }
}
