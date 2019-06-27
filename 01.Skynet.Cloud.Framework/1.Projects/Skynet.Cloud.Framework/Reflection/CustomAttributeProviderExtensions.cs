using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomAttributeProviderExtensions
    {
        /// <summary>
        /// 得到成员元数据
        /// </summary>
        /// <typeparam name="T">元数据类型</typeparam>
        /// <param name="member">成员</param>
        /// <param name="inherit">是否集成</param>
        /// <returns>返回元数据</returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider member, bool inherit = false)
            where T : Attribute
        {
            Guard.NotNull(member, "member");
            T[] attributes = member.GetCustomAttributes(typeof(T), inherit) as T[];

            if ((attributes == null) || (attributes.Length == 0))
                return null;
            else
                return attributes[0];
        }


        /// <summary>
        /// 得到成员元数据数组
        /// </summary>
        /// <typeparam name="T">元数据类型</typeparam>
        /// <param name="member">成员</param>
        /// <param name="inherit">是否集成</param>
        /// <returns>返回成员元数据数组</returns>
        public static T[] GetAttributes<T>(this ICustomAttributeProvider member, bool inherit = false)
            where T : Attribute
        {
            Guard.NotNull(member, "member");
            return member.GetCustomAttributes(typeof(T), inherit) as T[];
        }



        /// <summary>
        /// 判断成员是否包含特点的元数据
        /// </summary>
        /// <typeparam name="T">元数据类型</typeparam>
        /// <param name="member">成员</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this ICustomAttributeProvider member, bool inherit = false)
            where T : Attribute
        {
            Guard.NotNull(member, "member");
            return member.IsDefined(typeof(T), inherit);
        }
    }
}
