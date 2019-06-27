using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud
{
    /// <summary>
    /// 
    /// </summary>
    
    public interface ITagFormatProvider
    {
        /// <summary>
        /// 
        /// </summary>
        bool SupportColon { get; }
        /// <summary>
        /// 
        /// </summary>
        string Tag { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        string Format(string str, params string[] args);
    }
}
