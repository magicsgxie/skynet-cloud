using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Extensions
{
    /// <summary>
    /// Converter 类，支持基本数据类型，以及Enum和Nullable数据类型间的相互转换
    /// </summary>
    public sealed class Converter
    {
        static MethodInfo enumParseMethod = typeof(Enum).GetMethod("Parse", new Type[] { typeof(Type), typeof(string), typeof(bool) });
        internal static readonly Dictionary<KeyAndValue, MethodInfo> Mappers;
        internal static readonly HashSet<KeyAndValue> ExplictMappers;
        static readonly Dictionary<KeyAndValue, Delegate> Converters = new Dictionary<KeyAndValue, Delegate>();

        static Converter()
        {
            ExplictMappers = new HashSet<KeyAndValue>();

            //Char
            RegisterExplictMapping<Byte, Char>();

            //Int16
            RegisterExplictMapping<SByte, Int16>();
            RegisterExplictMapping<Byte, Int16>();

            //UInt16
            RegisterExplictMapping<Char, UInt16>();
            RegisterExplictMapping<Byte, UInt16>();


            //Int32
            RegisterExplictMapping<Byte, Int32>();
            RegisterExplictMapping<SByte, Int32>();
            RegisterExplictMapping<Char, Int32>();
            RegisterExplictMapping<UInt16, Int32>();
            RegisterExplictMapping<Int16, Int32>();


            //UInt32
            RegisterExplictMapping<Char, UInt32>();
            RegisterExplictMapping<Byte, UInt32>();
            RegisterExplictMapping<UInt16, UInt32>();


            //Int64
            RegisterExplictMapping<Byte, Int64>();
            RegisterExplictMapping<SByte, Int64>();
            RegisterExplictMapping<Char, Int64>();
            RegisterExplictMapping<Int16, Int64>();
            RegisterExplictMapping<UInt16, Int64>();
            RegisterExplictMapping<Int32, Int64>();
            RegisterExplictMapping<UInt32, Int64>();

            //UInt64
            RegisterExplictMapping<Byte, UInt64>();
            RegisterExplictMapping<Char, UInt64>();
            RegisterExplictMapping<UInt16, UInt64>();
            RegisterExplictMapping<UInt32, UInt64>();

            //Single
            RegisterExplictMapping<UInt32, Single>();
            RegisterExplictMapping<UInt64, Single>();
            RegisterExplictMapping<Byte, Single>();
            RegisterExplictMapping<Int16, Single>();
            RegisterExplictMapping<UInt16, Single>();
            RegisterExplictMapping<Int32, Single>();
            RegisterExplictMapping<SByte, Single>();
            RegisterExplictMapping<Int64, Single>();
            RegisterExplictMapping<Decimal, Single>();
            RegisterExplictMapping<Double, Single>();

            //Double
            RegisterExplictMapping<UInt32, Double>();
            RegisterExplictMapping<UInt64, Double>();
            RegisterExplictMapping<Byte, Double>();
            RegisterExplictMapping<Int16, Double>();
            RegisterExplictMapping<UInt16, Double>();
            RegisterExplictMapping<Int32, Double>();
            RegisterExplictMapping<SByte, Double>();
            RegisterExplictMapping<Int64, Double>();
            RegisterExplictMapping<Single, Double>();
            RegisterExplictMapping<Decimal, Double>();

            //Decimal
            RegisterExplictMapping<UInt32, Decimal>();
            RegisterExplictMapping<UInt64, Decimal>();
            RegisterExplictMapping<Byte, Decimal>();
            RegisterExplictMapping<Int16, Decimal>();
            RegisterExplictMapping<UInt16, Decimal>();
            RegisterExplictMapping<Int32, Decimal>();
            RegisterExplictMapping<SByte, Decimal>();
            RegisterExplictMapping<Int64, Decimal>();
            RegisterExplictMapping<Single, Decimal>();
            RegisterExplictMapping<Double, Decimal>();

            Mappers = new Dictionary<KeyAndValue, MethodInfo>();
            var items = (from m in typeof(Convert).GetMethods().Where(p => p.Name.StartsWith("To") && p.GetParameters().Length == 1)
                         let ps = m.GetParameters()
                         let tuple = new KeyAndValue(ps[0].ParameterType, m.ReturnType)
                         where !ExplictMappers.Contains(tuple)
                         select new { Tuple = tuple, Method = m })
                        .ToArray();

            foreach (var item in items)
                Mappers[item.Tuple] = item.Method;

            var types = new Type[] { Types.Boolean, Types.Char, Types.Int16, Types.Int32, Types.Int64, Types.UInt32, Types.UInt64, Types.Single, Types.Double };
            foreach (var type in types)
                Mappers[new KeyAndValue(type, Types.ByteArray)] = typeof(BitConverter).GetMethod("GetBytes", new Type[] { type });

            CacheDefaultConverters();

        }

        static void CacheDefaultConverters()
        {


            var items = new List<KeyAndValue>();
            foreach (var tuple in ExplictMappers.Union(Mappers.Keys))
            {
                items.Add(tuple);

                var from = tuple.FromType;
                var to = tuple.ToType;

                if (to.IsValueType)
                    items.Add(new KeyAndValue(from, typeof(Nullable<>).MakeGenericType(to)));
                else
                    items.Add(new KeyAndValue(from, to));

                if (from.IsValueType)
                {
                    from = typeof(Nullable<>).MakeGenericType(from);
                    if (to.IsValueType)
                        items.Add(new KeyAndValue(from, typeof(Nullable<>).MakeGenericType(to)));
                    else
                        items.Add(new KeyAndValue(from, to));
                }
            }
            items = items.OrderBy(p => p.ToType.Name).Distinct().ToList();

            foreach (var tuple in items)
            {
                var fromExp = Expression.Parameter(tuple.FromType, "from");
                var body = GetConvertExpression(tuple.FromType, tuple.ToType, fromExp);
                var lambda = LambdaExpression.Lambda(typeof(Func<,>).MakeGenericType(tuple.FromType, tuple.ToType), body, fromExp);


                var converter = lambda.Compile();
                Converters[tuple] = converter;
            }

        }

        static void RegisterExplictMapping<TFrom, TTo>()
        {
            ExplictMappers.Add(new KeyAndValue(typeof(TFrom), typeof(TTo)));
        }

        /// <summary>
        /// The primitive types are Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single,Decimal,DateTime,DBNull,Enum,Nullable.
        /// </summary>
        /// <returns></returns>
        public static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive //
                              || Types.String == type
                              || Types.Decimal == type
                              || Types.Guid == type
                              || Types.DateTime == type
                              || Types.DBNull == type
                              || TypeHelper.IsEnumType(type)
                              || (type.IsNullable() && IsPrimitiveType(Nullable.GetUnderlyingType(type)));
        }

     
        private static Expression GetDefaultValueExpression(Type type)
        {
            return Expression.Default(type);

        }

        internal static Expression GetConvertExpression(Type fromType, Type toType, Expression from)
        {
            if (fromType == null || fromType == Types.Void || fromType == Types.DBNull)
                return GetDefaultValueExpression(toType);
            if (toType == fromType)
            {
                if (from.Type == toType)
                    return from;
                return Expression.Convert(from, toType);
            }

            if (toType.IsAssignableFrom(fromType) && !toType.IsNullable() && !toType.IsEnum)
            {
                if (from.Type == toType)
                    return from;
                return Expression.Convert(from, toType);
            }

            if (toType == Types.String)
            {
                if (fromType == typeof(char[]))
                    return Expression.New(Types.String.GetConstructor(new Type[] { typeof(char[]) }), from);
                return Expression.Call(from, Types.Object.GetMethod("ToString"));
            }

            var type = toType.IsNullable() ? Nullable.GetUnderlyingType(toType) : toType;
            if (fromType.IsNullable())
            {
                fromType = Nullable.GetUnderlyingType(fromType);
                from = Expression.Condition(Expression.Property(from, "HasValue"), Expression.Property(from, "Value"), GetDefaultValueExpression(fromType));
            }
            if (fromType == Types.DBNull)
                return GetDefaultValueExpression(toType);
            if (fromType.IsEnum)
            {
                from = Expression.Convert(from, Enum.GetUnderlyingType(fromType));
                fromType = Enum.GetUnderlyingType(fromType);
            }

            Expression to = from;

            if (type.IsEnum)
            {
                if (fromType == Types.String)
                    to = Expression.Convert(
                                                        Expression.Call(
                                                        null
                                                        , enumParseMethod
                                                        , Expression.Constant(type)
                                                        , to
                                                        , Expression.Constant(true))
                                   , type);
                else
                {
                    var typeCode = Type.GetTypeCode(fromType);
                    if (typeCode == TypeCode.Single || typeCode == TypeCode.Decimal || typeCode == TypeCode.Double)
                    {
                        from = Expression.Call(
                                                    null
                                                    , Mappers[new KeyAndValue(fromType, Types.Int64)]
                                                    , from);
                        fromType = Types.Int64;
                        typeCode = TypeCode.Int64;
                    }

                    switch (typeCode)
                    {
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            to = Expression.Convert(
                                                      Expression.Call(
                                                      null
                                                      , typeof(Enum).GetMethod("ToObject", new Type[] { Types.Type, fromType })
                                                      , Expression.Constant(type)
                                                      , from)
                                  , type);
                            break;
                        default:
                            throw new InvalidCastException(string.Format("from '{0}' -> '{1}'", fromType, toType));
                    }


                    return toType.IsNullable()
                 ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                 : to;
                }

            }

            if (type == typeof(TimeSpan))
            {
                if (fromType == typeof(string))
                {
                    to = Expression.Call(null, Types.TimeSpan.GetMethod("Parse", new Type[] { Types.String }), from);
                    return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
                }
                if (fromType == typeof(DateTime))
                {
                    to = Expression.Property(Expression.Call(
                        null
                        , Types.DateTime.GetMethod("Parse", new Type[] { Types.String, typeof(CultureInfo) })
                        , Expression.Call(from, Types.DateTime.GetMethod("ToString"))
                        , Expression.Property(null, typeof(CultureInfo).GetProperty("InvariantCulture")))
                        , Types.DateTime.GetProperty("TimeOfDay"));
                    return toType.IsNullable()
                  ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                  : to;
                }
                if (fromType == typeof(DateTimeOffset))
                {
                    to = Expression.Property(
                             Expression.Call(
                                             null
                                             , typeof(DateTimeOffset).GetMethod("Parse", new Type[] { Types.String })
                                             , Expression.Call(from, typeof(DateTimeOffset).GetMethod("ToString"))
                                             , Expression.Property(null, typeof(CultureInfo).GetProperty("InvariantCulture")))
                             , typeof(DateTimeOffset).GetProperty("TimeOfDay"));

                    return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
                }

                to = Expression.New(
                    Types.TimeSpan.GetConstructor(new Type[] { Types.Int64 })
                    , Expression.Convert(
                        Expression.Call(
                                null
                                , typeof(Convert).GetMethod("ChangeType", new Type[] { Types.Object, Types.Type, typeof(CultureInfo) })
                                , from
                                , Expression.Constant(typeof(long))
                                , Expression.Property(null, typeof(CultureInfo).GetProperty("InvariantCulture")))
                        , Types.Int64)
                    );

                return toType.IsNullable()
              ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
              : to;
            }

            if (type == Types.ByteArray)
            {
                var fromTypeCode = Type.GetTypeCode(fromType);
                switch (fromTypeCode)
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return Expression.Call(typeof(BitConverter).GetMethod("GetBytes", new Type[] { fromType }), from);
                    case (TypeCode)21://Guid
                        return Expression.Call(from, fromType.GetMethod("ToByteArray"));
                        //case TypeCode.DateTime:
                        //    break;TODO:
                }
            }

            if (fromType == typeof(TimeSpan))
            {
                if (type == typeof(DateTime))
                {
                    to = Expression.Add(GetDefaultValueExpression(Types.DateTime), from);
                    return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
                }
                if (type == typeof(DateTimeOffset))
                {
                    to = Expression.Add(GetDefaultValueExpression(typeof(DateTimeOffset)), from);
                    return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
                }

                to = Expression.Convert(Expression.Call(
                    null
                    , typeof(Convert).GetMethod("ChangeType", new Type[] { Types.Object, Types.Type, typeof(CultureInfo) })
                    , Expression.Property(from, Types.TimeSpan.GetProperty("Ticks"))
                    , Expression.Constant(type)
                    , Expression.Property(null, typeof(CultureInfo).GetProperty("InvariantCulture")))
                    , type);

                return toType.IsNullable()
              ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
              : to;
            }
            if (type == typeof(DateTime) && fromType == typeof(DateTimeOffset))
            {
                to = Expression.Property(from, typeof(DateTimeOffset).GetProperty("DateTime"));
                return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
            }
            if (type == typeof(DateTimeOffset) && fromType == typeof(DateTime))
            {
                to = Expression.New(typeof(DateTimeOffset).GetConstructor(new Type[] { Types.DateTime }), from);
                return toType.IsNullable()
                ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                : to;
            }

            if (fromType == Types.String)
            {

                if (type == Types.Char)
                {
                    var itemMethod = Types.String.GetProperty("Chars");
                    to = Expression.Property(from, itemMethod, Expression.Constant(0));
                    return toType.IsNullable()
                                  ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                                  : to;
                }


                if (type == Types.Guid)
                {
                    to = Expression.New(Types.Guid.GetConstructor(new Type[] { Types.String }), from);

                    return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
                }
            }
            else if (fromType == typeof(char[]) && type == Types.String)
            {
                to = Expression.New(Types.String.GetConstructor(new Type[] { typeof(char[]) }), from);

                return toType.IsNullable()
               ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
               : to;
            }
            else if (fromType == Types.ByteArray)
            {

                if (type == Types.Guid)
                {
                    to = Expression.New(Types.Guid.GetConstructor(new Type[] { Types.ByteArray }), from);
                    return toType.IsNullable()
                ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                : to;
                }
                else if (toType.FullName == "System.Drawing.Image")
                {
                    to = Expression.Call(
                        null
                        , toType.GetMethod("FromStream", new Type[] { typeof(Stream) })
                        , Expression.New(typeof(MemoryStream).GetConstructor(new Type[] { Types.ByteArray }), from));

                    return toType.IsNullable()
                             ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                             : to;
                }


                var toTypeCode = Type.GetTypeCode(type);
                switch (toTypeCode)
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    //case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        {
                            to = Expression.Call(
                                null
                                , typeof(BitConverter).GetMethod("To" + toTypeCode.ToString())
                                , from
                                , Expression.Constant(0, Types.Int32));
                            return toType.IsNullable()
                           ? Expression.Convert(to, toType)
                           : to;
                        }
                    case TypeCode.String:
                        {
                            return Expression.Call(
                               Expression.Property(null, typeof(Encoding).GetProperty("Default"))
                               , typeof(Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) })
                               , from);
                        }
                    case TypeCode.DateTime:
                        {
                            return GetConvertExpression(Types.String, toType, Expression.Call(
                               Expression.Property(null, typeof(Encoding).GetProperty("Default"))
                               , typeof(Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) })
                               , from));
                        }
                    case TypeCode.Object:
                        {
                            var BinaryFormatter = Expression.New(typeof(BinaryFormatter));
                            var stream = Expression.New(typeof(MemoryStream).GetConstructor(new Type[] { Types.ByteArray }), from);
                            from = Expression.Call(BinaryFormatter, typeof(BinaryFormatter).GetMethod("Deserialize", new Type[] { typeof(Stream) }), stream);
                            Expression.Call(stream, typeof(IDisposable).GetMethod("Dispose"));
                            return GetConvertExpression(from.Type, toType, from);
                        }
                }
            }
            else if (fromType == Types.Guid)
            {
                if (toType == Types.ByteArray)
                {
                    return Expression.Call(
                      from
                      , fromType.GetMethod("ToByteArray"));

                }
            }

            if (type != fromType)
            {
                var key = new KeyAndValue(fromType, type);

                if (ExplictMappers.Contains(key))
                    to = Expression.Convert(from, type);
                else if (Mappers.ContainsKey(key))
                    to = Expression.Call(
                                                    null
                                                    , Mappers[key]
                                                    , from);
                else if (fromType.Namespace + "." + fromType.Name == "MySql.Data.Types.MySqlDateTime" && type == Types.DateTime)
                {
                    to = Expression.Call(
                                        null
                                        , fromType.GetMethod("op_Explicit", new Type[] { fromType })
                                        , Expression.Convert(from, fromType));
                }
                else if (type == Types.ByteArray)
                {
                    var binaryFormatter = Expression.New(typeof(BinaryFormatter));
                    var stream = Expression.New(typeof(MemoryStream));
                    Expression.Call(binaryFormatter, typeof(BinaryFormatter).GetMethod("Serialize", new Type[] { typeof(Stream), Types.Object }), stream, from);
                    to = Expression.Call(stream, typeof(MemoryStream).GetMethod("ToArray"));
                    Expression.Call(stream, typeof(IDisposable).GetMethod("Dispose"));
                    return to;
                }
            }

            return toType.IsNullable()
                ? Expression.Convert(to, toType)// Expression.New(toType.GetConstructor(new Type[] { type }), to)
                : to;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TTo Convert<TFrom, TTo>(TFrom from)
        {
            if (from == null)
                return default(TTo);

            var fromType = typeof(TFrom);
            var toType = typeof(TTo);

            if (toType.IsAssignableFrom(fromType))
                return (TTo)(object)from;

            var converter = GetConverter(fromType, toType) as Func<TFrom, TTo>;
            return converter(from);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public static object Convert(object from, Type toType)
        {
            try
            {
                if (from == null || from == DBNull.Value)
                    return GetConverter(null, toType).DynamicInvoke();
                return GetConverter(from.GetType(), toType).DynamicInvoke(from);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        internal static Delegate GetConverter(Type fromType, Type toType)
        {

            var key = new KeyAndValue(fromType, toType);

            Delegate @delegate;
            if (!Converters.TryGetValue(key, out @delegate))
            {
                LambdaExpression lambda = null;

                if (fromType != null)
                {
                    var fromExp = Expression.Parameter(fromType, "from");
                    var body = GetConvertExpression(fromType, toType, fromExp);
                    if (body.Type != toType)
                        body = Expression.Convert(body, toType);
                    lambda = LambdaExpression.Lambda(typeof(Func<,>).MakeGenericType(fromType, toType), body, fromExp);

                }
                else
                    lambda = LambdaExpression.Lambda(typeof(Func<>).MakeGenericType(toType), GetDefaultValueExpression(toType));

                var converter = lambda.Compile();
                lock (Converters)
                    Converters[key] = converter;
                return converter;
            }
            return @delegate;
        }

        internal struct KeyAndValue : IEquatable<KeyAndValue>
        {
            private int hashCode;
            private string key;
            private RuntimeTypeHandle? FromTypeHandle;
            private RuntimeTypeHandle ToTypeHandle;

            public Type FromType
            {
                get
                {
                    return FromTypeHandle.HasValue
                        ? Type.GetTypeFromHandle(FromTypeHandle.Value)
                        : null;
                }
            }
            public Type ToType
            {
                get { return Type.GetTypeFromHandle(ToTypeHandle); }
            }

            public KeyAndValue(Type fromType, Type toType)
            {
                key = null;
                FromTypeHandle = null;

                if (fromType != null)
                {
                    key = fromType.GetHashCode() + "->";
                    FromTypeHandle = fromType.TypeHandle;
                }
                key = key + toType.GetHashCode();
                hashCode = key.GetHashCode();
                ToTypeHandle = toType.TypeHandle;
            }

            public override int GetHashCode()
            {
                return hashCode;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is KeyAndValue))
                    return false;
                return this.Equals((KeyAndValue)obj);
            }

            public bool Equals(KeyAndValue vt)
            {
                return vt.key == this.key;
            }

            public override string ToString()
            {
                return key;
            }
        }
    }
}
