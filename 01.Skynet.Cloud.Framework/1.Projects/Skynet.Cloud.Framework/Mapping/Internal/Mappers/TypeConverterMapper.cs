using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class TypeConverterMapper : MapperBase
    {
        private readonly TypeConverter fromTypeConverter;
        private readonly TypeConverter toConverter;

        public TypeConverterMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            fromTypeConverter = TypeConverterFactory.GetTypeConverter(fromType);
            toConverter = TypeConverterFactory.GetTypeConverter(toType);
        }

        public override void Map(ref object from, ref object to)
        {
            if (from != null)
            {
                if (_Info.CanUsingConverter(_Info.Key))
                    to = _Info.converters[_Info.Key].DynamicInvoke(from);
                else
                {
                    if (fromTypeConverter.CanConvertTo(_Info.To))
                    {
                        to = fromTypeConverter.ConvertTo(from, _Info.To);
                    }
                    else if (_Info.To == typeof(Color) && _Info.From == Types.Int32)
                    {
                        to = Color.FromArgb((int)from);
                    }
                    else
                    {
                        to = toConverter.ConvertFrom(from);
                    }
                }
            }
        }
    }
}
