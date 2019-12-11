

using UWay.Skynet.Cloud.Storage.Core.Helper;

namespace UWay.Skynet.Cloud.Storage.Core
{
    /// <summary>
    /// 错误
    /// </summary>
    public class StorageError
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 处理程序错误消息
        /// </summary>
        public string ProviderMessage { get; set; }
    }
}