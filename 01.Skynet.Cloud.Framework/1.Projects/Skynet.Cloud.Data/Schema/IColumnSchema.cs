using System;

namespace UWay.Skynet.Cloud.Data.Schema
{
    /// <summary>
    /// 列元数据
    /// </summary>
    public interface IColumnSchema
    {
        /// <summary>
        /// 列名
        /// </summary>
        string ColumnName { get; }
        /// <summary>
        /// 表
        /// </summary>
        ITableSchema Table { get; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        bool IsUniqule { get; }
        /// <summary>
        /// 是否主键
        /// </summary>
        bool IsPrimaryKey { get; }
        /// <summary>
        /// 是否计算列
        /// </summary>
        bool IsComputed { get; }
        /// <summary>
        /// 该列是否自动生成
        /// </summary>
        bool IsGenerated { get; }

        /// <summary>
        /// 该列是否运行为空
        /// </summary>
        bool IsNullable { get; }

        string Comment { get; }

        string ColumnType { get; }

        DBType DbType { get; }

        Type Type { get; }

        int Length { get; }

        int Precision { get; }

        int Scale { get; }

    }
}
