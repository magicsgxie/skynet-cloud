using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Security
{
    /// <summary>
    /// 存储容器接口
    /// </summary>
    public interface IStorageContainer
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="obj">要存储的对象</param>
        void Store(object obj);

        /// <summary>
        /// 获取容器存储的数据
        /// </summary>
        /// <returns></returns>
        object Get();
    }
}
