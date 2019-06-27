
namespace UWay.Skynet.Cloud.Data.Schema
{
    /// <summary>
    /// Table 元数据 
    /// </summary>
    public interface ITableSchema
    {
        bool IsView { get; set; }
        /// <summary>
        /// Schema
        /// </summary>
        string Schema { get; }
        /// <summary>
        /// 表名
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 列集合(包括主键列）
        /// </summary>
        IColumnSchema[] AllColumns { get; }

        /// <summary>
        /// 列集合(不包括主键列）
        /// </summary>
        IColumnSchema[] Columns { get; }

        /// <summary>
        /// 外键集合
        /// </summary>
        IForeignKeySchema[] ForeignKeys { get; }

        /// <summary>
        /// 主键列集合
        /// </summary>
        IColumnSchema[] PrimaryKeys { get; }

        /// <summary>
        /// 子表集合
        /// </summary>
        IRelationSchema[] Children { get; }
    }
}
