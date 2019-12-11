namespace UWay.Skynet.Cloud.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// 权限过滤器.
    /// </summary>
    [Authorize]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAttribute"/> class.
        /// 构造器.
        /// </summary>
        /// <param name="strurl">地址.</param>
        /// <param name="buttonType">按钮类型.</param>
        /// <param name="isPage">是否是页面.</param>
        public PermissionAttribute(string strurl = default(string), ButtonType buttonType = ButtonType.View, bool isPage = true)
            : base(typeof(RequiresPermissionAttributeExecutor))
        {
            this.Arguments = new object[] { new PermissionAuthorizationRequirement(strurl, buttonType, isPage) };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAttribute"/> class.
        /// 构造器.
        /// </summary>
        /// <param name="strurl">地址.</param>
        /// <param name="buttonType">按钮类型.</param>
        /// <param name="isPage">是否是页面.</param>
        public PermissionAttribute(string strurl, byte buttonType, bool isPage = true)
            : base(typeof(RequiresPermissionAttributeExecutor))
        {
            this.Arguments = new object[] { new PermissionAuthorizationRequirement(strurl, buttonType, isPage) };
        }

        private class RequiresPermissionAttributeExecutor : Attribute, IAsyncResourceFilter
        {
            private const string V = "";
            private readonly PermissionAuthorizationRequirement requiredPermissions;

            public RequiresPermissionAttributeExecutor(
                 PermissionAuthorizationRequirement requiredPermissions)
            {
                this.requiredPermissions = requiredPermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                string menuUrl = this.requiredPermissions.UrlAndButtonType.Url;

                // 判断用户权限
                if (string.IsNullOrEmpty(menuUrl))
                {
                    // 区域判断
                    var area = context.RouteData.Values["area"];
                    var controller = context.RouteData.Values["controller"];
                    var action = context.RouteData.Values["action"];
                    if (area == null)
                    {
                        menuUrl = "_" + controller + "_" + action;
                    }
                    else
                    {
                        menuUrl = "_" + area + "_" + controller + "_" + action;
                    }
                }

                menuUrl = menuUrl.Trim();

                if (context.HttpContext.User.HasPermission(menuUrl))
                {
                    await next().ConfigureAwait(false);
                }
                else
                {
                    string innermsg = PermissionStatusCodes.Status2Unauthorized + V;
                    context.Result = new ContentResult()
                    {
                        Content = innermsg,
                    };

                    await context.Result.ExecuteResultAsync(context).ConfigureAwait(false);
                }
            }
        }
    }
}
