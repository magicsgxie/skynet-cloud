

namespace UWay.Skynet.Cloud.Nacos
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using UWay.Skynet.Cloud.Nacos.Exceptions;
    using UWay.Skynet.Cloud.Nacos.Utilities;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Threading;
    using System.Net.Http.Headers;

    public class NacosNamingClient : INacosNamingClient
    {



        internal readonly ILogger _logger;
        protected readonly NacosDiscoveryOptions _options;
        internal readonly IHttpClientFactory _clientFactory;
        //internal readonly ILocalConfigInfoProcessor _processor;
        //internal readonly List<Listener> listeners;
        internal readonly ServerAddressManager _serverAddressManager;

        

        //private class NacosClientConfigurationContainer
        //{

        //    internal readonly bool skipClientDispose;
        //    internal readonly HttpClient HttpClient;

        //    internal readonly HttpClientHandler HttpHandler;

        //    public readonly NacosClientConfiguration Config;

        //    public NacosClientConfigurationContainer()
        //    {
        //        Config = new NacosClientConfiguration();
        //        HttpHandler = new HttpClientHandler();
        //        HttpClient = new HttpClient(HttpHandler);
        //        HttpClient.Timeout = TimeSpan.FromMinutes(15);
        //        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpClient.DefaultRequestHeaders.Add("Keep-Alive", "true");
        //    }

        //    #region Old style config handling
        //    public NacosClientConfigurationContainer(NacosClientConfiguration config, HttpClient client)
        //    {
        //        skipClientDispose = true;
        //        Config = config;
        //        HttpClient = client;
        //    }

        //    public NacosClientConfigurationContainer(NacosClientConfiguration config)
        //    {
        //        Config = config;
        //        HttpHandler = new HttpClientHandler();
        //        HttpClient = new HttpClient(HttpHandler);
        //        HttpClient.Timeout = TimeSpan.FromMinutes(15);
        //        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpClient.DefaultRequestHeaders.Add("Keep-Alive", "true");
        //    }
        //    #endregion
        //}

        //public NacosClient() : this(null)
        //{

        //}

        //public NacosClient(Action<NacosClientConfiguration> configOverride) : this(configOverride, null)
        //{

        //}

        //public NacosClient(Action<NacosClientConfiguration> configOverride, Action<HttpClient> clientOverride) : this(configOverride, clientOverride, null)
        //{

        //}

        //public NacosClient(Action<NacosClientConfiguration> configOverride, Action<HttpClient> clientOverride, Action<HttpClientHandler> handlerOverride)
        //{
        //    var ctr = new NacosClientConfigurationContainer();
        //    configOverride?.Invoke(ctr.Config);
        //    ApplyConfig(ctr.Config, ctr.HttpHandler, ctr.HttpClient);
        //    handlerOverride?.Invoke(ctr.HttpHandler);
        //    clientOverride?.Invoke(ctr.HttpClient);
        //    ConfigContainer = ctr;
        //}

        //void ApplyConfig(NacosClientConfiguration config, HttpClientHandler handler, HttpClient client)
        //{

        //}



        //public NacosNamingClient(
        //    ILoggerFactory loggerFactory
        //    , NacosDiscoveryOptions optionAccs
        //    , IHttpClientFactory clientFactory
        //    )
        //{

        //}


        public NacosNamingClient(
           ILoggerFactory loggerFactory
           , IOptionsMonitor<NacosDiscoveryOptions> optionAccs
           , IHttpClientFactory clientFactory
           ) 
        {
            this._logger = loggerFactory.CreateLogger<NacosNamingClient>();
            this._options = optionAccs.CurrentValue;
            this._clientFactory = clientFactory;

            //this.listeners = new List<Listener>();
            this._serverAddressManager = new ServerAddressManager(_options);
        }



        private string GetBaseUrl()
        {
            var hostAndPort = _serverAddressManager.GetCurrentServer();
            return this._options.IsSecure ? $"https://{hostAndPort}" : $"http://{hostAndPort}";
        }

        #region Instance
        public async Task<bool> RegisterInstanceAsync(RegisterInstanceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Post, $"{GetBaseUrl()}/nacos/v1/ns/instance", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.RegisterInstance] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.RegisterInstance] Register an instance to service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Register an instance to service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> RemoveInstanceAsync(RemoveInstanceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Delete, $"{GetBaseUrl()}/nacos/v1/ns/instance", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.RemoveInstance] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.RemoveInstance] Delete instance from service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Delete instance from service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> ModifyInstanceAsync(ModifyInstanceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Put, $"{GetBaseUrl()}/nacos/v1/ns/instance", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.ModifyInstance] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.ModifyInstance] Modify an instance of service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Modify an instance of service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<ListInstancesResult> ListInstancesAsync(ListInstancesRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/instance/list", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<ListInstancesResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.ListInstances] Query instance list of service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query instance list of service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        

        public async Task<GetInstanceResult> GetInstanceAsync(GetInstanceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/instance", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<GetInstanceResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.GetInstance] Query instance details of service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query instance details of service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> SendHeartbeatAsync(SendHeartbeatRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Put, $"{GetBaseUrl()}/nacos/v1/ns/instance/beat", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.SendHeartbeat] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.SendHeartbeat] Send instance beat failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Send instance beat failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> ModifyInstanceHealthStatusAsync(ModifyInstanceHealthStatusRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Put, $"{GetBaseUrl()}/nacos/v1/ns/health/instance", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.ModifyInstanceHealthStatus] server return {result} ");
                        return false;
                    }
                case System.Net.HttpStatusCode.BadRequest:
                    _logger.LogWarning($"[client.ModifyInstanceHealthStatus] health check is still working {responseMessage.StatusCode.ToString()}");
                    return false;
                default:
                    _logger.LogWarning($"[client.ModifyInstanceHealthStatus] Update instance health status failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Update instance health status failed {responseMessage.StatusCode.ToString()}");
            }
        }
        #endregion

        #region Metrics
        public async Task<GetMetricsResult> GetMetricsAsync()
        {
            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/operator/metrics", timeOut: _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<GetMetricsResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.GetMetrics] Query system metrics failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query system metrics failed {responseMessage.StatusCode.ToString()}");
            }
        }
        #endregion

        #region Services
        public async Task<bool> CreateServiceAsync(CreateServiceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Post, $"{GetBaseUrl()}/nacos/v1/ns/service", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.CreateService] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.CreateService] Create service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Create service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> RemoveServiceAsync(RemoveServiceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Delete, $"{GetBaseUrl()}/nacos/v1/ns/service", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.RemoveService] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.RemoveService] Delete a service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Delete a service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> ModifyServiceAsync(ModifyServiceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Put, $"{GetBaseUrl()}/nacos/v1/ns/service", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.ModifyService] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.ModifyService] Update a service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Update a service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<GetServiceResult> GetServiceAsync(GetServiceRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/service", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<GetServiceResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.GetService] Query a service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query a service failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<ListServicesResult> ListServicesAsync(ListServicesRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/service/list", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<ListServicesResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.ListServices] Query service list failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query service list failed {responseMessage.StatusCode.ToString()}");
            }
        }
        #endregion

        #region Switches
        public async Task<GetSwitchesResult> GetSwitchesAsync()
        {
            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/operator/switches", timeOut: _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<GetSwitchesResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.GetSwitches] Query system switches failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query system switches failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<bool> ModifySwitchesAsync(ModifySwitchesRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Put, $"{GetBaseUrl()}/nacos/v1/ns/operator/switches", request.ToQueryString());

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    if (result.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"[client.ModifySwitches] server return {result} ");
                        return false;
                    }
                default:
                    _logger.LogWarning($"[client.ModifySwitches] Update system switch failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Update system switch failed {responseMessage.StatusCode.ToString()}");
            }
        }

        #endregion

        #region Cluster
        public async Task<ListClusterServersResult> ListClusterServersAsync(ListClusterServersRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/operator/servers", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<ListClusterServersResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.ListClusterServers] Query server list failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query server list failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<GetCurrentClusterLeaderResult> GetCurrentClusterLeaderAsync()
        {
            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/raft/leader");

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var leader = result.GetPropValue("leader");
                    var obj = leader.JsonDeserialize<GetCurrentClusterLeaderResult>();
                    return obj;
                default:
                    _logger.LogWarning($"[client.GetCurrentClusterLeader] query the leader of current cluster failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"query the leader of current cluster failed {responseMessage.StatusCode.ToString()}");
            }
        }

        public async Task<List<string>> ListServiceAsync()
        {
            ListServicesRequest request = new ListServicesRequest { PageNo = 1, PageSize = int.MaxValue };

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{GetBaseUrl()}/nacos/v1/ns/service/list", request.ToQueryString(), _options.TimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    var obj = result.JsonDeserialize<ListServicesResult>();
                    return obj.Doms;
                default:
                    _logger.LogWarning($"[client.GetService] Query a service failed {responseMessage.StatusCode.ToString()}");
                    throw new NacosException((int)responseMessage.StatusCode, $"Query a service failed {responseMessage.StatusCode.ToString()}");
            }
        }
        #endregion
    }
}
