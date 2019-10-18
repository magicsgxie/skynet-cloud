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
        public IdAttribute() { }

        public IdAttribute(string name) : base(name) { }

        public IdAttribute(string name,string commment) : base(name, commment) { }

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
