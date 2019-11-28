using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data.Render;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SqlRenderType
    {
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// Mysql
        /// </summary>
        MySql,

        /// <summary>
        /// MSSQL
        /// </summary>
        SqlServer
    }

    /// <summary>
    /// Sql生成工厂
    /// </summary>
    public class SqlRenderFactories
    {
        static IDictionary<SqlRenderType, ISqlOmRenderer> renders = new Dictionary<SqlRenderType, ISqlOmRenderer>();


        static SqlRenderFactories()
        {
            renders.Add(SqlRenderType.Oracle, new OracleRenderer());
            renders.Add(SqlRenderType.MySql, new MySqlRenderer());
            renders.Add(SqlRenderType.SqlServer, new SqlServerRenderer());
        }

        public static ISqlOmRenderer GetRender(SqlRenderType render)
        {
            if (renders.Keys.Contains(render))
                return renders[render];
            return renders[render];
        }
    }
}
