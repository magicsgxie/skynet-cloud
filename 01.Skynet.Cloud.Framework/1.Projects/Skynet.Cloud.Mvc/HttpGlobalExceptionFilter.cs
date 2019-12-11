namespace UWay.Skynet.Cloud.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Filters;
    using NLog;

    /// <summary>
    /// 全局异常过滤.
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); // 获得日志实;

        /// <summary>
        /// 异常处理.
        /// </summary>
        /// <param name="context">context.</param>
        public void OnException(ExceptionContext context)
        {
            Guard.NotNull(context, "context");
            if (context.ExceptionHandled)
            {
                return;
            }

            var excep = context.Exception;
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            string errorMsg = $"在请求controller[{controllerName}] 的 action[{actionName}] 时产生异常[{excep.Message}]";

            this.nlog.Log(LogLevel.Error, context.Exception, errorMsg);
            context.ExceptionHandled = true; // Tag it is handled.
        }
    }
}
