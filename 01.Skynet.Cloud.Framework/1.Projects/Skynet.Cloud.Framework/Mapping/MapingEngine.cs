/*
 * Created by SharpDevelop.
 * User: issuser
 * Date: 2011-1-20
 * Time: 16:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using UWay.Skynet.Cloud.Mapping.Factories;
using System.Collections.Generic;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    class MapperFactoryRegistry : IMapperFactoryRegistry
    {
        private List<IMapperFactory> Factories = new List<IMapperFactory>();

        public MapperFactoryRegistry()
        {

            Add(MapperFactoryProvider.TypeConverterMapperFactory);
            Add(MapperFactoryProvider.PrimitiveMapperFactory);

            Add(MapperFactoryProvider.DictionaryMapperFactory);
            Add(MapperFactoryProvider.ClassToDictionaryMapperFactory);
            Add(MapperFactoryProvider.DictionaryToClassMapperFactory);
            Add(MapperFactoryProvider.DataReaderMapperFactory);
            Add(MapperFactoryProvider.DataTableMapperFactory);
            Add(MapperFactoryProvider.DataRowMapperFactory);
            Add(MapperFactoryProvider.ListSourceMapperFactory);
            Add(MapperFactoryProvider.CollectionMapperFactory);
            Add(MapperFactoryProvider.ClassMapperFactory);

            Factories = Factories.OrderBy(p => p.Order).ToList();
        }

        private void Add(IMapperFactory mapperFactory)
        {
            mapperFactory.OrderChanged += new EventHandler(mapperFactory_OrderChanged);
            Factories.Add(mapperFactory);
        }

        void mapperFactory_OrderChanged(object sender, EventArgs e)
        {
            Factories = Factories.OrderBy(p => p.Order).ToList();
        }

        private void Remove(IMapperFactory mapperFactory)
        {
            if (Factories.Contains(mapperFactory))
            {
                mapperFactory.OrderChanged -= mapperFactory_OrderChanged;
                Factories.Remove(mapperFactory);
            }
        }

        public void Register(IMapperFactory mapperFactory)
        {
            Add(mapperFactory);
            Factories = Factories.OrderBy(p => p.Order).ToList();
        }

        public void Unregister(IMapperFactory mapperFactory)
        {
            Remove(mapperFactory);
        }

        public IEnumerator<IMapperFactory> GetEnumerator()
        {
            return Factories.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class MapperRegistry : Dictionary<string, IMapper>, IMapperRegistry
    {
    }

    class MappingEngine : IMappingEngine
    {
        private readonly object Mutex = new object();
        public IMapperRegistry MapperRegistry { get; private set; }
        public IMapperFactoryRegistry FactoryRegistry { get; private set; }

        public MappingEngine() : this(new MapperFactoryRegistry(), new MapperRegistry()) { }
        public MappingEngine(IMapperFactoryRegistry factoryRegistry, IMapperRegistry mapperRegistry)
        {
            FactoryRegistry = factoryRegistry;
            MapperRegistry = mapperRegistry;
        }

        public IMapper<TFrom, TTo> CreateMapper<TFrom, TTo>()
        {
            return (IMapper<TFrom, TTo>)GetOrCreateMap(typeof(TFrom), typeof(TTo));
        }

        internal IMapper GetOrCreateMap(Type fromType, Type toType)
        {

            var key = fromType.FullName + "->" + toType.FullName;

            if (MapperRegistry.ContainsKey(key))
                return MapperRegistry[key];

            return CreateMap(fromType, toType, key);
        }

        private IMapper CreateMap(Type fromType, Type toType, string key)
        {
            IMapper mapper = null;
            foreach (var fac in FactoryRegistry)
            {
                if (fac.IsMatch(fromType, toType))
                {
                    mapper = CreateGenericMapper(fromType, toType, fac);
                    lock(Mutex)
                        MapperRegistry.Add(key, mapper);
                    break;
                }
            }

            if (mapper == null)
                throw new NotSupportedException("Mapper does not support " + key + ".");
            return mapper;
        }

        private static IMapper CreateGenericMapper(Type fromType, Type toType, IMapperFactory fac)
        {
            var mapper = fac.Create(fromType, toType);

            var genericMapperType = typeof(GenericMapper<,>)
             .MakeGenericType(fromType, toType);
            var ctor = genericMapperType.GetConstructor(new Type[] { typeof(MapperBase) });

            var genericMapper = (IMapper)ctor
                .FastInvoke(mapper);

            return genericMapper;
        }
    }
}
