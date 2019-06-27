using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Mvc.Result
{
    /// <summary>
    /// //TODO
    /// An action result which formats the given object as JSON.
    /// </summary>
    public class HttpResult : JsonResult
    {

        public HttpResult(object value) : base(value)
        {

        }

        public HttpResult(object value, JsonSerializerSettings serializerSettings) : base(value, serializerSettings)
        {
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return base.ExecuteResultAsync(context);
        }
    }

    /// <summary>
    /// for <see cref="HttpResult"/> Action Result
    /// </summary>
    public class HttpResultObject
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public HttpResultStatusCode? StatusCode { get; set; }
    }

    /// <summary>
    /// for <see cref="HttpResultObject"/> StatusCode
    /// </summary>
    public enum HttpResultStatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 0,
        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 2
    }
}
