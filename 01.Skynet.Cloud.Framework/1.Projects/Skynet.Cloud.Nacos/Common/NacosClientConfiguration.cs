using System;
using System.Collections.Generic;

namespace UWay.Skynet.Cloud.Nacos
{
    /// <summary>
    /// 
    /// </summary>
    public class NacosClientConfiguration
    {

        /// <summary>
        /// Nacos server addresses.
        /// </summary>
        /// <example>
        /// 10.1.12.123:8848,10.1.12.124:8848
        /// </example>
        public string ServerAddresses { get; set; } = "";
        
        /// <summary>
        /// default timeout, unit is second.
        /// </summary>
        public int DefaultTimeOut { get; set; } = 15;

        /// <summary>
        /// default namespace
        /// </summary>
        public string Namespace { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public bool IsSecure { set; get; }

        /// <summary>
        /// listen interval, unit is millisecond.
        /// </summary>
        public int Port { get; set; } = 1000;


        /// <summary>
        /// the name of the service.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// the name of the service.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// the name of the service.
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        /// the name of the service.
        /// </summary>
        public int Interval { get; set; } = 8000;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Metadata { set; get; }


    }
}
