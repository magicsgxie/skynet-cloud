using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.IE
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImporterAttribute : Attribute
    {
        /// <summary>
        ///     表头位置
        /// </summary>
        public int HeaderRowIndex { get; set; } = 1;
    }
}
