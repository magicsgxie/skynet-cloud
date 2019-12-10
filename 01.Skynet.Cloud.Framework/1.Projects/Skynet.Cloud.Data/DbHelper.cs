using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Driver;
using UWay.Skynet.Cloud.ExceptionHandle;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    class DbHelper : ConnectionHost, IDbHelper
    {
        internal DbConfiguration dbConfiguration;

        public IDriver Driver { get; set; }

        public DbConnection Connection { get { return connection; } }

        public DbConfiguration DbConfiguration { get { return dbConfiguration; } }

        public CommandType CommandType;

        ILogger log
        {
            get
            {
                return dbConfiguration.sqlLogger();
            }
        }

        /// <summary>
        /// 创建参数.
        /// </summary>
        /// <param name="name">参数名称.</param>
        /// <param name="value">参数值.</param>
        /// <returns></returns>
        public DbParameter Parameter(string name, object value)
        {
            var p = dbConfiguration.DbProviderFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return p;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="namedParameters">动态参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        public Task<int> ExecuteNonQueryAsync(string sql, dynamic namedParameters, bool isAutoClose = true)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {

                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmdParams = this.GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {

                //log.Log(LogLevel.Error, ex, GetSqlLogInfo("ExecuteNonQuery: sql:{0}\r\nparamters:{1}", sql, jsonString));
                err = ex;
                throw new PersistenceException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                //AddAttachInfo();
                //log.Log();
            }

        }


        /// <summary>
        /// 同步执行
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="namedParameters">动态参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, dynamic namedParameters, bool isAutoClose = true)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmdParams = GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    cmdSql = cmd.CommandText;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //string jsonString = namedParameters.ToJson();
                //log.Log(LogLevel.Error,ex, GetSqlLogInfo("ExecuteNonQuery: sql:{0}\r\nparamters:{1}", sql, jsonString));
                err = ex;
                throw new PersistenceException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                //AddAttachInfo();
                //log.Log();
            }

        }


        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="namedParameters">动态参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string sql, dynamic namedParameters, bool isAutoClose = true)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmdParams = GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();


                    //log.Log(LogLevel.Information, GetSqlLogInfo("ExecuteReader: sql:{0}\r\nparamters:{1}", sqlString, jsonString));
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }


        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="namedParameters">动态参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        public Task<DbDataReader> ExecuteReaderAsync(string sql, dynamic namedParameters, bool isAutoClose = true)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmdParams = GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();


                    //log.Log(LogLevel.Information, GetSqlLogInfo("ExecuteReader: sql:{0}\r\nparamters:{1}", sqlString, jsonString));
                    return cmd.ExecuteReaderAsync();
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                //err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }


        /// <summary>
        /// 同步执行查询获取DataTable
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="namedParameters">动态参数</param>
        /// <param name="isAutoClose">是否自动关闭连接</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, dynamic namedParameters, bool isAutoClose = true)
        {

            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = sql;
            string cmdParams = "";
            try
            {
                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmdParams = GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    cmd.CommandType = CommandType;
                    var adp = this.dbConfiguration.DbProviderFactory.CreateDataAdapter();
                    adp.SelectCommand = cmd;
                    var tb = new DataTable("Table1");
                    adp.Fill(tb);
                    //if(cmd.Parameters != null && cmd.Parameters)

                    //log.Log(LogLevel.Information, GetSqlLogInfo("ExecuteReader: sql:{0}\r\nparamters:{1}", sqlString, jsonString));
                    return tb;
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        private string GetParameters(DbParameterCollection parameters)
        {
            var cmdParameters = new StringBuilder();
            if (parameters != null && parameters.Count > 0)
            {
                foreach (DbParameter item in parameters)
                {
                    cmdParameters.AppendLine(item.ParameterName + ":" + item.Value);
                }
            }
            return cmdParameters.ToString();
        }

        private void LogSql(Exception ex, string sql, string params1)
        {
            if (ex == null)
            {
                LogInformation(sql, params1);
            }
            else
            {
                LogError(ex, sql, params1);
            }
        }

        private void LogInformation(string sql, string params1)
        {

            log.Log(LogLevel.Information, GetSqlLogInfo("ExecuteNonQuery: sql:{0}\r\nparamters:{1}", sql, params1));
        }

        private void LogError(Exception ex, string sql, string params1)
        {
            log.Log(LogLevel.Error, ex, GetSqlLogInfo("ExecuteNonQuery: sql:{0}\r\nparamters:{1}", sql, params1));
        }

        private string GetSqlLogInfo(string format, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------Sql Begin----------------");
            sb.AppendLine("-- Key:" + DbConfiguration.ConnectionString.Split(';')[0]);
            sb.AppendLine("-- DbProviderName:" + DbConfiguration.DbProviderName);
            sb.AppendFormat(format, args).AppendLine();
            sb.AppendLine("-------------------Sql End----------------");
            return sb.ToString();
        }


        public DataSet ExecuteDataSet(string sql, dynamic namedParameters, bool isAutoClose = true)
        {

            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (var cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {
                    cmd.CommandType = CommandType;
                    var adp = this.dbConfiguration.DbProviderFactory.CreateDataAdapter();
                    adp.SelectCommand = cmd;
                    var ds = new DataSet();
                    adp.Fill(ds);
                    cmdParams = GetParameters(cmd.Parameters);

                    cmdSql = cmd.CommandText;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        public Task<object> ExecuteScalarAsync(string sql, dynamic namedParameters, bool isAutoClose = true)
        {

            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (DbCommand cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {

                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    cmdParams = GetParameters(cmd.Parameters);

                    cmdSql = cmd.CommandText;
                    return cmd.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                //AddAttachInfo();
                ////log.AddMessage("")
                //log.Log();
            }
        }


        public object ExecuteScalar(string sql, dynamic namedParameters, bool isAutoClose = true)
        {

            Guard.NotNullOrEmpty(sql, "sql");
            Exception err = null;
            string cmdSql = string.Empty;
            string cmdParams = string.Empty;
            try
            {
                using (var cmd = Driver.CreateCommand(connection, sql, namedParameters))
                {

                    cmd.CommandType = CommandType;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    cmdParams = this.GetParameters(cmd.Parameters);
                    cmdSql = cmd.CommandText;
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                err = ex;
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParams);
                err = null;
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        /// <inheritdoc/>
        public DataSourceTableResult ExecutePage(string sql, long skip, long take, dynamic nameparameters, bool isAutoClose = true)
        {

            Guard.NotNullOrEmpty(sql, "sql");
            PagingHelper.SQLParts parts;

            DataSourceTableResult ds = new DataSourceTableResult();
            if (!PagingHelper.SplitSQL(sql, out parts))
            {
                throw new Exception("Unable to parse SQL statement for paged query");
            }

            var sqlCount = parts.sqlCount;
            var pageSql = sql;
            if (take > 0)
            {
                pageSql = Driver.BuildPageQuery(skip, take, parts, nameparameters);
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();
            var tempRowCount = 0;
            DataTable dt = null;
            try
            {
                if (take > 0)
                {
                    if (this.Driver.AllowsMultipleOpenReaders == false)
                    {
                        tempRowCount = (int)this.ExecuteScalar(parts.sqlCount, nameparameters, false);
                        dt = this.ExecuteDataTable(pageSql, nameparameters, false);
                    }
                    else
                    {
                        Parallel.Invoke(() => tempRowCount = (int)this.ExecuteScalar(parts.sqlCount, nameparameters, false), () => dt = this.ExecuteDataTable(pageSql, nameparameters, false));
                    }
                }
                else
                {
                    dt = this.ExecuteDataTable(pageSql, nameparameters, false);
                }
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                if (isAutoClose == true)
                {
                    if (this.connection.State != ConnectionState.Closed)
                    {
                        this.connection.Close();
                    }
                }
            }

            ds.Total = tempRowCount;
            ds.Data = dt;
            return ds;
        }

        /// <inheritdoc/>
        public DataSourceResult ExecutePage<T>(string sql, long skip, long take, dynamic nameparameters, bool isAutoClose = true) where T : new()
        {
            Guard.NotNullOrEmpty(sql, "sql");
            PagingHelper.SQLParts parts;

            DataSourceResult ds = new DataSourceResult();
            if (!PagingHelper.SplitSQL(sql, out parts))
            {
                throw new Exception("Unable to parse SQL statement for paged query");
            }

            var sqlCount = parts.sqlCount;
            var pageSql = sql;
            if (take > 0)
            {
                pageSql = Driver.BuildPageQuery(skip, take, parts, nameparameters);
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            var tempRowCount = 0;
            DbDataReader dr = null;
            try
            {
                if (take > 0)
                    System.Threading.Tasks.Parallel.Invoke(() => tempRowCount = (int)ExecuteScalar(parts.sqlCount, nameparameters, false), () => dr = ExecuteReader(pageSql, nameparameters, false));
                else
                    dr = ExecuteReader(pageSql, nameparameters, false);
            }
            catch (Exception ex)
            {
                throw new QueryException(ex.Message, ex);
            }
            finally
            {
                if (isAutoClose == true)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }

            ds.Total = tempRowCount;
            ds.Data = dr.ToList<T>();
            return ds;
        }
    }
}
