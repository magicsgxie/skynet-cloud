using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Collections
{
    /// <summary>
    /// 属性集对象
    /// </summary>
    [Serializable]
    
    public sealed class PropertySet : IPropertySet, IDictionary<string, object>
    {
        static readonly Type SerializedValueType = typeof(ComponentModel.SerializedValue);

        Dictionary<string, object> properties;

        /// <summary>
        /// 构造属性集对象
        /// </summary>
        public PropertySet()
        {
            properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 构造属性集对象
        /// </summary>
        /// <param name="comparer"></param>
        public PropertySet(StringComparer comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            properties = new Dictionary<string, object>(comparer);
        }

        /// <summary>
        /// 构造属性集对象
        /// </summary>
        /// <param name="properties"></param>
        public PropertySet(IPropertySet properties)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            this.properties = new Dictionary<string, object>();
            foreach (var item in properties)
                this.properties.Add(item.Key, item.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            properties.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<KeyValuePair<string, object>> items)
        {
            if (items != null && items.Count() > 0)
                foreach (var item in items)
                    this.properties.Add(item.Key, item.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object this[string property]
        {
            get
            {
                return Get(property);
            }
            set
            {
                Set(property, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Keys
        {
            get
            {
                return properties.Keys.ToArray();
            }
        }

        private object Get(string property)
        {
            object val;
            properties.TryGetValue(property, out val);
            return val;
        }

        private void Set(string propertyName, object value)
        {
            object oldValue = null;

            if (!properties.ContainsKey(propertyName))
            {
                lock (properties)
                    properties.Add(propertyName, value);
            }
            else
            {
                if (PropertyChanged != null)
                    oldValue = properties[propertyName];

                lock (properties)
                    properties[propertyName] = value;
            }
            OnPropertyChanged(propertyName, oldValue, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool Contains(string property)
        {
            return properties.ContainsKey(property);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return properties.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool Remove(string property)
        {
            lock (properties)
                return properties.Remove(property);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[Properties:{");
                foreach (KeyValuePair<string, object> entry in properties.ToArray())
                {
                    sb.Append(entry.Key);
                    sb.Append("=");
                    sb.Append(entry.Value);
                    sb.Append(",");
                }
                sb.Append("}]");
                return sb.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public T Get<T>(string property)
        {
            return Get<T>(property, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool TryGetPropertyValue(Type toType, string property, out object defaultValue)
        {
            if (!properties.TryGetValue(property, out defaultValue))
                return false;

            if (defaultValue == null)
                return false;

            var fromType = defaultValue.GetType();
            if (fromType.Name == "RuntimeType")
                return true;

            if (fromType == Types.String)
                defaultValue = StringFormatter.Format(defaultValue as string);
            else if (fromType == PropertySet.SerializedValueType)
            {
                try
                {
                    defaultValue = ((ComponentModel.SerializedValue)defaultValue).Deserialize(toType);
                }
                catch (Exception ex)
                {
                    new Exception("Error loading property '" + property + "': " + ex.Message, ex);
                }
            }

            defaultValue = Mapper.Map(defaultValue, fromType, toType);
            lock (properties)
                properties[property] = defaultValue;
            return defaultValue != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T Get<T>(string property, T defaultValue)
        {
            var o = (object)defaultValue;
            if (!TryGetPropertyValue(typeof(T), property, out o) || o == null)
                return defaultValue;
            return (T)o;
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public bool IsChanged { get; private set; }



        /// <summary>
        /// Accept change
        /// </summary>
        public void AcceptChanges()
        {
            IsChanged = false;
            ordinalState.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RejectChanges()
        {
            IsChanged = false;
            Restore(ordinalState);
            Restore(beginEditOrdinalState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        void OnPropertyChanged(string propertyName, object newValue, object oldValue)
        {
            if (IsRestore)
                return;

            IsChanged = true;

            if (IsEdit)
            {
                if (!beginEditOrdinalState.ContainsKey(propertyName))
                    beginEditOrdinalState[propertyName] = oldValue;
            }
            else
            {
                if (!ordinalState.ContainsKey(propertyName))
                    ordinalState[propertyName] = oldValue;

                if (PropertyChanged != null)
                    PropertyChanged(this, new ComponentModel.PropertyChangedEventArgs(propertyName, oldValue, newValue));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        private Dictionary<string, object> ordinalState = new Dictionary<string, object>();
        private Dictionary<string, object> beginEditOrdinalState = new Dictionary<string, object>();

        [XmlIgnore]
        [NonSerialized]
        private int _editLevel;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public bool IsEdit
        {
            get { return _editLevel > 0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BeginEdit()
        {
            _editLevel++;
        }
        /// <summary>
        /// 
        /// </summary>
        public void CancelEdit()
        {
            if (_editLevel > 0)
            {
                _editLevel--;
                if (!IsEdit)
                    Restore(beginEditOrdinalState);
            }
        }

        /// <summary>
        /// 是否在还原状态
        /// </summary>
        private bool IsRestore;
        private void Restore(Dictionary<string, object> state)
        {
            lock (this)
            {
                if (state.Count == 0)
                    return;

                IsRestore = true;

                foreach (var item in state)
                    this.SetProperty(item.Key, item.Value);

                state.Clear();

                IsRestore = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void EndEdit()
        {
            if (_editLevel > 0)
            {
                _editLevel--;

                if (!IsEdit)
                {
                    foreach (var item in beginEditOrdinalState)
                    {
                        if (!ordinalState.ContainsKey(item.Key))
                            ordinalState.Add(item.Key, item.Value);
                    }

                    beginEditOrdinalState.Clear();
                }
            }
        }

        #region IOriginator Members



        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void IDictionary<string, object>.Add(string key, object value)
        {
            properties.Add(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return properties.ContainsKey(key);
        }
        /// <summary>
        /// 
        /// </summary>
        ICollection<string> IDictionary<string, object>.Keys
        {
            get { return properties.Keys; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IDictionary<string, object>.Remove(string key)
        {
            return properties.Remove(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out object value)
        {
            return properties.TryGetValue(key, out value);
        }
        /// <summary>
        /// 
        /// </summary>
        ICollection<object> IDictionary<string, object>.Values
        {
            get { return properties.Values; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object IDictionary<string, object>.this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            (properties as ICollection<KeyValuePair<string, object>>).Add(item);
        }
        /// <summary>
        /// 
        /// </summary>
        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            (properties as ICollection<KeyValuePair<string, object>>).Clear();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return (properties as ICollection<KeyValuePair<string, object>>).Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            (properties as ICollection<KeyValuePair<string, object>>).CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { return properties.Count; }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return (properties as ICollection<KeyValuePair<string, object>>).IsReadOnly; }
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return (properties as ICollection<KeyValuePair<string, object>>).Remove(item);
        }
    }
}
