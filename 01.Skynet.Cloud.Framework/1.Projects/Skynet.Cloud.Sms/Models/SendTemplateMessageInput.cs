
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Sms.Core.Models
{
    /// <summary>
    ///     模板消息内容
    /// </summary>
    public class SendTemplateMessageInput
    {
        /// <summary>
        ///     接收服务目标，多个手机号码请用逗号分隔
        ///     支持单个或多个手机号码，传入号码为11位手机号码，不能加0或+86。群发短信需传入多个号码，以英文逗号分隔，一次调用最多传入200个号码。示例：18600000000,13911111111,13322222222
        /// </summary>
        public virtual string Destination { get; set; }

        /// <summary>
        ///     模板参数
        /// </summary>
        public virtual Dictionary<string, string> Data { get; set; }

        /// <summary>
        /// 扩展参数
        /// 会作为公共回传参数，在“消息返回”中会透传回该参数；举例：用户可以传入自己下级的会员ID，在消息返回时，该会员ID会包含在内，用户可以根据该会员ID识别是哪位会员使用了你的应用
        /// </summary>
        public virtual string ExtendParam { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        public virtual string SignName { get; set; }

        /// <summary>
        /// 模板Id
        /// </summary>
        public virtual string TemplateCode { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Destination:{0};", Destination);
            sb.AppendFormat("SignName:{0};", SignName);
            sb.AppendFormat("ExtendParam:{0};", ExtendParam);
            sb.AppendFormat("TemplateCode:{0};", TemplateCode);
            sb.AppendLine();
            sb.AppendLine("Data:");
            sb.Append("{");
            foreach (var item in Data)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", item.Key, item.Value);
            }
            return sb.ToString().TrimEnd(',')+"};";
        }
    }
}