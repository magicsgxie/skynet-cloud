using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 设置器扩展
    /// </summary>
    public static class SetterExtensions
    {
        private static readonly Dictionary<MemberInfo, Setter> setterCache = new Dictionary<MemberInfo, Setter>();
        /// <summary>
        /// 快速设置成员内容
        /// </summary>
        /// <param name="member">成员</param>
        /// <param name="target">目标对象</param>
        /// <param name="value">成员值</param>
        public static void Set(this MemberInfo member, object target, object value)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            Setter setter;
            if (!setterCache.TryGetValue(member, out setter))
                setterCache[member] = setter = member.GetSetter();

            setter(target, value);
        }
    }
}
