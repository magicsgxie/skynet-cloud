using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using System;
using UWay.Skynet.Cloud.CodeGen.Service;
using UWay.Skynet.Cloud.CodeGen.Service.Interface;
using UWay.Skynet.Cloud.Data;
using Xunit;
using UWay.Skynet.Cloud.FileHandling;
using System.IO;
using UWay.Skynet.Cloud.CodeGen.Entity;

namespace UWay.Skynet.Cloud.CodeGen.Service.Test
{
    public class UnitTest1
    {
        private DbContextOption dbContextOption = new DbContextOption()
        {
            ConnectionString = "data source=192.168.15.117:1521/ora10;user id=test_cnoap;password=test_cnoap;Pooling=true;Max Pool Size=100;Min Pool Size=5;",
            Container = "CODEGEN",
            LogggerFactory = GetLoggerFactory(),
            Provider = DbProviderNames.Oracle_Managed_ODP
        };

        [Fact]
        public void Test1()
        {
            var appsettings = @"
{
    'spring': {
        'application': {
            'name': 'myName'
        },
    },
    'skynet': {
        'cloud': {
            'filepath': 'c:/doc',
        }
    }
}";

            var path = TestHelpers.CreateTempFile(appsettings);
            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(directory);

            configurationBuilder.AddJsonFile(fileName);
            var config = configurationBuilder.Build();

            DbConfiguration.Configure(dbContextOption);
            IFileHandler fileHandler = new FileHandler(config, GetLoggerFactory().CreateLogger<FileHandler>());
            ICodeGenService codeGenService = new CodeGenService(fileHandler, GetLoggerFactory().CreateLogger<CodeGenService>());
            GenConfig genConfig = new GenConfig()
            {
                Author = "magic.s.g.xie",
                Comments = "流程业务",
                Email = "xiesg@uway.cn",
                Namespace = "UWay.Skynet.Cloud",
                ModuleName = "Uflow",
                TablePrefix = "UF_"
            };
            codeGenService.GeneratorCode(genConfig);
        }

        public static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));
            
            serviceCollection.AddLogging(builder => builder.AddConsole((opts) =>
            {
                opts.DisableColors = true;
            }));
            serviceCollection.AddLogging(builder => builder.AddDebug());
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
    }
}
