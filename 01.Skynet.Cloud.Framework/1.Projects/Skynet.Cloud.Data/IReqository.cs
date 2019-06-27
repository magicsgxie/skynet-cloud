using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IReqository : IQueryable
    {
        /// 通过实体Id获取对应的实体，id可以是单Id也可以是联合Id，如果是联合Id那么以数组的形式作为参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object Get(object id);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <returns></returns>
        int Insert(object instance);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <returns></returns>
        int Update(object instance);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        int Delete(object instance);

    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IOrderedQueryable<T>, IReqository
    {
        /// <summary>
        /// 插入并根据Lambda表达式返回特定信息
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        S Insert<S>(object instance, Expression<Func<T, S>> resultSelector);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="updateCheck">除了id条件之外的附加条件</param>
        /// <returns></returns>
        int Update(object instance, Expression<Func<T, bool>> updateCheck);


        /// <summary>
        /// 更新并根据Lambda表达式返回特定信息
        /// </summary>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="updateCheck">除了id条件之外的附加条件</param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        S Update<S>(object instance, Expression<Func<T, bool>> updateCheck, Expression<Func<T, S>> resultSelector);


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="deleteCheck">除了id条件之外的附加条件</param>
        /// <returns></returns>
        int Delete(object instance, Expression<Func<T, bool>> deleteCheck);


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据Lambda表达式对实体集合进行批量操作
        /// </summary>
        /// <param name="instances"></param>
        /// <param name="fnOperation"></param>
        /// <returns></returns>
        IEnumerable<int> Batch<M>(IEnumerable<M> instances, Expression<Func<IRepository<T>, M, int>> fnOperation);


        /// <summary>
        /// 通过实体Id获取对应的实体，id可以是单Id也可以是联合Id，如果是联合Id那么以数组的形式作为参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new T Get(object id);
    }

}
