// <copyright file="DbTable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UWay.Skynet.Cloud.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 数据库表格.
    /// </summary>
    [Serializable]
    public class DbTable
    {
        /// <summary>
        /// Gets or sets 表名.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets 表说明.
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// Gets or sets 字段集合.
        /// </summary>
        public virtual ICollection<DbTableColumn> Columns { get; set; } = new List<DbTableColumn>();
    }

    /// <summary>
    /// 数据库表格列.
    /// </summary>
    [Serializable]
    public class DbTableColumn
    {
        /// <summary>
        /// Gets or sets 字段名.
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否自增.
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否主键.
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets 字段数据类型.
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// Gets or sets 字段数据长度.
        /// </summary>
        public long? ColumnLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 是否允许为空.
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets 默认值.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets 字段说明.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets c#数据类型.
        /// </summary>
        public string CSharpType { get; set; }

        /// <summary>
        /// Gets or sets 数据精度.
        /// </summary>
        public int? DataPrecision { get; set; }

        /// <summary>
        /// 数据刻度.
        /// </summary>
        public int? DataScale { get; set; }
    }
}
