using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Render;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Mapping;

namespace UWay.Skynet.Cloud.Data.Driver
{
    /// <summary>
    /// Oracle 驱动
    /// </summary>
    class OracleODPDriver : OracleDriver
    {
        public override ISqlOmRenderer Render
        {
            get
            {
                return new OracleRenderer();
            }
        }


        private static readonly Regex OrderByAlias = new Regex(@"[\""\[\]\w]+\.([\[\]\""\w]+)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex chinaRegix = new Regex(@"[\u4e00-\u9fa5]", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        public override void AddParameter(DbCommand command, NamedParameter parameter, object value)
        {
            //Oracle.ManagedDataAccess.Client.OracleConnection
            //IDbDataParameter p = command.CreateParameter();
            OracleParameter p = new OracleParameter();
            InitializeParameter(p, parameter, value);
            command.Parameters.Add(p);
        }

        protected override void AddParameter(DbCommand cmd, string name, object item)
        {
            OracleParameter p = new OracleParameter();
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


        protected void InitializeParameter(OracleParameter p, NamedParameter parameter, object value)
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

        protected void ConvertDBTypeToNativeType(OracleParameter p, DBType dbType)
        {
            
            switch (dbType)
            {
                case DBType.Binary: p.OracleDbType = OracleDbType.Blob; break;
                case DBType.Boolean: p.OracleDbType = OracleDbType.Boolean; break;
                case DBType.Byte: p.OracleDbType = OracleDbType.Byte; break;
                case DBType.Char: p.OracleDbType = OracleDbType.Char; break;
                case DBType.DateTime: p.OracleDbType = OracleDbType.Date; break;
                case DBType.Decimal: p.OracleDbType = OracleDbType.Decimal; break;
                case DBType.Double: p.OracleDbType = OracleDbType.Double; break;
                case DBType.Guid: p.OracleDbType = OracleDbType.Blob; break;
                case DBType.Image: p.OracleDbType = OracleDbType.Blob; break;
                case DBType.Int16: p.OracleDbType = OracleDbType.Int16; break;
                case DBType.Int32: p.OracleDbType = OracleDbType.Int32; break;
                case DBType.Int64: p.OracleDbType = OracleDbType.Int64; break;
                case DBType.NChar: p.OracleDbType = OracleDbType.NChar; break;
                case DBType.NText: p.OracleDbType = OracleDbType.NClob; break;
                case DBType.NVarChar: p.OracleDbType = OracleDbType.NVarchar2; break;
                case DBType.Single: p.OracleDbType = OracleDbType.Single; break;
                case DBType.Text: p.OracleDbType = OracleDbType.Clob; break;
                case DBType.Currency: p.OracleDbType = OracleDbType.Decimal; break;
                case DBType.VarChar: p.OracleDbType = OracleDbType.Varchar2; break;
            }
        }

        protected override void InitializeParameter(object item, DbParameter p, Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.SByte:
                    p.DbType = DbType.Byte;
                    item = (byte)item;
                    break;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
               
                    p.DbType = DbType.Int32;
                    item = Convert.ToInt32(item);
                    break;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    p.DbType = DbType.Int64;
                    item = Convert.ToInt64(item);
                    break;
                default:
                    if (item is Guid)
                    {
                        p.DbType = DbType.Binary;
                        item = ((Guid)item).ToByteArray();
                    }
                    break;

            }
            p.Value = item;
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

        public override void AddParameters(DbCommand cmd, object namedParameters)
        {
            var type = namedParameters.GetType();
            var dic = namedParameters as IDictionary<string, object>;
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                    this.AddParameter(cmd, key, dic[key]);
                return;
            }

            var nvc = namedParameters as NameValueCollection;
            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                    AddParameter(cmd, key, nvc[key]);
                return;
            }

            var hs = namedParameters as Hashtable;
            if (hs != null)
            {
                foreach (string key in hs.Keys.OfType<string>())
                    AddParameter(cmd, key, hs[key]);
                return;
            }



            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var items = from m in type
                       .GetFields(bindingFlags)
                       .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                       .Where(p => !p.Name.Contains("k__BackingField"))
                       .Cast<MemberInfo>()
                       .Union(type
                           .GetProperties(bindingFlags)
                           .Where(p => p.CanRead)
                           .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                           .Cast<MemberInfo>()
                           ).Distinct()
                        select m;

            foreach (var item in items.ToArray())
                AddParameter(cmd, item.Name, item.GetGetter()(namedParameters));
        }

    }
}
