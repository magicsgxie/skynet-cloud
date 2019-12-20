using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.ApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //WebHost.CreateDefaultBuilder(args).UseNLog()
            WebHost.CreateDefaultBuilder(args).ConfigureLogging((hostingContext, logging) =>
            {
                //从appsettings.json中获取Logging的配置
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //添加控制台输出
                logging.AddConsole();
                //添加调试输出
                logging.AddDebug();
            })
            .UseStartup<Startup>();
    }
}
