using Microsoft.Extensions.Logging;
using System;
using System.Data;
using UWay.Skynet.Cloud.Threading;

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// 工作单元门面类
    /// </summary>
    public static class UnitOfWork
    {


        /// <summary>
        /// 初始化才可以使用
        /// </summary>
        /// <param name="containerName">连接名称</param>
        /// <param name="loggerFactory">日志工厂</param>
        /// <returns></returns>
        public static IDbContext Get(string containerName, ILoggerFactory loggerFactory = null)
        {
            return Get(new DbContextOption { Container = containerName, LogggerFactory = loggerFactory });
        }

        /// <summary>
        /// 得到工作单元对象
        /// </summary>
        /// <param name="dbContextOption">配置信息</param>
        /// <returns></returns>
        public static IDbContext Get(DbContextOption dbContextOption)
        {
            Guard.NotNullOrEmpty(dbContextOption.Container, "dbConfigurationName");

            return DbConfiguration.Configure(dbContextOption).CreateDbContext();
        }
    }

}
