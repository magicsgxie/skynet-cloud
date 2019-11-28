using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 连接字信息
    /// </summary>
    public class DbConnectionString
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        public string ContainerName
        { set; get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 提供者
        /// </summary>
        public string Provider
        {
            set;
            get;
        }
    }
}
