namespace UWay.Skynet.Cloud.Mvc
{
    using Microsoft.AspNetCore.Http;
    using System;

    internal static class Helper
    {
        /// <summary>
        ///     获取浏览器信息.
        /// </summary>
        /// <param name="httpContext">HttpContext.</param>
        /// <returns>string.</returns>
        internal static string GetBrowserInfo(this HttpContext httpContext)
        {
            return string.Empty;
        }

        /// <summary>
        ///     获取客户端IP信息.
        /// </summary>
        /// <param name="httpContext">HttpContext.</param>
        /// <returns>string.</returns>
        internal static string GetClientIpAddress(this HttpContext httpContext)
        {
            //var clientIp = httpContext.Request.Fr["HTTP_X_FORWARDED_FOR"] ??
            //               httpContext.Request.ServerVariables["REMOTE_ADDR"];

            //try
            //{
            //    foreach (var hostAddress in Dns.GetHostAddresses(clientIp))
            //        if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
            //            return hostAddress.ToString();
            //    foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            //        if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
            //            return hostAddress.ToString();
            //}
            //catch (Exception)
            //{
            //}

            return httpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        ///     获取电脑名称.
        /// </summary>
        /// <param name="httpContext">httpContext.</param>
        /// <returns></returns>
        internal static string GetComputerName(this HttpContext httpContext)
        {
            //if (!httpContext.Request.IsLocal)
            //    return null;

            //try
            //{
            //    var clientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
            //                   HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //    return Dns.GetHostEntry(IPAddress.Parse(clientIp)).HostName;
            //}
            //catch
            //{
            //    return null;
            //}
            //httpContext.Request.Headers.
            return string.Empty;
        }
    }
}
