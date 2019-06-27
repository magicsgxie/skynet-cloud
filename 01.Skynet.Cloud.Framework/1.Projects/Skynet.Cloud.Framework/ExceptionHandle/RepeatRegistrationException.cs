using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.ExceptionHandle
{
    /// <summary>
    /// 重复注册异常类
    /// </summary>
    [Serializable]

    public class RepeatRegistrationException : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public RepeatRegistrationException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RepeatRegistrationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingInfo"></param>
        /// <param name="context"></param>
        public RepeatRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}
