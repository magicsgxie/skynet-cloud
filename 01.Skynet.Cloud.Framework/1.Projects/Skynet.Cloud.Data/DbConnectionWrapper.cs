using System.Data;
using System.Data.Common;

namespace UWay.Skynet.Cloud.Data
{
    class TransactionWrapper : DbTransaction
    {
        private DbTransaction Inner;
        private new DbConnectionWrapper Connection;

        internal int transactionCount = 1;

        public TransactionWrapper(DbConnectionWrapper connection, DbTransaction tran)
        {
            this.Inner = tran;
            Connection = connection;
            IsActive = true;
        }

        public bool IsActive { get; private set; }
        public bool WasCommitted { get; private set; }
        public bool WasRolledBack { get; private set; }

        public override void Commit()
        {
            transactionCount--;
            if (transactionCount == 0)
            {
                Inner.Commit();
                Connection.transaction = null;
                Inner.Dispose();
                Inner = null;
                WasCommitted = true;
            }
        }

        public void Enlist(IDbCommand command)
        {
            if (command.Connection == Inner.Connection)
                command.Transaction = Inner;
        }

        public override void Rollback()
        {
            transactionCount = 0;
            Inner.Rollback();
            Connection.transaction = null;
            Inner.Dispose();
            Inner = null;
            WasRolledBack = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Commit();
            }
        }

        protected override DbConnection DbConnection
        {
            get { return Connection; }
        }

        public override IsolationLevel IsolationLevel
        {
            get { return Inner.IsolationLevel; }
        }
    }

    class DbConnectionWrapper : DbConnection
    {
        internal DbConnection innerConnection;

        internal TransactionWrapper transaction;
        internal DbConfiguration dbConfiguration;

        internal bool IsFileDatabase = false;

        public DbConnectionWrapper(DbConfiguration dbConfiguration, DbConnection conn)
        {
            this.innerConnection = conn;
            var connTypeName = conn.GetType().FullName;
            this.dbConfiguration = dbConfiguration;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            if (transaction == null)
                transaction = new TransactionWrapper(this, innerConnection.BeginTransaction(isolationLevel));
            else
                transaction.transactionCount++;
            return transaction;
        }

        public override void ChangeDatabase(string databaseName)
        {
            innerConnection.ChangeDatabase(databaseName);
        }

        protected bool hasClosed;
        public override void Close()
        {
            if (!hasClosed)
            {
                DisposeTransaction();
                innerConnection.Close();
                hasClosed = true;
            }
        }

        public override string ConnectionString
        {
            get
            {
                return innerConnection.ConnectionString;
            }
            set
            {
                innerConnection.ConnectionString = value;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            var cmd = innerConnection.CreateCommand();
            if (transaction != null)
                transaction.Enlist(cmd);
            return cmd;
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

            dbConfiguration.totalOpenConnection = System.Threading.Interlocked.Increment(ref dbConfiguration.totalOpenConnection);
        }

        public override string ServerVersion
        {
            get { return innerConnection.ServerVersion; }
        }

        public override ConnectionState State
        {
            get { return innerConnection.State; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
                base.Dispose(disposing);
            }
        }

        protected virtual void DisposeTransaction()
        {
            if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
            {
                try
                {
                    transaction.Commit();
                }
                catch
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {
                    }
#if DEBUG
                    throw;
#endif
                }
                finally
                {
                    transaction.Dispose();
                    transaction = null;
                }
            }
        }
    }

}
