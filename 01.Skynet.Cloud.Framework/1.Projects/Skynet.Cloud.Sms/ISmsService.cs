using System;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Sms.Core.Models;

namespace UWay.Skynet.Cloud.Sms
{
    /// <summary>
    ///     短信服务
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        Task<SendResult> SendCodeAsync(string phone, string code);

        /// <summary>
        ///     发送模板消息（适用于阿里大鱼等）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SendResult> SendTemplateMessageAsync(SendTemplateMessageInput message);

        /// <summary>
        /// 发送服务类的短信消息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SendResult> SendServerMessageAsync(string phone, string message);
    }
}
