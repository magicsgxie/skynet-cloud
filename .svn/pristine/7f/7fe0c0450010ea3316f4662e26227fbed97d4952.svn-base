using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 缺省约定映射策略类，比如ClassName到TableName的转换约定，字段或属性到列名的转换约定
    /// </summary>
    public static class MappingConversion
    {
        /// <summary>
        /// 缺省策略，表示类名和表名完全一致，字段或属性的名称和列名完全一致
        /// </summary>
        public static readonly IMappingConversion Default = new DefaultMappingConversion();

        /// <summary>
        /// 表名复数策略，字段或属性的名称和列名完全一致
        /// </summary>
        public static readonly IMappingConversion Plural = new ProxyMappingConversion(className => Inflector.Plural(className));

        internal static IMappingConversion Current { get; set; }
    }


    class DefaultMappingConversion : IMappingConversion
    {
        public string TableName(string className)
        {
            return className;
        }

        public string ColumnName(string memberName)
        {
            return memberName;
        }
    }

    /// <summary>
    /// 约定映射代理类
    /// </summary>
    public class ProxyMappingConversion : IMappingConversion
    {
        private Func<string, string> fnTableName;
        private Func<string, string> fnColumnName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fnTableName"></param>
        /// <param name="fnColumnName"></param>
        public ProxyMappingConversion(Func<string, string> fnTableName, Func<string, string> fnColumnName)
        {
            Guard.NotNull(fnTableName, "fnTableName");
            Guard.NotNull(fnColumnName, "fnColumnName");
            this.fnTableName = fnTableName;
            this.fnColumnName = fnColumnName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fnTableName"></param>
        public ProxyMappingConversion(Func<string, string> fnTableName) : this(fnTableName, memberName => memberName) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public string TableName(string className)
        {
            return fnTableName(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public string ColumnName(string memberName)
        {
            return fnColumnName(memberName);
        }
    }

}
