using System;
using System.Globalization;
using UWay.Skynet.Cloud.Mapping;


namespace UWay.Skynet.Cloud.Data.Common
{
    public sealed class SqlFunctions
    {
        private SqlFunctions() { }

        const string Linq_DbFunctionDirectCall = "This function can only be invoked from LINQ to SQL.";

        #region Common

        #endregion

        #region Guid
        public static Guid NewGuid()
        {
            return Guid.NewGuid();
        }
        #endregion

        #region Convert Functions
        public static TTo Convert<TFrom, TTo>(TFrom from)
        {
            return Converter.Convert<TFrom, TTo>(from);
        }

        public static object Convert(object from, Type toType)
        {
            return Converter.Convert(from, toType);
        }
        #endregion

        #region String Functions
        internal static int? Length(string str)
        {
            return str == null ? null : (int?)str.Length;
        }
        //changed
        internal static string Substring(string str, int? startIndex, int? length)
        {
            return str == null || startIndex == null || length == null ? null : str.Substring(startIndex.Value - 1, length.Value);
        }

        internal static string Concat(params string[] values)
        {
            return string.Concat(values);
        }
        //changed
        public static bool Like(string str, string value, bool hasStart, bool hasEnd, bool hasEscape)
        {
            if (str == null || value == null) return false;
            if (hasStart && hasEnd)
                return str.Contains(value);
            if (hasStart)
                return str.StartsWith(value);
            if (hasEnd)
                return str.EndsWith(value);
            return true;
        }

        internal static bool Contains(string str, string value)
        {
            return str.Contains(value);
        }

        internal static bool StartsWith(string str, string value)
        {
            return str.StartsWith(value);
        }

        internal static bool EndsWith(string str, string value)
        {
            return str.EndsWith(value);
        }


        internal static string TrimEnd(string str)
        {
            return str == null ? null : str.TrimEnd();
        }

        internal static string TrimStart(string str)
        {
            return str == null ? null : str.TrimStart();
        }

        internal static string Insert(string str, int? startIndex, string value)
        {
            if (str == null || startIndex == null || value == null)
                return null;

            return str.Insert(startIndex.Value - 1, value);
        }
        internal static int? IndexOf(string str, string value)
        {
            if (str == null || value == null)
                return null;

            return str.IndexOf(value);
        }

        internal static int? IndexOf(string str, string value, int? startIndex)
        {
            if (str == null || value == null || startIndex == null)
                return null;

            return str.IndexOf(value, startIndex.Value - 1);
        }

        internal static int? IndexOf(string str, string value, int? startIndex, int? count)
        {
            if (str == null || value == null || startIndex == null || count == null)
                return null;

            return str.IndexOf(value, startIndex.Value - 1, count.Value);
        }

        internal static int? LastIndexOf(string str, string value)
        {
            if (str == null || value == null)
                return null;

            return str.LastIndexOf(value);
        }

        internal static int? LastIndexOf(string str, string value, int? startIndex)
        {
            if (str == null || value == null || startIndex == null)
                return null;

            return str.LastIndexOf(value, startIndex.Value - 1);
        }

        internal static int? LastIndexOf(string str, string value, int? startIndex, int? count)
        {
            if (str == null || value == null || startIndex == null)
                return null;

            return str.LastIndexOf(value, startIndex.Value - 1, count.Value);
        }


        internal static string Reverse(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        //changed
        internal static string Remove(string str, int? startIndex)
        {
            return startIndex == null || str == null || str.Length < startIndex - 1 ? null : str.Remove(startIndex.Value - 1);
        }

        internal static string LeftOf(string str, string value)
        {
            return str == null || value == null ? null : Helper.LeftOf(str, value);
        }

        internal static string RightOf(string str, string value)
        {
            return str == null || value == null ? null : Helper.RightOf(str, value);
        }

        internal static string Left(string str, int length)
        {
            return str == null || str.Length < length ? null : str.Substring(0, length);
        }

        internal static string Right(string str, int length)
        {
            return str == null || str.Length < length ?
                null :
                str.Substring(str.Length - length);
        }
        //changed
        internal static string Remove(string str, int? startIndex, int? count)
        {
            return str == null || startIndex == null || count == null ?
                null :
                str.Remove(startIndex.Value - 1, count.Value);
        }


        internal static string PadLeft(string str, int? totalWidth, char? paddingChar)
        {
            return str == null || totalWidth == null || paddingChar == null ?
                null :
                str.PadLeft(totalWidth.Value, paddingChar.Value);
        }

        internal static string PadRight(string str, int? totalWidth, char? paddingChar)
        {
            return str == null || totalWidth == null || paddingChar == null ?
                null :
                str.PadRight(totalWidth.Value, paddingChar.Value);
        }

        internal static string Replace(string str, string oldValue, string newValue)
        {
            return str == null || oldValue == null || newValue == null ?
                null :
                str.Replace(oldValue, newValue);
        }

        internal static string Replace(string str, char? oldValue, char? newValue)
        {
            return str == null || oldValue == null || newValue == null ?
                null :
                str.Replace(oldValue.Value, newValue.Value);
        }

        internal static string Trim(string str)
        {
            return str == null ? null : str.Trim();
        }

        internal static string TrimLeft(string str)
        {
            return str == null ? null : str.TrimStart();
        }

        internal static string TrimRight(string str)
        {
            return str == null ? null : str.TrimEnd();
        }

        internal static string Trim(string str, char? ch)
        {
            return str == null || ch == null ? null : str.Trim(ch.Value);
        }

        internal static string TrimLeft(string str, char? ch)
        {
            return str == null || ch == null ? null : str.TrimStart(ch.Value);
        }

        internal static string TrimRight(string str, char? ch)
        {
            return str == null || ch == null ? null : str.TrimEnd(ch.Value);
        }

        internal static string ToLower(string str)
        {
            return str == null ? null : str.ToLower();
        }

        internal static string ToUpper(string str)
        {
            return str == null ? null : str.ToUpper();
        }

        internal static char? ToLower(char? str)
        {
            return str == null ? (char?)null : Char.ToLower(str.Value);
        }

        internal static char? ToUpper(char? str)
        {
            return str == null ? (char?)null : char.ToUpper(str.Value);
        }

        #endregion

        #region DateTime
        //CurrentTimestamp 由 DateTime.Now来处理
        internal static System.DateTime? Current()
        {
            return DateTime.Now;
        }
        internal static System.DateTime CurrentTimestamp()
        {
            return DateTime.Now;
        }
        internal static System.DateTime Now()
        {
            return DateTime.Now;
        }
        internal static System.DateTime GetDate()
        {
            return DateTime.Now;
        }
        public static DateTime? ToDateTime(int? year, Int32? month, Int32? day, Int32? hour, Int32? minute, Int32? second)
        {
            return year == null || month == null || day == null || hour == null || minute == null || second == null ?
                  (DateTime?)null :
                  new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, second.Value);
        }
        internal static DateTime ToDate(DateTime date)
        {
            return date.Date;
        }
        internal static TimeSpan ToTime(DateTime date)
        {
            return date.TimeOfDay;
        }

        public static DateTime? ToDate(int? year, Int32? month, Int32? day)
        {
            return year == null || month == null || day == null ?
                 (DateTime?)null :
                 new DateTime(year.Value, month.Value, day.Value);
        }

        public static DateTime? DateAdd(DateParts part, DateTime? date, double? number)
        {
            if (number == null || date == null)
                return null;

            switch (part)
            {
                case DateParts.Year: return date.Value.AddYears(Converter.Convert<double, int>(number.Value));
                case DateParts.Quarter: return date.Value.AddMonths(Converter.Convert<double, int>(number.Value) * 3);
                case DateParts.Month: return date.Value.AddMonths(Converter.Convert<double, int>(number.Value));
                case DateParts.DayOfYear: return date.Value.AddDays(number.Value);
                case DateParts.Day: return date.Value.AddDays(number.Value);
                case DateParts.Week: return date.Value.AddDays(number.Value * 7);
                case DateParts.DayOfWeek: return date.Value.AddDays(number.Value);
                case DateParts.Hour: return date.Value.AddHours(number.Value);
                case DateParts.Minute: return date.Value.AddMinutes(number.Value);
                case DateParts.Second: return date.Value.AddSeconds(number.Value);
                case DateParts.Millisecond: return date.Value.AddMilliseconds(number.Value);
                //By zxm
                //case DateParts.: return date.Value.AddMilliseconds
            }
            throw new NotSupportedException(Linq_DbFunctionDirectCall);
        }


        public static int? DateDiff(DateParts part, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
                return null;

            return DateDiff(part, startDate.Value, endDate.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int DateDiff(DateParts part, DateTime startDate, DateTime endDate)
        {
            switch (part)
            {
                case DateParts.Day: return (endDate - startDate).Days;
                case DateParts.Hour: return (endDate - startDate).Hours;
                case DateParts.Minute: return (endDate - startDate).Minutes;
                case DateParts.Second: return (endDate - startDate).Seconds;
                case DateParts.Millisecond: return (endDate - startDate).Milliseconds;
            }
            throw new NotSupportedException(Linq_DbFunctionDirectCall);
        }
        public static int? DatePart(DateParts part, DateTime? date)
        {
            if (date == null)
                return null;

            switch (part)
            {
                case DateParts.Year: return date.Value.Year;
                case DateParts.Quarter: return (date.Value.Month - 1) / 3 + 1;
                case DateParts.Month: return date.Value.Month;
                case DateParts.DayOfYear: return date.Value.DayOfYear;
                case DateParts.Day: return date.Value.Day;
                case DateParts.Week: return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.Value, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                case DateParts.DayOfWeek: return (int)date.Value.DayOfWeek;
                case DateParts.Hour: return date.Value.Hour;
                case DateParts.Minute: return date.Value.Minute;
                case DateParts.Second: return date.Value.Second;
                case DateParts.Millisecond: return date.Value.Millisecond;
            }
            throw new NotSupportedException(Linq_DbFunctionDirectCall);
        }
        #endregion

        #region Binary
        internal static int? Length(byte[] value)
        {
            return value == null ? (int?)null : value.Length;
        }
        #endregion

        #region Math Functions
        internal static double PI
        {
            get
            {
                return Math.PI;
            }
        }
        internal static double E
        {
            get
            {
                return Math.E;
            }
        }

        internal static Double? Cot(Double? value) { return value == null ? null : (Double?)Math.Cos(value.Value) / Math.Sin(value.Value); }

        internal static Decimal RoundToEven(Decimal value)
        {
            return (Decimal)Math.Round(value, MidpointRounding.ToEven);
        }
        internal static Double RoundToEven(Double value)
        {
            return Math.Round(value, MidpointRounding.ToEven);
        }

        internal static Decimal RoundToEven(Decimal value, int precision)
        {
            return Math.Round(value, precision, MidpointRounding.ToEven);
        }

        internal static Double RoundToEven(Double value, int precision)
        {
            return Math.Round(value, precision, MidpointRounding.ToEven);
        }

        #endregion

        #region Between
        #region Number
        public static bool? Between(int? value, int? begin, int? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(int value, int begin, int end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(short? value, short? begin, short? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(short value, short begin, short end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(long? value, long? begin, long? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(long value, long begin, long end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(double? value, double? begin, double? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(double value, double begin, double end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(float? value, float? begin, float? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(float value, float begin, float end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(decimal? value, decimal? begin, decimal? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(decimal value, decimal begin, decimal end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(UInt16? value, UInt16? begin, UInt16? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(UInt16 value, UInt16 begin, UInt16 end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(UInt32? value, UInt32? begin, UInt32? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(UInt32 value, UInt32 begin, UInt32 end)
        {
            return value >= begin && value <= end;
        }

        public static bool? Between(UInt64? value, UInt64? begin, UInt64? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(UInt64 value, UInt64 begin, UInt64 end)
        {
            return value >= begin && value <= end;
        }
        #endregion

        #region Text

        public static bool Between(string value, string begin, string end)
        {
            return (value.CompareTo(begin) == 1 || value.CompareTo(begin) == 0)
                && (value.CompareTo(end) == -1 || value.CompareTo(end) == 0);
        }

        #endregion

        #region Date

        public static bool? Between(DateTime? value, DateTime? begin, DateTime? end)
        {
            if (value == null || begin == null || end == null)
                return null;
            return value.Value >= begin.Value && value.Value <= end.Value;
        }

        public static bool Between(DateTime value, DateTime begin, DateTime end)
        {
            return value >= begin && value <= end;
        }



        #endregion
        #endregion

    }
}
