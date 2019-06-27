using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UWay.Skynet.Cloud.ComponentModel
{
    sealed class DebuggerEnumerableView<T>
    {
        private IEnumerable<T> m_enumerable;

        public DebuggerEnumerableView(IEnumerable<T> enumerable)
        {
            m_enumerable = enumerable;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get { return m_enumerable.ToArray(); }
        }
    }

    [Serializable]
    internal class SerializedValue
    {
        string content;

        public string Content
        {
            get { return content; }
        }

        public object Deserialize(Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (var reader = new StringReader(content))
                return serializer.Deserialize(reader);
        }

        public SerializedValue(string content)
        {
            this.content = content;
        }
    }
}
