using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.ExceptionHandle
{
    /// <summary>
    /// 数据库异常
    /// </summary>
    [Serializable]
    public class DatabaseException : DbException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public DatabaseException(string message) : base(message) { }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 持久化异常
    /// </summary>
    [Serializable]
    public class PersistenceException : DatabaseException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PersistenceException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public PersistenceException(string message) : base(message) { }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public PersistenceException(string message, Exception innerException) : base(message, innerException) { }
    }
    /// <summary>
    /// 连接异常
    /// </summary>
    [Serializable]
    public class ConnectionException : DatabaseException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConnectionException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ConnectionException(string message) : base(message) { }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public ConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 插入异常
    /// </summary>
    [Serializable]
    public class InsertException : PersistenceException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InsertException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public InsertException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public InsertException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 删除异常
    /// </summary>
    [Serializable]
    public class DeleteException : PersistenceException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeleteException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public DeleteException(string message) : base(message) { }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public DeleteException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 更新异常
    /// </summary>
    [Serializable]
    public class UpdateException : PersistenceException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UpdateException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public UpdateException(string message) : base(message) { }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public UpdateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
