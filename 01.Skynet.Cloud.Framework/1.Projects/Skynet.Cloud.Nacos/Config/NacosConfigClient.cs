using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos.Exceptions;
using UWay.Skynet.Cloud.Nacos.Utilities;
using System.Threading;
using System.IO;
using YamlDotNet.Serialization;


namespace UWay.Skynet.Cloud.Nacos.Config
{
    public class NacosConfigClient: INacosConfigClient
    {
        private readonly ILogger _logger;
        private readonly NacosClientConfiguration _options;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILocalConfigInfoProcessor _processor;
        private readonly List<Listener> listeners;
        private readonly ServerAddressManager _serverAddressManager;

        public NacosConfigClient(
            ILoggerFactory loggerFactory
            , IOptionsMonitor<NacosClientConfiguration> optionAccs
            , IHttpClientFactory clientFactory
            , ILocalConfigInfoProcessor processor): this(loggerFactory, optionAccs.CurrentValue, clientFactory, processor)
        {
            
        }

        public NacosConfigClient(
                ILoggerFactory loggerFactory
                , NacosClientConfiguration optionAccs
                , IHttpClientFactory clientFactory
                , ILocalConfigInfoProcessor processor)
        {
            this._logger = loggerFactory.CreateLogger<NacosConfigClient>();
            this._options = optionAccs;
            this._clientFactory = clientFactory;
            this._processor = processor;

            this.listeners = new List<Listener>();
            this._serverAddressManager = new ServerAddressManager(_options);
        }

        public async Task<string> GetConfigAsync(GetConfigRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.Tenant = string.IsNullOrWhiteSpace(request.Tenant) ? _options.Namespace : request.Tenant;
            request.Group = string.IsNullOrWhiteSpace(request.Group) ? ConstValue.DefaultGroup : request.Group;

            request.CheckParam();

            // read from local cache at first
            var config = await _processor.GetFailoverAsync(request.DataId, request.Group, request.Tenant);

            if (!string.IsNullOrWhiteSpace(config))
            {
                _logger.LogInformation($"[get-config] get failover ok, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, config ={config}");
                return config;
            }

            try
            {
                config = await DoGetConfigAsync(request);
            }
            catch (NacosException ex)
            {
                if (ConstValue.NO_RIGHT == ex.ErrorCode)
                {
                    throw;
                }

                _logger.LogWarning($"[get-config] get from server error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, msg={ex.Message}");
            }

            if (!string.IsNullOrWhiteSpace(config))
            {
                _logger.LogInformation($"[get-config] content from server {config}, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}");
                await _processor.SaveSnapshotAsync(request.DataId, request.Group, request.Tenant, config);
                return config;
            }

            config = await _processor.GetSnapshotAync(request.DataId, request.Group, request.Tenant);

            return config;
        }

        private string GetBaseUrl()
        {
            var hostAndPort = _serverAddressManager.GetCurrentServer();
            return this._options.IsSecure ? $"https://{hostAndPort}" : $"http://{hostAndPort}";
        }


        private IList<string> GetBaseUrls()
        {
            var list = _serverAddressManager.GetServers();
            return list.Select(hostAndPort => this._options.IsSecure ? $"https://{hostAndPort}" : $"http://{hostAndPort}").ToList();
            //var hostAndPort = _serverAddressManager.GetCurrentServer();
            //return this._options.IsSecure ? $"https://{hostAndPort}" : $"http://{hostAndPort}";
        }

        private string ConvertJsonString(Stream str)
        {
            //return null;
            //格式化json字符串
            var deserializer = new Deserializer();
            using (TextReader r = new StreamReader(str))
            {
                var yamlObject = deserializer.Deserialize(r);
                return Newtonsoft.Json.JsonConvert.SerializeObject(yamlObject);
            }
            //{
            //    using (JsonTextReader jtr = new JsonTextReader(tr))
            //    {
            //        object obj = serializer.Deserialize(jtr);
            //        if (obj != null)
            //        {
            //            StringWriter textWriter = new StringWriter();
            //            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            //            {
            //                Formatting = Formatting.Indented,
            //                Indentation = 4,
            //                IndentChar = ' '
            //            };
            //            serializer.Serialize(jsonWriter, obj);
            //            return textWriter.ToString();
            //        }
            //        return string.Empty;
            //    }
            //}
        }

        private async Task<string> DoGetConfigAsync(GetConfigRequest request)
        {
            var item = 0;
            var urls = GetBaseUrls();
            foreach(var url in urls)
            {
                var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Get, $"{url}/nacos/v1/cs/configs", request.ToQueryString(), _options.DefaultTimeOut);

                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        //var stream = await responseMessage.Content.ReadAsStreamAsync();
                        //var result = await responseMessage.Content.ReadAsStringAsync();
                        return await responseMessage.Content.ReadAsStringAsync();
                    //return ConvertJsonString(stream);
                    case System.Net.HttpStatusCode.NotFound:
                        await _processor.SaveSnapshotAsync(request.DataId, request.Group, request.Tenant, null);
                        return null;
                    case System.Net.HttpStatusCode.Forbidden:
                        throw new NacosException(ConstValue.NO_RIGHT, $"Insufficient privilege.");
                    default:
                        if(item == urls.Count -1)
                        {
                            throw new NacosException((int)responseMessage.StatusCode, responseMessage.StatusCode.ToString());
                        }
                        break;
                        //
                }
                item++;
            }
            return null;
           
        }

        public async Task<bool> PublishConfigAsync(PublishConfigRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.Tenant = string.IsNullOrWhiteSpace(request.Tenant) ? _options.Namespace : request.Tenant;
            request.Group = string.IsNullOrWhiteSpace(request.Group) ? ConstValue.DefaultGroup : request.Group;

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Post, $"{GetBaseUrl()}/nacos/v1/cs/configs", request.ToQueryString(), _options.DefaultTimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    _logger.LogInformation($"[publish-single] ok, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, config={request.Content}");
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    return result.Equals("true", StringComparison.OrdinalIgnoreCase);
                case System.Net.HttpStatusCode.Forbidden:
                    _logger.LogWarning($"[publish-single] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                    throw new NacosException(ConstValue.NO_RIGHT, $"Insufficient privilege.");
                default:
                    _logger.LogWarning($"[publish-single] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                    return false;
            }
        }

        public async Task<bool> RemoveConfigAsync(RemoveConfigRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            request.Tenant = string.IsNullOrWhiteSpace(request.Tenant) ? _options.Namespace : request.Tenant;
            request.Group = string.IsNullOrWhiteSpace(request.Group) ? ConstValue.DefaultGroup : request.Group;

            request.CheckParam();

            var responseMessage = await _clientFactory.DoRequestAsync(HttpMethod.Delete, $"{GetBaseUrl()}/nacos/v1/cs/configs", request.ToQueryString(), _options.DefaultTimeOut);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    _logger.LogInformation($"[remove] ok, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}");
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    return result.Equals("true", StringComparison.OrdinalIgnoreCase);
                case System.Net.HttpStatusCode.Forbidden:
                    _logger.LogWarning($"[remove] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                    throw new NacosException(ConstValue.NO_RIGHT, $"Insufficient privilege.");
                default:
                    _logger.LogWarning($"[remove] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                    return false;
            }
        }

        public Task AddListenerAsync(AddListenerRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            if (string.IsNullOrWhiteSpace(request.Tenant)) request.Tenant = _options.Namespace;
            if (string.IsNullOrWhiteSpace(request.Group)) request.Group = ConstValue.DefaultGroup;

            request.CheckParam();

            var name = BuildName(request.Tenant, request.Group, request.DataId);

            if (listeners.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning($"[add-listener] error, {name} has been added.");
                return Task.CompletedTask;
            }

            Timer timer = new Timer(async x =>
            {
                await PollingAsync(x);

            }, request, 0, _options.Interval);

            listeners.Add(new Listener(name, timer));

            return Task.CompletedTask;
        }

        public Task RemoveListenerAsync(RemoveListenerRequest request)
        {
            if (request == null) throw new NacosException(ConstValue.CLIENT_INVALID_PARAM, "request param invalid");

            if (string.IsNullOrWhiteSpace(request.Tenant)) request.Tenant = _options.Namespace;
            if (string.IsNullOrWhiteSpace(request.Group)) request.Group = ConstValue.DefaultGroup;

            request.CheckParam();

            var name = BuildName(request.Tenant, request.Group, request.DataId);

            if (!listeners.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning($"[remove-listener] error, {name} was not added.");
                return Task.CompletedTask;
            }

            var list = listeners.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            // clean timer
            foreach (var item in list)
            {
                item.Timer.Dispose();
                item.Timer = null;
            }

            // remove listeners
            listeners.RemoveAll(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            foreach (var cb in request.Callbacks)
            {
                try
                {
                    cb();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"[remove-listener] call back throw exception, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}");
                }
            }

            return Task.CompletedTask;
        }

        private string BuildName(string tenant, string group, string dataId)
        {
            return $"{tenant}-{group}-{dataId}";
        }

        private async Task PollingAsync(object requestInfo)
        {
            var request = (AddListenerRequest)requestInfo;

            // read the last config
            var lastConfig = await _processor.GetSnapshotAync(request.DataId, request.Group, request.Tenant);
            request.Content = lastConfig;

            try
            {
                var client = _clientFactory.CreateClient(ConstValue.ClientName);

                // longer than long pulling timeout
                client.Timeout = TimeSpan.FromSeconds(ConstValue.LongPullingTimeout + 10);

                var stringContent = new StringContent(request.ToQueryString());
                stringContent.Headers.TryAddWithoutValidation("Long-Pulling-Timeout", (ConstValue.LongPullingTimeout * 1000).ToString());
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{GetBaseUrl()}/nacos/v1/cs/configs/listener")
                {
                    Content = stringContent
                };

                var responseMessage = await client.SendAsync(requestMessage);

                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var content = await responseMessage.Content.ReadAsStringAsync();
                        await ConfigChangeAsync(content, request);
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        _logger.LogWarning($"[listener] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                        throw new NacosException(ConstValue.NO_RIGHT, $"Insufficient privilege.");
                    default:
                        _logger.LogWarning($"[listener] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}, code={(int)responseMessage.StatusCode} msg={responseMessage.StatusCode.ToString()}");
                        throw new NacosException((int)responseMessage.StatusCode, responseMessage.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[listener] error, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}");
            }
        }

        private async Task ConfigChangeAsync(string content, AddListenerRequest request)
        {
            // config was changed
            if (!string.IsNullOrWhiteSpace(content))
            {
                var config = await DoGetConfigAsync(new GetConfigRequest
                {
                    DataId = request.DataId,
                    Group = request.Group,
                    Tenant = request.Tenant
                });

                // update local cache
                await _processor.SaveSnapshotAsync(request.DataId, request.Group, request.Tenant, config);

                // callback
                foreach (var cb in request.Callbacks)
                {
                    try
                    {
                        cb(config);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"[listener] call back throw exception, dataId={request.DataId}, group={request.Group}, tenant={request.Tenant}");
                    }
                }
            }
        }

     
    }
}
