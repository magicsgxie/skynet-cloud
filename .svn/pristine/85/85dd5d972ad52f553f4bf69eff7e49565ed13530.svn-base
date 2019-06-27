using System;
using System.Data;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// DbConfigurationName
        /// </summary>
        /// <remarks>
        /// 用DbConfigurationName标致不同的UnitOfWork,目的是为了支持多数据库
        /// </remarks>
        string DbConfigurationName { get; }
        /// <summary>
        /// 创建仓储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateRepository<T>();

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
