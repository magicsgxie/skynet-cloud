using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Extensions
{
    public static class CollectionExtension
    {
        /// <summary>
        ///     添加集合到现有集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            foreach (var current in items)
            {
                list.Add(current);
            }
            return list;
        }

        /// <summary>
        ///     Each 迭代操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (var current in source)
                {
                    action(current);
                }
            }
            return source;
        }

        /// <summary>
        ///     将集合转换到 Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="name">做为键值对 name 的属性。</param>
        /// <param name="value">做为键值对 value 的属性。</param>
        /// <returns></returns>
        public static IDictionary<object, object> ConvertToDictionary<T, TProperty>(this ICollection<T> source,
            Expression<Func<T, TProperty>> name, Expression<Func<T, TProperty>> value)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "name 属性不能为空。");
            if (value == null) throw new ArgumentNullException(nameof(value), "value 属性不能为空。");

            var dict = new Dictionary<object, object>();

            source.Each(delegate(T item) { dict.Add(item.GetPropertyValue(name), item.GetPropertyValue(value)); });

            return dict;
        }

        /// <summary>
        ///     取集合的键值对集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="name">做为键值对 name 的属性。</param>
        /// <param name="value">做为键值对 value 的属性。</param>
        /// <returns></returns>
        public static NameValueCollection ConvertToNameValueCollection<T, TProperty>(this ICollection<T> source,
            Expression<Func<T, TProperty>> name, Expression<Func<T, TProperty>> value)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "name 属性不能为空。");
            if (value == null) throw new ArgumentNullException(nameof(value), "value 属性不能为空。");

            var nvc = new NameValueCollection();
            foreach (var current in source)
            {
                nvc.Add(current.GetPropertyValue(name).ToString(), current.GetPropertyValue(value).ToString());
            }
            return nvc;
        }
    }

    // <summary>
    /// 数据状态集合对象，主要用来解耦Asp.net内置的状态对象，如：Session，Application，Cache等
    /// </summary>
    public interface IDataCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[object key] { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(object key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// 
        /// </summary>
        IDataCollection Data { get; }
    }
}