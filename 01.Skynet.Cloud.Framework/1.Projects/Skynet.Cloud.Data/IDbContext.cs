using System;
using System.Data;
using System.Data.Common;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Db上下文接口
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// DbConfiguration Name
        /// </summary>
        string DbConfigurationName { get; }
        /// <summary>
        /// 得到DbConfiguration对象
        /// </summary>
        DbConfiguration DbConfiguration { get; }
        /// <summary>
        /// 得到连接对象
        /// </summary>
        DbConnection Connection { get; }
        /// <summary>
        /// 得到DbHelper对象
        /// </summary>
        IDbHelper DbHelper { get; }

        /// <summary>
        /// 得到存储过程助手对象
        /// </summary>
        IDbHelper SpHelper { get; }
        /// <summary>
        /// 得到对应的DbSet对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDbSet<T> Set<T>();
        /// <summary>
        /// 得到对应的DbSet对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IDbSet Set(Type type);
        /// <summary>
        /// 启用Ado.net事务
        /// </summary>
        /// <param name="action"></param>
        void UsingTransaction(Action action);
        /// <summary>
        /// 启用Ado.net事务
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isolationLevel"></param>
        void UsingTransaction(Action action, IsolationLevel isolationLevel);
    }


}