using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Collections;
using UWay.Skynet.Cloud.Data.DataAttribute;
using UWay.Skynet.Cloud.ExceptionHandle;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Threading.Internal;

namespace UWay.Skynet.Cloud.Mapping
{
    /// <summary>
    /// 映射引擎接口
    /// </summary> 
    //[Contract]
    public interface IMappingEngine
    {
        /// <summary>
        /// 得到映射器注册表
        /// </summary>
        IMapperRegistry MapperRegistry { get; }
        /// <summary>
        /// 得到映射器工厂注册表
        /// </summary>
        IMapperFactoryRegistry FactoryRegistry { get; }
        /// <summary>
        /// 创建映射器
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        IMapper<TFrom, TTo> CreateMapper<TFrom, TTo>();

    }

    /// <summary>
    /// 映射器接口
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 得到映射器元数据信息
        /// </summary>
        IMapperInfo Info { get; }

        /// <summary>
        /// 
        /// </summary>
        IErrorState State { get; }

        /// <summary>
        /// 把from对象映射到to对象中
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void Map(ref object from, ref object to);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        object Map(object from, Type fromType, Type toType);
    }

    /// <summary>
    /// 映射器注册表接口
    /// </summary>
    //[Contract]
    public interface IMapperRegistry : IDictionary<string, IMapper> { }

    /// <summary>
    /// 映射器工厂接口
    /// </summary>
    //[Contract]
    public interface IMapperFactory
    {
        /// <summary>
        /// 序号
        /// </summary>
        int Order { get; set; }
        /// <summary>
        /// Order changed event
        /// </summary>
        event EventHandler OrderChanged;
        /// <summary>
        /// 是否支持fromType到toType的映射
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        bool IsMatch(Type fromType, Type toType);

        /// <summary>
        /// 创建fromType到toType的映射器
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        IMapper Create(Type fromType, Type toType);
    }

    /// <summary>
    /// 映射器工厂基类
    /// </summary>
    [Component( Lifestyle= LifestyleFlags.Singleton)]
    public abstract class MapperFactory : IMapperFactory
    {
        /// <summary>
        /// 序号
        /// </summary>
        protected int order;
        /// <summary>
        /// 序号
        /// </summary>
        public int Order
        {
            get { return order; }
            set
            {
                if (order != value)
                {
                    order = value;

                    var tmpOrderChanged = OrderChanged;
                    if (tmpOrderChanged != null)
                        tmpOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Order changed event
        /// </summary>
        public event EventHandler OrderChanged;
        /// <summary>
        /// 是否支持fromType到toType的映射
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public abstract bool IsMatch(Type fromType, Type toType);
        /// <summary>
        /// 创建fromType到toType的映射器
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public abstract IMapper Create(Type fromType, Type toType);
    }

    /// <summary>
    /// 映射器工厂注册表
    /// </summary>
    //[Contract]
    public interface IMapperFactoryRegistry : IEnumerable<IMapperFactory>
    {
        /// <summary>
        /// 注册MapperFactory
        /// </summary>
        /// <param name="mapperFactory"></param>
        void Register(IMapperFactory mapperFactory);
        /// <summary>
        /// 注销MapperFactory
        /// </summary>
        /// <param name="mapperFactory"></param>
        void Unregister(IMapperFactory mapperFactory);
    }


    /// <summary>
    /// 泛型映射器接口
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    public interface IMapper<TFrom, TTo>
    {
        /// <summary>
        /// 得到映射器元数据信息
        /// </summary>
        IMapperInfo Info { get; }

        /// <summary>
        /// 
        /// </summary>
        IErrorState State { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreCase(bool flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreUnderscore(bool flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationMember"></param>
        /// <param name="memberOptions"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> ForMember(Expression<Func<TTo, object>> destinationMember, Func<TFrom, object> memberOptions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreSourceMember(Expression<Func<TFrom, object>> member);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreDestinationMember(Expression<Func<TTo, object>> member);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreSourceMembers(Func<Type, IEnumerable<string>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> IgnoreDestinationMembers(Func<Type, IEnumerable<string>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> MatchMembers(Func<string, string, bool> match);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="converter"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> ConvertUsing<From, To>(Func<From, To> converter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> BeforeMap(Action<TFrom, TTo> handler);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IMapper<TFrom, TTo> AfterMap(Action<TFrom, TTo> handler);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TTo Map(TFrom from);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void Map(TFrom from, ref TTo to);



    }



    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field
         | AttributeTargets.Property
         , AllowMultiple = false, Inherited = true)]
    public class SpliteAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly char[] Separator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        public SpliteAttribute(params char[] separator)
        {
            if (separator == null || separator.Length == 0)
                throw new ArgumentNullException("separator");

            this.Separator = separator;
        }
    }

    namespace Internal
    {
        //static class MetadataMapper
        //{
        //    public static To Map<From, To>() where To : class
        //    {
        //        return typeof(From).GetMappings().GetMappingView<To>();
        //    }

        //    public static IDictionary<string, object> Map<From>()
        //    {
        //        return typeof(From).GetMappings();
        //    }

        //    public static To Map<To>(PropertySet mappings)
        //    {
        //        return mappings.GetMappingView<To>();
        //    }

        //}

        //static class MetadataService
        //{
        //    public static PropertySet GetMappings(this Type type)
        //    {
        //        var metadata = new PropertySet() as IDictionary<string, object>;

        //        var items = (from item in type.GetCustomAttributes(false)
        //                     let p = item as IMetadata
        //                     where p != null
        //                     group p by p.Name into g
        //                     select new { Name = g.Key, g }
        //                 ).ToList();

        //        var otherItems = (from item in type.GetCustomAttributes(false)
        //                          let itemType = item.GetType()
        //                          where itemType.HasAttribute<MetadataAttributeAttribute>(true)
        //                          group item by itemType into g
        //                          select g).ToList();

        //        foreach (var item in otherItems)
        //        {
        //            var group = item.ToList();

        //            var props = (from p in item.Key.GetProperties()
        //                         where !(p.Name == "TypeId" && p.PropertyType == typeof(object))
        //                         select new { Name = p.Name, Handler = p.GetGetMethod(), Result = new List<object>() }).ToList();

        //            var fields = (from p in item.Key.GetFields()
        //                          select new { Name = p.Name, Field = p, Result = new List<object>() }).ToList();

        //            foreach (var i in group)
        //            {
        //                foreach (var p in props)
        //                    p.Result.Add(p.Handler.FastFuncInvoke(i, new object[0]));

        //                foreach (var p in fields)
        //                    p.Result.Add(p.Field.GetValue(i));
        //            }

        //            foreach (var p in props)
        //            {
        //                if (p.Result.Count == 1)
        //                    metadata.Add(p.Name, p.Result[0]);
        //                else if (p.Result.Count > 1)
        //                {
        //                    try
        //                    {
        //                        metadata.Add(p.Name, p.Result.ToArray());
        //                    }
        //                    catch
        //                    {
        //                        Console.WriteLine(p.Name);
        //                    }

        //                }
        //            }

        //            foreach (var p in fields)
        //            {
        //                if (p.Result.Count == 1)
        //                    metadata.Add(p.Name, p.Result[0]);
        //                else if (p.Result.Count > 1)
        //                    metadata.Add(p.Name, p.Result.ToArray());
        //            }

        //        }


        //        foreach (var item in items)
        //        {
        //            var vals = item.g.ConvertAll<IMetadata, object>(p => p.Value).ToArray();
        //            if (vals.Length > 1)
        //                metadata.Add(item.Name, vals);
        //            else if (vals.Length == 1)
        //                metadata.Add(item.Name, vals[0]);
        //        }

        //        return metadata as PropertySet;
        //    }
        //}

        //        internal static class MetadataViewGenerator
        //        {
        //            public const string MappingViewType = "MappingViewType";
        //            public const string MappingItemKey = "MappingItemKey";
        //            public const string MappingItemTargetType = "MappingItemTargetType";
        //            public const string MappingItemSourceType = "MappingItemSourceType";
        //            public const string MappingItemValue = "MappingItemValue";

        //            private static System.Threading.ReaderWriterLockSlim _lock = new System.Threading.ReaderWriterLockSlim();
        //            private static Dictionary<Type, Type> _proxies = new Dictionary<Type, Type>();

        //            private static AssemblyName ProxyAssemblyName = new AssemblyName(string.Format(CultureInfo.InvariantCulture, "MappingViewProxies_{0}", Guid.NewGuid()));
        //            private static AssemblyBuilder ProxyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(ProxyAssemblyName, AssemblyBuilderAccess.Run);
        //            private static ModuleBuilder ProxyModuleBuilder = ProxyAssemblyBuilder.DefineDynamicModule("MappingViewProxiesModule");
        //            private static Type[] CtorArgumentTypes = new Type[] { typeof(PropertySet) };
        //            private static MethodInfo _mdvDictionaryTryGetPropertyValue = CtorArgumentTypes[0].GetMethod("TryGetPropertyValue", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        //            private static readonly MethodInfo ObjectGetType = typeof(object).GetMethod("GetType", Type.EmptyTypes);

        //            public static Type GenerateView(Type viewType)
        //            {
        //                Guard.NotNull(viewType, "viewType");
        //                Guard.IsTrue(viewType.IsInterface);

        //                Type proxyType;
        //                bool foundProxy;

        //                using (new ReadLock(_lock))
        //                {
        //                    foundProxy = _proxies.TryGetValue(viewType, out proxyType);
        //                }

        //                // No factory exists
        //                if (!foundProxy)
        //                {
        //                    // Try again under a write lock if still none generate the proxy
        //                    using (new WriteLock(_lock))
        //                    {
        //                        foundProxy = _proxies.TryGetValue(viewType, out proxyType);

        //                        if (!foundProxy)
        //                        {
        //                            proxyType = GenerateInterfaceViewProxyType(viewType);
        //                            Guard.NotNull(proxyType, "proxyType");

        //                            _proxies.Add(viewType, proxyType);
        //                        }
        //                    }
        //                }
        //                return proxyType;
        //            }

        //            private static void GenerateLocalAssignmentFromDefaultAttribute(this ILGenerator IL, DefaultValueAttribute[] attrs, LocalBuilder local)
        //            {
        //                if (attrs.Length > 0)
        //                {
        //                    DefaultValueAttribute defaultAttribute = attrs[0];
        //                    IL.LoadValue(defaultAttribute.Value);
        //                    if ((defaultAttribute.Value != null) && (defaultAttribute.Value.GetType().IsValueType))
        //                    {
        //                        IL.Emit(OpCodes.Box, defaultAttribute.Value.GetType());
        //                    }

        //                    IL.Emit(OpCodes.Stloc, local);
        //                }
        //            }

        //            private static void GenerateFieldAssignmentFromLocalValue(this ILGenerator IL, LocalBuilder local, FieldBuilder field)
        //            {
        //                IL.Emit(OpCodes.Ldarg_0);
        //                IL.Emit(OpCodes.Ldloc, local);
        //                IL.UnboxOrCast(field.FieldType);
        //                IL.Emit(OpCodes.Stfld, field);
        //            }

        //            private static void GenerateLocalAssignmentFromFlag(this ILGenerator IL, LocalBuilder local, bool flag)
        //            {
        //                IL.Emit(flag ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
        //                IL.Emit(OpCodes.Stloc, local);
        //            }

        //            // This must be called with _readerWriterLock held for Write
        //            private static Type GenerateInterfaceViewProxyType(Type viewType)
        //            {
        //                // View binderType is an interface let's cook an component
        //                Type proxyType;
        //                TypeBuilder proxyTypeBuilder;
        //                Type[] interfaces = { viewType };

        //                proxyTypeBuilder = ProxyModuleBuilder.DefineType(
        //                    string.Format(CultureInfo.InvariantCulture, "_proxy_{0}_{1}", viewType.FullName, Guid.NewGuid()),
        //                    TypeAttributes.Public,
        //                    typeof(object),
        //                    interfaces);

        //                // Implement Constructor
        //                ILGenerator proxyCtorIL = proxyTypeBuilder.CreateGeneratorForPublicConstructor(CtorArgumentTypes);
        //                LocalBuilder exception = proxyCtorIL.DeclareLocal(typeof(Exception));
        //                LocalBuilder exceptionData = proxyCtorIL.DeclareLocal(typeof(IDictionary));
        //                LocalBuilder sourceType = proxyCtorIL.DeclareLocal(typeof(Type));
        //                LocalBuilder value = proxyCtorIL.DeclareLocal(typeof(object));
        //                LocalBuilder usesExportedMD = proxyCtorIL.DeclareLocal(typeof(bool));

        //                Label tryConstructView = proxyCtorIL.BeginExceptionBlock();

        //                #region Implement interface properties
        //                // 
        //                foreach (PropertyInfo propertyInfo in viewType.GetAllProperties())
        //                {
        //                    string fieldName = string.Format(CultureInfo.InvariantCulture, "_{0}_{1}", propertyInfo.Name, Guid.NewGuid());

        //                    // Cache names and binderType for exception
        //                    string propertyName = string.Format(CultureInfo.InvariantCulture, "{0}", propertyInfo.Name);

        //                    Type[] propertyTypeArguments = new Type[] { propertyInfo.PropertyType };
        //                    Type[] optionalModifiers = null;
        //                    Type[] requiredModifiers = null;

        //#if !SILVERLIGHT
        //                    // PropertyInfo does not support GetOptionalCustomModifiers and GetRequiredCustomModifiers on Silverlight
        //                    optionalModifiers = propertyInfo.GetOptionalCustomModifiers();
        //                    requiredModifiers = propertyInfo.GetRequiredCustomModifiers();
        //                    Array.Reverse(optionalModifiers);
        //                    Array.Reverse(requiredModifiers);
        //#endif
        //                    // Generate field
        //                    FieldBuilder proxyFieldBuilder = proxyTypeBuilder.DefineField(
        //                        fieldName,
        //                        propertyInfo.PropertyType,
        //                        FieldAttributes.Private);

        //                    // Generate property
        //                    PropertyBuilder proxyPropertyBuilder = proxyTypeBuilder.DefineProperty(
        //                        propertyName,
        //                        System.Reflection.PropertyAttributes.None,
        //                        propertyInfo.PropertyType,
        //                        propertyTypeArguments);

        //                    // Generate constructor code for retrieving the metadata value and setting the field
        //                    Label tryCastValue = proxyCtorIL.BeginExceptionBlock();
        //                    Label innerTryCastValue;

        //                    DefaultValueAttribute[] attrs = propertyInfo.GetAttributes<DefaultValueAttribute>(false);
        //                    if (attrs.Length > 0)
        //                    {
        //                        innerTryCastValue = proxyCtorIL.BeginExceptionBlock();
        //                    }

        //                    // In constructor set the backing field with the value from the dictionary
        //                    Label doneGettingDefaultValue = proxyCtorIL.DefineLabel();

        //                    GenerateLocalAssignmentFromFlag(proxyCtorIL, usesExportedMD, true);

        //                    //propertySet.TryGetPropertyValue(Type toType, string property, out object defaultValue)
        //                    proxyCtorIL.Emit(OpCodes.Ldarg_1);
        //                    proxyCtorIL.LoadTypeOf(propertyInfo.PropertyType);
        //                    proxyCtorIL.Emit(OpCodes.Ldstr, propertyName);
        //                    proxyCtorIL.Emit(OpCodes.Ldloca, value);
        //                    proxyCtorIL.Emit(OpCodes.Callvirt, _mdvDictionaryTryGetPropertyValue);
        //                    proxyCtorIL.Emit(OpCodes.Brtrue, doneGettingDefaultValue);

        //                    proxyCtorIL.GenerateLocalAssignmentFromFlag(usesExportedMD, false);
        //                    proxyCtorIL.GenerateLocalAssignmentFromDefaultAttribute(attrs, value);

        //                    proxyCtorIL.MarkLabel(doneGettingDefaultValue);
        //                    proxyCtorIL.GenerateFieldAssignmentFromLocalValue(value, proxyFieldBuilder);
        //                    proxyCtorIL.Emit(OpCodes.Leave, tryCastValue);

        //                    // catch blocks for innerTryCastValue start here
        //                    if (attrs.Length > 0)
        //                    {
        //                        proxyCtorIL.BeginCatchBlock(typeof(InvalidCastException));
        //                        {
        //                            Label notUsesExportedMd = proxyCtorIL.DefineLabel();
        //                            proxyCtorIL.Emit(OpCodes.Ldloc, usesExportedMD);
        //                            proxyCtorIL.Emit(OpCodes.Brtrue, notUsesExportedMd);
        //                            proxyCtorIL.Emit(OpCodes.Rethrow);
        //                            proxyCtorIL.MarkLabel(notUsesExportedMd);
        //                            proxyCtorIL.GenerateLocalAssignmentFromDefaultAttribute(attrs, value);
        //                            proxyCtorIL.GenerateFieldAssignmentFromLocalValue(value, proxyFieldBuilder);
        //                        }
        //                        proxyCtorIL.EndExceptionBlock();
        //                    }

        //                    // catch blocks for tryCast start here
        //                    proxyCtorIL.BeginCatchBlock(typeof(NullReferenceException));
        //                    {
        //                        proxyCtorIL.Emit(OpCodes.Stloc, exception);

        //                        proxyCtorIL.GetExceptionDataAndStoreInLocal(exception, exceptionData);
        //                        proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingItemKey, propertyName);
        //                        proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingItemTargetType, propertyInfo.PropertyType);
        //                        proxyCtorIL.Emit(OpCodes.Rethrow);
        //                    }

        //                    proxyCtorIL.BeginCatchBlock(typeof(InvalidCastException));
        //                    {
        //                        proxyCtorIL.Emit(OpCodes.Stloc, exception);

        //                        proxyCtorIL.GetExceptionDataAndStoreInLocal(exception, exceptionData);
        //                        proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingItemKey, propertyName);
        //                        proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingItemTargetType, propertyInfo.PropertyType);
        //                        proxyCtorIL.Emit(OpCodes.Rethrow);
        //                    }

        //                    proxyCtorIL.EndExceptionBlock();

        //                    if (propertyInfo.CanWrite)
        //                    {
        //                        // The MetadataView '{0}' is invalid because property '{1}' has a property set method.
        //                        throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
        //                            "InvalidSetterOnMappingField",
        //                            viewType,
        //                            propertyName));
        //                    }
        //                    if (propertyInfo.CanRead)
        //                    {
        //                        // Generate "get" method component.
        //                        MethodBuilder getMethodBuilder = proxyTypeBuilder.DefineMethod(
        //                            string.Format(CultureInfo.InvariantCulture, "get_{0}", propertyName),
        //                            MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final,
        //                            CallingConventions.HasThis,
        //                            propertyInfo.PropertyType,
        //                            requiredModifiers,
        //                            optionalModifiers,
        //                            Type.EmptyTypes, null, null);

        //                        proxyTypeBuilder.DefineMethodOverride(getMethodBuilder, propertyInfo.GetGetMethod());
        //                        ILGenerator getMethodIL = getMethodBuilder.GetILGenerator();
        //                        getMethodIL.Emit(OpCodes.Ldarg_0);
        //                        getMethodIL.Emit(OpCodes.Ldfld, proxyFieldBuilder);
        //                        getMethodIL.Emit(OpCodes.Ret);

        //                        proxyPropertyBuilder.SetGetMethod(getMethodBuilder);
        //                    }
        //                }

        //                #endregion

        //                proxyCtorIL.Emit(OpCodes.Leave, tryConstructView);

        //                // catch blocks for constructView start here
        //                proxyCtorIL.BeginCatchBlock(typeof(NullReferenceException));
        //                {
        //                    proxyCtorIL.Emit(OpCodes.Stloc, exception);

        //                    proxyCtorIL.GetExceptionDataAndStoreInLocal(exception, exceptionData);
        //                    proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingViewType, viewType);
        //                    proxyCtorIL.Emit(OpCodes.Rethrow);
        //                }
        //                proxyCtorIL.BeginCatchBlock(typeof(InvalidCastException));
        //                {
        //                    proxyCtorIL.Emit(OpCodes.Stloc, exception);

        //                    proxyCtorIL.GetExceptionDataAndStoreInLocal(exception, exceptionData);
        //                    proxyCtorIL.Emit(OpCodes.Ldloc, value);
        //                    proxyCtorIL.Emit(OpCodes.Call, ObjectGetType);
        //                    proxyCtorIL.Emit(OpCodes.Stloc, sourceType);
        //                    proxyCtorIL.AddItemToLocalDictionary(exceptionData, MappingViewType, viewType);
        //                    proxyCtorIL.AddLocalToLocalDictionary(exceptionData, MappingItemSourceType, sourceType);
        //                    proxyCtorIL.AddLocalToLocalDictionary(exceptionData, MappingItemValue, value);
        //                    proxyCtorIL.Emit(OpCodes.Rethrow);
        //                }
        //                proxyCtorIL.EndExceptionBlock();

        //                // Finished implementing interface and constructor
        //                proxyCtorIL.Emit(OpCodes.Ret);
        //                proxyType = proxyTypeBuilder.CreateType();

        //                return proxyType;
        //            }

        //        }

        //        static class MetadataViewProvider
        //        {
        //            internal static readonly Type DefaultMappingViewType = typeof(PropertySet);

        //            public static object GetMappingView(this PropertySet mappings, Type metadataViewType)
        //            {
        //                if (typeof(IEnumerable<KeyValuePair<string, object>>).IsAssignableFrom(metadataViewType))
        //                    return mappings;

        //                Type proxyType;
        //                if (metadataViewType.IsInterface)
        //                {
        //                    try
        //                    {
        //                        proxyType = MetadataViewGenerator.GenerateView(metadataViewType);
        //                    }
        //                    catch (TypeLoadException ex)
        //                    {
        //                        throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "NotSupportedInterfaceMappingView", metadataViewType.FullName), ex);
        //                    }
        //                }
        //                else
        //                {
        //                    proxyType = metadataViewType;
        //                }

        //                try
        //                {
        //                    return Activator.CreateInstance(proxyType, mappings);
        //                }
        //                catch (MissingMethodException ex)
        //                {
        //                    // Unable to create an Instance of the Metadata view '{0}' because a constructor could not be selected.  Ensure that the binderType implements a constructor which takes an argument of binderType IDictionary<string, object>.
        //                    throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
        //                        "CompositionException_MappingViewInvalidConstructor",
        //                        proxyType.AssemblyQualifiedName), ex);
        //                }
        //                catch (TargetInvocationException ex)
        //                {
        //                    //Unwrap known failures that we want to present as CompositionContractMismatchException
        //                    if (metadataViewType.IsInterface)
        //                    {
        //                        if (ex.InnerException.GetType() == typeof(InvalidCastException))
        //                        {
        //                            // Unable to create an Instance of the Metadata view {0} because the exporter exported the metadata for the item {1} with the value {2} as binderType {3} but the view imports it as binderType {4}.
        //                            throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
        //                                "InvalidCastOnMappingField",
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingViewType],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemKey],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemValue],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemSourceType],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemTargetType]), ex);
        //                        }
        //                        else if (ex.InnerException.GetType() == typeof(NullReferenceException))
        //                        {
        //                            // Unable to create an Instance of the Metadata view {0} because the exporter exported the metadata for the item {1} with a null value and null is not a valid value for binderType {2}.
        //                            throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
        //                                "NullReferenceOnMetadataField",
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingViewType],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemKey],
        //                                ex.InnerException.Data[MetadataViewGenerator.MappingItemTargetType]), ex);
        //                        }
        //                    }
        //                    throw;
        //                }
        //            }

        //            [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        //            public static TMappingView GetMappingView<TMappingView>(this PropertySet mappings)
        //            {
        //                Type metadataViewType = typeof(TMappingView);
        //                return (TMappingView)GetMappingView(mappings, typeof(TMappingView));

        //            }
        //        }
        //    }
    }
}
