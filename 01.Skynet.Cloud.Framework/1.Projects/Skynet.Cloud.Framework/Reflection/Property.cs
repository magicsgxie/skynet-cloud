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


    /// <summary>
    /// 动态属性
    /// </summary>
    public class DynamicProperty
    {
        string name;
        Type type;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public DynamicProperty(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (type == null) throw new ArgumentNullException("type");
            this.name = name;
            this.type = type;
        }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
        }


        /// <summary>
        /// 类型
        /// </summary>
        public Type Type
        {
            get { return type; }
        }
    }
}
