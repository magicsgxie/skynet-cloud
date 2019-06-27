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
    }
}
