using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Data.Dialect
{
    class TypeNames
    {
        public const string LengthPlaceHolder = "$l";
        public const string PrecisionPlaceHolder = "$pair";
        public const string ScalePlaceHolder = "$s";

        private readonly Dictionary<DBType, SortedList<int, string>> weighted =
            new Dictionary<DBType, SortedList<int, string>>();

        private readonly Dictionary<DBType, string> defaults = new Dictionary<DBType, string>();

        public string Get(DBType typecode)
        {
            string result;
            defaults.TryGetValue(typecode, out result);
            return result;
        }

        public string Get(DBType typecode, int size, int precision, byte scale)
        {
            if (size == 0 && precision == 0 && scale == 0)
                return Get(typecode);
            SortedList<int, string> map;
            weighted.TryGetValue(typecode, out map);
            if (map != null && map.Count > 0)
                foreach (KeyValuePair<int, string> entry in map)
                    if (size <= entry.Key || precision > 0 || scale > 0)
                        return Replace(entry.Value, size, precision, scale);
            return Replace(Get(typecode), size, precision, scale);
        }

        public void Put(DBType typecode, int size, string value)
        {
            SortedList<int, string> map;
            if (!weighted.TryGetValue(typecode, out map))
            {
                weighted[typecode] = map = new SortedList<int, string>();
            }
            map[size] = value;
        }

        public void Put(DBType typecode, string value)
        {
            defaults[typecode] = value;
        }

        static string Replace(string type, int size, int precision, byte scale)
        {
            if (string.IsNullOrEmpty(type))
                return null;

            type = ReplaceOnce(type, LengthPlaceHolder, size.ToString());

            if (precision > 0 || scale > 0)
            {
                type = ReplaceOnce(type, ScalePlaceHolder, scale.ToString());
                type = ReplaceOnce(type, PrecisionPlaceHolder, precision.ToString());
            }

            return type;
        }
        static string ReplaceOnce(string template, string placeholder, string replacement)
        {
            if (string.IsNullOrEmpty(template))
                return null;
            int loc = template.IndexOf(placeholder);
            if (loc < 0)
                return template;
            else
            {
                return new StringBuilder(template.Substring(0, loc))
                    .Append(replacement)
                    .Append(template.Substring(loc + placeholder.Length))
                    .ToString();
            }
        }
    }
}
