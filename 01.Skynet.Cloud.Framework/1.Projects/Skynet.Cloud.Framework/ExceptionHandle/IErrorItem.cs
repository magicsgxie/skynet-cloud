using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.ExceptionHandle
{
    public interface IErrorItem
    {
        /// <summary>
        /// 
        /// </summary>
        string Message { get; }
        /// <summary>
        /// 
        /// </summary>
        string Key { get; }
    }
}
