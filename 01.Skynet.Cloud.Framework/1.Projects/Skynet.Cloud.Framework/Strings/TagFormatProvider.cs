
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Collections;
using UWay.Skynet.Cloud.Extensions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud
{
    sealed class DateColonFormatProvider : ITagFormatProvider
    {
        public bool SupportColon { get { return true; } }

        public string Tag { get { return "DATE"; } }

        public string Format(string str, params string[] args)
        {
            return DateTime.Now.ToString(str);
        }
    }

    sealed class DateFormatProvider : ITagFormatProvider
    {
        public bool SupportColon { get { return false; } }

        public string Tag { get { return "DATE"; } }

        public string Format(string str, params string[] args)
        {
            return DateTime.Today.ToShortDateString();
        }
    }

    sealed class TimeFormatProvider : ITagFormatProvider
    {
        public bool SupportColon { get { return false; } }
        public string Tag { get { return "TIME"; } }
        public string Format(string str, params string[] args)
        {
            return DateTime.Today.ToShortTimeString();
        }
    }

    //sealed class ProductNameFormatProvider : ITagFormatProvider
    //{
    //    public bool SupportColon { get { return false; } }
    //    public string Tag { get { return "PRODUCTNAME"; } }

    //    public string Format(string str, params string[] args)
    //    {
    //        return NLiteEnvironment.ProductName;
    //    }
    //}

    sealed class GuidFormateProvider : ITagFormatProvider
    {
        public bool SupportColon { get { return false; } }
        public string Tag { get { return "GUID"; } }

        public string Format(string str, params string[] args)
        {
            return Guid.NewGuid().ToString();
        }
    }

    

    sealed class EnvironmentVariableFormatProvider : ITagFormatProvider
    {
        public bool SupportColon { get { return true; } }
        public string Tag { get { return "ENV"; } }

        public string Format(string str, params string[] args)
        {
            return Environment.GetEnvironmentVariable(str);
        }
    }

    

    /// <summary>
    /// ${property:PropertyName}
    /// ${property:PropertyName??DefaultValue}
    /// </summary>
    sealed class PropertyFormatProvider : ITagFormatProvider
    {
        /// <summary>
        /// Support括号
        /// </summary>
        public bool SupportColon { get { return true; } }

        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get { return "PROPERTY"; } }

        public string Format(string str, params string[] args)
        {
            string defaultValue = "";
            int pos = str.LastIndexOf("??", StringComparison.Ordinal);
            if (pos >= 0)
            {
                defaultValue = str.Substring(pos + 2);
                str = str.Substring(0, pos);
            }
            pos = str.IndexOf('/');
            if (pos >= 0)
            {
                PropertySet properties = PropertyManager.Instance.Properties.Get(str.Substring(0, pos), new PropertySet());
                str = str.Substring(pos + 1);
                pos = str.IndexOf('/');
                while (pos >= 0)
                {
                    properties = properties.Get(str.Substring(0, pos), new PropertySet());
                    str = str.Substring(pos + 1);
                }
                return properties.Get(str, defaultValue);
            }
            else
            {
                return PropertyManager.Instance.Properties.Get(str, defaultValue);
            }
        }
    }
}
