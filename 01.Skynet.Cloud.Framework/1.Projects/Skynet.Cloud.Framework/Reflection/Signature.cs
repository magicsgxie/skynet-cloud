using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 签名
    /// </summary>
    public  class Signature : IEquatable<Signature>
    {
        /// <summary>
        /// 动态属性
        /// </summary>
        public DynamicProperty[] properties;

        /// <summary>
        /// Hash Code
        /// </summary>
        public int hashCode;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="properties"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.properties = properties.ToArray();
            hashCode = 0;
            foreach (DynamicProperty p in properties)
            {
                hashCode ^= p.Name.GetHashCode() ^ p.Type.GetHashCode();
            }
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return hashCode;
        }


        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Signature ? Equals((Signature)obj) : false;
        }


        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Signature other)
        {
            if (properties.Length != other.properties.Length) return false;
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name != other.properties[i].Name ||
                    properties[i].Type != other.properties[i].Type) return false;
            }
            return true;
        }
    }

}
