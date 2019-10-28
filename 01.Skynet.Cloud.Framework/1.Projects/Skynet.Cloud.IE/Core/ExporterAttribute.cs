using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.IE.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExporterAttribute : Attribute
    {
        /// <summary>
        ///     名称(比如当前Sheet 名称)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     头部字体大小
        /// </summary>
        public float? HeaderFontSize { set; get; } = 11;

        /// <summary>
        ///     正文字体大小
        /// </summary>
        public float? FontSize { set; get; } = 11;
    }
}
