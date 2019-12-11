
using System;

namespace UWay.Skynet.Cloud.Storage.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class StorageException : Exception
    {
        public StorageException(StorageError error, Exception ex) : base(error.Message, ex)
        {
            ErrorCode = error.Code;
            ProviderMessage = ex?.Message;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// 提供程序消息
        /// </summary>
        public string ProviderMessage { get; set; }
    }
}