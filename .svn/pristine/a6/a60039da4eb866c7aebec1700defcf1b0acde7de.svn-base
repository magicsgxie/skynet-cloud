using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Data
{
    partial class Table
    {
        internal Dictionary<string, int> Fields;

        internal int AddField(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (Fields.ContainsKey(name)) throw new InvalidOperationException("Field already exists: " + name);
            Fields[name] = Fields.Count;
            return Fields.Count;
        }

        internal Table()
        {
            Fields = new Dictionary<string, int>(0);
        }

        internal Table(IDataReader reader)
        {
            var fieldCount = reader.FieldCount;
            Fields = new Dictionary<string, int>(fieldCount, StringComparer.InvariantCultureIgnoreCase);
            for (int i = 0; i < fieldCount; i++)
                Fields.Add(reader.GetName(i), i);
        }
    }
}
