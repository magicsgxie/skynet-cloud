using System;

namespace UWay.Skynet.Cloud.Data.Mapping.Fluent
{
    /// <summary>
    /// Table 映射表达式
    /// </summary>
    public class TableExpression
    {

        internal TableAttribute Attribute;
        internal TableExpression()
        {
            Attribute = new TableAttribute();
        }

        /// <summary>
        /// 设置Table为只读的
        /// </summary>
        /// <returns></returns>
        public TableExpression Readonly()
        {
            Attribute.Readonly = true;
            return this;
        }

        /// <summary>
        /// 设置Table的Schema
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public TableExpression Schema(string schema)
        {
            Guard.NotNullOrEmpty(schema, "schema");
            Attribute.Schema = schema;
            return this;
        }

        /// <summary>
        /// 设置Table的DatabaseName
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public TableExpression Database(string databaseName)
        {
            Guard.NotNullOrEmpty(databaseName, "databaseName");
            Attribute.DatabaseName = databaseName;
            return this;
        }

        /// <summary>
        /// 设置Database的Server
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public TableExpression Server(string serverName)
        {
            Guard.NotNullOrEmpty(serverName, "databaseName");
            Attribute.Server = serverName;
            return this;
        }
    }
}
