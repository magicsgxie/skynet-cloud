using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 自动练级的属性
    /// </summary>

    [AttributeUsage(AttributeTargets.Property)]
    public class AutoJoinAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoJoinAttribute() { }
    }

    /// <summary>
    /// Ansi 字符床
    /// </summary>
    public class AnsiString
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">字符串</param>
        public AnsiString(string str)
        {
            Value = str;
        }

        /// <summary>
        /// 字符串
        /// </summary>
        public string Value { get; private set; }
    }


    ///// <summary>
    ///// For explicit pocos, marks property as a result column and optionally supplies column name
    ///// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    //public class ResultColumnAttribute : ColumnAttribute
    //{
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public ResultColumnAttribute() { }

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="name">列名</param>
    //    public ResultColumnAttribute(string name) : base(name) { }
    //}

    /// <summary>
    ///  Poco's marked [Explicit] require all column properties to be marked
    /// </summary>
    
    public class ExplicitColumnsAttribute : Attribute
    {
    }

    /// <summary>
    /// 数据库列类型属性
    /// </summary>

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnTypeAttribute : Attribute
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">类型</param>
        public ColumnTypeAttribute(Type type)
        {
            Type = type;
        }
    }

    /// <summary>
    /// 别名属性
    /// </summary>
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }

    /// <summary>
    /// 列版本属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class VersionColumnAttribute : ColumnAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VersionColumnAttribute() { }
        public VersionColumnAttribute(string name) : base(name) { }
    }

    /// <summary>
    /// 列计算属性
    /// </summary>

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ComputedColumnAttribute : ColumnAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComputedColumnAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">计算列名</param>
        public ComputedColumnAttribute(string name) : base(name) { }
    }
}
