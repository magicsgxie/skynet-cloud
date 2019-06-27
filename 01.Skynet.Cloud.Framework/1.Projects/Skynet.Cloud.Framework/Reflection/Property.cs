using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    class Property
    {
        public readonly PropertyInfo Name;
        public readonly object Owner;

        public Property(PropertyInfo name, object owner)
        {
            Name = name;
            Owner = owner;
        }


    }

    class TypeDeepCalculator
    {
        public int Deep
        {
            get
            {
                return deep == 1 ? 0 : deep - 2;
            }
        }

        private int deep = 0;
        public bool Calculate(Type parentType, Type type)
        {
            if (type == null)
                return false;

            deep = deep + 1;
            if (parentType.IsSubclassOf(type))
                return true;

            if (parentType.IsInterface)
                return type.GetInterface(parentType.Name) != null;

            return Calculate(parentType, type.BaseType);
        }
    }

    public class DynamicProperty
    {
        string name;
        Type type;

        public DynamicProperty(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (type == null) throw new ArgumentNullException("type");
            this.name = name;
            this.type = type;
        }

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }
    }
}
