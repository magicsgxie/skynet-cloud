using System.Data.Common;
using UWay.Skynet.Cloud.Reflection;
using System.Data;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using UWay.Skynet.Cloud.Disposables;
using System.Collections.Concurrent;

namespace UWay.Skynet.Cloud.Data
{
    class ODPConnectionWrapper : DbConnectionWrapper
    {
        static Setter _bindByNameProperty;

        public ODPConnectionWrapper(DbConfiguration dbConfiguraiton, DbConnection conn, string @namespace = "Oracle.DataAccess.Client")
            : base(dbConfiguraiton, conn)
        {
            if (_bindByNameProperty == null)
                _bindByNameProperty = conn.GetType().Module.GetType(@namespace + ".OracleCommand").GetProperty("BindByName").GetSetter();
        }

        protected override DbCommand CreateDbCommand()
        {
            var cmd = base.CreateDbCommand();
            _bindByNameProperty(cmd, true);
            return cmd;
        }
    }

    class ManagedODPConnectionWrapper : ODPConnectionWrapper
    {
        public ManagedODPConnectionWrapper(DbConfiguration dbConfiguraiton, DbConnection conn)
            : base(dbConfiguraiton, conn, "Oracle.ManagedDataAccess.Client")
        {
        }
    }



    class OracleClientFactory : DbProviderFactory
    {
        private const string ODPClientFactoryTypeName = "Oracle.ManagedDataAccess.Client.OracleClientFactory,Oracle.ManagedDataAccess";

        internal static DbProviderFactory _innerFactory;

        internal static MethodInfo ClearAllPoolsMethod;

        static OracleClientFactory()
        {
            var type = Type.GetType(ODPClientFactoryTypeName);

            Guard.NotNull(type, "OracleClientFactoryName");

            _innerFactory = Activator.CreateInstance(type) as DbProviderFactory;

            var conn = _innerFactory.CreateConnection();

            ClearAllPoolsMethod = conn.GetType().GetMethod("ClearAllPools", BindingFlags.Public | BindingFlags.Static);
        }


        private static Dictionary<string, OracleConnectionPool> connectionPoolDictioanry = new Dictionary<string, OracleConnectionPool>();

        private OracleConnectionPool connectionPool;
        internal void Init(string connectionString)
        {
            Guard.NotNullOrEmpty(connectionString, "connectionString");

            connectionPool = new OracleConnectionPool(connectionString);
            lock (connectionPoolDictioanry)
            {
                connectionPoolDictioanry[connectionString] = connectionPool;
            }
        }

        public static readonly OracleClientFactory Instance = new OracleClientFactory();

        public override bool CanCreateDataSourceEnumerator
        {
            get
            {
                return _innerFactory.CanCreateDataSourceEnumerator;
            }
        }

        public override DbConnection CreateConnection()
        {
            return new OracleConnection(connectionPool);
        }

        public override DbCommand CreateCommand()
        {
            return _innerFactory.CreateCommand();
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return _innerFactory.CreateCommandBuilder();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return _innerFactory.CreateConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return _innerFactory.CreateDataAdapter();
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return _innerFactory.CreateDataSourceEnumerator();
        }

        public override DbParameter CreateParameter()
        {
            return _innerFactory.CreateParameter();
        }

        //public override System.Security.CodeAccessPermission CreatePermission(System.Security.Permissions.PermissionState state)
        //{
        //    return _innerFactory.CreatePermission(state);
        //}

        abstract class DbConnectionBase : DbConnection
        {
            internal DbConnection innerConnection;

            public DbConnectionBase(DbConnection conn)
            {
                this.innerConnection = conn;
            }
        }

        class OracleConnection : DbConnectionBase
        {
            private OracleConnectionPool connectionPool;
            private ConnectionState state = ConnectionState.Closed;

            public OracleConnection(OracleConnectionPool connectionPool)
                : base(connectionPool.Pop())
            {
                this.connectionPool = connectionPool;
            }

            public static void ClearAllPools()
            {
                Guard.NotNull(OracleClientFactory.ClearAllPoolsMethod, "ClearAllPoolsMethod");

                foreach (var pool in OracleClientFactory.connectionPoolDictioanry.Values)
                {
                    pool.ClearPool();
                }

                try
                {
                    //先清理所有BJMT内部的连接池
                    OracleClientFactory.ClearAllPoolsMethod.Invoke(null, null);
                }
                catch (System.Reflection.TargetInvocationException)
                {
                    throw;
                }
            }

            public static void ClearPool(OracleConnection conn)
            {
                Guard.NotNull(conn, "conn");

                OracleConnectionPool pool;
                if (OracleClientFactory.connectionPoolDictioanry.TryGetValue(conn.ConnectionString, out pool))
                    pool.ClearPool();
            }

            protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
            {
                return innerConnection.BeginTransaction(isolationLevel);
            }

            public override void ChangeDatabase(string databaseName)
            {
                innerConnection.ChangeDatabase(databaseName);
            }

            public override void Close()
            {
                connectionPool.Push(innerConnection);
                innerConnection = null;
                state = ConnectionState.Closed;
            }

            public override string ConnectionString
            {
                get
                {
                    return innerConnection.ConnectionString;
                }
                set
                {
                    if (innerConnection.ConnectionString != value)
                    {
                        innerConnection.ConnectionString = value;
                    }
                }
            }

            protected override DbCommand CreateDbCommand()
            {
                return innerConnection.CreateCommand();
            }

            public override string DataSource
            {
                get { return innerConnection.DataSource; }
            }

            public override string Database
            {
                get { return innerConnection.Database; }
            }

            public override void Open()
            {
                if (innerConnection == null)
                {
                    innerConnection = connectionPool.Pop();
                }


                switch (innerConnection.State)
                {
                    case ConnectionState.Closed:
                        innerConnection.Open();
                        break;
                    case ConnectionState.Broken:
                        try
                        {
                            innerConnection.Close();
                        }
                        catch
                        {
                        }
                        try
                        {
                            innerConnection.Open();
                        }
                        catch
                        {
                        }
                        break;
                }
                state = innerConnection.State;
            }

            protected override DbProviderFactory DbProviderFactory
            {
                get
                {
                    return OracleClientFactory._innerFactory;
                }
            }

            public override string ServerVersion
            {
                get { return innerConnection.ServerVersion; }
            }

            public override System.Data.ConnectionState State
            {
                get { return state; }
            }


            public override int ConnectionTimeout
            {
                get
                {
                    return innerConnection.ConnectionTimeout;
                }
            }

            public override void EnlistTransaction(System.Transactions.Transaction transaction)
            {
                innerConnection.EnlistTransaction(transaction);
            }

            public override DataTable GetSchema()
            {
                return innerConnection.GetSchema();
            }

            public override DataTable GetSchema(string collectionName)
            {
                return innerConnection.GetSchema(collectionName);
            }

            public override DataTable GetSchema(string collectionName, string[] restrictionValues)
            {
                return innerConnection.GetSchema(collectionName, restrictionValues);
            }

            public override event StateChangeEventHandler StateChange
            {
                remove { innerConnection.StateChange -= value; }
                add { innerConnection.StateChange += value; }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    Close();
                }
                base.Dispose(disposing);
            }
        }



        class OracleConnectionPool : BooleanDisposable
        {
            BlockingCollection<DbConnection> connectionPool;
            private string ConnectionString;

            public OracleConnectionPool(string connectionString)
            {
                Init(connectionString);
            }

            private void Init(string connectionString)
            {
                var builder = OracleClientFactory._innerFactory.CreateConnectionStringBuilder();
                builder.ConnectionString = connectionString;

                bool pooling = true;
                if (builder.ContainsKey("POOLING"))
                {
                    pooling = (bool)builder["POOLING"];
                }

                if (!pooling)
                {
                    //throw new System.Configuration.ConfigurationErrorsException("BJMT Oracle 驱动仅仅支持带连接池的配置!");
                }

                var minPoolSize = (int)builder["MIN POOL SIZE"];
                var maxPoolSize = (int)builder["MAX POOL SIZE"];

                if (minPoolSize > maxPoolSize)
                {
                    //throw new System.Configuration.ConfigurationErrorsException("minPoolSize > maxPoolSize!");
                }

                ConnectionString = connectionString;

                connectionPool = new BlockingCollection<DbConnection>(maxPoolSize);

                if (minPoolSize >= 1)
                {
                    connectionPool.Add(CreateConnection());
                }

                var length = minPoolSize - 1;
                if (length > 0)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(state =>
                    {
                        for (var i = 0; i < length - 1; i++)
                        {
                            connectionPool.Add(CreateConnection());
                        };
                    });
                }
            }

            private DbConnection CreateConnection()
            {
                var conn = OracleClientFactory._innerFactory.CreateConnection();
                conn.ConnectionString = ConnectionString;

                conn.Open();

                return conn;
            }

            public int Count
            {
                get { return connectionPool.Count; }
            }

            public DbConnection Pop()
            {
                lock (this)
                {
                    if (connectionPool.Count == 0)
                    {
                        var conn = CreateConnection();
                        connectionPool.Add(conn);
                    }

                    return connectionPool.Take();
                }
            }

            public void Push(DbConnection conn)
            {
                lock (this)
                {
                    connectionPool.Add(conn);
                }
            }

            public void ClearPool()
            {
                lock (this)
                {
                    while (connectionPool.Count != 0)
                    {
                        DbConnection instance;

                        connectionPool.TryTake(out instance);

                        try
                        {
                            instance.Close();
                        }
                        catch { }

                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (!disposing)
                    return;

                lock (this)
                {
                    while (connectionPool.Count != 0)
                    {
                        DbConnection instance;

                        connectionPool.TryTake(out instance);

                        var disp = instance as IDisposable;
                        if (disp != null)
                        {
                            disp.Dispose();
                        }

                        instance = null;

                    }
                }
                base.Dispose(disposing);
            }
        }
    }
   
}
