using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 上下文配置
    /// </summary>
    public class DbContextOption
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        public string Container { set; get; }

        /// <summary>
        /// 默认连接
        /// </summary>
        public bool Default { set; get; }


        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 实体程序集名称
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// 实体程序集名称
        /// </summary>
        public string MappingFile { get; set; }

        /// <summary>
        /// 实体程序集名称
        /// </summary>
        public string ModuleAssemblyName { get; set; }

        /// <summary>
        /// 日志工厂
        /// </summary>
        public ILoggerFactory LogggerFactory { set; get; }
    }
}
