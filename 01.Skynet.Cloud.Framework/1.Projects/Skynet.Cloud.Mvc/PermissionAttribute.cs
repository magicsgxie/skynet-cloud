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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strurl"></param>
        /// <param name="buttonType"></param>
        /// <param name="isPage"></param>
        public PermissionAuthorizationRequirement(string strurl, ButtonType buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = strurl,
                ButtonType = (byte)buttonType,
                IsPage = isPage
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="buttonType"></param>
        /// <param name="isPage"></param>
        public PermissionAuthorizationRequirement(string strurl, byte buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = strurl,
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
        public PermissionAttribute(string strurl = default(string), ButtonType buttonType = ButtonType.View, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(strurl, buttonType, isPage) };
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="buttonType">按钮类型</param>
        /// <param name="isPage">是否是页面</param>
        public PermissionAttribute(string strurl, byte buttonType, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(strurl, buttonType, isPage) };
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
                //var authorities = context.HttpContext.User.Claims.Where(p => p.Type.Equals( "authorities")).Select(o => o.Value).ToList();
                if(context.HttpContext.User.HasPermission(menuUrl))
                {
                    await next().ConfigureAwait(false);
                } else
                {
                    string innermsg = PermissionStatusCodes.Status2Unauthorized+"";
                    context.Result = new ContentResult()
                    {
                        Content = innermsg
                    };

                    await context.Result.ExecuteResultAsync(context).ConfigureAwait(false);
                }
            }
        }

    }
}
