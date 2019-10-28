using System;
using System.Linq;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UWay.Skynet.Cloud.Extensions;
using System.IO;
using UWay.Skynet.Cloud.Helpers;
using System.Text.RegularExpressions;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Nom.Service.Interface;
using UWay.Skynet.Cloud.Linq;
using System.Collections.Generic;
using UWay.Skynet.Cloud.Request;
using Skynet.Cloud.Noap;

namespace UWay.Skynet.Cloud.Nom.XUnitTest
{
    public class UnitTest1
    {



        [Fact]
        public void Test1()
        {

            //var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            //var dir = new DirectoryInfo(path);

            //var files = dir.GetFiles("*.service.*");
            //foreach (var item in files)
            //{
            //    Console.WriteLine(item.Name);
            //}
            //Regex regex = new Regex("*.Service");
            //var serviceAssemblys = RuntimeHelper.GetServicesAssembly();
            //var item = serviceAssemblys.FirstOrDefault().GetName(true).Name+".";
            //var interfaceAssembly = RuntimeHelper.GetAssembly(item);
            //Assert.NotNull(interfaceAssembly);
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
            var provider = GetServiceProvider();
            DbContextOption dbContextOption = new DbContextOption()
            {
                ConnectionString = "data source=192.168.15.117:1521/ora10;user id=test_cnoap;password=test_cnoap;Pooling=true;Max Pool Size=100;Min Pool Size=5;",
                Container = "1",
                LogggerFactory = provider.GetService<ILoggerFactory>(),
                MappingFile = "uway.cdma.normal.mapping.xml",
                Provider = DbProviderNames.Oracle_Managed_ODP
            };
            DbConfiguration.Configure(dbContextOption);
            INeService service = provider.GetService<INeService>();

            CompositeFilterDescriptor filter = new CompositeFilterDescriptor();
            filter.LogicalOperator = FilterCompositionLogicalOperator.Or;
            FilterDescriptor filterDescriptor = new FilterDescriptor()
            {
                Member = "NeName",
                Operator = FilterOperator.Contains,
                Value = "¹ÄÂ¥"
            };

            FilterDescriptor filterDescriptor2 = new FilterDescriptor()
            {
                Member = "NeName",
                Operator = FilterOperator.Contains,
                Value = "ÖÐÉ½"
            };
            filter.FilterDescriptors.Add(filterDescriptor);
            filter.FilterDescriptors.Add(filterDescriptor2);
            var bts = service.GetNeBtsByCondition(NetType.CDMA, new List<IFilterDescriptor>() { filter });
            Assert.NotNull(bts);
            Assert.True(bts.Count() > 0);
            DataSourceRequest request = new DataSourceRequest();
            request.Page = 1;
            request.PageSize = 10;
            request.Filters = new List<IFilterDescriptor>() { filter };
            request.Sorts = new List<SortDescriptor>(){ new SortDescriptor() { Member = "NeName", SortDirection = System.ComponentModel.ListSortDirection.Ascending }};
            var page = service.BtsPage(NetType.CDMA, request);
            Assert.True(page.Total > 0);

            var btsResult = service.GetNeBtsPageByCondition(NetType.CDMA, request);
            Assert.True(btsResult.Total > 0);

        }

        public static ServiceProvider GetServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var dir = new DirectoryInfo(path);
            //var files = dir.GetFiles("*.service.*");
            //foreach(var item )

            //serviceCollection.AddScopedAssembly("Skynet.Cloud.Nom.Service.Interface", "Skynet.Cloud.Nom.Service");
            serviceCollection.AddScopServiceAssmbly();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));

            serviceCollection.AddLogging(builder => builder.AddConsole((opts) =>
            {
                opts.DisableColors = true;
            }));
            serviceCollection.AddLogging(builder => builder.AddDebug());
            return serviceCollection.BuildServiceProvider();
        }
    }
}
