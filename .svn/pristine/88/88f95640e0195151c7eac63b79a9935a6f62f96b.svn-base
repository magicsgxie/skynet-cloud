using System.Data;
using System.Data.Common;
using UWay.Skynet.Cloud.Disposables;

namespace UWay.Skynet.Cloud.Data
{
    class ConnectionHost : BooleanDisposable
    {
        internal bool HasSelfCreateConnection;
        internal DbConnection connection;

        void Close()
        {
            
            if (!HasSelfCreateConnection) return;
            if (connection != null
                && connection.State != ConnectionState.Closed
                /* && connection is ConnectionWrapper*/)
            {
                try
                {
                    connection.Close();
                }
                catch { }
                connection.Dispose();
                connection = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }
    }
}
