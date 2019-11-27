using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Nacos.Config;
using UWay.Skynet.Cloud.Extensions;
using Steeltoe.Common.Http;
using System.IO;
using Steeltoe.Extensions.Configuration.CloudFoundry;


namespace Steeltoe.Configuration.NacosServerBase
{
    class ConfigNacosConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        

        /// <summary>
        /// The prefix (<see cref="IConfigurationSection"/> under which all Spring Cloud Config Server
        /// configuration settings (<see cref="ConfigServerClientSettings"/> are found.
        ///   (e.g. spring:cloud:config:env, spring:cloud:config:uri, spring:cloud:config:enabled, etc.)
        /// </summary>
        public const string PREFIX = "spring:cloud:nacos:config";


        private const string ArrayPattern = @"(\[[0-9]+\])*$";
        private const string SHARED_CONFIG_SEPARATOR_CHAR = "[,]";
        private static IList<string> SUPPORT_FILE_EXTENSION = new List<string>{"properties",
            "yaml", "yml"};

        private const string DELIMITER_STRING = ".";
        private const char DELIMITER_CHAR = '.';
        private const char ESCAPE_CHAR = '\\';
        private const string ESCAPE_STRING = "\\";
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILocalConfigInfoProcessor _processor;
        private ConfigNacosClientSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationProvider"/> class with default
        /// configuration settings. <see cref="ConfigServerClientSettings"/>
        /// </summary>
        /// <param name="logFactory">optional logging factory</param>
        public ConfigNacosConfigurationProvider(IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
            : this(new ConfigNacosClientSettings(), clientFactory, processor, logFactory)
        {
        }

        private INacosConfigClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationProvider"/> class.
        /// </summary>
        /// <param name="settings">the configuration settings the provider uses when accessing the server.</param>
        /// <param name="logFactory">optional logging factory</param>
        public ConfigNacosConfigurationProvider(ConfigNacosClientSettings settings, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            _loggerFactory = logFactory;
            _clientFactory = clientFactory;
            _processor = processor?? new MemoryLocalConfigInfoProcessor();
            _logger = logFactory?.CreateLogger<ConfigNacosConfigurationProvider>();
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _client = null;
            _configuration = new ConfigurationBuilder().Build();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationProvider"/> class.
        /// </summary>
        /// <param name="settings">the configuration settings the provider uses when accessing the server.</param>
        /// <param name="httpClient">a HttpClient the provider uses to make requests of the server.</param>
        /// <param name="logFactory">optional logging factory</param>
        public ConfigNacosConfigurationProvider(ConfigNacosClientSettings settings, INacosConfigClient httpClient, ILocalConfigInfoProcessor processor,ILoggerFactory logFactory = null)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logFactory?.CreateLogger<ConfigNacosConfigurationProvider>();
            _loggerFactory = logFactory;
            _configuration = new ConfigurationBuilder().Build();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerConfigurationProvider"/> class from a <see cref="ConfigServerConfigurationSource"/>
        /// </summary>
        /// <param name="source">the <see cref="ConfigServerConfigurationSource"/> the provider uses when accessing the server.</param>
        public ConfigNacosConfigurationProvider( ConfigNacosConfigurationSource source)
            : this(source.DefaultSettings, source.ClientFactory, source.Processor, source.LogFactory)
        {
            var root = source.Configuration as IConfigurationRoot;
            _configuration = WrapWithPlaceholderResolver(source.Configuration);
            ConfigurationSettingsHelper.Initialize(PREFIX, _settings, _configuration);
        }

        private IConfiguration WrapWithPlaceholderResolver(IConfiguration configuration)
        {
            var root = configuration as IConfigurationRoot;
            return new ConfigurationRoot(new List<IConfigurationProvider>() { new PlaceholderResolverProvider(new List<IConfigurationProvider>(root.Providers)) });
        }


        protected ILogger _logger;
        protected ILoggerFactory _loggerFactory;
        protected IConfiguration _configuration;

        [Obsolete("Will be removed in next release, use the ConfigServerConfigurationSource")]
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            ConfigurationBuilder config = new ConfigurationBuilder();
            foreach (IConfigurationSource s in builder.Sources)
            {
                if (s == this)
                {
                    break;
                }

                config.Add(s);
            }

            _configuration = WrapWithPlaceholderResolver(config.Build());
            ConfigurationSettingsHelper.Initialize(PREFIX, _settings, _configuration);
            return this;
        }

        /// <summary>
        /// Loads configuration data from the Spring Cloud Configuration Server as specified by
        /// the <see cref="Settings"/>
        /// </summary>
        public override void Load()
        {
            Load(true);
        }

        //[Obsolete("Will be removed in next release, use the ConfigServerConfigurationSource")]
        //public virtual IConfigurationProvider Build(IConfigurationBuilder builder)
        //{
        //    ConfigurationBuilder config = new ConfigurationBuilder();
        //    foreach (IConfigurationSource s in builder.Sources)
        //    {
        //        if (s == this)
        //        {
        //            break;
        //        }

        //        config.Add(s);
        //    }

        //    _configuration = WrapWithPlaceholderResolver(config.Build());
        //    ConfigurationSettingsHelper.Initialize(PREFIX, _settings, _configuration);
        //    return this;
        //}

        internal void Load(bool updateDictionary = true)
        {
            // Refresh settings with latest configuration values
            ConfigurationSettingsHelper.Initialize(PREFIX, _settings, _configuration);

            if (!_settings.Enabled)
            {
                _logger?.LogInformation("Config Server client disabled, did not fetch configuration!");
                return;
                //return null;
            }

            //if (IsDiscoveryFirstEnabled())
            //{
            //    var discoveryService = new ConfigServerDiscoveryService(_configuration, _settings, _loggerFactory);
            //    DiscoverServerInstances(discoveryService);
            //}

            // Adds client settings (e.g spring:cloud:config:uri, etc) to the Data dictionary
            AddConfigServerClientSettings();

            //if (_settings.RetryEnabled && _settings.FailFast)
            //{
            //    var attempts = 0;
            //    var backOff = _settings.RetryInitialInterval;
            //    do
            //    {
            //        _logger?.LogInformation("Fetching config from server at: {0}", _settings.Uri);
            //        try
            //        {
            //            return DoLoad(updateDictionary);
            //        }
            //        catch (ConfigNacosException e)
            //        {
            //            _logger?.LogInformation("Failed fetching config from server at: {0}, Exception: {1}", _settings.Uri, e);
            //            attempts++;
            //            if (attempts < _settings.RetryAttempts)
            //            {
            //                Thread.CurrentThread.Join(backOff);
            //                var nextBackoff = (int)(backOff * _settings.RetryMultiplier);
            //                backOff = Math.Min(nextBackoff, _settings.RetryMaxInterval);
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //    }
            //    while (true);
            //}
            //else
            //{
            _logger?.LogInformation("Fetching config from server at: {0}", _settings.ServerAddr);
            DoLoad(updateDictionary);
            //return 
            //}
        }

        internal void DoLoad(bool updateDictionary = true)
        {
            Exception error = null;
            IList<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            //IList<string> result = new List<string>();
            try
            {
                var task1 = RemoteLoadShareAsync();
                task1.Wait();
                result.AddRange(task1.Result);
                var task2 = RemoteLoadExtAsync();
                task2.Wait();
                result.AddRange(task2.Result);
                var task3 = RemoteLoadApplicationAsync();
                task3.Wait();
                result.Add(task3.Result);

            }
            catch (Exception e)
            {
                error = e;
            }

            _logger?.LogWarning("Could not locate PropertySource: " + error?.ToString());

            foreach(var item in result)
            {
                AddPropertySource(Deserialize(item.Key,item.Value));
            }
            //Task task = 
            // Get arrays of config server uris to check
            //            var uris = _settings.GetUris();

            //            try
            //            {
            //                foreach (string label in GetLabels())
            //                {
            //                    Task<ConfigEnvironment> task = null;

            //                    if (uris.Length > 1)
            //                    {
            //                        _logger?.LogInformation("Multiple Config Server Uris listed.");

            //                        // Invoke config servers
            //                        task = RemoteLoadAsync(uris, label);
            //                    }
            //                    else
            //                    {
            //                        // Single, server make Config Server URI from settings
            //#pragma warning disable CS0618 // Type or member is obsolete
            //                        var path = GetConfigServerUri(label);

            //                        // Invoke config server
            //                        task = RemoteLoadAsync(path);
            //#pragma warning restore CS0618 // Type or member is obsolete
            //                    }

            //                    // Wait for results from server
            //                    task.Wait();
            //                    ConfigEnvironment env = task.Result;

            //                    // Update config Data dictionary with any results
            //                    if (env != null)
            //                    {
            //                        _logger?.LogInformation(
            //                            "Located environment: {name}, {profiles}, {label}, {version}, {state}", env.Name, env.Profiles, env.Label, env.Version, env.State);
            //                        if (updateDictionary)
            //                        {
            //                            if (!string.IsNullOrEmpty(env.State))
            //                            {
            //                                Data["spring:cloud:config:client:state"] = env.State;
            //                            }

            //                            if (!string.IsNullOrEmpty(env.Version))
            //                            {
            //                                Data["spring:cloud:config:client:version"] = env.Version;
            //                            }

            //                            var sources = env.PropertySources;
            //                            if (sources != null)
            //                            {
            //                                int index = sources.Count - 1;
            //                                for (; index >= 0; index--)
            //                                {
            //                                    AddPropertySource(sources[index]);
            //                                }
            //                            }
            //                        }

            //                        return env;
            //                    }
            //                }
            //            }
            //            catch (Exception e)
            //            {
            //                error = e;
            //            }

            //            _logger?.LogWarning("Could not locate PropertySource: " + error?.ToString());

            //            if (_settings.FailFast)
            //            {
            //                throw new ConfigNacosException("Could not locate PropertySource, fail fast property is set, failing", error);
            //            }

            //return result;
        }


        protected internal async Task<string> RemoteLoadAsync(string dataId, string group, bool isRefreable)
        {
            // Get client if not already set
            if (_client == null)
            {
                _client = GetNacosClient(_settings, _clientFactory, _processor, _loggerFactory);
            }

            GetConfigRequest request = new GetConfigRequest()
            {
                DataId = dataId,
                Group = group,
            };
            if(isRefreable)
            {
                AddListenerRequest addListenerRequest = new AddListenerRequest()
                {
                    DataId = dataId, Group = group, Callbacks = new List<Action<string>> { x => { AddPropertySource(Deserialize(dataId,x)); } }
                };

                _client.AddListenerAsync(addListenerRequest);
            }

            Exception error = null;
            try
            {
                var result = await _client.GetConfigAsync(request);
                return result;
            }
            catch (Exception e)
            {
                error = e;
            }


            if (error != null)
            {
                throw error;
            }

            return string.Empty;
        }
        protected internal async Task<KeyValuePair<string,string>> RemoteLoadApplicationAsync()
        //protected internal async Task<string> RemoteLoadApplicationAsync()
        {
            //KeyValuePair<string, string> list = new KeyValuePair<string, string>();
            string dataIdPrefix = _settings.Prefix;
            if (!string.IsNullOrEmpty(dataIdPrefix)&&!string.IsNullOrWhiteSpace(dataIdPrefix))
            {
                dataIdPrefix = _settings.Name;
            }

            if (string.IsNullOrEmpty(dataIdPrefix) && string.IsNullOrWhiteSpace(dataIdPrefix))
            {
                dataIdPrefix = string.Format("{0}-{1}.{2}", _settings.Name, _settings.Environment,_settings.FileExtension);
            }

            var result = await RemoteLoadAsync(dataIdPrefix, _settings.GroupName, _settings.Interval > 0);
            
            return new KeyValuePair<string, string>(dataIdPrefix, result);
        }

        protected internal async Task<IList<KeyValuePair<string, string>>> RemoteLoadExtAsync()
        //protected internal async Task<IList< string>> RemoteLoadExtAsync()
        {
            IDictionary<string,string> list = new Dictionary<string,string>();
            //IList<string> list = new List<string>();
            if (_settings.ExtConfigs ==  null || _settings.ExtConfigs.Count <= 0)
            {
                return list.ToList();
            }

            checkExtConfiguration(_settings.ExtConfigs);
            
            foreach(var extConfig in _settings.ExtConfigs)
            {
                var result = await RemoteLoadAsync(extConfig.DataId, extConfig.GroupName, _settings.Interval > 0);
                list.Add(extConfig.DataId,result);
                //list.Add(result);
            }

            return list.ToList();
            
        }


        private void checkExtConfiguration(IList<ExtConfig> extConfigs)
        {
            string[] dataIds = new string[extConfigs.Count];
            var i = 0;
            foreach (var item in extConfigs)
            {
                string dataId = item.DataId;
                if (string.IsNullOrEmpty(dataId) || string.IsNullOrWhiteSpace(dataId))
                {
                    throw new ArgumentNullException(string.Format(
                            "the [ spring.cloud.nacos.config.ext-config{0} ] must give a dataid",
                            i));
                }
                dataIds[i] = dataId;
                i++;
            }
            checkDataIdFileExtension(dataIds);
        }

        private static void checkDataIdFileExtension(string[] dataIdArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < dataIdArray.Length; i++)
            {
                bool isLegal = false;
                foreach (var fileExtension in SUPPORT_FILE_EXTENSION)
                {
                    if (dataIdArray[i].IndexOf(fileExtension) > 0)
                    {
                        isLegal = true;
                        break;
                    }
                }
                // add tips
                if (!isLegal)
                {
                    stringBuilder.Append(dataIdArray[i] + ",");
                }
            }

            if (stringBuilder.Length > 0)
            {
                String result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
                throw new ArgumentNullException(string.Format(
                        "[{0}] must contains file extension with properties|yaml|yml",
                        result));
            }
        }
        protected internal async Task<IList<KeyValuePair<string, string>>> RemoteLoadShareAsync()
        //protected internal async Task<IList<string>>  RemoteLoadShareAsync()
        {
            IDictionary<string,string> list = new Dictionary<string, string>();

            //IList<string> list = new List<string>();


            if (string.IsNullOrEmpty(_settings.ShareDataIds))
            {
                return list.ToList();
            }

            //List<string> refreshDataIds = _settings.RefreshableDataIds.Split(SHARED_CONFIG_SEPARATOR_CHAR.ToCharArray()).ToList();
            List<string> sharedDataIds = _settings.ShareDataIds.Split(SHARED_CONFIG_SEPARATOR_CHAR.ToCharArray()).ToList();
            
            foreach (var shareDataId in sharedDataIds)
            {
                string fileExtension = shareDataId.Substring(shareDataId.LastIndexOf(".") + 1);
                bool isRefreshable = checkDataIdIsRefreshbable(_settings.RefreshableDataIds,
                    shareDataId);
              
                if (isRefreshable)
                {
                    var result =  await RemoteLoadAsync(shareDataId, _settings.GroupName, _settings.Interval > 0);
                    list.Add(shareDataId,result);
                    //list.Add(shareDataId, result);
                }

            }
            return list.ToList();

        }

        

        private bool checkDataIdIsRefreshbable(string refreshDataIds,
            string sharedDataId)
        {
            if (string.IsNullOrEmpty(refreshDataIds) || string.IsNullOrEmpty(refreshDataIds) || string.IsNullOrWhiteSpace(refreshDataIds))
            {
                return false;
            }

            string[] refreshDataIdArry = refreshDataIds.Split(SHARED_CONFIG_SEPARATOR_CHAR.ToArray());
            foreach(string refreshDataId in refreshDataIdArry)
            {
                if (refreshDataId.Equals(sharedDataId))
                {
                    return true;
                }
            }

            return false;
        }

        public PropertySource Deserialize(string name, string jsonText)
        {
            
            using (Stream stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(jsonText);
                    writer.Flush();
                    writer.Close();
                }

                var parse = new YamlConfigurationFileParser();


                return new PropertySource(name,parse.Parse(jsonText));
                //return SerializationHelper.Deserialize<PropertySource>(stream, _logger);
            }
            
            //var memStream = CloudFoundryConfigurationProvider.GetMemoryStream(jsonText)
            //return new PropertySource(dataId, jsonText.JsonDeserialize<IDictionary<string, object>>());
            //using (Stream stream = new MemoryStream())
            //{
            //    using (var writer = new StreamWriter(stream))
            //    {
            //        writer.Write(jsonText);
            //        writer.Flush();
            //        writer.Close();
            //    }
            //    return SerializationHelper.Deserialize<PropertySource>(stream, _logger);
            //}
                
            //var deserializer = new Deserializer();
            //var yamlObject = deserializer.Deserialize(r);

            //var serializer = new Newtonsoft.Json.JsonSerializer();
            //serializer.Serialize(Console.Out, yamlObject);
            ////PropertySource propertySource = jsonText.JsonDeserialize<PropertySource>();
            //return jsonText.JsonDeserialize<PropertySource>();
            //jsonText = ConvertJsonString(jsonText);
            //using (Stream stream = new MemoryStream())
            //{
            //    using (var writer = new StreamWriter(stream))
            //    {
            //        writer.Write(jsonText);
            //        writer.Flush();
            //        writer.Close();
            //    }

            //    IDictionary<string, object> properties = SerializationHelper.Deserialize<IDictionary<string, object>>(stream, _logger);
            //    return new PropertySource(name, properties);
            //}
            //IDictionary<string, object> properties = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonText);
            //return new PropertySource(name, properties);

        }



        protected internal virtual void AddConfigServerClientSettings()
        {
            Data["spring:cloud:nacos:config:server-addr"] = _settings.ServerAddr;
            Data["spring:cloud:nacos:config:group-name"] = _settings.GroupName;
            Data["spring:cloud:nacos:config:file-extension"] = _settings.FileExtension;
            Data["spring:cloud:nacos:config:refreshable-dataids"] = _settings.RefreshableDataIds;
            Data["spring:cloud:nacos:config:secret-key"] = _settings.SecretKey;
            Data["spring:cloud:nacos:config:access-key"] = _settings.AccessKey;
            Data["spring:cloud:nacos:config:ext-config"] = JsonConvert.SerializeObject(_settings.ExtConfigs);
            Data["spring:cloud:nacos:config:cluster-name"] = _settings.ClusterName;
            Data["spring:cloud:nacos:config:shared-dataids"] = _settings.ShareDataIds;
            Data["spring:cloud:nacos:config:context-path"] = _settings.ContextPath;
            Data["spring:cloud:nacos:config:interval"] = _settings.Interval.ToString();
            Data["spring:cloud:nacos:config:prefix"] = _settings.Prefix;
            Data["spring:cloud:nacos:config:endpoint"] = _settings.Endpoint;
            Data["spring:cloud:nacos:config:enabled"] = _settings.Enabled.ToString();
            Data["spring:cloud:nacos:config:encode"] = _settings.Encode;
        }


        /// <summary>
        /// Creates an appropriatly configured HttpClient that will be used in communicating with the
        /// Spring Cloud Configuration Server
        /// </summary>
        /// <param name="settings">the settings used in configuring the HttpClient</param>
        /// <returns>The HttpClient used by the provider</returns>
        protected static INacosConfigClient GetNacosClient(ConfigNacosClientSettings settings, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory loggerFactory = null)
        {
            NacosClientConfiguration nacosClientConfiguration = new NacosClientConfiguration()
            {
                ServerAddresses = settings.ServerAddr,
                IsSecure = settings.IsSecure,
                ServiceName = settings.Name
            };
            return new NacosConfigClient(loggerFactory, nacosClientConfiguration, clientFactory, processor);
        }


        /// <summary>
        /// Adds values from a PropertySource to the Configurtation Data dictionary managed
        /// by this provider
        /// </summary>
        /// <param name="source">a property source to add</param>
        protected internal virtual void AddPropertySource(PropertySource source)
        {
            if (source == null || source.Source == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> kvp in source.Source)
            {
                try
                {
                    string key = ConvertKey(kvp.Key);
                    string value = ConvertValue(kvp.Value);
                    Data[key] = value;
                }
                catch (Exception e)
                {
                    _logger?.LogError("Config Nacos exception, property: {0}={1}", kvp.Key, kvp.Value.GetType(), e);
                }
            }
        }

        protected internal virtual string ConvertKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return key;
            }

            string[] split = Split(key);
            StringBuilder sb = new StringBuilder();
            foreach (var part in split)
            {
                string keyPart = ConvertArrayKey(part);
                sb.Append(keyPart);
                sb.Append(ConfigurationPath.KeyDelimiter);
            }

            return sb.ToString(0, sb.Length - 1);
        }

        protected internal virtual string[] Split(string source)
        {
            var result = new List<string>();

            int segmentStart = 0;
            for (int i = 0; i < source.Length; i++)
            {
                bool readEscapeChar = false;
                if (source[i] == ESCAPE_CHAR)
                {
                    readEscapeChar = true;
                    i++;
                }

                if (!readEscapeChar && source[i] == DELIMITER_CHAR)
                {
                    result.Add(UnEscapeString(
                      source.Substring(segmentStart, i - segmentStart)));
                    segmentStart = i + 1;
                }

                if (i == source.Length - 1)
                {
                    result.Add(UnEscapeString(source.Substring(segmentStart)));
                }
            }

            return result.ToArray();

            string UnEscapeString(string src)
            {
                return src.Replace(ESCAPE_STRING + DELIMITER_STRING, DELIMITER_STRING)
                  .Replace(ESCAPE_STRING + ESCAPE_STRING, ESCAPE_STRING);
            }
        }

        protected internal virtual string ConvertArrayKey(string key)
        {
            return Regex.Replace(key, ArrayPattern, (match) =>
            {
                string result = match.Value.Replace("[", ":").Replace("]", string.Empty);
                return result;
            });
        }

        protected internal virtual string ConvertValue(object value)
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        ///// <summary>
        ///// Encode the username password for a http request
        ///// </summary>
        ///// <param name="user">the username</param>
        ///// <param name="password">the password</param>
        ///// <returns>Encoded user + password</returns>
        //protected internal string GetEncoded(string user, string password)
        //{
        //    return HttpClientHelper.GetEncodedUserPassword(user, password);
        //}
    }
}
