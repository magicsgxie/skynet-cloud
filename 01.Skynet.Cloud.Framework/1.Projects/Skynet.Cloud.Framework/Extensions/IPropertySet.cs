using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Collections
{
    /// <summary>
    /// 属性集接口
    /// </summary>
    public interface IPropertySet : IEnumerable<KeyValuePair<string, object>>, IEditableObject, IChangeTracking
    {
        /// <summary>
        /// 通过属性名得到或设置属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        object this[string propertyName] { get; set; }

        /// <summary>
        /// 返回所有的属性Key
        /// </summary>
        string[] Keys { get; }

        /// <summary>
        /// 通过属性名得到属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        T Get<T>(string property);

        /// <summary>
        /// 通过属性名得到属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string property, T defaultValue);

        ///// <summary>
        ///// 设置属性值
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <param name="value"></param>
        //void Set(string propertyName, object value);

        /// <summary>
        /// 判断属性集中是否包含特定的属性名
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        bool Contains(string propertyName);

        /// <summary>
        /// 返回属性的数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 通过属性名移除属性
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        bool Remove(string propertyName);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void AddRange(IEnumerable<KeyValuePair<string, object>> items);

        /// <summary>
        /// 属性改变事件
        /// </summary>
        event EventHandler<PropertyChangedEventArgs> PropertyChanged;


    }

}
