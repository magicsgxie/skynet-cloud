using System;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Mapping
{
    /// <summary>
    /// 实体Member 映射元数据接口
    /// </summary>
    public interface IMemberMapping
    {
        /// <summary>
        /// 实体成员
        /// </summary>
        MemberInfo Member { get; }
        /// <summary>
        /// 
        /// </summary>
        MemberInfo StorageMember { get; }
        /// <summary>
        /// SqlType
        /// </summary>
        SqlType SqlType { get; }
        /// <summary>
        /// 关联实体类型
        /// </summary>
        Type RelatedEntityType { get; }
        /// <summary>
        /// 实体元数据
        /// </summary>
        IEntityMapping Entity { get; }
        /// <summary>
        /// 关联实体元数据
        /// </summary>
        IEntityMapping RelatedEntity { get; }
        /// <summary>
        /// 实体Member映射的数据表列名
        /// </summary>
        string ColumnName { get; }
        /// <summary>
        /// 实体Member映射的数据表列的别名
        /// </summary>
        string AliasName { get; }
        /// <summary>
        /// 实体Member的类型
        /// </summary>
        Type MemberType { get; }
        /// <summary>
        /// 实体Member是否是关系
        /// </summary>
        bool IsRelationship { get; }
        /// <summary>
        /// 是否是多对一关系,如果是false那么就是一对多关系
        /// </summary>
        bool IsManyToOne { get; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        bool IsUniqule { get; }
        /// <summary>
        /// 是否有检查约束条件
        /// </summary>
        bool IsCheck { get; }
        /// <summary>
        /// 是否是列映射
        /// </summary>
        bool IsColumn { get; }
        /// <summary>
        /// 是否是主键映射
        /// </summary>
        bool IsPrimaryKey { get; }
        /// <summary>
        /// 是否计算列映射
        /// </summary>
        bool IsComputed { get; }
        /// <summary>
        /// 序列名称
        /// </summary>
        string SequenceName { get; }
        /// <summary>
        /// 该列是否自动生成
        /// </summary>
        bool IsGenerated { get; }
        /// <summary>
        /// 该列是否可更新
        /// </summary>
        bool IsUpdatable { get; }

        /// <summary>
        /// 该列是否版本列
        /// </summary>
        bool IsVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        IMemberMapping[] ThisKeyMembers { get; }
        /// <summary>
        /// 
        /// </summary>
        IMemberMapping[] OtherKeyMembers { get; }

        object GetValue(object target);

        void SetValue(object target, object value);
    }
}
