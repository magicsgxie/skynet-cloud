using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 数据库表标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 得到或设置服务器名
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 得到或设置数据库数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TableAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public TableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 得到或设置数据库表的Schema
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// 得到或设置表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 得到或设置表是否是只读的
        /// </summary>
        public bool Readonly { get; set; }
    }

    //public abstract class MappingAttribute : Attribute
    //{
    //}

    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    //public class ExtensionTableAttribute : TableBaseAttribute
    //{
    //    public string KeyColumns { get; set; }
    //    public string RelatedAlias { get; set; }
    //    public string RelatedKeyColumns { get; set; }
    //}


    //public abstract class TableBaseAttribute : MappingAttribute
    //{
    //    public virtual string Name { get; set; }
    //    public string Alias { get; set; }
    //}

    ///// <summary>
    ///// 表属性
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Class)]
    //public class TableAttribute: TableBaseAttribute
    //{
    //    private string tableName;
    //    /// <summary>
    //    /// 表名称
    //    /// </summary>
    //    public override string Name
    //    {
    //        get { return tableName; }
    //        set { tableName = value; }
    //    }

    //    public Type EntityType { get; set; }

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public TableAttribute()
    //    {
    //        this.tableName = null;
    //    }
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="tableName">表明</param>
    //    public TableAttribute(string tableName)
    //    {
    //        this.tableName = tableName;
    //    }
    //}


}
