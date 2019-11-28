namespace UWay.Skynet.Cloud.Nacos.Exceptions
{
    using System;
    /// <summary>
    /// 
    /// </summary>
    public class NacosException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public NacosException(string message) : base(message)
        {
            this.ErrorMsg = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public NacosException(int code, string message) : base(message)
        {
            this.ErrorCode = code;
            this.ErrorMsg = message;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}