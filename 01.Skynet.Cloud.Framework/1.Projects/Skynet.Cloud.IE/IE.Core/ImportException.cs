// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ImportException.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//           
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using System;
using System.Runtime.Serialization;

namespace UWay.Skynet.Cloud.IE.Core
{
    [Serializable]
    public class ImportException : Exception
    {
        public ImportException()
        {
        }

        public ImportException(string message) : base(message)
        {
        }

        public ImportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}