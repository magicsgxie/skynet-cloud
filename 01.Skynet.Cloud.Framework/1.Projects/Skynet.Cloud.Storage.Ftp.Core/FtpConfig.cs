using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Storage.Ftp.Core
{
    /// <summary>
    /// Ftp配置.
    /// </summary>
    public class FtpConfig
    {
        /// <summary>
        /// Gets or sets ftp 标识.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets FTP地址.
        /// </summary>
        public string FtpUri { get; set; }

        /// <summary>
        /// Gets or sets FTP端口.
        /// </summary>
        public int FtpPort { get; set; }

        /// <summary>
        /// Gets or sets FTP路径(/test).
        /// </summary>
        public string FtpPath { get; set; }

        /// <summary>
        /// Gets or sets FTP用户名.
        /// </summary>
        public string FtpUserID { get; set; }

        /// <summary>
        /// Gets or sets FTP密码.
        /// </summary>
        public string FtpPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets FTP密码是否被加密.
        /// </summary>
        public bool IsEncrypt { get; set; }

        /// <summary>
        /// Gets or sets 读取或写入超时之前的毫秒数。默认值为 30,000 毫秒.
        /// </summary>
        public int FtpReadWriteTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为true.
        /// </summary>
        public bool FtpUseBinary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true，被动模式；false，主动模式(主动模式可能被防火墙拦截)。默认值为true.
        /// </summary>
        public bool FtpUsePassive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否保持连接.
        /// </summary>
        public bool FtpKeepAlive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否启用SSL.
        /// </summary>
        public bool FtpEnableSsl { get; set; }

        /// <summary>
        /// Gets or sets 描述.
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Gets or sets 重试次数.
        /// </summary>
        public int RetryTimes { get; set; }

        /// <summary>
        /// Gets or sets 版本号.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 下载文档本地存储根路径.
        /// </summary>
        public string LocalRootPath { get; set; }
    }
}
