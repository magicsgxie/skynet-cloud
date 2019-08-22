using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud;
namespace UWay.Skynet.Cloud.Security.Filters
{
    public class InnerFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            string header = context.HttpContext.Request.Headers[SecurityContants.FROM];
            if (!header.Equals(SecurityContants.FROM_IN, StringComparison.InvariantCultureIgnoreCase)) {
                
            } else
            {

            }
            
        }
    }
}
