using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using UWay.Skynet.Cloud.Data;
using Xunit;

namespace Skynet.Cloud.Uflow.Service.XUnitTest
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
