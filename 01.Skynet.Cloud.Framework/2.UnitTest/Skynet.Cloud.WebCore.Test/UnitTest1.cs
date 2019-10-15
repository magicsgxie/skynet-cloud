using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Mvc;
using System;
using System.IO;
using System.Linq;
using Xunit;
using UWay.Skynet.Cloud.Nacos;
using Steeltoe.Discovery.Nacos.Registry;
using Steeltoe.Common.HealthChecks;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
    },
    'p-sso': {
      
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
        [Fact]
        public void Test2()
        {
            //var rsa = RSA.Create();
            //rsa.KeySize = 2048;
            //rsa.ImportParameters(new RSAParameters
            //{
            //    Modulus = Convert.FromBase64String(""),
            //    Exponent = Convert.FromBase64String("AQAB")
            //});
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(1),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = "made by uway",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("schealth365skynetcloud"))
            };

            // Verify token
            IdentityModelEventSource.ShowPII = true;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsaWNlbnNlIjoibWFkZSBieSB1d2F5IiwidXNlcl9uYW1lIjoiYWRtaW4iLCJzY29wZSI6WyJzZXJ2ZXIiXSwiZXhwIjoxNTcxMDg2NTkzLCJhdXRob3JpdGllcyI6WyJzeXNfY2xpZW50X2VkaXQiLCJzeXNfZGljdF9hZGQiLCJST0xFXzEiLCJzeXNfZGVwdF9kZWwiLCJzeXNfdXNlcl9kZWwiLCJzeXNfbWVudV9hZGQiLCJzeXNfcm9sZV9hZGQiLCJzeXNfY2xpZW50X2FkZCIsInN5c19kZXB0X2VkaXQiLCJzeXNfdXNlcl9lZGl0Iiwic3lzX2RpY3RfZWRpdCIsInN5c19tZW51X2RlbCIsInN5c19tZW51X2VkaXQiLCJzeXNfcm9sZV9lZGl0Iiwic3lzX3JvbGVfcGVybSIsInN5c191c2VyX2FkZCIsInN5c190b2tlbl9kZWwiLCJzeXNfZGVwdF9hZGQiLCJzeXNfbG9nX2RlbCIsInN5c19yb2xlX2RlbCIsInN5c19jbGllbnRfZGVsIiwic3lzX2RpY3RfZGVsIl0sImp0aSI6Ijg5MTNhNzBlLTY3ODQtNDdhZC05OTYwLTdhZDk5NTMzNjliYiIsImNsaWVudF9pZCI6InBpZyJ9.uDfO8Ulx6HAznNLcHWeZ227iOLUPx9I7IuiZ1Saw9Ts";
            // exception is thrown on the next line:
            var user = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var values = user.Claims.Select(c => c.Value).ToList();
            var types = user.Claims.Select(c => c.Type).ToList();
            //ClaimTypes.Name
            //user.Identity
            var userName = user.Claims.SingleOrDefault(c => c.Type.Equals("user_name", StringComparison.InvariantCultureIgnoreCase));
            var propInfos = user.Claims.Select(c => c.Properties).ToList();
            //ClaimTypes.Name
            Console.Out.WriteLine("name" + userName);
            //foreach (var role in user.Claims.Where(c => c.Type == ClaimTypes.Role))
            //{
            //    Console.WriteLine("Role: " + role.Value);
            //}
        }
    }
}
