using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UWay.Skynet.Cloud.Uflow.Entity
{
    /// <summary>
    /// 扩展字段
    /// </summary>
    [XmlRoot(ElementName = "EXTENDFIELD", Namespace = "")]
    public class ExtendField
    {
        /// <summary>
        /// 序号(1至6)
        /// </summary>
        [XmlElement(ElementName = "NO", Namespace = "")]
        public int No;
        /// <summary>
        /// 内容
        /// </summary>
        [XmlElement(ElementName = "CONTENT", Namespace = "")]
        public string Content;
    }
}
