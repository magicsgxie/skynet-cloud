using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Reflection
{
    /// <summary>
    /// 常用类型
    /// </summary>
    public class Types
    {
        /// <summary>
        /// Object 类型
        /// </summary>
        public static readonly Type Object = typeof(Object);

        /// <summary>
        /// Type 类型
        /// </summary>
        public static readonly Type Type = typeof(Type);

        /// <summary>
        /// Stirng 类型
        /// </summary>
        public static readonly Type String = typeof(String);

        /// <summary>
        /// Char 类型
        /// </summary>
        public static readonly Type Char = typeof(Char);

        /// <summary>
        /// Boolean 类型
        /// </summary>
        public static readonly Type Boolean = typeof(Boolean);

        /// <summary>
        /// Byte 类型
        /// </summary>
        public static readonly Type Byte = typeof(Byte);


        /// <summary>
        /// Byte 数组类型
        /// </summary>
        public static readonly Type ByteArray = typeof(Byte[]);

        /// <summary>
        /// SByte 类型
        /// </summary>
        public static readonly Type SByte = typeof(SByte);

        /// <summary>
        /// Int16 类型
        /// </summary>
        public static readonly Type Int16 = typeof(Int16);

        /// <summary>
        /// UInt16 类型
        /// </summary>
        public static readonly Type UInt16 = typeof(UInt16);

        /// <summary>
        /// Int32 类型
        /// </summary>
        public static readonly Type Int32 = typeof(Int32);

        /// <summary>
        /// UInt32 类型
        /// </summary>
        public static readonly Type UInt32 = typeof(UInt32);

        /// <summary>
        /// Int64 类型
        /// </summary>
        public static readonly Type Int64 = typeof(Int64);

        /// <summary>
        /// UInt64 类型
        /// </summary>
        public static readonly Type UInt64 = typeof(UInt64);

        /// <summary>
        /// Double 类型
        /// </summary>
        public static readonly Type Double = typeof(Double);

        /// <summary>
        /// Single 类型
        /// </summary>
        public static readonly Type Single = typeof(Single);

        /// <summary>
        /// Decimal 类型
        /// </summary>
        public static readonly Type Decimal = typeof(Decimal);

        /// <summary>
        /// Guid 类型
        /// </summary>
        public static readonly Type Guid = typeof(Guid);

        /// <summary>
        /// DateTime 类型
        /// </summary>
        public static readonly Type DateTime = typeof(DateTime);

        /// <summary>
        /// TimeSpan 类型
        /// </summary>
        public static readonly Type TimeSpan = typeof(TimeSpan);

        /// <summary>
        /// Nullable 类型
        /// </summary>
        public static readonly Type Nullable = typeof(Nullable<>);

        /// <summary>
        /// ValueType 类型
        /// </summary>
        public static readonly Type ValueType = typeof(ValueType);

        /// <summary>
        /// void 类型
        /// </summary>
        public static readonly Type Void = typeof(void);

        /// <summary>
        /// DBNull 类型
        /// </summary>
        public static readonly Type DBNull = typeof(DBNull);

        /// <summary>
        /// Delegate 类型
        /// </summary>
        public static readonly Type Delegate = typeof(Delegate);

        /// <summary>
        /// ByteEnumerable 类型
        /// </summary>
        public static readonly Type ByteEnumerable = typeof(IEnumerable<Byte>);

        /// <summary>
        /// IEnumerable 类型
        /// </summary>
        public static readonly Type IEnumerableofT = typeof(System.Collections.Generic.IEnumerable<>);

        /// <summary>
        /// IEnumerable 类型
        /// </summary>
        public static readonly Type IEnumerable = typeof(System.Collections.IEnumerable);

        /// <summary>
        /// IListSource 类型
        /// </summary>
        public static readonly Type IListSource = typeof(System.ComponentModel.IListSource);

        /// <summary>
        /// IDictionary 类型
        /// </summary>
        public static readonly Type IDictionary = typeof(System.Collections.IDictionary);

        /// <summary>
        /// IDictionary 类型
        /// </summary>
        public static readonly Type IDictionaryOfT = typeof(IDictionary<,>);
        /// <summary>
        /// Dictionary 类型
        /// </summary>
        public static readonly Type DictionaryOfT = typeof(Dictionary<,>);

        /// <summary>
        /// StringDictionary 类型
        /// </summary>
        public static readonly Type StringDictionary = typeof(StringDictionary);

        /// <summary>
        /// NameValueCollection 类型
        /// </summary>
        public static readonly Type NameValueCollection = typeof(NameValueCollection);

        /// <summary>
        /// IDataReader 类型
        /// </summary>
        public static readonly Type IDataReader = typeof(System.Data.IDataReader);

        /// <summary>
        /// DataTable 类型
        /// </summary>
        public static readonly Type DataTable = typeof(System.Data.DataTable);

        /// <summary>
        /// DataRow 类型
        /// </summary>
        public static readonly Type DataRow = typeof(System.Data.DataRow);

        /// <summary>
        /// IDictionary 类型
        /// </summary>
        public static readonly Type IDictionaryOfStringAndObject = typeof(IDictionary<string, object>);

        /// <summary>
        /// IDictionary 类型
        /// </summary>
        public static readonly Type IDictionaryOfStringAndString = typeof(IDictionary<string, string>);
    }

    /// <summary>
    /// 类型助手
    /// </summary>
    public static class TypeHelper
    {
        public static Type FindIEnumerable(this Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            //Guard.NotNull(type, "type");
            return type.IsGenericType && type.GetGenericTypeDefinition() == Types.Nullable;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static Type GetNonNullableType(Type type)
        //{
        //    if (TypeHelper.IsNullable(type))
        //        return Nullable.GetUnderlyingType(type);
        //    return type;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(Type type)
        {
            //Guard.NotNull(type, "type");
            return ObjectCreator.Create(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsReadOnly(this MemberInfo member)
        {
            //Guard.NotNull(member, "member");
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return (((FieldInfo)member).Attributes & FieldAttributes.InitOnly) != 0;
                case MemberTypes.Property:
                    PropertyInfo pi = (PropertyInfo)member;
                    return !pi.CanWrite || pi.GetSetMethod() == null;
                default:
                    return true;
            }
        }
        /// <summary>
        /// 得到集合元素类型
        /// </summary>
        /// <param name="enumerableType">集合类型</param>
        /// <returns>返回集合元素类型</returns>
        internal static Type GetElementType(Type enumerableType)
        {
            //Guard.NotNull(enumerableType, "enumerableType");
            return GetElementType(enumerableType, null);
        }

        /// <summary>
        /// 得到枚举类型
        /// </summary>
        /// <param name="enumType">枚举类型或Nullable枚举类型</param>
        /// <returns>返回枚举类型</returns>
        public static Type GetEnumType(Type enumType)
        {
            //Guard.NotNull(enumType, "enumType");
            if (enumType.IsNullable())
                enumType = enumType.GetGenericArguments()[0];
            if (enumType.IsEnum)
                return enumType;
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CanConvertToEnum(Type type)
        {
            //Guard.NotNull(type, "type");
            if (TypeHelper.IsEnumType(type))
                return true;

            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);

            return
                                TypeHelper.IsEnumType(type)
                                || Types.String == type
                                || Types.Decimal == type
                                || Types.Double == type
                                || Types.Int16 == type
                                || Types.Int32 == type
                                || Types.Int64 == type
                                || Types.Single == type
                                || Types.UInt16 == type
                                || Types.UInt32 == type
                                || Types.UInt64 == type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerableType"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        internal static Type GetElementType(Type enumerableType, IEnumerable enumerable)
        {
            //Guard.NotNull(enumerableType, "enumerableType");
            if (enumerableType.HasElementType)
            {
                return enumerableType.GetElementType();
            }
            if (enumerableType.IsGenericType && enumerableType.GetGenericTypeDefinition().Equals(Types.IEnumerableofT))
            {
                return enumerableType.GetGenericArguments()[0];
            }
            Type iEnumerableType = GetIEnumerableType(enumerableType);
            if (iEnumerableType != null)
            {
                return iEnumerableType.GetGenericArguments()[0];
            }
            if (!Types.IEnumerable.IsAssignableFrom(enumerableType))
            {
                throw new ArgumentException(string.Format("Unable to find the element type for type '{0}'.", enumerableType), "enumerableType");
            }
            if (enumerable != null)
            {
                object obj2 = enumerable.Cast<object>().FirstOrDefault<object>();
                if (obj2 != null)
                {
                    return obj2.GetType();
                }
            }
            return typeof(object);
        }

        private static Type GetIEnumerableType(Type enumerableType)
        {
            try
            {
                return enumerableType.GetInterface("IEnumerable`1", false);
            }
            catch (AmbiguousMatchException)
            {
                if (enumerableType.BaseType != typeof(object))
                {
                    return GetIEnumerableType(enumerableType.BaseType);
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsGuidType(Type type)
        {
            //Guard.NotNull(type, "type");
            if (type == Types.Guid)
                return true;

            if (type.IsNullable()
                && type.GetGenericArguments()[0] == typeof(Guid))
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsEnumType(this Type type)
        {
           // Guard.NotNull(type, "type");
            return GetEnumType(type) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsFlagsEnum(Type type)
        {
            //Guard.NotNull(type, "type");
            return TypeHelper.IsEnumType(type) && type.GetCustomAttributes(typeof(FlagsAttribute), false).Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsGenericDictionaryType(this Type type)
        {
            //Guard.NotNull(type, "type");
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return true;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType);
            var baseDefinitions = genericInterfaces.Select(t => t.GetGenericTypeDefinition());
            return baseDefinitions.Any(t => t == typeof(IDictionary<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsIDictionaryType(this Type type)
        {
            //Guard.NotNull(type, "type");
            return Types.IDictionary.IsAssignableFrom(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsDictionaryType(this Type type)
        {
            //Guard.NotNull(type, "type");
            return IsIDictionaryType(type) || IsGenericDictionaryType(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Type GetGenericDictionaryType(this Type type)
        {
            //Guard.NotNull(type, "type");
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return type;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            return genericInterfaces.FirstOrDefault();
        }

        static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        static bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || IsNullableValueType(type));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCollectionTypeExcludeStringAndDictionaryType(this Type type)
        {
            //Guard.NotNull(type, "type");
            return Types.IEnumerable.IsAssignableFrom(type)
                && type != Types.String
                && !Types.IDictionaryOfStringAndObject.IsAssignableFrom(type);
        }

        internal static readonly Type[] PredefinedTypes = {
            typeof(Object),
            typeof(Boolean),
            typeof(Char),
            typeof(String),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        };

        public static bool IsPredefinedType(this Type type)
        {
            foreach (Type t in PredefinedTypes)
            {
                if (t == type)
                {
                    return true;
                }
            }
            return false;
        }

 

        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetNonNullableType(this Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }
        
        public static string GetTypeName(this Type type)
        {
            Type baseType = GetNonNullableType(type);
            string s = baseType.Name;
            if (type != baseType) s += '?';
            return s;
        }

        public static bool IsNumericType(this Type type)
        {
            return GetNumericTypeKind(type) != 0;
        }

        public static bool IsSignedIntegralType(this Type type)
        {
            return GetNumericTypeKind(type) == 2;
        }

        public static bool IsUnsignedIntegralType(this Type type)
        {
            return GetNumericTypeKind(type) == 3;
        }

        public static int GetNumericTypeKind(this Type type)
        {
            if (type == null)
            {
                return 0;
            }

            type = GetNonNullableType(type);

            if (type.IsEnum)
            {
                return 0;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return 1;
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return 2;
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return 3;
                default:
                    return 0;
            }
        }

        public static PropertyInfo GetIndexerPropertyInfo(this Type type, params Type[] indexerArguments)
        {
            return
                (from p in type.GetProperties()
                 where AreArgumentsApplicable(indexerArguments, p.GetIndexParameters())
                 select p).FirstOrDefault();
        }

        private static bool AreArgumentsApplicable(IEnumerable<Type> arguments, IEnumerable<ParameterInfo> parameters)
        {
            var argumentList = arguments.ToList();
            var parameterList = parameters.ToList();

            if (argumentList.Count != parameterList.Count)
            {
                return false;
            }

            for (int i = 0; i < argumentList.Count; i++)
            {
                if (parameterList[i].ParameterType != argumentList[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsCompatibleWith(this Type source, Type target)
        {
            if (source == target) return true;
            if (!target.IsValueType) return target.IsAssignableFrom(source);
            Type st = source.GetNonNullableType();
            Type tt = target.GetNonNullableType();
            if (st != source && tt == target) return false;
            TypeCode sc = st.IsEnum ? TypeCode.Object : Type.GetTypeCode(st);
            TypeCode tc = tt.IsEnum ? TypeCode.Object : Type.GetTypeCode(tt);
            switch (sc)
            {
                case TypeCode.SByte:
                    switch (tc)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.Byte:
                    switch (tc)
                    {
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.Int16:
                    switch (tc)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.UInt16:
                    switch (tc)
                    {
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.Int32:
                    switch (tc)
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.UInt32:
                    switch (tc)
                    {
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.Int64:
                    switch (tc)
                    {
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.UInt64:
                    switch (tc)
                    {
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;
                case TypeCode.Single:
                    switch (tc)
                    {
                        case TypeCode.Single:
                        case TypeCode.Double:
                            return true;
                    }
                    break;
                default:
                    if (st == tt) return true;
                    break;
            }
            return false;
        }

        public static Type FindGenericType(this Type type, Type genericType)
        {
            while (type != null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType) return type;
                if (genericType.IsInterface)
                {
                    foreach (Type intfType in type.GetInterfaces())
                    {
                        Type found = intfType.FindGenericType(genericType);
                        if (found != null) return found;
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        public static string GetName(this Type type)
        {
            return type.FullName.Replace(type.Namespace + ".", "");
        }

        public static object DefaultValue(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        public static MemberInfo FindPropertyOrField(this Type type, string memberName)
        {
            MemberInfo memberInfo = type.FindPropertyOrField(memberName, false);

            if (memberInfo == null)
            {
                memberInfo = type.FindPropertyOrField(memberName, true);
            }

            return memberInfo;
        }

        public static MemberInfo FindPropertyOrField(this Type type, string memberName, bool staticAccess)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.DeclaredOnly |
                (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
            foreach (Type t in type.SelfAndBaseTypes())
            {
                MemberInfo[] members = t.FindMembers(MemberTypes.Property | MemberTypes.Field,
                    flags, Type.FilterNameIgnoreCase, memberName);
                if (members.Length != 0) return members[0];
            }
            return null;
        }


        public static IEnumerable<Type> SelfAndBaseTypes(this Type type)
        {
            if (type.IsInterface)
            {
                List<Type> types = new List<Type>();
                AddInterface(types, type);
                return types;
            }
            return SelfAndBaseClasses(type);
        }

        public static IEnumerable<Type> SelfAndBaseClasses(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }

        static void AddInterface(List<Type> types, Type type)
        {
            if (!types.Contains(type))
            {
                types.Add(type);
                foreach (Type t in type.GetInterfaces()) AddInterface(types, t);
            }
        }

        public static bool IsDataRow(this Type type)
        {
            return type.IsCompatibleWith(typeof(DataRow)) || type.IsCompatibleWith(typeof(DataRowView));
        }

        public static bool IsDynamicObject(this Type type)
        {
            return type == typeof(object) || type.IsCompatibleWith(typeof(System.Dynamic.IDynamicMetaObjectProvider));
        }

        public static bool IsDateTime(this Type type)
        {
            return type == typeof(DateTime) || type == typeof(DateTime?);
        }

        public static string FirstSortableProperty(this Type type)
        {
            PropertyInfo firstSortableProperty = type.GetProperties().Where(property => property.PropertyType.IsPredefinedType()).FirstOrDefault();

            if (firstSortableProperty == null)
            {
                throw new NotSupportedException("找不到排序信息");
            }

            return firstSortableProperty.Name;
        }

        public static string ToJavaScriptType(this Type type)
        {
            if (type == null)
            {
                return "Object";
            }

            if (type == typeof(char) || type == typeof(char?))
            {
                return "String";
            }

            if (IsNumericType(type))
            {
                return "Number";
            }

            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return "Date";
            }

            if (type == typeof(string))
            {
                return "String";
            }

            if (type == typeof(bool) || type == typeof(bool?))
            {
                return "Boolean";
            }

            if (type.GetNonNullableType().IsEnum)
            {
                return "Number";
            }

            if (type.GetNonNullableType() == typeof(Guid))
            {
                return "String";
            }

            return "Object";
        }

        public static bool IsPlainType(this Type type)
        {
            return !type.IsDynamicObject() && !type.IsDataRow() && !(type.IsCompatibleWith(typeof(ICustomTypeDescriptor)));
        }
    }
}
