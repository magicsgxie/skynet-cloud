using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// 组合键
    /// </summary>
    public class CompoundKey : IEquatable<CompoundKey>, IEnumerable<object>, IEnumerable
    {
        object[] values;
        int hc;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="values"></param>
        public CompoundKey(params object[] values)
        {
            this.values = values;
            for (int i = 0, n = values.Length; i < n; i++)
            {
                object value = values[i];
                if (value != null)
                {
                    hc ^= (value.GetHashCode() + i);
                }
            }
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return hc;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CompoundKey other)
        {
            if (other == null || other.values.Length != values.Length)
                return false;
            for (int i = 0, n = other.values.Length; i < n; i++)
            {
                if (!object.Equals(this.values[i], other.values[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 虚幻
        /// </summary>
        /// <returns></returns>
        public IEnumerator<object> GetEnumerator()
        {
            return ((IEnumerable<object>)values).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
