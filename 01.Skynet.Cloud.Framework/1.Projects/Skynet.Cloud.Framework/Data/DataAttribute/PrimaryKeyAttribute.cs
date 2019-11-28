using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
   

    /// <summary>
    /// 主键标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IdAttribute : ColumnAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IdAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">主键名称</param>
        public IdAttribute(string name) : base(name) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">主键名称</param>
        /// <param name="commment">注释</param>
        public IdAttribute(string name,string commment) : base(name, commment) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">主键名称</param>
        /// <param name="dBType">类型</param>
        /// <param name="commment">注释</param>
        public IdAttribute(string name,DBType dBType, string commment) : base(name, dBType, commment) { }



        /// <summary>
        /// 得到或设置数据表的主键是否为自增的
        /// </summary>
        public bool IsDbGenerated { get; set; }

        /// <summary>
        /// 得到或设置序列名称
        /// </summary>
        public string SequenceName { get; set; }

    }
}
