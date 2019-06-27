using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 列映射标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : MemberAttribute
    {
        public ColumnAttribute()
        {
            DbType = DBType.Unkonw;
            IsNullable = true;
        }

        public ColumnAttribute(string name)
        {
            Name = name;
            DbType = DBType.Unkonw;
            IsNullable = true;
        }

        /// <summary>
        /// 得到或设置列名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 得到或设置列的别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 得到或设置列的数据类型
        /// </summary>
        public DBType DbType { get; set; }
        /// <summary>
        /// 得到或设置列的长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 得到或设置列是否允许为空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 得到或设置Precision
        /// </summary>
        public byte Precision { get; set; }
        /// <summary>
        /// 得到或设置Scale
        /// </summary>
        public byte Scale { get; set; }

        /// <summary>
        /// 是否壮话Utc时间
        /// </summary>
        public bool ForceToUtc { get; set; }
    }
    
}
