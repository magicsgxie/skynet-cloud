using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CR = System.Security.Cryptography;
using UWay.Skynet.Cloud.Collections;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Steganogram;
using UWay.Skynet.Cloud.Helpers;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud
{

   

    /// <summary>
    /// 枚举一次列表
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public class EnumerateOnce<T> : IEnumerable<T>, IEnumerable
    {
        IEnumerable<T> enumerable;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumerable"></param>
        public EnumerateOnce(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        /// <summary>
        /// 获取循环器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var en = Interlocked.Exchange(ref enumerable, null);
            if (en != null)
            {
                return en.GetEnumerator();
            }
            throw new Exception("Enumerated more than once.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// 普通可枚举类型
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    class GenericEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable source;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEnumerable{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public GenericEnumerable(IEnumerable source)
        {
            this.source = source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T item in this.source)
            {
                yield return item;
            }
        }
    }


    /// <summary>
    /// 基础平台扩展类
    /// </summary>
    public static partial class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, Pagination paging)
        {
            if (paging != null)
                return paging.ParseQuery(query) as IQueryable<T>;
            return query;
        }


        /// <summary>
        /// get enum description by name
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="enumItemName">the enum name</param>
        /// <returns></returns>
        public static string GetDescriptionByName<T>(this T enumItemName)
        {
            FieldInfo fi = enumItemName.GetType().GetField(enumItemName.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumItemName.ToString();
            }
        }

        /// <summary>
        /// 获取字符的MD5
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns></returns>
        public static string MD5(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return string.Empty;

            string result = string.Empty;
            CR.MD5 md5 = CR.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(val));

            //result = BitConverter.ToString(s).Replace("-", string.Empty);
            StringBuilder sBuilder = new StringBuilder(32);
            for (int i = 0; i < s.Length; i++)
                sBuilder.Append(s[i].ToString("x2"));

            return sBuilder.ToString();
        }

        private const string AntExpressionPrefix = "${";
        private const string AntExpressionSuffix = "}";
        private const string RegMetadatas = @"$\.*+?|(){}^";
        private const string RegExpressionWithPrefix = @"(?<!\\{0}*)(?<={0})(?<exp>\w+)";
        private const string GroupName = "exp";

        /// <summary>
        /// string 匹配
        /// </summary>
        /// <param name="src"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
        public static bool Match(this string src, string reg)
        {
            Regex regex = new Regex(reg);
            return regex.IsMatch(src);
        }

        /// <summary>
        /// 返回所有匹配指定前缀与后缀的字符串
        /// </summary>
        /// <param name="src"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] Matches(this string src, string prefix, string suffix)
        {

            var expressions = new List<string>();
            var start = src.IndexOf(prefix);

            while (start >= 0)
            {
                int end = src.IndexOf(suffix, start + prefix.Length);

                if (end == -1)
                {
                    //没有找到则退出循环。。。
                    start = -1;
                }
                else
                {
                    //求蚂蚁表达式
                    string exp = src.Substring(start + prefix.Length, end - start - prefix.Length);

                    if (String.IsNullOrEmpty(exp))
                    {
                        throw new FormatException(string.Format("Empty {0}{1} value found in text : '{2}'.",
                            prefix,
                            suffix,
                            src));
                    }

                    exp = exp.Trim();
                    if (expressions.IndexOf(exp) < 0)
                    {
                        expressions.Add(exp);
                    }
                    start = src.IndexOf(prefix, end);
                }
            }
            return expressions.ToArray();
        }

        /// <summary>
        /// 字符串首字母小写
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string FirstLower(this string src)
        {
            Guard.NotNullOrEmpty(src, "scr");
            return src[0].ToString().ToLower() + src.Remove(0, 1);
        }

        /// <summary>
        /// 字符串首字母大写
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string FirstUpper(this string src)
        {
            Guard.NotNullOrEmpty(src, "scr");
            return src[0].ToString().ToUpper() + src.Remove(0, 1);
        }

        /// <summary>
        /// 返回所有匹配前缀为'${'，后缀为'}'的蚂蚁表达式
        /// </summary>
        /// <exception cref="System.FormatException">
        /// 如果提供的表达式为空(<c>${}</c>).
        /// </exception>
        public static string[] MatchAntExpressions(this string src)
        {
            return Matches(src, AntExpressionPrefix, AntExpressionSuffix);
        }

        /// <summary>
        /// 将指定字符串中的蚂蚁表达式替换成指定的内容
        /// </summary>
        /// <param name="src"></param>
        /// <param name="expression">蚂蚁表达式</param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string SetAntExpression(string src, string expression, object newValue)
        {
            if (newValue == null)
            {
                newValue = String.Empty;
            }
            return src.Replace(Surround(AntExpressionPrefix, expression, AntExpressionSuffix), newValue.ToString());
        }

        /// <summary>
        /// 字符扩展添加前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="target"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string Surround(this string target, string prefix, string suffix)
        {
            return string.Format("{0}{1}{2}", prefix, target, suffix);
        }



        /// <summary>
        /// 返回所有匹配指定前缀的字符串
        /// </summary>
        public static string[] Matches(this string src, char prefix)
        {

            // ////Guard.NotNullOrEmpty(src, "scr");

            #region Regex

            var regexExp = string.Format(RegExpressionWithPrefix,
                RegMetadatas.IndexOf(prefix) != -1 ? "\\" + prefix : prefix.ToString());

            var matches = Regex.Matches(src, regexExp, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var expressions = new List<string>();
            foreach (Match match in matches)
            {
                if (expressions.IndexOf(match.Groups["exp"].Value) < 0)
                {
                    expressions.Add(match.Groups[GroupName].Value);
                }
            }

            return expressions.ToArray();

            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RightOfRightmostOf(this string text, char c)
        {
            if (text.IsNullOrEmpty())
            {
                return text;
            }

            int i = text.LastIndexOf(c);

            if (i == -1)
            {
                return text;
            }

            return text.Substring(i + 1);
        }

        /// <summary>
        /// 条件转换
        /// </summary>
        /// <param name="filters">条件</param>
        /// <param name="paramaters">参数</param>
        /// <returns></returns>
        public static string ToCondition(this IList<IFilterDescriptor> filters, IDictionary<string, object> paramaters)
        {
            if (paramaters == null)
                paramaters = new Dictionary<string, object>();

            IList<string> list = new List<string>();
            foreach (var filter in filters)
            {
                list.Add(filter.CreateFilter(paramaters));
            }
            return list.JoinString(" AND ");
        }

        /// <summary>
        /// 转化排序SQL
        /// </summary>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static string ToSort(this IList<SortDescriptor> sorts)
        {
            IList<string> list = new List<string>();
            foreach (var sort in sorts)
            {
                list.Add(sort.ToSortString());
            }
            return string.Format(" order by  {0} ", list.JoinString(" , "));
        }

        //public static 



        /// <summary>
        /// 驼峰匹配截断
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string BreakUpCamelCase(this string s)
        {
            var patterns = new[]
            {
                "([a-z])([A-Z])",
                "([0-9])([a-zA-Z])",
                "([a-zA-Z])([0-9])"
            };
            var output = patterns.Aggregate(s, (current, pattern) => Regex.Replace(current, pattern, "$1 $2", RegexOptions.IgnorePatternWhitespace));
            return output;
        }

        /// <summary>
        /// 转换到Base64
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] bytes = encoder.GetBytes(str);

            string encodestr = string.Empty;
            try
            {
                encodestr = Convert.ToBase64String(bytes);
            }
            catch
            {
                encodestr = str;
            }
            return encodestr;
        }

        /// <summary>
        ///返回匹配项在指定字符串中第一次出现位置左边的内容
        /// </summary>
        public static string LeftOf(this string src, string value)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            string ret = src;
            int idx = src.IndexOf(value);
            if (idx != -1)
            {
                ret = src.Substring(0, idx);
            }
            return ret;
        }


        /// <summary>
        ///返回匹配项在指定字符串中第一次出现位置左边的内容
        /// </summary>
        public static string LeftOf(this string src, char value)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            return LeftOf(src, value.ToString());
        }

        /// <summary>
        /// 返回匹配项在指定字符串中第‘n’次出现位置左边的内容
        /// </summary>
        public static string LeftOf(this string src, string value, int n)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            //Guard.NotNullOrEmpty(value, "value");

            string ret = src;
            int idx = -1;
            while (n > 0)
            {
                idx = src.IndexOf(value, idx + 1);
                if (idx == -1)
                {
                    break;
                }
                --n;
            }
            if (idx != -1)
            {
                ret = src.Substring(0, idx);
            }
            return ret;
        }

        /// <summary>
        /// 返回匹配项在指定字符串中第‘n’次出现位置左边的内容
        /// </summary>
        public static string LeftOf(this string src, char value, int n)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            return LeftOf(src, value.ToString(), n);
        }

        /// <summary>
        /// 返回匹配项在指定字符串中第一次出现位置右边的内容
        /// </summary>
        public static string RightOf(this string src, string value)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            //Guard.NotNullOrEmpty(value, "value");

            string ret = String.Empty;
            int idx = src.IndexOf(value);
            if (idx != -1)
            {
                idx += value.Length - 1;
                ret = src.Substring(idx + 1);
            }
            return ret;
        }


        /// <summary>
        /// 排序顺序获取
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static ListSortDirection? Next(this ListSortDirection? direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                return ListSortDirection.Descending;
            }

            if (direction == ListSortDirection.Descending)
            {
                return null;
            }

            return ListSortDirection.Ascending;
        }

        /// <summary>
        /// 返回匹配项在指定字符串中第一次出现位置右边的内容
        /// </summary>
        public static string RightOf(this string src, char value)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            return RightOf(src, value.ToString());
        }

        /// <summary>
        /// 返回匹配项在指定字符串中第n次出现位置右边的内容
        /// </summary>
        public static string RightOf(this string src, string value, int n)
        {
            //Guard.NotNullOrEmpty(src, "scr");

            string ret = String.Empty;
            int idx = -1;
            while (n > 0)
            {
                idx = src.IndexOf(value, idx + 1);
                if (idx == -1)
                {
                    break;
                }
                --n;
            }

            if (idx != -1)
            {
                ret = src.Substring(idx + 1);
            }

            return ret;
        }

        /// <summary>
        /// 返回字符在指定字符串中第n次出现位置右边的内容
        /// </summary>
        public static string RightOf(this string src, char value, int n)
        {
            //Guard.NotNullOrEmpty(src, "scr");
            return RightOf(src, value.ToString(), n);
        }

        /// <summary>
        /// 从Base64还原
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            string decodestr = string.Empty;
            byte[] bytes = Convert.FromBase64String(str);
            try
            {
                decodestr = encoder.GetString(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                decodestr = str;
            }
            return decodestr;
        }

        /// <summary>
        /// 扩展是否相等
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="compare">比较字符串</param>
        /// <returns></returns>
        public static bool Equals(this string s, string compare)
        {
            return string.Equals(s, compare, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 扩展是否为空
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }


        /// <summary>
        /// 判断是否有乱码
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns></returns>
        public static bool IsGarbled(this string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            //239 191 189   
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 集合连接成字符串
        /// </summary>
        /// <param name="objs">对象列表</param>
        /// <param name="splite">分割符</param>
        /// <param name="isAddMark"></param>
        /// <returns></returns>
        public static string JoinString<T>(this IEnumerable<T> objs, string splite = ",", bool isAddMark = false)
        {
            if (!objs.Any())
                return string.Empty;
            return string.Join(splite, objs.Select(p => string.Format("{0}{1}{0}", isAddMark == true ? "'" : "", p.ToString())));
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseSensitiveEqual(this string instance, string comparing)
        {
            return string.CompareOrdinal(instance, comparing) == 0;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseInsensitiveEqual(this string instance, string comparing)
        {
            return string.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }


        private const char SEPARATOR = '_';
        /// <summary>
        /// 转化为驼峰匹配
        /// </summary>
        /// <param name="instance">字符串</param>
        /// <returns></returns>
        public static string ToCamelCase(this string instance)
        {

            if (instance == null)
            {
                return null;
            }
            instance = instance.ToLowerInvariant();
            StringBuilder sb = new StringBuilder(instance.Length);
            bool upperCase = false;
            for (int i = 0; i < instance.Length; i++)
            {
                char c = instance.ToCharArray()[i];
                if(i == 0)
                {
                    sb.Append(Char.ToUpperInvariant(c));
                }
                else if (c == SEPARATOR)
                {
                    upperCase = true;
                }
                else if (upperCase)
                {
                    sb.Append(Char.ToUpperInvariant(c));
                    upperCase = false;
                }
                else
                {
                    sb.Append(char.ToLowerInvariant(c));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 驼峰字符串转化为下划线字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUnderlineName(this string s)
        {
            if (s == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            bool upperCase = false;
            var charArray = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                char c = charArray[i];
                bool nextUpperCase = true;
                if (i < (s.Length - 1))
                {
                    
                    nextUpperCase = Char.IsUpper(charArray[i + 1]);
                }
                if ((i >= 0) && Char.IsUpper(c))
                {
                    if (!upperCase || !nextUpperCase)
                    {
                        if (i > 0) sb.Append(SEPARATOR);
                    }
                    upperCase = true;
                }
                else
                {
                    upperCase = false;
                }

                sb.Append(Char.ToLowerInvariant(c));
            }

            return sb.ToString();
        }



        private static readonly Regex NameExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);
        private static readonly Regex EntityExpression = new Regex("(&amp;|&)#([0-9]+;)", RegexOptions.Compiled);


        /// <summary>
        /// 过滤 Html 
        /// </summary>
        /// <param name="html">HTML文本</param>
        /// <returns></returns>
        public static string EscapeHtmlEntities(this string html)
        {
            return EntityExpression.Replace(html, "$1\\\\#$2");
        }


        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="instance">格式</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }

        /// <summary>
        /// 作为标题
        /// </summary>
        /// <param name="value">文本</param>
        /// <returns></returns>
        public static string AsTitle(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            int lastIndex = value.LastIndexOf(".", StringComparison.Ordinal);

            if (lastIndex > -1)
            {
                value = value.Substring(lastIndex + 1);
            }

            return value.SplitPascalCase();
        }

        /// <summary>
        /// 按照下划线拆分文本
        /// </summary>
        /// <param name="value">文本</param>
        /// <returns></returns>
        public static string SplitPascalCase(this string value)
        {
            return NameExpression.Replace(value, " $1").Trim();
        }


        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static int ToInt(this string value)
        {
            int result = 0;
            if (int.TryParse(value, out result))
                return result;
            return result;
        }

        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static long ToLong(this string value)
        {
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            return result;
        }

        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static long? ToNullLong(this string value)
        {
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            return null;
        }

        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static double ToDouble(this object value)
        {
            long result = 0;
            if (long.TryParse(value.ToString(), out result))
                return result;
            return result;
        }

        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static double ToDouble(this string value)
        {
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            return result;
        }


        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字,如果失败返回0</returns>
        public static double? ToNullDouble(this string value)
        {
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            return null;
        }

        /// <summary>
        /// 字符转化为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static sbyte? ToNullSbyte(this string value)
        {
            sbyte result = 0;
            if (sbyte.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 字符转换为数字
        /// </summary>
        /// <param name="value">输入</param>
        /// <returns>转换成功的数字</returns>
        public static int? ToNullInt(this string value)
        {
            int result = 0;
            if (int.TryParse(value, out result))
                return result;
            return null;
        }

        /// <summary>
        /// 将字符转换成对应的枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符</param>
        /// <returns>对应的枚举值</returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, false);
        }

        /// <summary>
        /// 将字符转换成对应的枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符</param>
        /// <param name="defaultValue">枚举默认值</param>
        /// <returns>对应的枚举值</returns>
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            if (!value.HasValue())
            {
                return defaultValue;
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转化为Unix ticks
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTicks(this DateTime dt)
        {
            var unixTimestampOrigin = new DateTime(1970, 1, 1);
            return (long)new TimeSpan(dt.Ticks - unixTimestampOrigin.Ticks).TotalMilliseconds;
        }

        /// <summary>
        /// 转化时间
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this DateTimeOffset? offset)
        {
            if (offset.HasValue)
            {
                return offset.Value.DateTime;
            }

            return null;
        }

        /// <summary>
        /// Convert String To DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime time;
            DateTime.TryParse(value, out time);
            return time;
        }

        /// <summary>
        /// Convert String To DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object value)
        {
            bool result;
            bool.TryParse(value.ToString(), out result);
            return result;
        }


        /// <summary>
        /// Convert String To DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value)
        {
            DateTime time;
            DateTime.TryParse(value.ToString(), out time);
            return time;
        }

        /// <summary>
        /// 转化可空时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToNullDateTime(this string value)
        {
            DateTime time;
            if (DateTime.TryParse(value, out time))
            {
                return time;
            }
            return null;
        }

        /// <summary>
        /// 格式化字符 格式:hello {youname};
        /// </summary>
        /// <param name="dict">键值对</param>
        /// <param name="format">字符格式</param>
        /// <returns>格式化结果</returns>
        /// <example><code><![CDATA[
        ///    Dictionary<string, object> dict = new Dictionary<string, object>();
        ///    dict.Add("value", 12.3m);
        ///    string format = "Product price is {value:00.00}";
        ///    string expected = "Product price is 12.30";
        ///    string actual;
        ///    actual = Helper.Format(dict, format);
        ///    Assert.AreEqual(expected, actual);
        ///  
        /// ]]></code></example>
        public static string Format(this Dictionary<string, object> dict, string format)
        {
            return Format(format, (field, f) =>
            {
                object value = null;
                dict.TryGetValue(field, out value);
                return string.Format(f, value);
            });
        }


        /// <summary>
        /// 格式化字符 格式:hello {youname};
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="fun">回调函数：@p0 被替换字符名称 @p1 格式 return 返回值</param>
        /// <returns>格式化结果></returns>
        /// <example><coce>
        /// <![CDATA[
        ///  string format = "href='http:\\localhost\\DataPresentation\\{page}.aspx?id={Id}'";
        ///  string expected = "href='http:\\localhost\\DataPresentation\\TestView.aspx?id=1'";
        ///  string actual;
        ///  actual = Helper.Format(format, (field, f) =>
        ///     {
        ///         if (field == "Id")
        ///             return string.Format(f, 1);
        ///         else
        ///             return string.Format(f, "TestView");
        ///     });
        ///  Assert.AreEqual(expected, actual);
        /// ]]>
        /// </coce>
        /// </example>
        public static string Format(this string format, Func<string, string, string> fun)
        {
            var reg = new Regex(@"{(?<name>\w+)[\:\S]*?}");
            //reg.Replace(format, 
            return reg.Replace(format, new MatchEvaluator(m =>
            {
                string field = m.Groups["name"].Value;
                string f = m.Groups[0].Value.Replace(field, "0");
                return fun(field, f);

            }));
        }

        /// <summary>
        /// 新建GUID序列
        /// </summary>
        /// <returns></returns>
        public static Guid NewSequentialGuid()
        {
            byte[] uid = Guid.NewGuid().ToByteArray();
            byte[] binDate = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            byte[] secuentialGuid = new byte[uid.Length];

            secuentialGuid[0] = uid[0];
            secuentialGuid[1] = uid[1];
            secuentialGuid[2] = uid[2];
            secuentialGuid[3] = uid[3];
            secuentialGuid[4] = uid[4];
            secuentialGuid[5] = uid[5];
            secuentialGuid[6] = uid[6];
            // set the first part of the 8th byte to '1100' so     
            // later we'll be able to validate it was generated by us   
            secuentialGuid[7] = (byte)(0xc0 | (0xf & uid[7]));
            // the last 8 bytes are sequential,    
            // it minimizes index fragmentation   
            // to a degree as long as there are not a large    
            // number of Secuential-Guids generated per millisecond  
            secuentialGuid[9] = binDate[0];
            secuentialGuid[8] = binDate[1];
            secuentialGuid[15] = binDate[2];
            secuentialGuid[14] = binDate[3];
            secuentialGuid[13] = binDate[4];
            secuentialGuid[12] = binDate[5];
            secuentialGuid[11] = binDate[6];
            secuentialGuid[10] = binDate[7];

            return new Guid(secuentialGuid);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="Tfrom"></typeparam>
        ///// <typeparam name="Tto"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static List<Tto> ConvertTo<Tfrom, Tto>(this List<Tfrom> value)
        //    where Tto : class, new()
        //    where Tfrom : class
        //{
        //    return value.Select(c => ConvertTo<Tfrom, Tto>(c)).ToList();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="Tfrom"></typeparam>
        ///// <typeparam name="Tto"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static Tto ConvertTo<Tfrom, Tto>(Tfrom value)
        //    where Tto : class, new()
        //    where Tfrom : class
        //{
        //    if (value == default(Tfrom))
        //        return default(Tto);

        //    var result = new Tto();
        //    var ps = typeof(Tfrom).GetProperties(false, false);

        //    foreach (var p in ps)
        //    {
        //        var val = value.GetValue(p.Name);
        //        result.SetValue(p.Name, val);
        //    }

        //    return result;
        //}


        /// <summary>
        /// 获取加密字符串
        /// </summary>
        /// <param name="encryptValue">需加密的字符串</param>
        /// <returns></returns>
        public static string DesEncryption(this string encryptValue)
        {
            string key = "UWAY@SOFT2009";
            //byte[] keyByte = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            // byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string urlParam = null;
            try
            {
                DESCryption desCry = new DESCryption(key);
                urlParam = desCry.Encrypt(encryptValue);

            }
            catch (Exception ex)
            {
                //Log4NetHelper.WriteError(typeof(Helper), ex);
                return "";
            }
            return urlParam;
        }

        /// <summary>
        /// 强制转化为string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToStr(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            if (obj.Equals(DBNull.Value))
            {
                return string.Empty;
            }
            return Convert.ToString(obj);
        }

        /// <summary>
        /// 获取解密字符串
        /// </summary>
        /// <param name="decryptValue">需解密的字符串</param>
        /// <returns></returns>
        public static string DesDecryption(this string decryptValue)
        {
            string key = "UWAY@SOFT2009";
            // byte[] keyByte = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            // byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string urlParam = null;
            try
            {
                DESCryption desCry = new DESCryption(key);
                urlParam = desCry.Decrypt(decryptValue);

            }
            catch (Exception ex)
            {
                //Log4NetHelper.WriteError(typeof(Helper), ex);
                return "";
            }
            return urlParam;
        }




        /// <summary>
        /// 深度复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SourceObj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T SourceObj) where T : new()
        {
            string rtnStr = SourceObj.JsonSerialize();
            object obj = rtnStr.JsonDeserialize<T>();
            T DestObj = default(T);
            if (obj != null && obj is T)
            {
                DestObj = (T)obj;
            }
            return DestObj;
        }


        /// <summary>
        /// 深度复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SourceObj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this object SourceObj)
        {
            string rtnStr = SourceObj.JsonSerialize();
            object obj = rtnStr.JsonDeserialize<T>();
            T DestObj = default(T);
            if (obj != null && obj is T)
            {
                DestObj = (T)obj;
            }
            return DestObj;
        }
        //#endif

        /// <summary>
        /// 是否包含字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string target, StringComparison comp)
        {
            return source.IndexOf(target, comp) > -1;
        }

        /// <summary>
        /// 替换字符特殊方法，括号中的不替换
        /// </summary>
        /// <param name="target"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="bracketsLevel">括号层级，从最外层开始算。</param>
        /// <returns></returns>
        public static string SpecialReplace(this string target, string oldValue, string newValue, int bracketsLevel = 0)
        {
            List<string> split = new List<string>();
            StringBuilder str = new StringBuilder();
            int i = 0;

            foreach (var s in target)
            {
                if (s.Equals('('))
                {
                    if (i == bracketsLevel)
                    {
                        split.Add(str.ToString().Replace(oldValue, newValue));
                        str.Clear();
                    }
                    i++;
                    str.Append(s);
                }
                else if (s.Equals(')'))
                {
                    str.Append(s);
                    i--;
                    if (i == bracketsLevel)
                    {
                        split.Add(str.ToString());
                        str.Clear();
                    }
                }
                else
                {
                    str.Append(s);
                }
            }
            split.Add(str.ToString().Replace(oldValue, newValue));
            return string.Join("", split);
        }

        /// <summary>
        /// 消除冗余的括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SimplifyBracket(this string str)
        {
            Dictionary<int, Stack<int>> levels = new Dictionary<int, Stack<int>>();
            int level = -1;
            int lastLeft = -1;//上一次级别对应左括号索引
            bool isRightBracket = false;
            List<string> builder = new List<string>();

            for (int i = 0; i < str.Length; i++)
            {
                builder.Add(str[i].ToString());

                if (str[i].Equals('('))
                {
                    level++;
                    if (!levels.ContainsKey(level))
                    {
                        levels[level] = new Stack<int>();
                    }

                    levels[level].Push(i);
                    isRightBracket = false;

                }
                else if (str[i].Equals(')'))
                {
                    if (level < 0 || levels[level].Count == 0)
                    {
                        return str;
                    }

                    int curLeft = levels[level].Peek();
                    if (isRightBracket && level > 0)
                    {

                        for (int k = lastLeft - 1; k >= 0; k--)
                        {
                            if (k == curLeft)
                            {
                                builder[curLeft] = "";
                                builder[i] = "";
                                break;
                            }

                            if (!string.IsNullOrWhiteSpace(builder[k]))
                            {
                                break;
                            }

                        }

                    }

                    levels[level].Pop();
                    isRightBracket = true;
                    lastLeft = curLeft;
                    level--;
                }
                else if (!str[i].Equals(' '))
                {
                    isRightBracket = false;
                }

            }

            return string.Join("", builder);
        }

        /// <summary>
        /// Clone实体属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="old"></param>
        /// <returns></returns>
        public static T CloneEntity<T>(this T old) where T : new()
        {
            if (old == null)
            {
                return default(T);
            }
            T t = new T();

            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                p.SetValue(t, p.GetValue(old, null), null);
            }

            return t;
        }

        #region IEnumerable<T>.MapReduce
        #region -- IDictionary --
        /// <summary>
        /// 字典值集合中添加值？
        /// Adds an Enumerable to a Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="list"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        public static void Add<T, TK, TV>(this IDictionary<TK, List<TV>> dictionary,
          IEnumerable<T> list,
          Func<T, TK> keySelector,
          Func<T, TV> valueSelector)
        {
            lock (dictionary)
            {
                foreach (var item in list)
                {
                    var key = keySelector(item);
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary[key] = new List<TV>();
                    }
                    dictionary[key].Add(valueSelector(item));
                }
            }
        }
        #endregion
        #region -- IEnumberable --
        /// <summary>
        /// Divides an enumerable into equal parts and performs an action on those parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="parts"></param>
        /// <param name="action"></param>
        public static void DivvyUp<T>(this IEnumerable<T> enumerable, int parts, Action<IEnumerable<T>, int, int> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            var actions = new List<Action>();
            if (parts == 0)
                parts = 1;
            int count = enumerable.Count();
            int itemsPerPart = count / parts;
            if (itemsPerPart == 0)
                itemsPerPart = 1;
            for (int i = 0; i < parts; i++)
            {
                var collection = enumerable
                    .Skip(i * itemsPerPart)
                    .Take(i == parts - 1 ? count : itemsPerPart);
                int j = i; // access to modified closure safety
                actions.Add(() => action(collection, j, itemsPerPart));
            }
            //并行执行对象列表中方法
            Parallel.Invoke(actions.ToArray());
        }
        /// <summary>
        /// 接口的扩展方法（可枚举对象的扩展方法）
        /// Divides an enumerable into equal parts and performs an action on those parts
        /// </summary>
        /// <typeparam name="T">
        /// ?KeyValuePair&lt;   T3,List&lt;T4&gt;&gt;?
        /// </typeparam>
        /// <param name="enumerable">可枚举对象</param>
        /// <param name="parts">均分数</param>
        /// <param name="action">执行方法</param>
        public static void DivvyUp<T>(this IEnumerable<T> enumerable, int parts, Action<IEnumerable<T>> action)
        {
            DivvyUp(enumerable, parts, (subset, i, j) => action(subset));
        }
        #endregion

        #endregion

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static string JsonSerialize(this object val)
        {
            var iso = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(val, iso);
        }

        /// <summary>
        /// 获取JSON的属性值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string GetPropValue(this string json, string prop)
        {
            if (string.IsNullOrWhiteSpace(json)) return string.Empty;

            var jObj = JObject.Parse(json);

            return jObj.Value<string>(prop);
        }

        //public static dynamic ToDynamic(this string json)
        //{
        //    return JObject.Parse(json);

        //}
        //public static string ToJson(dynamic d)
        //{
        //    return JObject.FromObject(d).ToString();
        //}



        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static object JsonDeserialize(this string val)
        {
            return JsonConvert.DeserializeObject(val);
        }


        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string val)
        {
            return JsonConvert.DeserializeObject<T>(val);
        }


        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Array 中的第一个匹配元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] items, Predicate<T> predicate)
        {
            return Array.Find(items, predicate);
        }

        /// <summary>
        ///  检索与指定谓词定义的条件匹配的所有元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] items, Predicate<T> predicate)
        {
            return Array.FindAll(items, predicate);
        }


        /// <summary>
        /// combination helper method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T item1, T item2, params T[] items)
        {
            if (item1 == null && item2 == null)
                throw new ArgumentNullException("item1 and item2");

            if (item1 == null)
                return Combine<T>(item2, items);

            if (item2 == null)
                return Combine<T>(item1, items);

            //if we reached here then item1 and item2 are not null
            if (items == null)
            {
                return new T[2] { item1, item2 };
            }
            else
            {
                T[] combination = new T[items.Length + 2];
                combination[0] = item1;
                combination[1] = item2;
                for (int i = 2; i < combination.Length; i++)
                {
                    combination[i] = items[i - 2];
                }
                return combination;
            }
        }
        /// <summary>
        /// combination helper method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T item, params T[] items)
        {
            if (items == null)
            {
                return new T[1] { item };
            }
            else
            {
                T[] combination = new T[items.Length + 1];
                combination[0] = item;
                for (int i = 1; i < combination.Length; i++)
                {
                    combination[i] = items[i - 1];
                }
                return combination;
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IQueryable<T> queryable)
        {
            return queryable.Count() != 0;
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool TrueForAll<T>(this IEnumerable<T> coll, Predicate<T> predicate)
        {
            Trace.Assert(coll != null, "coll == null");
            Trace.Assert(predicate != null, "predicate == null");

            using (var it = coll.GetEnumerator())
                while (it.MoveNext())
                {
                    if (!predicate(it.Current))
                        return false;
                }

            return true;
        }

        /// <summary>
        /// 遍历当前对象，并且调用方法进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="coll">实例</param>
        /// <param name="action">方法</param>
        /// <returns>当前集合</returns>
        public static void ForEach<T>(this IEnumerable<T> coll, Action<T> action)
        {
            Trace.Assert(coll != null, "coll == null");
            Trace.Assert(action != null, "action == null");

            var it = coll.GetEnumerator();
            while (it.MoveNext())
                action(it.Current);
        }

        /// <summary>
        /// 遍历当前对象，并且调用方法进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="action">方法</param>
        /// <returns>当前集合</returns>
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in instance)
                action(item, index++);
        }

        /// <summary>
        /// 匹配算法
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="recursiveSelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source,
           Func<TSource, IEnumerable<TSource>> recursiveSelector)
        {
            Stack<IEnumerator<TSource>> stack = new Stack<IEnumerator<TSource>>();
            stack.Push(source.GetEnumerator());

            try
            {
                while (stack.Count > 0)
                {
                    if (stack.Peek().MoveNext())
                    {
                        TSource current = stack.Peek().Current;

                        yield return current;

                        IEnumerable<TSource> children = recursiveSelector(current);
                        if (children != null)
                        {
                            stack.Push(children.GetEnumerator());
                        }
                    }
                    else
                    {
                        stack.Pop().Dispose();
                    }
                }
            }
            finally
            {
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
        }


        /// <summary>
        /// 转化为只读集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> collection)
        {
            ReadOnlyCollection<T> roc = collection as ReadOnlyCollection<T>;
            if (roc == null)
            {
                if (collection == null)
                {
                    roc = EmptyReadOnlyCollection<T>.Empty;
                }
                else
                {
                    roc = new List<T>(collection).AsReadOnly();
                }
            }
            return roc;
        }


        /// <summary>
        /// 转化为泛型集合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable AsGenericEnumerable(this IEnumerable source)
        {
            Type elementType = typeof(Object);

            Type type = source.GetType().FindGenericType(typeof(IEnumerable<>));
            if (type != null)
            {
                return source;
            }

            IEnumerator enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    elementType = enumerator.Current.GetType();
                    try
                    {
                        enumerator.Reset();
                    }
                    catch
                    {
                    }
                    break;
                }
            }

            Type genericType = typeof(GenericEnumerable<>).MakeGenericType(elementType);
            object[] constructorParameters = new object[] { source };

            return (IEnumerable)Activator.CreateInstance(genericType, constructorParameters);
        }


        /// <summary>
        /// 动态计算
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Consolidate<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return ZipIterator(first, second, resultSelector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(
            IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            using (IEnumerator<TFirst> e1 = first.GetEnumerator())
            using (IEnumerator<TSecond> e2 = second.GetEnumerator())
                while (e1.MoveNext() && e2.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current);
        }

        /// <summary>
        /// 查询元素位置
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="lst">IEnumerable</param>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> lst, Predicate<T> predicate)
        {
            var index = -1;
            foreach (var item in lst)
            {
                index += 1;
                if (predicate(item))
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// 查找集合中的某个元素的位置
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="item">元素</param>
        /// <returns></returns>
        public static int IndexOf(this IEnumerable source, object item)
        {
            int index = 0;
            foreach (object i in source)
            {
                if (Equals(i, item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// 集合类型转化
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="coll"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static IEnumerable<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> coll, Converter<TInput, TOutput> converter)
        {
            Trace.Assert(coll != null, "coll == null");
            Trace.Assert(converter != null, "converter == null");

            return from input in coll
                   select converter(input);
        }


        /// <summary>
        /// 按照条件判断是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> coll, Predicate<T> predicate)
        {
            Trace.Assert(coll != null, "coll == null");
            Trace.Assert(predicate != null, "predicate == null");

            var it = coll.GetEnumerator();
            while (it.MoveNext())
                if (predicate(it.Current))
                    return true;

            return false;
        }

        /// <summary>
        ///  确定序列中的任何元素是否都满足条件。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool Any<T>(this T t, IEnumerable<T> c)
        {
            return c.Any(i => i.Equals(t));
        }

        /// <summary>
        /// JSON OBJECT转化IDictionary
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<IDictionary<string, object>> ToJson(this IEnumerable<JsonObject> items)
        {
            return items.Select(i => i.ToJson());
        }


        /// <summary>
        /// 转化成CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="delim"></param>
        /// <returns></returns>
        public static string ToCSV<T>(this IEnumerable<T> collection, string delim)
        {
            if (collection == null)
            {
                return "";
            }

            StringBuilder result = new StringBuilder();
            foreach (T value in collection)
            {
                result.Append(value);
                result.Append(delim);
            }
            if (result.Length > 0)
            {
                result.Length -= delim.Length;
            }
            return result.ToString();
        }

        /// <summary>
        /// 默认只读集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class DefaultReadOnlyCollection<T>
        {
            private static ReadOnlyCollection<T> defaultCollection;

            internal static ReadOnlyCollection<T> Empty
            {
                get
                {
                    if (defaultCollection == null)
                    {
                        defaultCollection = new ReadOnlyCollection<T>(new T[0]);
                    }
                    return defaultCollection;
                }
            }
        }

        class EmptyReadOnlyCollection<T>
        {
            internal static readonly ReadOnlyCollection<T> Empty = new List<T>().AsReadOnly();
        }
    }

    
}


