using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// 序列实体
    /// </summary>
    public interface IDescriptor
    {
        /// <summary>
        /// 反序列
        /// </summary>
        /// <param name="source"></param>
        void Deserialize(string source);

        /// <summary>
        /// 序列
        /// </summary>
        /// <returns></returns>
        string Serialize();
    }
}
