using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc.Extensions
{
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 判断是否是DEBUG模式
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
