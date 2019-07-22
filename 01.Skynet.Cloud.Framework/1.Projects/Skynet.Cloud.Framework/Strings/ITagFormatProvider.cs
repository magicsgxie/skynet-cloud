using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 基础命名空间
/// </summary>
namespace UWay.Skynet.Cloud
{
    /// <summary>
    /// 标签格式化提供者
    /// </summary>
    
    public interface ITagFormatProvider
    {
        /// <summary>
        /// 支持括号
        /// </summary>
        bool SupportColon { get; }
        /// <summary>
        /// Tag标签
        /// </summary>
        string Tag { get; }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        string Format(string str, params string[] args);
    }
}
