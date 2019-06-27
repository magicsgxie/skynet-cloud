using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UWay.Skynet.Cloud.Collections;
using UWay.Skynet.Cloud.Reflection.Internal;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 属性管理器接口
    /// </summary>
    public interface IPropertyManager
    {
        /// <summary>
        /// 得到属性集
        /// </summary>
        IPropertySet Properties { get; }

        /// <summary>
        /// 加载属性集
        /// </summary>
        void Load();

        /// <summary>
        /// 保存属性集
        /// </summary>
        void Save();

        
    }

    /// <summary>
    /// 属性管理器
    /// </summary>
    public class PropertyManager : IPropertyManager
    {
        /// <summary>
        /// 得到属性集
        /// </summary>
        public IPropertySet Properties { get; private set; }

        //FileSystemWatcher watcher;
        private PropertySetOriginator Originator;
        private PropertySetCaretaker Caretaker;
        private static readonly object locker = new object();
       
        const string propertyXmlRootNodeName = "Properties";

        /// <summary>
        /// 初始化属性集
        /// </summary>
        public PropertyManager() : this(".Skynet.Cloude.Properties.xml")
        {
        }

        /// <summary>
        /// 初始化属性集
        /// </summary>
        /// <param name="propFile">属性文件</param>
        public PropertyManager(string propFile)
        {
            Properties = new PropertySet(StringComparer.InvariantCultureIgnoreCase);
            Originator = new PropertySetOriginator(Properties);
            Caretaker = new PropertySetCaretaker();

            Caretaker.FileName = propFile;

            try
            {
                if (!File.Exists(Caretaker.FileName))
                    Caretaker.Save(Originator.CreateMemento());
            }
            catch
            {
            }

            Load();

            //if (File.Exists(propFile))
            //{
            //    watcher = new FileSystemWatcher();
            //    watcher.Path = Path.GetDirectoryName(propFile);
            //    watcher.Filter = Path.GetFileName(propFile);
            //    watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            //    watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            //    watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            //    watcher.EnableRaisingEvents = true;
            //}
        }

        //void watcher_Renamed(object sender, RenamedEventArgs e)
        //{
        //    Caretaker.FileName = e.FullPath;
        //}

        //void watcher_Deleted(object sender, FileSystemEventArgs e)
        //{
        //    Caretaker.FileName = null;
        //    Properties.Clear();
        //    if (FileChanged != null)
        //        FileChanged(this, EventArgs.Empty);
        //}

        //void watcher_Changed(object sender, FileSystemEventArgs e)
        //{
        //    Load();
        //    if (FileChanged != null)
        //        FileChanged(this, EventArgs.Empty);
        //}

        private static PropertyManager instance;

        /// <summary>
        /// 得到属性管理器单利对象
        /// </summary>
        public static IPropertyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new PropertyManager();

                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 得到属性管理器的属性集
        /// </summary>
        public static IPropertySet Items
        {
            get { return Instance.Properties; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(string property, T defaultValue)
        {
            return Instance.Properties.Get<T>(property, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T Get<T>(string property)
        {
            return Instance.Properties.Get<T>(property);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            Originator.RestoreMemento(Caretaker.Get());
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            Caretaker.Save(Originator.CreateMemento());
        }
    }

    namespace Internal
    {
        class PropertySetCaretaker
        {
            public string FileName { get; set; }



            public IPropertySet Get()
            {
                return PropertySetHelper.Load(FileName);
            }

            public void Save(IPropertySet memento)
            {
                PropertySetHelper.Save(memento, FileName);
            }


        }

        class PropertySetOriginator
        {
            public PropertySetOriginator(IPropertySet state)
            {
                State = state;
            }


            public IPropertySet State { get; private set; }



            public IPropertySet CreateMemento()
            {
                return new PropertySet(State);
            }

            public void RestoreMemento(IPropertySet memento)
            {
                State.Clear();
                State.AddRange(memento);
            }
        }

        #region Helper method

        class PropertySetHelper
        {

            private static IPropertySet ReadProperties(XmlReader reader, string endElement)
            {
                var memento = new PropertySet();
                if (reader.IsEmptyElement)
                    return memento;

                memento.BeginEdit();

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.EndElement:
                            if (reader.LocalName == endElement)
                                return memento;
                            break;
                        case XmlNodeType.Element:
                            string propertyName = reader.LocalName;
                            if (propertyName == "Properties")
                            {
                                propertyName = reader.GetAttribute(0);
                                memento[propertyName] = ReadProperties(reader, "Properties");
                            }
                            else if (propertyName == "Array")
                            {
                                propertyName = reader.GetAttribute(0);
                                memento[propertyName] = ReadArray(reader);
                            }
                            else if (propertyName == "SerializedValue")
                            {
                                propertyName = reader.GetAttribute(0);
                                memento[propertyName] = new ComponentModel.SerializedValue(reader.ReadInnerXml());
                            }
                            else
                            {
                                memento[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                            }
                            break;
                    }
                }

                memento.EndEdit();
                memento.AcceptChanges();
                return memento;
            }

            private static List<string> ReadArray(XmlReader reader)
            {
                if (reader.IsEmptyElement)
                    return new List<string>(0);
                List<string> l = new List<string>();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.EndElement:
                            if (reader.LocalName == "Array")
                                return l;
                            break;
                        case XmlNodeType.Element:
                            l.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                            break;
                    }
                }
                return l;
            }

            private static void WriteProperties(XmlWriter writer, IEnumerable<KeyValuePair<string, object>> properties)
            {
                lock (properties)
                {
                    List<KeyValuePair<string, object>> sortedProperties = new List<KeyValuePair<string, object>>(properties);
                    sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));

                    foreach (KeyValuePair<string, object> entry in sortedProperties)
                    {
                        object val = entry.Value;
                        if (TypeDescriptor.GetConverter(val).CanConvertFrom(typeof(string)))
                        {
                            writer.WriteStartElement(entry.Key);
                            WriteValue(writer, val);
                            writer.WriteEndElement();
                            continue;
                        }

                        var dic = val as IDictionary<string, object>;
                        if (dic != null)
                        {
                            writer.WriteStartElement("Properties");
                            writer.WriteAttributeString("name", entry.Key);
                            WriteProperties(writer, dic);
                            writer.WriteEndElement();
                            continue;
                        }

                        var array = val as IEnumerable;
                        if (array != null)
                        {
                            writer.WriteStartElement("Array");
                            writer.WriteAttributeString("name", entry.Key);
                            foreach (object o in array)
                            {
                                writer.WriteStartElement("Element");
                                WriteValue(writer, o);
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                            continue;
                        }

                        var svalue = val as ComponentModel.SerializedValue;
                        if (svalue != null)
                        {
                            writer.WriteStartElement("SerializedValue");
                            writer.WriteAttributeString("name", entry.Key);
                            writer.WriteRaw(svalue.Content);
                            writer.WriteEndElement();
                        }
                        else
                        {
                            writer.WriteStartElement("SerializedValue");
                            writer.WriteAttributeString("name", entry.Key);
                            XmlSerializer serializer = new XmlSerializer(val.GetType());
                            serializer.Serialize(writer, val, null);
                            writer.WriteEndElement();
                        }
                    }
                }
            }

            private static void WriteValue(XmlWriter writer, object val)
            {
                if (val != null)
                {
                    if (val is string)
                    {
                        writer.WriteAttributeString("value", val.ToString());
                    }
                    else
                    {
                        TypeConverter c = TypeDescriptor.GetConverter(val.GetType());
                        writer.WriteAttributeString("value", c.ConvertToInvariantString(val));
                    }
                }
            }

            internal static void Save(IEnumerable<KeyValuePair<string, object>> properties, string fileName)
            {
                using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartElement("Properties");
                    WriteProperties(writer, properties);
                    writer.WriteEndElement();
                }
            }

            internal static IPropertySet Load(string fileName)
            {
                if (!File.Exists(fileName))
                    return new PropertySet();
                using (XmlTextReader reader = new XmlTextReader(fileName))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.LocalName)
                            {
                                case "Properties":
                                    return ReadProperties(reader, "Properties");
                            }
                        }
                    }
                }

                return new PropertySet();
            }
        }

        #endregion
    }
}
