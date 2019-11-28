using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Request
{
    /// <summary>
    /// 返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class R<T>
    {
        /// <summary>
        /// Code
        /// </summary>
        public int Code { get; set; } = 0;

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public R()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public R(T data)
        {
            this.Data = data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public R(T data, int code, string msg)
        {
            this.Data = data;
            this.Code = code;
            this.Msg = msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        public R(T data, int code)
        {
            this.Data = data;
            this.Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errMsg"></param>
        public R(string errMsg)
        {
            Msg = errMsg;
            Code = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public R(Exception ex)
        {
            Msg = ex.Message;
            Code = 1;
        }
    }
}
