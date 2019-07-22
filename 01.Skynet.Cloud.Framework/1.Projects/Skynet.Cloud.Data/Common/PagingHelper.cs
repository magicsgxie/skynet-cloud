using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data.Common
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    public class PagingHelper
    {
        /// <summary>
        /// 查询列匹配方式
        /// </summary>
        public static Regex rxColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// 排序列匹配方式
        /// </summary>
        public static Regex rxOrderBy = new Regex(@"\bORDER\s+BY\s+(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\.\[\] ""`])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\.\[\] ""`])+(?:\s+(?:ASC|DESC))?)*(?!.*FROM)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// 分页SQL
        /// </summary>
        public struct SQLParts
        {
            /// <summary>
            /// SQL
            /// </summary>
            public string sql;

            /// <summary>
            /// COUNT SQL
            /// </summary>
            public string sqlCount;

            /// <summary>
            /// SQL SELECT Remove
            /// </summary>
            public string sqlSelectRemoved;

            /// <summary>
            /// sql排序
            /// </summary>
            public string sqlOrderBy;

            /// <summary>
            /// SQL不排序
            /// </summary>
            public string sqlUnordered;

            /// <summary>
            /// SQL列
            /// </summary>
            public string sqlColumns;
        }

        /// <summary>
        /// 分割SQL
        /// </summary>
        /// <param name="sql">主SQL</param>
        /// <param name="parts">分页SQL信息</param>
        /// <returns></returns>
        public static bool SplitSQL(string sql, out SQLParts parts)
        {
            parts.sql = sql;
            parts.sqlSelectRemoved = null;
            parts.sqlCount = null;
            parts.sqlOrderBy = null;
            parts.sqlUnordered = sql.Trim().Trim(';');
            parts.sqlColumns = "*";

            // Extract the columns from "SELECT <whatever> FROM"
            var m = rxColumns.Match(sql);
            if (!m.Success) return false;

            // Save column list  [and replace with COUNT(*)]
            Group g = m.Groups[1];
            parts.sqlSelectRemoved = sql.Substring(g.Index);

            // Look for the last "ORDER BY <whatever>" clause not part of a ROW_NUMBER expression
            m = rxOrderBy.Match(parts.sql);
            if (m.Success)
            {
                g = m.Groups[0];
                parts.sqlOrderBy = g.ToString();
                parts.sqlUnordered = rxOrderBy.Replace(parts.sqlUnordered, "");
            }

            parts.sqlCount = string.Format(@"SELECT COUNT(*) FROM ({0}) peta_tbl", parts.sqlUnordered);

            return true;
        }


    }
}
