using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Helpers;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.IoC;
using UWay.Skynet.Cloud.Mvc;
using Microsoft.AspNetCore.Http;
using Skynet.Cloud.Upms.Test.Service.Interface;
using Skynet.Cloud.Upms.Test.Service;

namespace UWay.Skynet.Cloud.ApiDemo
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
            //使用Mysql数据库连接获取主要数据库连接信息
            services.UseMysql(Configuration);
            var dbContext = UnitOfWork.Get("upms");
            //设置授权模式
            services.AddCustomAuthentication(Configuration);

            //设置MVC版本
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //设置MVC配置信息
            services.AddCustomMvc();

            //初始化SwaggerUI文档
            services.AddSwaggerDocumentation(Configuration);
            //IOC容器
            services.InitIoC();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //
                app.UseDeveloperExceptionPage();
                //初始化SwaggerUI文档
                app.UseSwaggerDocumentation(Configuration);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //使用授权模式
            app.UseAuthentication();
            //使用MVC API
            app.UseMvc();

        }







    }
}
