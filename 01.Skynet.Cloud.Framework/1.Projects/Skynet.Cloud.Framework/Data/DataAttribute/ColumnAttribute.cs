﻿using System;
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
        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnAttribute()
        {
            DbType = DBType.Unkonw;
            IsNullable = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名</param>
        public ColumnAttribute(string name):this(name, DBType.Unkonw, string.Empty)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="comment">注释</param>
        public ColumnAttribute(string name, string comment) : this(name, DBType.Unkonw, comment)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="dBType">数据类型</param>
        /// <param name="comment">注释</param>
        public ColumnAttribute(string name, DBType dBType, string comment)
        {
            Name = name;
            DbType = DBType.Unkonw;
            Comment = comment;
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

        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { set; get; }

        
    }
    
}
