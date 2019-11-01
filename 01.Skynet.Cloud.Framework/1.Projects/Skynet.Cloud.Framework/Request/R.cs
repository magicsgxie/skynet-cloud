using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Request
{
    public class R<T>
    {
        public int Code { get; set; } = 0;
        public string Msg { get; set; }

        public T Data { set; get; }

        public R(T data)
        {
            this.Data = data;
        }

        public R(string errMsg)
        {
            Msg = errMsg;
            Code = 1;
        }

        public R(Exception ex)
        {
            Msg = ex.Message;
            Code = 1;
        }
    }
}
