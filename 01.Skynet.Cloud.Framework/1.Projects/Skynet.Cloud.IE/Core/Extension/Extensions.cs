using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UWay.Skynet.Cloud.IE.Core
{
    public static class Extensions
    {
        private const string SHEET_KEY = "cnname";
        private const string COLUMN_IS_REQUIRED = "IS_REQUIRE";
        private const string COLUMN_IS_PK = "IS_REQUIRE";
        private const string DICTIONARY_VALUE = "DICTIONARY";
        private const string EXPRESSION_KEY = "COL_EXPRESSION";
        private const string EXPRESSION_LOG_KEY = "LOG_KEY";
        private const string LINK_COLUMN = "LINK_COLUMN";
        public static void AddLinkColumn(this DataColumn dt, string linkColumnName)
        {
            dt.ExtendedProperties.Add(LINK_COLUMN, linkColumnName);
        }



        public static string GetLinkColumn(this DataColumn dt)
        {
            if (dt.ExtendedProperties.ContainsKey(LINK_COLUMN))
            {
                return dt.ExtendedProperties[LINK_COLUMN].ToString();
            }
            return string.Empty;
        }
        public static void AddRegexExpression(this DataColumn dt, string expression)
        {
            dt.ExtendedProperties.Add(EXPRESSION_KEY, expression);
        }

        public static string GetRegexExpression(this DataColumn dt)
        {
            if (dt.ExtendedProperties.ContainsKey(EXPRESSION_KEY))
            {
                return dt.ExtendedProperties[EXPRESSION_KEY].ToString();
            }
            return string.Empty;
        }

        public static void AddExceptionFormat(this DataColumn dt, string expressionLog)
        {
            dt.ExtendedProperties.Add(EXPRESSION_LOG_KEY, expressionLog);
        }

        public static string GetExceptionFormat(this DataColumn dt)
        {
            if (dt.ExtendedProperties.ContainsKey(EXPRESSION_LOG_KEY))
            {
                return dt.ExtendedProperties[EXPRESSION_LOG_KEY].ToString();
            }
            return string.Empty;
        }


        public static void AddSheetName(this DataTable dt, string descrription)
        {
            dt.ExtendedProperties.Add(SHEET_KEY, descrription);
        }

        public static string GetSheet(this DataTable dt)
        {
            if(dt.ExtendedProperties.ContainsKey(SHEET_KEY))
            {
                return dt.ExtendedProperties[SHEET_KEY].ToString();
            }
            return dt.TableName;
        }

        public static void AddPrimaryKey(this DataColumn dc, bool isRequired)
        {
            dc.ExtendedProperties.Add(COLUMN_IS_PK, isRequired);
        }


        public static bool? PrimaryKey(this DataColumn dc)
        {
            if (dc.ExtendedProperties.ContainsKey(COLUMN_IS_PK))
                return dc.ExtendedProperties[COLUMN_IS_PK] as bool?;
            return false;
        }

        public static void AddRequire(this DataColumn dc, bool isRequired)
        {
            dc.ExtendedProperties.Add(COLUMN_IS_REQUIRED, isRequired);
        }


        public static bool? IsRequire(this DataColumn dc)
        {
            if(dc.ExtendedProperties.ContainsKey(COLUMN_IS_REQUIRED))
                return dc.ExtendedProperties[COLUMN_IS_REQUIRED] as bool?;
            return false;
        }

        public static void AddDictionary(this DataColumn dt, IDictionary<string, string> descrription)
        {
            dt.ExtendedProperties.Add(DICTIONARY_VALUE, descrription);
        }

        public static void AddDictionary(this DataColumn dt, IDictionary<string, int> descrription)
        {
            dt.ExtendedProperties.Add(DICTIONARY_VALUE, descrription);
        }

        public static IDictionary<string, string> GetDictionary(this DataColumn dt)
        {
            if (dt.ExtendedProperties.ContainsKey(SHEET_KEY))
            {
                return dt.ExtendedProperties[DICTIONARY_VALUE] as IDictionary<string, string>;
            }
            return null;
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="errorMessage"></param>
        public static void SetValue(this DataRow currentRow, string fieldName, object value)
        {
            currentRow[fieldName] = value;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        public static byte[] GetAsByteArray(this Workbook excelPackage)
        {
            using (var stream = excelPackage.SaveToStream())
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                return buffer;
            }
        }

#if NETSTANDARD2_1
        /// <summary>
        /// 将集合转成DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var table = Cache<T>.SchemeFactory();

            foreach (var item in source)
            {
                var row = table.NewRow();
                Cache<T>.Fill(row, item);
                table.Rows.Add(row);
            }

            return table;
        }

        private static class Cache<T>
        {
            // ReSharper disable StaticMemberInGenericType
            private static readonly PropertyInfo[] PropertyInfos;
            // ReSharper restore StaticMemberInGenericType

            // ReSharper disable StaticMemberInGenericType
            public static readonly Func<DataTable> SchemeFactory;
            // ReSharper restore StaticMemberInGenericType
            public static readonly Action<DataRow, T> Fill;

            static Cache()
            {
                PropertyInfos =
                    typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                SchemeFactory = GenerateSchemeFactory();
                Fill = GenerateFill();
            }

            private static Func<DataTable> GenerateSchemeFactory()
            {
                var dynamicMethod =
                    new DynamicMethod($"__Extensions__SchemeFactory__Of__{typeof(T).Name}", typeof(DataTable),
                        Type.EmptyTypes, typeof(T), true);

                var generator = dynamicMethod.GetILGenerator();
                // ReSharper disable AssignNullToNotNullAttribute
                generator.Emit(OpCodes.Newobj, typeof(DataTable).GetConstructor(Type.EmptyTypes));
                // ReSharper restore AssignNullToNotNullAttribute
                generator.Emit(OpCodes.Dup);

                // ReSharper disable PossibleNullReferenceException
                generator.Emit(OpCodes.Callvirt, typeof(DataTable).GetProperty("Columns").GetMethod);
                // ReSharper restore PossibleNullReferenceException

                foreach (var propertyInfo in PropertyInfos)
                {
                    generator.Emit(OpCodes.Dup);
                    generator.Emit(OpCodes.Ldstr, propertyInfo.Name);
                    generator.Emit(OpCodes.Ldtoken, propertyInfo.PropertyType);
                    // ReSharper disable AssignNullToNotNullAttribute
                    generator.Emit(OpCodes.Call,
                        typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }));
                    // ReSharper restore AssignNullToNotNullAttribute
                    // ReSharper disable AssignNullToNotNullAttribute
                    generator.Emit(OpCodes.Callvirt,
                        typeof(DataColumnCollection).GetMethod("Add", new[] { typeof(string), typeof(Type) }));
                    // ReSharper restore AssignNullToNotNullAttribute
                    generator.Emit(OpCodes.Pop);
                }

                generator.Emit(OpCodes.Pop);
                generator.Emit(OpCodes.Ret);

                return (Func<DataTable>)dynamicMethod.CreateDelegate(typeof(Func<DataTable>));
            }

            private static Action<DataRow, T> GenerateFill()
            {
                var dynamicMethod =
                    new DynamicMethod($"__Extensions__Fill__Of__{typeof(T).Name}", typeof(void),
                        new[] { typeof(DataRow), typeof(T) }, typeof(T), true);

                var generator = dynamicMethod.GetILGenerator();
                for (var i = 0; i < PropertyInfos.Length; i++)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                    switch (i)
                    {
                        case 0:
                            generator.Emit(OpCodes.Ldc_I4_0);
                            break;
                        case 1:
                            generator.Emit(OpCodes.Ldc_I4_1);
                            break;
                        case 2:
                            generator.Emit(OpCodes.Ldc_I4_2);
                            break;
                        case 3:
                            generator.Emit(OpCodes.Ldc_I4_3);
                            break;
                        case 4:
                            generator.Emit(OpCodes.Ldc_I4_4);
                            break;
                        case 5:
                            generator.Emit(OpCodes.Ldc_I4_5);
                            break;
                        case 6:
                            generator.Emit(OpCodes.Ldc_I4_6);
                            break;
                        case 7:
                            generator.Emit(OpCodes.Ldc_I4_7);
                            break;
                        case 8:
                            generator.Emit(OpCodes.Ldc_I4_8);
                            break;
                        default:
                            if (i <= 127)
                            {
                                generator.Emit(OpCodes.Ldc_I4_S, (byte)i);
                            }
                            else
                            {
                                generator.Emit(OpCodes.Ldc_I4, i);
                            }
                            break;
                    }
                    generator.Emit(OpCodes.Ldarg_1);
                    generator.Emit(OpCodes.Callvirt, PropertyInfos[i].GetGetMethod(true));
                    if (PropertyInfos[i].PropertyType.IsValueType)
                    {
                        generator.Emit(OpCodes.Box, PropertyInfos[i].PropertyType);
                    }
                    // ReSharper disable AssignNullToNotNullAttribute
                    generator.Emit(OpCodes.Callvirt, typeof(DataRow).GetMethod("set_Item", new[] { typeof(int), typeof(object) }));
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                generator.Emit(OpCodes.Ret);
                return (Action<DataRow, T>)dynamicMethod.CreateDelegate(typeof(Action<DataRow, T>));
            }

        }
#endif

#if NETSTANDARD2_0
        /// <summary>
        /// 将集合转成DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this ICollection<T> source)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p =>
                new DataColumn(p.PropertyType.GetAttribute<ExporterAttribute>()?.Name ?? p.GetDisplayName() ?? p.Name,
                    p.PropertyType)).ToArray());
            if (source.Count <= 0) return dt;

            for (var i = 0; i < source.Count; i++)
            {
                var tempList = new ArrayList();
                foreach (var obj in props.Select(pi => pi.GetValue(source.ElementAt(i), null))) tempList.Add(obj);
                var array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }

            return dt;
        }

#endif

        /// <summary>
        /// 读取嵌入式资源
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="embeddedFileName"></param>
        /// <returns></returns>
        public static string ReadManifestString(this Assembly assembly, string embeddedFileName)
        {
            var resourceName = assembly.GetManifestResourceNames().First(s =>
                s.EndsWith(embeddedFileName, StringComparison.CurrentCultureIgnoreCase));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"无法加载嵌入式资源，请确认路径是否正确：{embeddedFileName}。");
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     获取显示名
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDisplayName(this ICustomAttributeProvider customAttributeProvider, bool inherit = false)
        {
            string displayName = null;
            var displayAttribute = customAttributeProvider.GetAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                displayName = displayAttribute.Name;
            }
            else
            {
                var displayNameAttribute = customAttributeProvider.GetAttribute<DisplayNameAttribute>();
                if (displayNameAttribute != null)
                    displayName = displayNameAttribute.DisplayName;
            }

            return displayName;
        }


        /// <summary>
        /// 获取字典分组名称
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetGroupName(this ICustomAttributeProvider customAttributeProvider, bool inherit = false)
        {
            string displayName = null;
            var displayAttribute = customAttributeProvider.GetAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                displayName = displayAttribute.GroupName;
            }

            return displayName;
        }

        /// <summary>
        ///     获取类型描述
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDescription(this ICustomAttributeProvider customAttributeProvider, bool inherit = false)
        {
            var des = string.Empty;
            var desAttribute = customAttributeProvider.GetAttribute<DescriptionAttribute>();
            if (desAttribute != null) des = desAttribute.Description;
            return des;
        }

        /// <summary>
        ///     获取类型描述或显示名
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetTypeDisplayOrDescription(this ICustomAttributeProvider customAttributeProvider,
            bool inherit = false)
        {
            var dispaly = customAttributeProvider.GetDescription(inherit);
            if (dispaly.IsNullOrWhiteSpace()) dispaly = customAttributeProvider.GetDisplayName(inherit);
            return dispaly ?? string.Empty;
        }


        /// <summary>
        ///     获取程序集属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider assembly, bool inherit = false)
            where T : Attribute
        {
            return assembly
                .GetCustomAttributes(typeof(T), inherit)
                .OfType<T>()
                .FirstOrDefault();
        }

        /// <summary>
        ///     检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="assembly">The assembly<see cref="ICustomAttributeProvider" /></param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this ICustomAttributeProvider assembly, bool inherit = false)
            where T : Attribute
        {
            return assembly.GetCustomAttributes(typeof(T), inherit).Any(m => m as T != null);
        }

        /// <summary>
        ///     是否必填
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsRequired(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetAttribute<RequiredAttribute>(true) != null) return true;
            //Boolean、Byte、SByte、Int16、UInt16、Int32、UInt32、Int64、UInt64、Char、Double、Single
            if (propertyInfo.PropertyType.IsPrimitive) return true;
            switch (propertyInfo.PropertyType.Name)
            {
                case "DateTime":
                case "Decimal":
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     获取当前程序集中应用此特性的类
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="assembly"></param>
        /// <param name="inherit">The inherit<see cref="bool" /></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesWith<TAttribute>(this Assembly assembly, bool inherit)
            where TAttribute : Attribute
        {
            var attrType = typeof(TAttribute);
            foreach (var type in assembly.GetTypes().Where(type => type.GetCustomAttributes(attrType, true).Length > 0))
                yield return type;
        }

        /// <summary>
        ///     获取枚举定义列表
        /// </summary>
        /// <returns>返回枚举列表元组（名称、值、描述）</returns>
        public static List<Tuple<string, int, string>> GetEnumDefinitionList(this Type type)
        {
            var list = new List<Tuple<string, int, string>>();
            var attrType = type;
            if (!attrType.IsEnum) return null;
            var names = Enum.GetNames(attrType);
            var values = Enum.GetValues(attrType);
            var index = 0;
            foreach (var value in values)
            {
                var name = names[index];
                string des = null;
                var objAttrs = value.GetType().GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objAttrs != null &&
                    objAttrs.Length > 0)
                {
                    var descAttr = objAttrs[0] as DescriptionAttribute;
                    des = descAttr?.Description;
                }

                var item = new Tuple<string, int, string>(name, Convert.ToInt32(value), des);
                list.Add(item);
                index++;
            }

            return list;
        }

        /// <summary>
        ///     获取枚举显示名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDictionary<string, int> GetEnumDisplayNames(this Type type)
        {
            if (!type.IsEnum) throw new InvalidOperationException();
            var names = Enum.GetNames(type);
            IDictionary<string, int> displayNames = new Dictionary<string, int>();
            foreach (var name in names)
            {
                if (type.GetField(name)
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .SingleOrDefault() is DisplayAttribute displayAttribute)
                {
                    var value = (int)Enum.Parse(type, name);
                    displayNames.Add(displayAttribute.Name, value);
                }
            }

            return displayNames;
        }

        /// <summary>
        ///     获取类的所有枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, List<Tuple<string, int, string>>> GetClassEnumDefinitionList(this Type type)
        {
            var enumPros = type.GetProperties().Where(p => p.PropertyType.IsEnum);
            var dic = new Dictionary<string, List<Tuple<string, int, string>>>();
            foreach (var item in enumPros) dic.Add(item.Name, item.PropertyType.GetEnumDefinitionList());
            return dic;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetCSharpTypeName(this Type type)
        {
            var sb = new StringBuilder();
            var name = type.Name;
            if (!type.IsGenericType) return name;
            sb.Append(name.Substring(0, name.IndexOf('`')));
            sb.Append("<");
            sb.Append(string.Join(", ", type.GetGenericArguments()
                .Select(t => t.GetCSharpTypeName())));
            sb.Append(">");
            return sb.ToString();
        }
    }
}
