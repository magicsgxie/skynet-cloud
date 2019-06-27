using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class PrimitiveMaper : MapperBase
    {
        Delegate converter;
        Delegate defaultValue;
        public PrimitiveMaper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            converter = Converter.GetConverter(fromType, toType);
            defaultValue = Converter.GetConverter(null, toType);
        }

        public override void Map(ref object from, ref object to)
        {
            try
            {
                to = from == null || from == DBNull.Value ? defaultValue.DynamicInvoke() : converter.DynamicInvoke(from);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
