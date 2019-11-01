using Microsoft.Extensions.Logging;
using Skynet.Cloud.Upms.Test.Entity;
using Skynet.Cloud.Upms.Test.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Discovery.Abstract;
using Steeltoe.Common.Discovery;
using UWay.Skynet.Cloud.Request;
using Microsoft.AspNetCore.Http;

namespace Skynet.Cloud.Upms.Test.Service
{
    public class RemoteTest : BaseDiscoveryService, IRemoteTest
    {



        public RemoteTest(IDiscoveryClient client, ILoggerFactory loggerFactory, IHttpContextAccessor context) : base(client, loggerFactory.CreateLogger<RemoteTest>(), context)
        {

        }

        private const string USER_URL = "http://skynet-cloud-upms/user";

        public async Task<R<User>> GetUser(long id)
        {
            var client = await GetClientAsync();
            return await DoRequest<R<User>>(client,GetRequest(HttpMethod.Get, $"{USER_URL}/{id}"));

        }
    }
}
