using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace UWay.Skynet.Cloud.Extensions
{
    public static class XmlNodeExtensions
    {
        /// <exception cref="ArgumentException">
        /// Child element with name specified by <paramref name="childName"/> does not exists.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static string ChildElementInnerText(this XmlNode node, string childName)
        {
            XmlElement innerElement = node[childName];

            if (innerElement == null)
            {
                string message = string.Format(
                    CultureInfo.CurrentCulture, "Child element with specified name: {0} cannot be found.", childName);
                Trace.WriteLine(message);

                return null;
            }

            return innerElement.InnerText;
        }
    }


    public static class XmlExtensions
    {
        public static T GetAttributeValue<T>(this XElement e, string name)
        {
            if (e.Attribute(name) != null)
                return Converter.Convert<string, T>(e.Attribute(name).Value);

            var attr = e.Attributes().FirstOrDefault(p => string.Equals(p.Name.LocalName, name, StringComparison.InvariantCultureIgnoreCase));

            if (attr == null)
                return default(T);
            return Converter.Convert<string, T>(attr.Value);
        }

        /// <summary>
        /// 判断一个字符串是否是Xml格式的文档
        /// </summary>
        /// <param name="AXml">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsXml(this string AXml)
        {
            string vData = AXml;
            if (!vData.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
                vData = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + vData;

            try
            {
                XmlDocument vDoc = new XmlDocument();
                vDoc.LoadXml(vData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建XML文档对象
        /// </summary>
        /// <param name="AXml">XML文档内容</param>
        /// <returns></returns>
        public static XmlDocument CreateXmlDocument(this string AXml)
        {
            string vData = AXml;
            if (!vData.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
                vData = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + vData;

            XmlDocument vDoc = new XmlDocument();
            vDoc.LoadXml(vData);
            return vDoc;
        }

        /// <summary>
        /// 取得指定标记下的结点内容，标记按ID,Name,TagName的顺序依次寻找
        /// </summary>
        /// <param name="AXml">XML文档内容</param>
        /// <param name="AFlag">标记</param>
        /// <returns></returns>
        public static string GetInnerText(this string AXml, string AFlag)
        {
            XmlDocument vDoc = CreateXmlDocument(AXml);
            return GetInnerText(vDoc, AFlag);
        }

        /// <summary>
        /// 取得指定标记下的结点内容，标记按ID,Name,TagName的顺序依次寻找
        /// </summary>
        /// <param name="ADoc">文档对象</param>
        /// <param name="AFlag">标记</param>
        /// <returns></returns>
        public static string GetInnerText(this XmlDocument ADoc, string AFlag)
        {
            string vRes = "";
            vRes = GetInnerTextById(ADoc, AFlag);
            if (!string.IsNullOrWhiteSpace(vRes))
                return vRes;

            vRes = GetInnerTextByName(ADoc, AFlag);
            if (!string.IsNullOrWhiteSpace(vRes))
                return vRes;

            vRes = GetInnerTextByTagName(ADoc, AFlag);
            return vRes;
        }

        /// <summary>
        /// 取得指定ID下的结点内容
        /// </summary>
        /// <param name="ADoc">XML文档内容InnerText</param>
        /// <param name="AID">结点ID</param>
        /// <returns></returns>
        public static string GetInnerTextById(this string AXml, string AID)
        {
            XmlDocument vDoc = CreateXmlDocument(AXml);
            return GetInnerTextById(vDoc, AID);
        }

        /// <summary>
        /// 取得指定ID下的结点内容
        /// </summary>
        /// <param name="ADoc">文档对象</param>
        /// <param name="AID">结点ID</param>
        /// <returns></returns>
        public static string GetInnerTextById(this XmlDocument ADoc, string AID)
        {
            XmlElement vEle = GetElementByAttributeName(ADoc.DocumentElement, "id", AID);
            if (vEle == null)
                return string.Empty;
            else
                return vEle.InnerText;
        }

        /// <summary>
        /// 取得指定Name下的结点内容
        /// </summary>
        /// <param name="AXml">XML文档内容InnerText</param>
        /// <param name="AId">结点Name</param>
        /// <returns></returns>
        public static string GetInnerTextByName(this string AXml, string AName)
        {
            XmlDocument vDoc = CreateXmlDocument(AXml);
            return GetInnerTextByName(vDoc, AName);
        }

        /// <summary>
        /// 取得指定Name下的结点内容
        /// </summary>
        /// <param name="ADoc">文档对象</param>
        /// <param name="AId">结点Name</param>
        /// <returns></returns>
        public static string GetInnerTextByName(this XmlDocument ADoc, string AName)
        {
            XmlElement vEle = GetElementByAttributeName(ADoc.DocumentElement, "name", AName);
            if (vEle == null)
                return string.Empty;
            else
                return vEle.InnerText;
        }


        /// <summary>
        /// 取得指定TagName下的结点内容
        /// </summary>
        /// <param name="AXml">XML文档内容InnerText</param>
        /// <param name="AId">结点TagName</param>
        /// <returns></returns>
        public static string GetInnerTextByTagName(this string AXml, string ATagName)
        {
            XmlDocument vDoc = CreateXmlDocument(AXml);
            return GetInnerTextByTagName(vDoc, ATagName);
        }

        /// <summary>
        /// 取得指定TagName下的结点内容，如果有多个只取第一个
        /// </summary>
        /// <param name="ADoc">文档对象</param>
        /// <param name="AId">结点TagName</param>
        /// <returns></returns>
        public static string GetInnerTextByTagName(this XmlDocument ADoc, string ATagName)
        {
            XmlNodeList vNodeList = ADoc.GetElementsByTagName(ATagName);
            if (vNodeList.Count <= 0)
                return string.Empty;
            else
                return vNodeList[0].InnerText;
        }

        /// <summary>
        /// 取得Xml元素中首个属性值符合要求的Xml元素
        /// </summary>
        /// <param name="AElement">Xml元素</param>
        /// <param name="AName">属性名</param>
        /// <param name="AValue">属性值</param>
        /// <returns></returns>
        public static XmlElement GetElementByAttributeName(this XmlElement AElement, string AName, string AValue)
        {
            if (AElement.HasAttribute(AName))
            {
                string vVal = AElement.GetAttribute(AName);
                if (vVal.Equals(AValue, StringComparison.OrdinalIgnoreCase))
                    return AElement;
            }
            XmlElement vEle = null;
            XmlElement vResEle = null;
            foreach (XmlNode vNode in AElement.ChildNodes)
            {
                if (vNode.NodeType != XmlNodeType.Element)
                    continue;
                vEle = vNode as XmlElement;
                vResEle = GetElementByAttributeName(vEle, AName, AValue);
                if (vResEle != null)
                    return vResEle;
            }
            return vResEle;
        }

    }
}
