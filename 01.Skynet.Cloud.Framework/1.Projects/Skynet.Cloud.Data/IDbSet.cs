using System;
using System.Data.Common;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 表示用于执行插入、读取、更新和删除操作的类型化实体集，对实体集的所有增、删、改、查操作会立即同步到数据库对应的表中
    /// </summary>
    public interface IDbSet : IReqository
    {
        /// <summary>
        /// 得到连接对象
        /// </summary>
        DbConnection Connection { get; }
        /// <summary>
        /// 实体映射元数据
        /// </summary>
        IEntityMapping Mapping { get; }
        /// <summary>
        /// 得到DbSet关联的DbContext对象
        /// </summary>
        IDbContext DbContext { get; }
        /// <summary>
        /// 得到Linq表达式翻译后的参数化SQL语句
        /// </summary>
        string SqlText { get; }
        /// <summary>
        /// 得到Linq表达式的执行计划（包含SQL语句、参数、数据访问、投影信息等，该信息在调试ULinq时非常有用）
        /// </summary>
        string ExecutePlan { get; }

        /// <summary>
        /// 立即加载针对特定关系检索的对象。
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        IDbSet Include(string memberName);
        /// <summary>
        /// 立即加载针对特定关系检索的对象。
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IDbSet Include(System.Reflection.MemberInfo member);
    }


    /// <summary>
    /// 表示用于执行插入、读取、更新和删除操作的类型化实体集，对实体集的所有增、删、改、查操作会立即同步到数据库对应的表中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbSet<T> : IRepository<T>, IDbSet
    {



        /// <summary>
        /// 立即加载针对特定关系检索的对象。
        /// </summary>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        IDbSet<T> Include(Expression<Func<T, object>> fnMember);
        /// <summary>
        /// 立即加载针对特定关系检索的对象。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fnMember"></param>
        /// <returns></returns>
        IDbSet<T> IncludeWith<TEntity>(Expression<Func<TEntity, object>> fnMember);
    }

}
