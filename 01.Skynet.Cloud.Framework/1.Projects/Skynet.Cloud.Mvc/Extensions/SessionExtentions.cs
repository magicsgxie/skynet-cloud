using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc.Extensions
{
    /// <summary>
    /// Session扩展
    /// </summary>
    public static class SessionExtentions
    {
        /// <summary>
        /// set object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, value.JsonSerialize());
        }

        /// <summary>
        /// get object
        /// </summary>
        /// <typeparam name="T">值 类型</typeparam>
        /// <param name="session"></param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                value.JsonDeserialize<T>();
        }
    }
}
