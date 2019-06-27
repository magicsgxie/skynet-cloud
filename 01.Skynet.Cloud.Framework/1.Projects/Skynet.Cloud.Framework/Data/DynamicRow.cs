using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    partial class DynamicRow : IDictionary<string, object>
    {
        private Table table;
        private object[] values;

        internal DynamicRow(Table table, object[] items)
        {
            this.table = table;
            values = items;
        }

        public object this[string fieldName]
        {
            get
            {
                object val; TryGetValue(fieldName, out val); return val;
            }
            set
            {
                SetValue(fieldName, value);
            }
        }


        public int Count
        {
            get
            {
                return values.Length;
            }
        }

        public bool TryGetValue(string name, out object value)
        {
            int index = -1;
            if (!table.Fields.TryGetValue(name, out index))
            {
                value = null;
                return false;
            }

            value = index < values.Length ? values[index] : null;
            if (value is Null)
            {
                value = null;
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("{");
            foreach (var kv in this)
            {
                var value = kv.Value;
                sb.Append(", ").Append(kv.Key);
                if (value != null)
                {
                    sb.Append(" = '").Append(kv.Value).Append('\'');
                }
                else
                {
                    sb.Append(" = NULL");
                }
            }

            return sb.Append('}').ToString();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var item in table.Fields)
            {
                var v = values[item.Value];
                if (v == Null.Value)
                    v = null;
                yield return new KeyValuePair<string, object>(item.Key, v);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in table.Fields)
            {
                var v = values[item.Value];
                if (v == Null.Value)
                    v = null;
                yield return new KeyValuePair<string, object>(item.Key, v);
            }
        }



        #region Implementation of ICollection<KeyValuePair<string,object>>

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            IDictionary<string, object> dic = this;
            dic.Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = Null.Value;
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            object value;
            return TryGetValue(item.Key, out value) && Equals(value, item.Value);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            foreach (var kv in this)
            {
                array[arrayIndex++] = kv; // if they didn't leave enough space; not our fault
            }
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            IDictionary<string, object> dic = this;
            return dic.Remove(item.Key);
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IDictionary<string,object>

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            int index;
            table.Fields.TryGetValue(key, out index);
            if (index < 0 || index >= values.Length || values[index] is Null) return false;
            return true;
        }

        void IDictionary<string, object>.Add(string key, object value)
        {
            IDictionary<string, object> dic = this;
            dic[key] = value;
        }

        bool IDictionary<string, object>.Remove(string key)
        {
            int index;
            table.Fields.TryGetValue(key, out index);
            if (index < 0 || index >= values.Length || values[index] is Null) return false;
            values[index] = Null.Value;
            return true;
        }


        public object SetValue(string key, object value)
        {
            if (key == null) throw new ArgumentNullException("key");
            int index;
            table.Fields.TryGetValue(key, out index);
            if (index < 0)
            {
                index = table.AddField(key);
            }
            if (values.Length <= index)
            {   // we'll assume they're doing lots of things, and
                // grow it to the full width of the table
                Array.Resize(ref values, table.Fields.Count);
            }
            return values[index] = value;
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get { return this.Select(kv => kv.Key).ToArray(); }
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get { return this.Select(kv => kv.Value).ToArray(); }
        }

        #endregion
    }
}
