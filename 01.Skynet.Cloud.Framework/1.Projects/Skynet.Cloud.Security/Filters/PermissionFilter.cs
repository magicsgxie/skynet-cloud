using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Security.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionFilter : Attribute, IAuthorizationFilter
    {
        public PermissionFilter(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var routeInfo = context.ActionDescriptor.AttributeRouteInfo;
            //context.HttpContext.User.Identity;
            var name = routeInfo.Name;
            var route = context.ActionDescriptor.FilterDescriptors;
            var item = context.HttpContext.Request.Headers[SecurityContants.FROM];

            //if(item.)
            throw new NotImplementedException();
        }
    }
}
