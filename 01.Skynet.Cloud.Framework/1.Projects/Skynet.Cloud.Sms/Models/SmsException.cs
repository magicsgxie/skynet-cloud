using System;
using System.Collections.Generic;
using System.Text;

namespace Magicodes.Sms.Core.Models
{
    /// <summary>
    /// 短信异常
    /// </summary>
    public class SmsException : Exception
    {
        public SmsException() : base() { }

        public SmsException(string message) : base(message)
        {
        }

        public SmsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
