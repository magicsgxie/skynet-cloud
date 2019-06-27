using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Mapping.Internal;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Mapping.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MapperFactoryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly IMapperFactory
            ClassMapperFactory
            , ClassToDictionaryMapperFactory
            , DictionaryToClassMapperFactory
            , ListSourceMapperFactory
            , CollectionMapperFactory
            , DataReaderMapperFactory
            , DataRowMapperFactory
            , DataTableMapperFactory
            //, EnumMapperFactory
            //, NullableMapperFactory
            , PrimitiveMapperFactory
            , TypeConverterMapperFactory
            //, StringMapperFactory
            //, FlagsEnumMapperFactory
            , DictionaryMapperFactory;

        static MapperFactoryProvider()
        {
            ClassMapperFactory = new ClassMapperFactory();
            ClassToDictionaryMapperFactory = new ClassToDictionaryMapperFactory();
            DictionaryToClassMapperFactory = new DictionaryToClassMapperFactory();

            ListSourceMapperFactory = new ListSourceMapperFactory();
            CollectionMapperFactory = new CollectionMapperFactory();
            DataReaderMapperFactory = new DataReaderMapperFactory();
            DataRowMapperFactory = new DataRowMapperFactory();
            DataTableMapperFactory = new DataTableMapperFactory();
            //EnumMapperFactory = new EnumMapperFactory();
            //NullableMapperFactory = new NullableMapperFactory();
            PrimitiveMapperFactory = new PrimitiveMapperFactory();
            TypeConverterMapperFactory = new TypeConverterMapperFactory();
            //StringMapperFactory = new StringMapperFactory();
            //FlagsEnumMapperFactory = new FlagsEnumMapperFactory();
            DictionaryMapperFactory = new DictionaryMapperFactory();
        }
    }
}
