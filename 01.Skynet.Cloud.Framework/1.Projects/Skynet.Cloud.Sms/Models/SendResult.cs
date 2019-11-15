using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Sms.Core.Models
{
    /// <summary>
    /// 发送结果
    /// </summary>
    public class SendResult
    {
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
