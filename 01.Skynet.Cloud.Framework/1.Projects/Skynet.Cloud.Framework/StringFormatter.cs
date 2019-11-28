using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud
{
    /// <summary>
    /// 字符串格式化工具
    /// </summary>
    public sealed class StringFormatter
    {
        private static Dictionary<string, ITagFormatProvider> formatters = new Dictionary<string, ITagFormatProvider>();
        private static Dictionary<string, ITagFormatProvider> colonFormatters = new Dictionary<string, ITagFormatProvider>();

        static StringFormatter()
        {
            Register(new DateColonFormatProvider());
            Register(new DateFormatProvider());
            Register(new TimeFormatProvider());
            //Register(new ProductNameFormatProvider());
            Register(new GuidFormateProvider());
            //Register(new SdkPathFormateProvider());
            Register(new EnvironmentVariableFormatProvider());
            //Register(new ResourceFormatProvider());
            Register(new PropertyFormatProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public static void Register(ITagFormatProvider provider)
        {
            Guard.NotNull(provider, "provider");
            var fmts = provider.SupportColon ? colonFormatters : formatters;

            if (fmts.ContainsKey(provider.Tag))
                fmts.Remove(provider.Tag);
            fmts.Add(provider.Tag, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(string fmt, params string[] args)
        {
            Guard.NotNull(fmt, "fmt");
            if (args == null || args.Length == 0)
                return fmt;

            int pos = 0;
            StringBuilder output = null;
            var length = fmt.Length;

            var capacity = length + args.Sum(p => p.Length);

            do
            {
                int oldPos = pos;
                pos = fmt.IndexOf("${", pos, StringComparison.Ordinal);
                if (pos < 0)//未找到
                {
                    if (output == null)
                        return fmt;
                    else
                    {
                        if (oldPos < length)
                            output.Append(fmt, oldPos, length - oldPos);
                        return output.ToString();
                    }
                }

                if (output == null)
                {
                    if (pos == 0)
                        output = new StringBuilder(capacity);
                    else
                        output = new StringBuilder(fmt, 0, pos, capacity);
                }
                else
                {
                    if (pos > oldPos)
                        output.Append(fmt, oldPos, pos - oldPos);
                }
                int end = fmt.IndexOf('}', pos + 1);
                if (end < 0)
                {
                    output.Append("${");
                    pos += 2;
                }
                else
                {
                    var property = fmt.Substring(pos + 2, end - pos - 2);
                    var val = InternalFormat(property, args);

                    if (string.IsNullOrEmpty(val))//如果没有格式化就原样返回
                    {
                        output.Append("${");
                        output.Append(property);
                        output.Append('}');
                    }
                    else
                        output.Append(val);

                    pos = end + 1;
                }
            } while (pos < length);
            return output.ToString();
        }

        private static string InternalFormat(string propertyName, params string[] args)
        {
            int k = propertyName.IndexOf(':');

            if (k > 0)
            {
                var tag = propertyName.Substring(0, k).ToUpper();

                propertyName = propertyName.Substring(k + 1);

                ITagFormatProvider fmt;
                if (colonFormatters.TryGetValue(tag, out fmt))
                    return fmt.Format(propertyName, args);
                return propertyName;
            }
            else
            {
                ITagFormatProvider fmt;
                if (formatters.TryGetValue(propertyName.ToUpper(), out fmt))
                    return fmt.Format(propertyName, args);
                return propertyName;
            }


        }

    }
}
