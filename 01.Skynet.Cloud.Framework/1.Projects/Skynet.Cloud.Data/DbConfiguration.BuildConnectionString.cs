using System;

namespace UWay.Skynet.Cloud.Data
{
    partial class DbConfiguration
    {
        #region Build SQLExpress Connection String
        /// <summary>
        /// 构造SQLExpress Connection String
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <returns></returns>
        public static string BuildSQLExpressConnectionString(string databaseFile)
        {
            return string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;User Instance=True;MultipleActiveResultSets=true;AttachDbFilename='{0}'", databaseFile);
        }
        #endregion

        #region Build Access Connection String
        /// <summary>
        /// 构造Access连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <returns></returns>
        public static string BuildAccessConnectionString(string databaseFile)
        {
            string dbLower = databaseFile.ToLower();
            if (dbLower.Contains(".mdb"))
                return BuildAccessConnectionString(AccessOleDbProvider2000, databaseFile);
            else if (dbLower.Contains(".accdb"))
                return BuildAccessConnectionString(AccessOleDbProvider2007, databaseFile);
            else
                throw new InvalidOperationException(string.Format("Unrecognized file extension on database file '{0}'", databaseFile));
        }

        private static string BuildAccessConnectionString(string provider, string databaseFile)
        {
            return string.Format("Provider={0};Data Source={1}", provider, databaseFile);
        }

        static readonly string AccessOleDbProvider2000 = "Microsoft.Jet.OLEDB.4.0";
        static readonly string AccessOleDbProvider2007 = "Microsoft.ACE.OLEDB.12.0";
        #endregion

        #region Build SqlCe Connection String
        /// <summary>
        /// 构造SqlCe连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <returns></returns>
        public static string BuildSqlCeConnectionString(string databaseFile)
        {
            return string.Format(@"Data Source='{0}'", databaseFile);
        }
        #endregion

        #region Build SQLite ConnectionString
        /// <summary> 
        /// 构造SQLite连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(string databaseFile)
        {
            return string.Format("Data Source={0};", databaseFile);
        }

        /// <summary>
        /// 构造SQLite连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(string databaseFile, string password)
        {
            return string.Format("Data Source={0};Password={1};", databaseFile, password);
        }

        /// <summary>
        /// 构造SQLite连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="failIfMissing"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(string databaseFile, bool failIfMissing)
        {
            return string.Format("Data Source={0};FailIfMissing={1};", databaseFile, failIfMissing ? bool.TrueString : bool.FalseString);
        }

        /// <summary>
        /// 构造SQLite连接字符串
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="password"></param>
        /// <param name="failIfMissing"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(string databaseFile, string password, bool failIfMissing)
        {
            return string.Format("Data Source={0};Password={1};FailIfMissing={2};", databaseFile, password, failIfMissing ? bool.TrueString : bool.FalseString);
        }
        #endregion

    }
}
