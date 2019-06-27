using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;
using Microsoft.Extensions.Logging;

namespace UWay.Skynet.Cloud.Data.Exceptions
{
    class ExceptionHelper
    {
        public const string SQLNotAvailable = "SQL not available";

        public static Exception Convert(ILogger log, ISQLExceptionConverter converter, DbExceptionContextInfo exceptionContextInfo)
        {
            //if (exceptionContextInfo == null)
            //{
            //    throw new ArgumentNullException("The argument exceptionContextInfo is null.");
            //}
            var sql = TryGetActualSqlQuery(exceptionContextInfo.SqlException, exceptionContextInfo.Sql);
            //log.Log(LogLevel.Error, exceptionContextInfo.SqlException,
            //                                   ExtendMessage(exceptionContextInfo.Message, sql));
            return converter.Convert(exceptionContextInfo);
        }

        public static Exception Convert(ILogger log, ISQLExceptionConverter converter,DbExceptionContextInfo exceptionContextInfo,
                                           object[] parameterValues, NamedParameter[] namedParameters)
        {
            var sql = TryGetActualSqlQuery(exceptionContextInfo.SqlException, exceptionContextInfo.Sql);
            string extendMessage = ExtendMessage(
                exceptionContextInfo.Message, 
                sql != null ? sql.ToString() : null, 
                exceptionContextInfo.Entity,
                parameterValues, 
                namedParameters);
            //log.Log(LogLevel.Error,exceptionContextInfo.SqlException, extendMessage);

            return converter.Convert(exceptionContextInfo);
        }

        public static DbException ExtractDbException(Exception sqlException)
        {
            Exception baseException = sqlException;
            var result = sqlException as DbException;
            while (result == null && baseException != null)
            {
                baseException = baseException.InnerException;
                result = baseException as DbException;
            }
            return result;
        }

        public static string ExtendMessage(string message, string sql, object entity = null, object[] parameterValues = null, NamedParameter[] parameters = null)
        {
            var sb = new StringBuilder(512).AppendLine();
            if (entity != null)
            {
                try
                {
                    sb.Append("[Entity Name:").Append(entity.GetType().Name).AppendLine("]");
                    sb.AppendLine(entity.JsonSerialize());
                }
                catch
                {
                }
            }

            sb.Append(message).Append(Environment.NewLine).Append("[ ").Append(sql ?? SQLNotAvailable).Append(" ]");

            if (parameters != null && parameters.Length > 0)
            {
                sb.AppendLine();
                for (int i = 0, n = parameters.Length; i < n; i++)
                {
                    object value = parameterValues[i] ?? "null";
                    sb.Append("  ").Append("Name:").Append(parameters[i].Name).Append(" - Value:").Append(value);
                    sb.AppendLine();
                }
            }
           

            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        public static string TryGetActualSqlQuery(Exception sqle, string sql)
        {
            var query = (string)sqle.Data["actual-sql-query"];
            return query ?? sql;
        }
    }
}
