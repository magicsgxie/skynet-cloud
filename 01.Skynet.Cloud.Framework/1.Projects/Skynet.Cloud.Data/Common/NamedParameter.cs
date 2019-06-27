using System;

namespace UWay.Skynet.Cloud.Data.Common
{

    public class NamedParameter
    {
        string name;
        Type type;
        internal SqlType sqlType;

        public NamedParameter(string name, Type type, SqlType sqlType)
        {
            this.name = name;
            this.type = type;
            this.sqlType = sqlType;
        }

        public string Name
        {
            get { return this.name; }
        }

        public Type Type
        {
            get { return this.type; }
        }

        public SqlType SqlType
        {
            get { return this.sqlType; }
        }

        public override string ToString()
        {
            return string.Format("Name='{0}',Type='{1}',DbType='{2}',Length='{3}'", name, type.Name, sqlType.DbType, sqlType.Length);
        }
    }
}
