

using System;
using Aliyun.OSS.Model;
using UWay.Skynet.Cloud.Storage.Core;

namespace UWay.Skynet.Cloud.Storage.AliyunOss.Core
{
    /// <summary>
    ///     扩展方法
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        ///     根据错误类型返回错误异常
        /// </summary>
        /// <returns></returns>
        public static TRespose HandlerError<TRespose>(this TRespose response, string friendlyMessage = null) where TRespose : GenericResult
        {
            var code = (int) response.HttpStatusCode;
            if (code < 300 || code >= 600) return response;
            var message = response.ResponseMetadata["Message"];
            var requestId = response.ResponseMetadata["RequestId"];
            var traceId = response.ResponseMetadata["TraceId"];
            var resource = response.ResponseMetadata["Resource"];
            throw new StorageException(
                new StorageError {Code = code, Message = friendlyMessage ?? message, ProviderMessage = message},
                new Exception($"阿里云存储错误,详细信息:RequestId:{requestId},traceId:{traceId},resource:{resource}"));
        }
    }
}