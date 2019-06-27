using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Common
{
    using System.Globalization;

    /// <summary>
    /// Linq 函数映射类，把成员操作映射到UWay.Skynet.Cloud.Data 所支持的Linq函数操作上
    /// </summary>
    public class MethodMapping
    {


        internal static LambdaExpression ConvertMember(MemberInfo mi)
        {
            LambdaExpression expr;
            Mappings.TryGetValue(mi, out expr);
            return expr;
        }

        /// <summary>
        /// 通过Lambda 得到Lambda函数内部的成员对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static MemberInfo Member<T>(Expression<Func<T, object>> func)
        {
            return Expressor.Member(func);
        }

        /// <summary>
        /// 通过Lambda 得到Lambda函数内部的成员对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static MemberInfo Member(Expression<Func<object>> func)
        {
            return Expressor.Member(func);
        }

        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<TR>(Expression<Func<TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, TR>(Expression<Func<T1, TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, T2, TR>(Expression<Func<T1, T2, TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, T2, T3, TR>(Expression<Func<T1, T2, T3, TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, T2, T3, T4, TR>(Expression<Func<T1, T2, T3, T4, TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, T2, T3, T4, T5, TR>(Expression<Func<T1, T2, T3, T4, T5, TR>> func)
        {
            return func;
        }
        /// <summary>
        /// 得到UWay.Skynet.Cloud.Data Linq 所支持的Lambda表达式
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static LambdaExpression Lambda<T1, T2, T3, T4, T5, T6, TR>(Expression<Func<T1, T2, T3, T4, T5, T6, TR>> func)
        {
            return func;
        }

        /// <summary>
        /// 成员映射表
        /// </summary>
        public static readonly Dictionary<MemberInfo, LambdaExpression> Mappings;

        static MethodMapping()
        {
            Mappings = new Dictionary<MemberInfo, LambdaExpression>();
            Mappings[Member(() => UWay.Skynet.Cloud.Helper.LeftOf("", ' '))] = Lambda<string, char?, string>((obj, p0) => SqlFunctions.LeftOf(obj, p0.ToString()));
            Mappings[Member(() => UWay.Skynet.Cloud.Helper.LeftOf("", ""))] = Lambda<string, string, string>((obj, p0) => SqlFunctions.LeftOf(obj, p0));
            Mappings[Member(() => UWay.Skynet.Cloud.Helper.RightOf("", ' '))] = Lambda<string, char?, string>((obj, p0) => SqlFunctions.RightOf(obj, p0.ToString()));
            Mappings[Member(() => UWay.Skynet.Cloud.Helper.RightOf("", ""))] = Lambda<string, string, string>((obj, p0) => SqlFunctions.RightOf(obj, p0));

            Mappings[Member(() => string.Concat((object)1))] = Lambda<object, string>((obj) => obj == null ? null : obj.ToString());
            Mappings[Member(() => Guid.NewGuid())] = Lambda<Guid>(() => SqlFunctions.NewGuid());
            Mappings[Member(() => Convert.ChangeType(1, typeof(bool)))] = Lambda<object, Type, object>((_, t) => SqlFunctions.Convert(_, t));
            Mappings[Member(() => char.ToLower(' '))] = Lambda<char?, char?>(c => SqlFunctions.ToLower(c));
            Mappings[Member(() => char.ToUpper(' '))] = Lambda<char?, char?>(c => SqlFunctions.ToUpper(c));
            //Members[M(() => new byte[0].Length)] = L<byte[], int>(c => Sql.Length(c).Value);
            Mappings[Member(() => "".Length)] = Lambda<string, int>(obj => SqlFunctions.Length(obj).Value);
            Mappings[Member(() => ""[0])] = Lambda<string, int, char>((obj, p0) => SqlFunctions.Convert<string, char>(SqlFunctions.Substring(obj, p0 + 1, 1)));
            Mappings[Member(() => "".Insert(0, ""))] = Lambda<string, int, string, string>((obj, p0, p1) => SqlFunctions.Insert(obj, p0 + 1, p1));
            Mappings[Member(() => "".Remove(0))] = Lambda<string, int, string>((obj, p0) => SqlFunctions.Remove(obj, p0 + 1));
            Mappings[Member(() => "".Remove(0, 0))] = Lambda<string, int, int, string>((obj, p0, p1) => SqlFunctions.Remove(obj, p0 + 1, p1));
            Mappings[Member(() => "".PadLeft(0))] = Lambda<string, int, string>((obj, p0) => SqlFunctions.PadLeft(obj, p0, ' '));
            Mappings[Member(() => "".PadLeft(0, ' '))] = Lambda<string, int, char, string>((obj, p0, p1) => SqlFunctions.PadLeft(obj, p0, p1));
            Mappings[Member(() => "".PadRight(0))] = Lambda<string, int, string>((obj, p0) => SqlFunctions.PadRight(obj, p0, ' '));
            Mappings[Member(() => "".PadRight(0, ' '))] = Lambda<string, int, char, string>((obj, p0, p1) => SqlFunctions.PadRight(obj, p0, p1));
            Mappings[Member(() => "".Replace("", ""))] = Lambda<string, string, string, string>((obj, p0, p1) => SqlFunctions.Replace(obj, p0, p1));
            Mappings[Member(() => "".Replace(' ', ' '))] = Lambda<string, char, char, string>((obj, p0, p1) => SqlFunctions.Replace(obj, p0, p1));
            Mappings[Member(() => "".Trim())] = Lambda<string, string>(obj => SqlFunctions.Trim(obj));
            Mappings[Member(() => "".TrimEnd())] = Lambda<string, char[], string>((obj, ch) => SqlFunctions.TrimEnd(obj));
            Mappings[Member(() => "".TrimStart())] = Lambda<string, char[], string>((obj, ch) => SqlFunctions.TrimStart(obj));
            Mappings[Member(() => "".ToLower())] = Lambda<string, string>(obj => SqlFunctions.ToLower(obj));
            Mappings[Member(() => "".ToUpper())] = Lambda<string, string>(obj => SqlFunctions.ToUpper(obj));
            Mappings[Member(() => "".Reverse())] = Lambda<string, string>((obj) => SqlFunctions.Reverse(obj));
            Mappings[Member(() => SqlFunctions.Left("", 0))] = Lambda<string, int, string>((obj, p0) => obj.Substring(1, p0));
            Mappings[Member(() => SqlFunctions.Right("", 0))] = Lambda<string, int, string>((obj, p0) => obj.Substring(SqlFunctions.Length(obj).Value, p0));

            Mappings[Member(() => string.IsNullOrEmpty(""))] = Lambda<string, bool>(p0 => p0 == null || p0.Length == 0);
            Mappings[Member(() => string.CompareOrdinal("", ""))] = Lambda<string, string, int>((s1, s2) => s1.CompareTo(s2));
            Mappings[Member(() => string.CompareOrdinal("", 0, "", 0, 0))] = Lambda<string, int, string, int, int, int>((s1, i1, s2, i2, l) => s1.Substring(i1, l).CompareTo(s2.Substring(i2, l)));
            Mappings[Member(() => string.Compare("", 0, "", 0, 0))] = Lambda<string, int, string, int, int, int>((s1, i1, s2, i2, l) => s1.Substring(i1, l).CompareTo(s2.Substring(i2, l)));
            Mappings[Member(() => string.Compare("", 0, "", 0, 0, true))] = Lambda<string, int, string, int, int, bool, int>((s1, i1, s2, i2, l, b) => b ? s1.Substring(i1, l).ToLower().CompareTo(s2.Substring(i2, l).ToLower()) : s1.Substring(i1, l).CompareTo(s2.Substring(i2, l)));
            Mappings[Member(() => "".IndexOf("", 0, 0, StringComparison.Ordinal))] = Lambda<string, string, int, int, StringComparison, int?>((obj, p0, p1, p2, sc) =>
                    sc == StringComparison.CurrentCulture || sc == StringComparison.InvariantCulture || sc == StringComparison.Ordinal
                    ? (SqlFunctions.IndexOf(obj, p0, p1 + 1, p2).Value)
                    : (SqlFunctions.IndexOf(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), p1 + 1, p2).Value));
            Mappings[Member(() => new DateTime(0, 0, 0, 0, 0, 0))] = Lambda<int, int, int, int, int, int, DateTime>((y, m, d, h, mm, s) => SqlFunctions.ToDateTime(y, m, d, h, mm, s).Value);


            Mappings[Member(() => string.Compare("", ""))] = Lambda<string, string, int>((s1, s2) => s1.CompareTo(s2));
            Mappings[Member(() => string.Compare("", "", true))] = Lambda<string, string, bool, int>((s1, s2, b) => b ? s1.ToLower().CompareTo(s2.ToLower()) : s1.CompareTo(s2));
            Mappings[Member(() => "".CompareTo("" as object))] = Lambda<string, object, int>((s, o) => s.CompareTo(o.ToString()));
            Mappings[Member(() => "".Equals("", StringComparison.Ordinal))] = Lambda<string, string, StringComparison, bool>((s1, s2, sc) =>
                sc == StringComparison.CurrentCulture || sc == StringComparison.InvariantCulture || sc == StringComparison.Ordinal
                ? s1.Equals(s2)
                : SqlFunctions.ToLower(s1).Equals(SqlFunctions.ToLower(s2)));
            Mappings[Member(() => string.Equals("", "", StringComparison.Ordinal))] = Lambda<string, string, StringComparison, bool>((s1, s2, sc) =>
               sc == StringComparison.CurrentCulture || sc == StringComparison.InvariantCulture || sc == StringComparison.Ordinal
               ? s1.Equals(s2)
               : SqlFunctions.ToLower(s1).Equals(SqlFunctions.ToLower(s2)));
            Mappings[Member(() => "".Contains(' '))] = Lambda<string, char, bool>((obj, p0) => SqlFunctions.Contains(obj, p0.ToString()));
            Mappings[Member(() => "".Contains(' '))] = Lambda<string, char, bool>((obj, p0) => SqlFunctions.Contains(obj, p0.ToString()));
            Mappings[Member(() => "".StartsWith(""))] = Lambda<string, string, bool>((obj, p0) => SqlFunctions.StartsWith(obj, p0.ToString()));
            Mappings[Member(() => "".StartsWith("", true, CultureInfo.CurrentCulture))] = Lambda<string, string, bool, CultureInfo, bool>((obj, p0, i, c) =>
                    i
                    ? SqlFunctions.StartsWith(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0))
                    : SqlFunctions.StartsWith(obj, p0));
            Mappings[Member(() => "".StartsWith("", StringComparison.OrdinalIgnoreCase))] = Lambda<string, string, StringComparison, bool>((obj, p0, i) =>
                    i == StringComparison.Ordinal || i == StringComparison.InvariantCulture || i == StringComparison.CurrentCulture
                    ? SqlFunctions.StartsWith(obj, p0)
                    : SqlFunctions.StartsWith(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0)));
            Mappings[Member(() => "".EndsWith(""))] = Lambda<string, string, bool>((obj, p0) => SqlFunctions.EndsWith(obj, p0.ToString()));
            Mappings[Member(() => "".EndsWith("", true, CultureInfo.CurrentCulture))] = Lambda<string, string, bool, CultureInfo, bool>((obj, p0, i, c) =>
                    i
                    ? SqlFunctions.EndsWith(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0.ToString()))
                    : SqlFunctions.EndsWith(obj, p0.ToString()));
            Mappings[Member(() => "".EndsWith("", StringComparison.OrdinalIgnoreCase))] = Lambda<string, string, StringComparison, bool>((obj, p0, i) =>
                    i == StringComparison.Ordinal || i == StringComparison.InvariantCulture || i == StringComparison.CurrentCulture
                    ? SqlFunctions.EndsWith(obj, p0.ToString())
                    : SqlFunctions.EndsWith(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0.ToString())));
            //Mappings[Member(() => "".Contains(' '))] = Lambda<string, char, bool>((obj, p0) => SqlFunctions.Like(obj, p0.ToString(), true, true, true));
            //Mappings[Member(() => "".Contains(""))] = Lambda<string, string, bool>((obj, p0) => SqlFunctions.Like(obj, p0, true, true, true));
            //Mappings[Member(() => "".StartsWith(""))] = Lambda<string, string, bool>((obj, p0) => SqlFunctions.Like(obj, p0, true, false, true));
            //Mappings[Member(() => "".StartsWith("", true, CultureInfo.CurrentCulture))] = Lambda<string, string, bool, CultureInfo, bool>((obj, p0, i, c) =>
            //        i
            //        ? SqlFunctions.Like(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), true, false, true)
            //        : SqlFunctions.Like(obj, p0, true, false, true));
            //Mappings[Member(() => "".StartsWith("", StringComparison.OrdinalIgnoreCase))] = Lambda<string, string, StringComparison, bool>((obj, p0, i) =>
            //        i == StringComparison.Ordinal || i == StringComparison.InvariantCulture || i == StringComparison.CurrentCulture
            //        ? SqlFunctions.Like(obj, p0, true, false, true)
            //        : SqlFunctions.Like(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), true, false, true));
            //Mappings[Member(() => "".EndsWith(""))] = Lambda<string, string, bool>((obj, p0) => SqlFunctions.Like(obj, p0, false, true, true));
            //Mappings[Member(() => "".EndsWith("", true, CultureInfo.CurrentCulture))] = Lambda<string, string, bool, CultureInfo, bool>((obj, p0, i, c) =>
            //        i
            //        ? SqlFunctions.Like(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), false, true, true)
            //        : SqlFunctions.Like(obj, p0, false, true, true));
            //Mappings[Member(() => "".EndsWith("", StringComparison.OrdinalIgnoreCase))] = Lambda<string, string, StringComparison, bool>((obj, p0, i) =>
            //        i == StringComparison.Ordinal || i == StringComparison.InvariantCulture || i == StringComparison.CurrentCulture
            //        ? SqlFunctions.Like(obj, p0, false, true, true)
            //        : SqlFunctions.Like(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), false, true, true));
            Mappings[Member(() => SqlFunctions.Like("","", true, true, true))] = Lambda<string, char, bool>((obj, p0) => SqlFunctions.Like(obj, p0.ToString(), true, true, true));
            Mappings[Member(() => "".Substring(0))] = Lambda<string, int, string>((obj, p0) => SqlFunctions.Substring(obj, p0 + 1, obj.Length - p0));
            Mappings[Member(() => "".Substring(0, 0))] = Lambda<string, int, int, string>((obj, p0, p1) => SqlFunctions.Substring(obj, p0 + 1, p1));
            //changed
            Mappings[Member(() => "".IndexOf(""))] = Lambda<string, string, int?>((obj, p0) => SqlFunctions.IndexOf(obj, p0).Value);
            //changed
            Mappings[Member(() => "".IndexOf("", 0))] = Lambda<string, string, int, int?>((obj, p0, p1) => SqlFunctions.IndexOf(obj, p0, p1 + 1).Value);
            //changed by zxm
            Mappings[Member(() => "".IndexOf("", 0, 0))] = Lambda<string, string, int, int, int?>((obj, p0, p1, p2) => SqlFunctions.IndexOf(obj, p0, p1 + 1, p2).Value);
            Mappings[Member(() => "".IndexOf(' '))] = Lambda<string, char, int?>((obj, p0) => (SqlFunctions.IndexOf(obj, p0.ToString()).Value));
            Mappings[Member(() => "".IndexOf(' ', 0))] = Lambda<string, char, int, int?>((obj, p0, p1) => (SqlFunctions.IndexOf(obj, p0.ToString(), p1 + 1).Value));
            //changed by zxm
            Mappings[Member(() => "".IndexOf(' ', 0, 0))] = Lambda<string, char, int, int, int?>((obj, p0, p1, p2) => (SqlFunctions.IndexOf(obj, p0.ToString(), p1 + 1, p2).Value));
            Mappings[Member(() => "".IndexOf("", StringComparison.Ordinal))] = Lambda<string, string, StringComparison, int?>((obj, p0, sc) =>
                   sc == StringComparison.CurrentCulture || sc == StringComparison.InvariantCulture || sc == StringComparison.Ordinal
                   ? (SqlFunctions.IndexOf(obj, p0).Value)
                   : (SqlFunctions.IndexOf(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0)).Value));
            Mappings[Member(() => "".IndexOf("", 0, StringComparison.Ordinal))] = Lambda<string, string, int, StringComparison, int?>((obj, p0, p1, sc) =>
                    sc == StringComparison.CurrentCulture || sc == StringComparison.InvariantCulture || sc == StringComparison.Ordinal
                    ? (SqlFunctions.IndexOf(obj, p0, p1 + 1).Value)
                    : (SqlFunctions.IndexOf(SqlFunctions.ToLower(obj), SqlFunctions.ToLower(p0), p1 + 1).Value));
            //Members[M(() => "".LastIndexOf(""))] = L<string, string, int?>((obj, p0) => Sql.LastIndexOf(obj,p0));
            //Members[M(() => "".LastIndexOf(' '))] = L<string, char, int?>((obj, p0) => Sql.LastIndexOf(obj, p0));
            //Members[M(() => "".LastIndexOf(' ', 0))] = L<string, char, int, int?>((obj, p0, p1) => Sql.LastIndexOf(obj, p0, p1 + 1));
            //Members[M(() => "".LastIndexOf(' ', 0, 0))] = L<string, char, int, int, int?>((obj, p0, p1, p2) => Sql.LastIndexOf(obj, p0, p1 + 1, p2));
            //changed by zxm
            //Mappings[Member(() => "".LastIndexOf(' ', 0))] = Lambda<string, char, int, int?>((obj, p0, p1) => (SqlFunctions.IndexOf(obj, p0.ToString(), p1 + 1).Value+1) == 0 ? -1 : obj.Length - (SqlFunctions.IndexOf(SqlFunctions.Reverse(obj.Substring(p1, obj.Length - p1)),p0.ToString()).Value+1));
            Mappings[Member(() => "".LastIndexOf(' ', 0))] = Lambda<string, char, int, int?>((obj, p0, p1) => SqlFunctions.LastIndexOf(obj, p0.ToString(), p1 + 1));
            Mappings[Member(() => "".LastIndexOf(' ', 0, 0))] = Lambda<string, char, int, int, int?>((obj, p0, p1, p2) => SqlFunctions.LastIndexOf(obj, p0.ToString(), p1 + 1, p2));
            Mappings[Member(() => "".LastIndexOf(' '))] = Lambda<string, char, int?>((obj, p0) => (SqlFunctions.IndexOf(obj, p0.ToString()).Value + 1) == 0 ? -1 : obj.Length - (SqlFunctions.IndexOf(SqlFunctions.Reverse(obj), p0.ToString()).Value + 1));
            Mappings[Member(() => "".LastIndexOf(""))] = Lambda<string, string, int?>((obj, p0) => p0.Length == 0 ? obj.Length - 1 : (SqlFunctions.IndexOf(obj, p0).Value + 1) == 0 ? -1 : obj.Length - (SqlFunctions.IndexOf(SqlFunctions.Reverse(obj), SqlFunctions.Reverse(p0)).Value + 1) - p0.Length + 1) /* Sql.LastIndexOf(obj,p0))*/;
            Mappings[Member(() => "".LastIndexOf("", 0))] = Lambda<string, string, int, int?>((obj, p0, p1) => SqlFunctions.LastIndexOf(obj, p0, p1 + 1));
            Mappings[Member(() => "".LastIndexOf("", 0, 0))] = Lambda<string, string, int, int, int?>((obj, p0, p1, p2) => SqlFunctions.LastIndexOf(obj, p0, p1 + 1, p2));


            Mappings[Member(() => DateTime.Now)] = Lambda<DateTime>(() => SqlFunctions.Now());
            Mappings[Member(() => DateTime.Today)] = Lambda<DateTime>(() => SqlFunctions.Now().Date);
            Mappings[Member(() => SqlFunctions.GetDate())] = Lambda<DateTime>(() => SqlFunctions.Now());
            Mappings[Member(() => SqlFunctions.Current())] = Lambda<DateTime>(() => SqlFunctions.Now());
            Mappings[Member(() => SqlFunctions.CurrentTimestamp())] = Lambda<DateTime>(() => SqlFunctions.Now());
            Mappings[Member(() => DateTime.Now.Year)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Year, obj).Value);
            Mappings[Member(() => DateTime.Now.Month)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Month, obj).Value);
            Mappings[Member(() => DateTime.Now.DayOfYear)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.DayOfYear, obj).Value);
            Mappings[Member(() => DateTime.Now.TimeOfDay)] = Lambda<DateTime, TimeSpan>(obj => SqlFunctions.ToTime(obj));
            Mappings[Member(() => DateTime.Now.Day)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Day, obj).Value);
            Mappings[Member(() => DateTime.Now.Date)] = Lambda<DateTime, DateTime>(obj => SqlFunctions.ToDate(obj));
            Mappings[Member(() => DateTime.Now.DayOfWeek)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.DayOfWeek, obj).Value);
            Mappings[Member(() => DateTime.Now.Hour)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Hour, obj).Value);
            Mappings[Member(() => DateTime.Now.Minute)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Minute, obj).Value);
            Mappings[Member(() => DateTime.Now.Second)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Second, obj).Value);
            Mappings[Member(() => DateTime.Now.Millisecond)] = Lambda<DateTime, int>(obj => SqlFunctions.DatePart(DateParts.Millisecond, obj).Value);
            Mappings[Member(() => DateTime.Now.AddYears(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Year, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddMonths(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Month, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddDays(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Day, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddHours(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Hour, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddMinutes(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Minute, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddSeconds(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Second, obj, p0).Value);
            Mappings[Member(() => DateTime.Now.AddMilliseconds(0))] = Lambda<DateTime, int, DateTime>((obj, p0) => SqlFunctions.DateAdd(DateParts.Millisecond, obj, p0).Value);
            Mappings[Member(() => new DateTime(0, 0, 0))] = Lambda<int, int, int, DateTime>((y, m, d) => SqlFunctions.ToDate(y, m, d).Value);
            Mappings[Member(() => DateTime.IsLeapYear(1))] = Lambda<int, bool>(y => y % 4 == 0 && (y % 100 != 0 || y % 400 == 0));
        }
    }
}
