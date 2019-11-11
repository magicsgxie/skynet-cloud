using System;
using System.Data;
using System.Threading;
using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Data.Test.TestModels;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.IoC;

namespace UWay.Skynet.Cloud.Data.Test
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void TestGetDataTableForOracle()
        {
            string username = "xiesg:123456";
            var base64 = username.ToBase64();

        }



        //public IServiceProvider BuildServiceForOracle()
        //{
        //    IServiceCollection services = new ServiceCollection();

        //    //services.Configure<CodeGenerateOption>(options =>
        //    //{
        //    //    options.OutputPath = "F:\\Test\\Oracle";
        //    //    options.ModelsNamespace = "Zxw.Framework.UnitTest.Models";
        //    //    options.IRepositoriesNamespace = "Zxw.Framework.UnitTest.IRepositories";
        //    //    options.RepositoriesNamespace = "Zxw.Framework.UnitTest.Repositories";
        //    //    options.ControllersNamespace = "Zxw.Framework.UnitTest.Controllers";
        //    //});
        //    //在这里注册EF上下文
        //    services = RegisterOracleDbContext(services);
        //    //services.AddOptions();
        //    return AspectCoreContainer.BuildServiceProvider(services); //接入AspectCore.Injector
        //}

        ///// <summary>
        ///// 注册Oracle上下文
        ///// </summary>
        ///// <param name="services"></param>
        ///// <returns></returns>
        //public IServiceCollection RegisterOracleDbContext(IServiceCollection services)
        //{
        //    services.Configure<DbContextOption>(options =>
        //    {
        //        options.ConnectionString = "DATA SOURCE=127.0.0.1:1234/testdb;USER ID=test;PASSWORD=123456;PERSIST SECURITY INFO=True;Pooling=True;Max Pool Size=100;Incr Pool Size=2;";
        //    });
        //    services.AddScoped<IDbContextCore, OracleDbContext>().AddScoped<IUserRepository, TestRepository>(); //注入EF上下文
        //    return services;
        //}
    }
}