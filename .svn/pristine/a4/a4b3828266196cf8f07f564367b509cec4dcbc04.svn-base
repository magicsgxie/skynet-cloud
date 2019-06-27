using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// DbSet的增、删、改的扩展类
    /// </summary>
    internal static class DbSet
    {
        /// <summary>
        /// 插入并根据Lambda表达式返回特定信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="repository"></param>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static S Insert<T, S>(this IRepository<T> repository, object instance, Expression<Func<T, S>> resultSelector)
        {
            Guard.NotNull(repository, "repository");
            Guard.NotNull(instance, "instance");
            var callMyself = Expression.Call(
                null,
                ((MethodInfo)MethodInfo.GetCurrentMethod()).MakeGenericMethod(typeof(T), typeof(S)),
                repository.Expression,
                Expression.Constant(instance),
                resultSelector != null ? (Expression)Expression.Quote(resultSelector) : Expression.Constant(null, typeof(Expression<Func<T, S>>))
                );
            return (S)repository.Provider.Execute(callMyself);
        }

        


        /// <summary>
        /// 更新并根据Lambda表达式返回特定信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="repository"></param>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="updateCheck">除了id条件之外的附加条件</param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        internal static S Update<T, S>(this IRepository<T> repository, object instance, Expression<Func<T, bool>> updateCheck, Expression<Func<T, S>> resultSelector)
        {
            Guard.NotNull(repository, "repository");
            Guard.NotNull(instance, "instance");
            var callMyself = Expression.Call(
                    null,
                    ((MethodInfo)MethodInfo.GetCurrentMethod()).MakeGenericMethod(typeof(T), typeof(S)),
                    repository.Expression,
                    Expression.Constant(instance),
                    updateCheck != null ? (Expression)Expression.Quote(updateCheck) : Expression.Constant(null, typeof(Expression<Func<T, bool>>)),
                    resultSelector != null ? (Expression)Expression.Quote(resultSelector) : Expression.Constant(null, typeof(Expression<Func<T, S>>))
                    );
            return (S)repository.Provider.Execute(callMyself);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="instance">支持PO、POCO、IDictionary、IDictionaryOfKV, NameValueCollection 类型</param>
        /// <param name="updateCheck">除了id条件之外的附加条件</param>
        /// <returns></returns>
        internal static int Update<T>(this IRepository<T> repository, object instance, Expression<Func<T, bool>> updateCheck)
        {
            return Update<T, int>(repository, instance, updateCheck, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="instance"></param>
        /// <param name="deleteCheck">除了id条件之外的附加条件</param>
        /// <returns></returns>
        public static int Delete<T>(this IRepository<T> repository, object instance, Expression<Func<T, bool>> deleteCheck)
        {
            Guard.NotNull(repository, "collection");
            Guard.NotNull(instance, "instance");

            var callMyself = Expression.Call(
                null,
                ((MethodInfo)MethodInfo.GetCurrentMethod()).MakeGenericMethod(typeof(T)),
                repository.Expression,
                Expression.Constant(instance),
                deleteCheck != null ? (Expression)Expression.Quote(deleteCheck) : Expression.Constant(null, typeof(Expression<Func<T, bool>>))
                );
            return (int)repository.Provider.Execute(callMyself);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int Delete<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
        {
            Guard.NotNull(repository, "collection");

            var callMyself = Expression.Call(
                null,
                ((MethodInfo)MethodInfo.GetCurrentMethod()).MakeGenericMethod(typeof(T)),
                repository.Expression,
                predicate != null ? (Expression)Expression.Quote(predicate) : Expression.Constant(null, typeof(Expression<Func<T, bool>>))
                );
            return (int)repository.Provider.Execute(callMyself);
        }

        /// <summary>
        /// 根据Lambda表达式对实体集合进行批量操作
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="repository"></param>
        /// <param name="instances"></param>
        /// <param name="fnOperation"></param>
        /// <returns></returns>
        internal static IEnumerable<int> Batch<U, T>(this IRepository<U> repository, IEnumerable<T> instances, Expression<Func<IRepository<U>, T, int>> fnOperation)
        {
            return Batch<U, T>(repository, instances.ToArray(), fnOperation);

        }

        static IEnumerable<int> Batch<U, T>(this IRepository<U> repository, T[] instances, Expression<Func<IRepository<U>, T, int>> fnOperation)
        {
            Guard.NotNull(instances, "instances");
            Guard.NotNull(repository, "collection");

            var callMyself = Expression.Call(
               null,
               ((MethodInfo)MethodInfo.GetCurrentMethod()).MakeGenericMethod(typeof(U), typeof(T)),
               repository.Expression,
               Expression.Constant(instances),
               fnOperation != null ? (Expression)Expression.Quote(fnOperation) : Expression.Constant(null, typeof(Expression<Func<IRepository<U>, T, int>>))
               );
            return (IEnumerable<int>)repository.Provider.Execute(callMyself);

        }
    }
}