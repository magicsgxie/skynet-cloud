using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class PostgreSQLDriver : AbstractDriver
    {
        private static readonly Regex OrderByAlias = new Regex(@"[\""\[\]\w]+\.([\[\]\""\w]+)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex chinaRegix = new Regex(@"[\u4e00-\u9fa5]", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        public override void AddParameter(DbCommand command, NamedParameter parameter, object value)
        {
            //IDbDataParameter p = command.CreateParameter();
            NpgsqlParameter p = new NpgsqlParameter();
            InitializeParameter(p, parameter, value);
            command.Parameters.Add(p);
        }

        protected void InitializeParameter(NpgsqlParameter p, NamedParameter parameter, object value)
        {

            p.ParameterName = parameter.Name;
            p.Value = value ?? DBNull.Value;
            var sqlType = parameter.SqlType;

            if (parameter.SqlType != null)
            {
                if (sqlType.Length > 0)
                    p.Size = sqlType.Length;
                if (sqlType.Precision > 0)
                    p.Precision = sqlType.Precision;
                if (sqlType.Scale > 0)
                    p.Scale = sqlType.Scale;
                if (sqlType.Required)
                    (p as DbParameter).IsNullable = false;

                switch (sqlType.DbType)
                {
                    case DBType.NChar:
                    case DBType.NVarChar:
                        {
                            var str = value as string;
                            if (!string.IsNullOrEmpty(str))
                            {
                                p.Size = str.Length;
                                int count = Encoding.Default.GetByteCount(str);

                                if (chinaRegix.IsMatch(str) && count > 4000 && str.Length <= 4000)
                                {
                                    //p.Size = 2000;
                                    p.Value = str.Substring(2000);
                                }

                                if (parameter.Name.StartsWith("CLOB_"))
                                {
                                    parameter.sqlType = SqlType.Get(DBType.Text, int.MaxValue);
                                }

                            }

                            break;
                        }
                    case DBType.Char:
                    case DBType.VarChar:
                        {
                            var str = value as string;
                            if (string.IsNullOrEmpty(str))
                                p.Size = 1;
                            else
                                p.Size = str.Length * 1;
                            break;
                        }
                    case DBType.Guid:
                        parameter.sqlType = SqlType.Get(DBType.Binary, 16);
                        if (value is Guid)
                            p.Value = ((Guid)value).ToByteArray();
                        else if (value != null)
                            p.Value = (((Guid?)value).Value).ToByteArray();
                        break;
                    case DBType.Binary:
                    case DBType.Image:
                        if (value is Guid)
                        {
                            p.Value = ((Guid)value).ToByteArray();
                            parameter.sqlType = SqlType.Get(DBType.Binary, 16);
                        }
                        else if (value is Guid?)
                        {
                            p.Value = ((Guid?)value).Value.ToByteArray();
                            parameter.sqlType = SqlType.Get(DBType.Binary, 16);
                        }
                        break;
                }
            }
            ConvertDBTypeToNativeType(p, parameter.sqlType.DbType);
        }

        protected void ConvertDBTypeToNativeType(NpgsqlParameter p, DBType dbType)
        {

            switch (dbType)
            {
                case DBType.Binary: p.NpgsqlDbType = NpgsqlDbType.Bytea; break;
                case DBType.Boolean: p.NpgsqlDbType = NpgsqlDbType.Boolean; break;
                case DBType.Byte: p.NpgsqlDbType = NpgsqlDbType.InternalChar; break;
                case DBType.Char: p.NpgsqlDbType = NpgsqlDbType.Char; break;
                case DBType.DateTime: p.NpgsqlDbType = NpgsqlDbType.Timestamp; break;
                case DBType.Decimal: p.NpgsqlDbType = NpgsqlDbType.Numeric; break;
                case DBType.Double: p.NpgsqlDbType = NpgsqlDbType.Double; break;
                case DBType.Guid: p.NpgsqlDbType = NpgsqlDbType.Uuid; break;
                case DBType.Image: p.NpgsqlDbType = NpgsqlDbType.Text; break;
                case DBType.Int16: p.NpgsqlDbType = NpgsqlDbType.Smallint; break;
                case DBType.Int32: p.NpgsqlDbType = NpgsqlDbType.Integer; break;
                case DBType.Int64: p.NpgsqlDbType = NpgsqlDbType.Bigint; break;
                case DBType.NChar: p.NpgsqlDbType = NpgsqlDbType.Char; break;
                case DBType.NText: p.NpgsqlDbType = NpgsqlDbType.Text; break;
                case DBType.NVarChar: p.NpgsqlDbType = NpgsqlDbType.Text; break;
                case DBType.Single: p.NpgsqlDbType = NpgsqlDbType.Real; break;
                case DBType.Text: p.NpgsqlDbType = NpgsqlDbType.Integer; break;
                //case DBType.Timestamp: p.DbType = OracleDbType.TimeStamp; break;
                case DBType.VarChar: p.NpgsqlDbType = NpgsqlDbType.Text; break;
            }
        }

        public override string BuildPageQuery(long skip, long take, PagingHelper.SQLParts parts, object namedParameters)
        {
            parts.sqlOrderBy = string.IsNullOrEmpty(parts.sqlOrderBy) ? null : OrderByAlias.Replace(parts.sqlOrderBy, "$1");
            var sqlPage = string.Format("SELECT {4} FROM (SELECT ROW_NUMBER() OVER ({0}) poco_rn, poco_base.* \nFROM ( \n{1}) poco_base ) poco_paged \nWHERE poco_rn > {2} AND poco_rn <= {3} \nORDER BY poco_rn",
                                                                    parts.sqlOrderBy ?? "ORDER BY (SELECT NULL /*poco_dual*/)", parts.sqlUnordered, skip, take, parts.sqlColumns);
            return sqlPage;
        }

        public override DbCommand CreateCommand(DbConnection conn, string sql, object namedParameters)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            var cmd = conn.CreateCommand();

            cmd.CommandText = GetReplaceSql(sql);
            if (namedParameters != null)
                AddParameters(cmd, namedParameters);
            return cmd;
        }

        private string GetReplaceSql(string sql)
        {
            if (NamedPrefix != '@')
                sql = ParameterHelper.rxParamsPrefix.Replace(sql, m => NamedPrefix + m.Value.Substring(1));
            return sql.Replace("@@", "@");
        }

    }
}
