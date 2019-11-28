using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Render;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Mapping;

namespace UWay.Skynet.Cloud.Data.Driver
{
    class MySqlDriver : AbstractDriver
    {
        public override string BuildPageQuery(long skip, long take, PagingHelper.SQLParts parts, object namedParameters)
        {
            //parts.sqlOrderBy = string.IsNullOrEmpty(parts.sqlOrderBy) ? null : OrderByAlias.Replace(parts.sqlOrderBy, "$1");
            //var sqlPage = string.Format("SELECT {4} FROM (SELECT ROW_NUMBER() OVER ({0}) poco_rn, poco_base.* \nFROM ( \n{1}) poco_base ) poco_paged \nWHERE poco_rn > {2} AND poco_rn <= {3} \nORDER BY poco_rn",
            //                                                        parts.sqlOrderBy ?? "ORDER BY (SELECT NULL /*poco_dual*/)", parts.sqlUnordered, skip, take, parts.sqlColumns);

            var sqlPage = string.Format("SELECT {3}  FROM ({0}) peta_tbl LIMIT {1},{2}", parts.sqlUnordered, skip, take - skip, parts.sqlColumns);

            return sqlPage;
            //return base.BuildPageQuery(skip, take, parts, namedParameters);
        }

        public override bool AllowsMultipleOpenReaders
        {
            get { return false; }
        }


        const string fmt = "yyyy-MM-dd HH:mm:ss";
        private void InitializeParameter(MySqlParameter p, Common.NamedParameter parameter, object value)
        {


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

                if (value != null && value is DateTime)
                {
                    var dt = (DateTime)value;
                    switch (sqlType.DbType)
                    {
                        case DBType.DateTime:
                        case DBType.NVarChar:
                            sqlType = SqlType.Get(DBType.NVarChar, 100);
                            value = dt.ToString(fmt);
                            break;

                    }
                }
            }
            p.ParameterName = parameter.Name;
            p.Value = value ?? DBNull.Value;
            ConvertDBTypeToNativeType(p, sqlType.DbType);

        }

        protected void ConvertDBTypeToNativeType(MySqlParameter p, DBType dbType)
        {

            switch (dbType)
            {
                case DBType.Binary: p.MySqlDbType = MySqlDbType.Blob; break;
                case DBType.Boolean: p.MySqlDbType = MySqlDbType.Bit; break;
                case DBType.Byte: p.MySqlDbType = MySqlDbType.Byte; break;
                case DBType.Char: p.MySqlDbType = MySqlDbType.VarChar; break;
                case DBType.DateTime: p.MySqlDbType = MySqlDbType.Date; break;
                case DBType.Decimal: p.MySqlDbType = MySqlDbType.Decimal; break;
                case DBType.Double: p.MySqlDbType = MySqlDbType.Double; break;
                case DBType.Guid: p.MySqlDbType = MySqlDbType.Blob; break;
                case DBType.Image: p.MySqlDbType = MySqlDbType.Blob; break;
                case DBType.Int16: p.MySqlDbType = MySqlDbType.Int16; break;
                case DBType.Int32: p.MySqlDbType = MySqlDbType.Int32; break;
                case DBType.Int64: p.MySqlDbType = MySqlDbType.Int64; break;
                case DBType.NChar: p.MySqlDbType = MySqlDbType.VarChar; break;
                case DBType.NText: p.MySqlDbType = MySqlDbType.Text; break;
                case DBType.NVarChar: p.MySqlDbType = MySqlDbType.VarChar; break;
                case DBType.Single: p.MySqlDbType = MySqlDbType.Binary; break;
                case DBType.Text: p.MySqlDbType = MySqlDbType.Text; break;
                case DBType.Currency: p.MySqlDbType = MySqlDbType.Decimal; break;
                case DBType.VarChar: p.MySqlDbType = MySqlDbType.VarChar; break;
            }
        }

        protected void InitializeParameter(object item, MySqlParameter p, System.Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            if (item != null && item is DateTime)
            {
                item = ((DateTime)item).ToString(fmt);
                p.DbType = DbType.String;
            }
            p.Value = item;
        }




        public override char NamedPrefix
        {
            get
            {
                return '?';
            }
        }

        public override ISqlOmRenderer Render
        {
            get
            {
                return new MySqlRenderer();
            }
        }

        //const string fmt = "yyyy-MM-dd HH:mm:ss";


        public override void AddParameter(DbCommand command, NamedParameter parameter, object value)
        {
            MySqlParameter p = new MySqlParameter();
            InitializeParameter(p, parameter, value);
            command.Parameters.Add(p);
        }

        protected override void AddParameter(DbCommand cmd, string name, object item)
        {
            MySqlParameter p = new MySqlParameter();
            p.ParameterName = name;
            if (item == null)
                p.Value = DBNull.Value;
            else
            {
                var type = item.GetType();
                if (type.IsNullable())
                    item = Converter.Convert(item, Nullable.GetUnderlyingType(type));

                InitializeParameter(item, p, type);
            }
            cmd.Parameters.Add(p);
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
            return sql.Replace("@@", "?");
        }

        public DbCommand GetCommand(DbConnection conn, string sql, NamedParameter[] namedParameters)
        {
            Guard.NotNullOrEmpty(sql, "sql");
            var cmd = conn.CreateCommand();

            cmd.CommandText = GetReplaceSql(sql);
            if (namedParameters != null)
                AddParameters(cmd, namedParameters);
            return cmd;
        }
    }
}
