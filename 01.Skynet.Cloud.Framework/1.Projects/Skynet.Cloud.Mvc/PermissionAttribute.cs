using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Mvc
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public UrlAndButtonType UrlAndButtonType { get; }

        public PermissionAuthorizationRequirement(string url, ButtonType buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = url,
                ButtonType = (byte)buttonType,
                IsPage = isPage
            };
        }
        public PermissionAuthorizationRequirement(string url, byte buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = url,
                ButtonType = buttonType,
                IsPage = isPage
            };
        }
    }
    /// <summary>
    /// 权限过滤器
    /// </summary>
    [Authorize]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="buttonType">按钮类型</param>
        /// <param name="isPage">是否是页面</param>
        public PermissionAttribute(string url = default(string), ButtonType buttonType = ButtonType.View, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(url, buttonType, isPage) };
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="buttonType">按钮类型</param>
        /// <param name="isPage">是否是页面</param>
        public PermissionAttribute(string url, byte buttonType, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(url, buttonType, isPage) };
        }

        private class RequiresPermissionAttributeExecutor : Attribute, IAsyncResourceFilter
        {
            private PermissionAuthorizationRequirement _requiredPermissions;

            public RequiresPermissionAttributeExecutor(
                 PermissionAuthorizationRequirement requiredPermissions)
            {
                _requiredPermissions = requiredPermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                string menuUrl = _requiredPermissions.UrlAndButtonType.Url;
                
                //判断用户权限
                if (string.IsNullOrEmpty(menuUrl))
                {
                    //区域判断
                    var area = context.RouteData.Values["area"];
                    var controller = context.RouteData.Values["controller"];
                    var action = context.RouteData.Values["action"];
                    if (area == null)
                    {
                        menuUrl = "_" + controller + "_" + action;
                    }
                    else
                    {
                        menuUrl = "_" + area + "_" + controller + "_" + action; ;
                    }
                }
                menuUrl = menuUrl.Trim();
                var authorities = context.HttpContext.User.Claims.Where(p => p.Type.Equals( "authorities")).Select(o => o.Value).ToList();
                if(authorities.Any(p => p.Equals(menuUrl,StringComparison.InvariantCultureIgnoreCase)))
                {
                    await next();
                } else
                {
                    context.Result = new ContentResult()
                    {
                        Content = PermissionStatusCodes.Status2Unauthorized.ToString()
                    };

                    await context.Result.ExecuteResultAsync(context);
                }
            }
        }

    }
}
