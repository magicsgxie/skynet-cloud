

namespace UWay.Skynet.Cloud.SwaggerUI.Models
{
    /// <summary>
    /// API配置.
    /// </summary>
    public class ApiConfigInfo
    {
        /// <summary>
        /// Gets or sets 支持部分路径匹配.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets hTTP方法配置（Get、Post），默认“*” .
        /// </summary>
        public string HttpMethod { get; set; } = "*";
    }
}