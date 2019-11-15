using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// JWT基础配置
    /// </summary>
    public class JwtOption
    {
        //token是谁颁发的
        public string Issuer { get; set; }
        //token可以给哪些客户端使用
        public string Audience { get; set; }
        //加密的key
        public string SecretKey { get; set; }

        
        public string Authority { get; set; }
    }
}
