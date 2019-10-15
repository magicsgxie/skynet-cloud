using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.Oracle;
using Steeltoe.CloudFoundry.Connector.Services;

using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.CloudFoundry.Connector;
using System;
using Xunit;

namespace Skynet.Cloud.ConnectorBase.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IServiceCollection services = new ServiceCollection();
            //Environment.SetEnvironmentVariable("VCAP_APPLICATION", TestHelpers.VCAP_APPLICATION);
            Environment.SetEnvironmentVariable("VCAP_SERVICES", OracleTestHelper.SingleServerVCAP);

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddCloudFoundry();
            var config = builder.Build();
            var connection = GetSSo(config);
        }

        private static string GetConnection(IConfiguration config, string serviceName = null)
        {
            OracleServiceInfo info = string.IsNullOrEmpty(serviceName)
                ? config.GetSingletonServiceInfo<OracleServiceInfo>()
                : config.GetRequiredServiceInfo<OracleServiceInfo>(serviceName);

            OracleProviderConnectorOptions mySqlConfig = new OracleProviderConnectorOptions(config);

            OracleProviderConnectorFactory factory = new OracleProviderConnectorFactory(info, mySqlConfig, null);

            return factory.CreateConnectionString();
        }

        private static string GetSSo(IConfiguration config, string serviceName = null)
        {
            SsoServiceInfo info = string.IsNullOrEmpty(serviceName)
                ? config.GetSingletonServiceInfo<SsoServiceInfo>()
                : config.GetRequiredServiceInfo<SsoServiceInfo>(serviceName);

            

            return info.ToString();
        }
    }
}
