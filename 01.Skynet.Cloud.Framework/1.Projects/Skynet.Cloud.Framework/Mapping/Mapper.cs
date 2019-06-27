using System;
using System.Reflection;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Mapping.Internal;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Mapper
    {

        static readonly  MethodInfo InternMap_Method = typeof(Mapper).GetMethod("InternalMap", BindingFlags.Static | BindingFlags.NonPublic);

        static IMappingEngine _current;

        static Mapper()
        {
            Current = new MappingEngine();
        }

        /// <summary>
        /// 得到当前活动的MappingEngine
        /// </summary>
        public static IMappingEngine Current
        {
            get
            {
                if (_current == null)
                    _current = new MappingEngine();
                return _current;
            }
            set
            {
                Guard.NotNull(value, "value");
                if (_current != value)
                    _current = value;
            }
        }

        /// <summary>
        /// 是否启用ErrorState进行跟踪
        /// </summary>
        public static bool EnableErrorState { get; set; }

       
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="from"></param>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public static object Map(object from, Type fromType, Type toType)
        {
            Guard.NotNull(toType,"toType");

            if (toType == Types.Object)
                return from;

            if (from != null)
                fromType = from.GetType();
            if(from == DBNull.Value)
            	return ObjectCreator.Create(toType);
            if (from == null && toType.IsValueType)
                return ObjectCreator.Create(toType);


            return InternMap_Method.MakeGenericMethod(fromType, toType).Invoke(null, new object[] { from });
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TTo Map<TFrom, TTo>(TFrom from)
        {
            return Current.CreateMapper<TFrom, TTo>().Map(from);
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void Map<TFrom, TTo>(TFrom from,ref TTo to)
        {
            Current.CreateMapper<TFrom, TTo>().Map(from,ref  to);
        }

        private static TTo InternalMap<TFrom, TTo>(TFrom from)
        {
            return Current.CreateMapper<TFrom, TTo>().Map(from);
        }

        /// <summary>
        /// 创建映射器
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static IMapper<TFrom, TTo> CreateMapper<TFrom, TTo>()
        {
            return Current.CreateMapper<TFrom, TTo>();
        }

        /// <summary>
        /// 重置映射器引擎
        /// </summary>
        public static void Reset()
        {
            Current.MapperRegistry.Clear();
        }
    }
}
