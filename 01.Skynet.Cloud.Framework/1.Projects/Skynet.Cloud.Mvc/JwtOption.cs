

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// JWT基础配置.
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// Gets or sets token是谁颁发的.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets token可以给哪些客户端使用.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets 加密的key.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets token是谁颁发的.
        /// </summary>
        public string Authority { get; set; }
    }
}
