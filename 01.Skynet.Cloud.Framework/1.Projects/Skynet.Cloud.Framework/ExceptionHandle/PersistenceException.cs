using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.ExceptionHandle
{
    [Serializable]
    public class DatabaseException : DbException
    {
        public DatabaseException() { }
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class PersistenceException : DatabaseException
    {
        public PersistenceException() { }
        public PersistenceException(string message) : base(message) { }
        public PersistenceException(string message, Exception innerException) : base(message, innerException) { }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ConnectionException : DatabaseException
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectionException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConnectionException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class InsertException : PersistenceException
    {
        public InsertException() { }
        public InsertException(string message) : base(message) { }
        public InsertException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class DeleteException : PersistenceException
    {
        public DeleteException() { }
        public DeleteException(string message) : base(message) { }
        public DeleteException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class UpdateException : PersistenceException
    {
        public UpdateException() { }
        public UpdateException(string message) : base(message) { }
        public UpdateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
