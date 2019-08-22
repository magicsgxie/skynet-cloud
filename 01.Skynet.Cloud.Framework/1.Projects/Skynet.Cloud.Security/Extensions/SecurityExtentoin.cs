using Microsoft.AspNetCore.Authentication.JwtBearer;
using Steeltoe.Security.Authentication.CloudFoundry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace UWay.Skynet.Cloud.Security.Extensions
{
    public static class SecurityExtentoin
    {
        public static IServiceCollection AddJwtAuthoriation(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCloudFoundryJwtBearer(config);
            //services.AddAuthentication(options =>
            //{
            //   options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //});
            return services;
        }


    }
}
