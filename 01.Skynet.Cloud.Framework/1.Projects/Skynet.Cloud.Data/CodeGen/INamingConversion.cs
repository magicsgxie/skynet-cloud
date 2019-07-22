using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data.Schema;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    /// <summary>
    /// 命名约定
    /// </summary>
    public interface INamingConversion
    {
        /// <summary>
        /// 表名转集合名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string QueryableName(string tableName);
        /// <summary>
        /// 表名转类名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string ClassName(string tableName);

        /// <summary>
        /// 列名转字段名
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        string FieldName(string columnName);

        /// <summary>
        /// 列名属性名转
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        string PropertyName(string columnName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        string DataType(IColumnSchema col);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fk"></param>
        /// <returns></returns>
        string ManyToOneName(IRelationSchema fk);
    }
}
