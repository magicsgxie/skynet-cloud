using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data.Linq
{
    public static class IfWhereCondition
    {
        /// <summary>
        /// Where 扩展，当满足conditon条件时则Append Where条件
        /// </summary>
        /// <typeparam name="T">查询类型</typeparam>
        /// <param name="query"><see cref="IQueryable"/></param>
        /// <param name="codition">条件</param>
        /// <param name="where">查询条件</param>
        /// <returns><see cref="IQueryable"/></returns>
        /// <example>
        /// <code><![CDATA[
        ///  var query = this.CreateQuery<CollectorInfo>();
        ///
        /// query = query.IfWhere(!string.IsNullOrEmpty(collectorCode), c => c.CollectorCode == collectorCode)
        ///     .IfWhere(type>0 ,c => c.Type == type )
        ///     .IfWhere(!string.IsNullOrEmpty(blueTooth),c => c.BlueTooth == blueTooth)
        ///     .Paging(paging);
        /// ]]>
        /// </code>
        /// </example>
        public static IQueryable<T> IfWhere<T>(this IQueryable<T> query, bool codition, Expression<Func<T, bool>> where)
        {
            if (codition)
                return query.Where(where);
            return query;
        }
    }
}
