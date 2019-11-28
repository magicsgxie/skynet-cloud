
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using UWay.Skynet.Cloud.Dicovery.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Mvc;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Skynet.Cloud.Upms.Test.Service;
using Skynet.Cloud.Upms.Test.Service.Interface;

namespace Skynet.Cloud.Cloud.CloudFoundryDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //JwtSecurityTokenHandler
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDiscoveryClient(Configuration);
            //services.AddMySwagger();
            services.UseMysql(Configuration);
            services.AddCustomAuthentication(Configuration);
            //services.AddHttpClient();
            services.AddSingleton<IRemoteTest, RemoteTest>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCustomMvc();
            services.AddSwaggerDocumentation(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation(Configuration);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseDiscoveryClient();
            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
}
