using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using UWay.Skynet.Cloud.Data.Exceptions;
using UWay.Skynet.Cloud.ExceptionHandle;

namespace UWay.Skynet.Cloud.Data.Common
{
    internal class ExecutionService
    {
        InternalDbContext dbContext;
        int rowsAffected;
        ILogger log;
        ISQLExceptionConverter exceptionConverter;

        static long counter;

        public ExecutionService(InternalDbContext dbContext)
        {
            this.dbContext = dbContext;
            log = dbContext.Log;

            if (exceptionConverter == null)
            {
                exceptionConverter = new SqlExceptionConverter();
            }
        }

        public IDbContext DbContext
        {
            get { return this.dbContext; }
        }

        public int RowsAffected
        {
            get { return this.rowsAffected; }
        }

        IDataReader ExecuteReader(DbCommand command)
        {
            DbDataReader reader = null;
            Exception err = null;
            var cmdSql = command.CommandText;
            var cmdParameters = string.Empty;
            if (command.Parameters != null && command.Parameters.Count > 0)
                cmdParameters = command.Parameters.JsonSerialize();
            try
            {
                reader = command.ExecuteReader();
            }
            catch (DbException ex)
            {
                var exceptionContext = new DbExceptionContextInfo
                {
                    SqlException = new QueryException(ex.Message, ex),
                    Message = "unable to select data.",
                    Sql = command.CommandText,
                    Paramsters = (command.Parameters == null ? "" :command.Parameters.JsonSerialize())

                };
                err = exceptionContext.SqlException;
                
                throw ExceptionHelper.Convert(log,exceptionConverter, exceptionContext);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParameters);
            }

            if (!this.dbContext.Driver.AllowsMultipleOpenReaders)
            {
                var table = reader.ToDataTable();
                reader = table.CreateDataReader();
            }

            return reader;
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
            sb.AppendLine("-- Key:" + dbContext.DbConfiguration.ConnectionString.Split(';')[0]);
            sb.AppendLine("-- DbProviderName:" + dbContext.DbConfiguration.DbProviderName);
            sb.AppendFormat(format, args).AppendLine();
            sb.AppendLine("-------------------Sql End----------------");
            return sb.ToString();
        }

        public IEnumerable<T> Query<T>(QueryContext<T> q)
        {
            
            try
            {
                using (var cmd = this.GetCommand(q.CommandText, q.Parameters, q.ParameterValues))
                {
                    
                    using (var reader = ExecuteReader(cmd))
                    {
                        
                        return Project(reader, q.FnProjector);
                        
                    }
                }
            }
            catch (ConnectionException ex)
            {
                throw ex;
            }
            catch (QueryException ex)
            {
                
                throw ex;
            }
            catch (ProjectionException ex)
            {
                
                throw ex;
            }

            catch (Exception ex)
            {
                
                throw new QueryException(ex.Message, ex);
            }
            
        }

        IEnumerable<T> Project<T>(IDataReader reader, Func<FieldReader, T> fnProjector)
        {
            var items = new List<T>();
            var freader = new FieldReader(reader);
            while (reader.Read())
            {
                T item = default(T);
                try
                {
                    item = fnProjector(freader);
                    items.Add(item);
                }
                catch (Exception ex)
                {
                    var exceptionContext = new DbExceptionContextInfo
                    {
                        SqlException = new ProjectionException(ex.Message, ex),
                        //Message = "data bind exception.",
                        Message = ex.Message,
                    };
                    throw ExceptionHelper.Convert(log, exceptionConverter, exceptionContext);
                }
                finally
                {
                }
               
            }
            
            
            return items;
        }

        public int ExecuteNonQuery(CommandContext ctx)
        {
            //this.log.AddCommand(ctx.CommandText, ctx.Parameters, ctx.ParameterValues);

            using (var cmd = this.GetCommand(ctx.CommandText, ctx.Parameters, ctx.ParameterValues))
            {
                var cmdSql = ctx.CommandText;
                var cmdParameters = ctx.Parameters.JsonSerialize();
                Exception err = null;
                try
                {
                    this.rowsAffected = cmd.ExecuteNonQuery();

                    if (ctx.SupportsVersionCheck && rowsAffected == 0)
                        throw new ConcurrencyException(ctx.Instance, ctx.OperationType);
                }
                catch (Exception ex)
                {
                    err = ex;
                    var exceptionContext = new DbExceptionContextInfo
                    {
                        EntityName = ctx.EntityType.FullName,
                        //Message = "unable to select data.",
                        Entity = ctx.Instance,
                        Sql = cmd.CommandText,
                        Paramsters = cmdParameters
                    };

                    switch (ctx.OperationType)
                    {
                        case OperationType.Insert:
                            exceptionContext.SqlException = new InsertException(ex.Message, ex);
                            break;
                        case OperationType.Update:
                            exceptionContext.SqlException = new UpdateException(ex.Message, ex);
                            break;
                        case OperationType.Delete:
                            exceptionContext.SqlException = new DeleteException(ex.Message, ex);
                            break;
                    }
                    throw ExceptionHelper.Convert(log, exceptionConverter, exceptionContext, ctx.ParameterValues, ctx.Parameters);
                }
                finally
                {
                    //log.Log();
                    LogSql(err, cmdSql, cmdParameters);
                }
            }

          
            return this.rowsAffected;
        }

        public IEnumerable<int> Batch(BatchContext q)
        {
            return this.ExecuteBatch(q.CommandText, q.Parameters, q.ParameterSets);
        }

        IEnumerable<int> ExecuteBatch(string commandText, NamedParameter[] parameters, IEnumerable<object[]> paramSets)
        {

            DataTable dataTable = new DataTable();
            for (int i = 0, n = parameters.Length; i < n; i++)
            {
                var qp = parameters[i];
                dataTable.Columns.Add(qp.Name, UWay.Skynet.Cloud.Reflection.TypeHelper.GetNonNullableType(qp.Type));
            }

            var cmdSql = commandText;
            var cmdParameters = new StringBuilder();
            Exception err = null;
            cmdParameters.AppendLine(parameters.JsonSerialize());
            foreach (var paramValues in paramSets)
            {
                dataTable.Rows.Add(paramValues);
                cmdParameters.AppendLine(paramValues.JsonSerialize());
                
            }

            var count = dataTable.Rows.Count;
            var result = new int[count];
            if (count > 0)
            {
                int n;



                try
                {
                    var dataAdapter = this.dbContext.dbConfiguration.DbProviderFactory.CreateDataAdapter();
                    var cmd = this.GetCommand(commandText, parameters, null);
                    for (int i = 0, m = parameters.Length; i < m; i++)
                        cmd.Parameters[i].SourceColumn = parameters[i].Name;
                    dataAdapter.InsertCommand = cmd;//系统会根据InsertCommand自动生成相关的DeleteCommand等
                    dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    n = dataAdapter.Update(dataTable);
                }
                catch (ConnectionException ex)
                {
                    err = ex;
                    throw;
                }
                catch (DBConcurrencyException ex)
                {
                    err = ex;
                    throw;
                }
                catch (Exception ex)
                {
                    err = ex;
                    var exceptionContext = new DbExceptionContextInfo
                    {
                        Sql = commandText,
                        SqlException = new PersistenceException(ex.Message, ex),
                    };
                    //log.LogError("Batch update operation unkown exception", ex);
                    throw ExceptionHelper.Convert(log, exceptionConverter, exceptionContext);
                }
                finally {
                    LogSql(err, cmdSql, cmdParameters.ToString());
                }

                for (int i = 0; i < count; i++)
                    result[i] = i < n ? 1 : 0;
                dataTable.Rows.Clear();
            }


           

            return result;
        }

        public IEnumerable<T> Batch<T>(BatchContext<T> q)
        {
            return this.ExecuteBatch(q.CommandText, q.Parameters, q.ParameterSets, q.FnProjector);
        }

        IEnumerable<T> ExecuteBatch<T>(string commandText, NamedParameter[] parameters, IEnumerable<object[]> paramSets, Func<FieldReader, T> fnProjector)
        {
            //log.AddMessage("开始时间："+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            //log.AddCommand(commandText, parameters, null);

            var cmdSql = string.Empty;
            var cmdParameters = string.Empty;
            Exception err = null;
            var items = new List<T>();

            try
            {
                using (DbCommand cmd = this.GetCommand(commandText, parameters, null))
                {
                    cmdSql = cmd.CommandText;
                    
                    cmdParameters  = paramSets.JsonSerialize();
                    foreach (var paramValues in paramSets)
                    {
                        cmd.Parameters.Clear();
                        SetParameterValues(parameters, cmd, paramValues);

                        using (var reader = cmd.ExecuteReader())
                        {
                            var freader = new FieldReader(reader);
                            if (reader.Read())
                                items.Add(fnProjector(freader));
                            else
                                items.Add(default(T));
                        }
                    }
                }
            }
            catch (ConnectionException ex)
            {
                err = ex;
                throw;
            }
            catch (DBConcurrencyException ex)
            {
                err = ex;
                throw;
            }
            catch (Exception ex)
            {
                err = ex;
                var exceptionContext = new DbExceptionContextInfo
                {
                    Sql = commandText,
                    SqlException = new PersistenceException(ex.Message, ex),
                };
                throw ExceptionHelper.Convert(log, exceptionConverter, exceptionContext);
            }
            finally
            {
                LogSql(err, cmdSql, cmdParameters);
            }

            return items;
        }

        static bool hasCreatingDatabase;
        protected void CreateDatabase()
        {
            hasCreatingDatabase = true;
            try
            {
                dbContext.dbConfiguration.CreateDatabase();
            }
            catch
            {
                throw;
            }
            finally
            {
                hasCreatingDatabase = false;
            }
            switch (dbContext.dbConfiguration.DbProviderName)
            {
                case DbProviderNames.SqlServer:
                    System.Threading.Thread.Sleep(6000);
                    break;
            }

        }

        static long Counter
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return counter += 1;
            }
        }

        DbCommand GetCommand(string commandText, NamedParameter[] parameters, object[] paramValues)
        {
            if (Counter == 1)
            {
                var dbExists = dbContext.dbConfiguration.DatabaseExists();
                if (dbExists)
                    dbContext.dbConfiguration.ValidateSchema();
                else if (!hasCreatingDatabase)
                    CreateDatabase();
            }


            var cmd = this.dbContext.Connection.CreateCommand();
            cmd.CommandText = commandText;
            SetParameterValues(parameters, cmd, paramValues);
            if (cmd.Connection.State != ConnectionState.Open)
            {
                try
                {
                    this.dbContext.Connection.Open();
                }
                catch (Exception ex)
                {
                    log.LogError("Get Command Connection Open", ex);
                    throw new ConnectionException(ex.Message, ex);
                }
                finally
                {
                }
            }

            return cmd;
        }

        void SetParameterValues(NamedParameter[] parameters, DbCommand command, object[] paramValues)
        {
            if (parameters.Length > 0 && command.Parameters.Count == 0)
            {
                for (int i = 0, n = parameters.Length; i < n; i++)
                {
                    dbContext.Driver.AddParameter(command, parameters[i], paramValues != null ? paramValues[i] : null);
                }
            }
            else if (paramValues != null)
            {
                for (int i = 0, n = command.Parameters.Count; i < n; i++)
                {
                    DbParameter p = command.Parameters[i];
                    if (p.Direction == System.Data.ParameterDirection.Input
                     || p.Direction == System.Data.ParameterDirection.InputOutput)
                    {
                        p.Value = paramValues[i] ?? DBNull.Value;
                    }
                }
            }
        }
    }
}
