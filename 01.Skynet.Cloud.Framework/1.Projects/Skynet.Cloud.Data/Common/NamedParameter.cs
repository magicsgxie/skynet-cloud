using System;

namespace UWay.Skynet.Cloud.Data.Common
{
    /// <summary>
    /// 参数信息
    /// </summary>
    public class NamedParameter
    {
        string name;
        Type type;
        internal SqlType sqlType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="sqlType">SQL类型</param>
        public NamedParameter(string name, Type type, SqlType sqlType)
        {
            this.name = name;
            this.type = type;
            this.sqlType = sqlType;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public SqlType SqlType
        {
            get { return this.sqlType; }
        }

        /// <summary>
        /// 转化String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name='{0}',Type='{1}',DbType='{2}',Length='{3}'", name, type.Name, sqlType.DbType, sqlType.Length);
        }
    }
}
