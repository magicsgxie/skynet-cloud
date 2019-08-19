using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Mvc;
using System;
using System.IO;
using Xunit;
using UWay.Skynet.Cloud.Nacos;
using Steeltoe.Discovery.Nacos.Registry;
using Steeltoe.Common.HealthChecks;
using System.Net.Http;

namespace UWay.Skynet.Cloud.Mvc.Test
{
    public class UnitTest1
    {
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
    'nacos': {
        'discovery': {
            'host': 'foo.bar',
            'register': false,
            'deregister': false,
            'instanceid': 'instanceid',
            'port': 1234
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

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddDiscoveryClient(config);
            var provider = services.BuildServiceProvider();
            var service1 = provider.GetService<INacosNamingClient>();
            Assert.NotNull(service1);
            var options = provider.GetService<NacosDiscoveryOptions>();
            Assert.NotNull(options);
            var httpClient = provider.GetService<IHttpClientFactory>();
            Assert.NotNull(httpClient);
            var service = provider.GetService<IDiscoveryClient>();
            Assert.NotNull(service);
       
            
            var service3 = provider.GetService<INacosServiceRegistry>();
            Assert.NotNull(service3);
            var service4 = provider.GetService<INacosRegistration>();
            Assert.NotNull(service4);
            var service5 = provider.GetService<INacosServiceRegistrar>();
            Assert.NotNull(service5);
            var service6 = provider.GetService<IHealthContributor>();
            Assert.NotNull(service6);
        }
    }
}
