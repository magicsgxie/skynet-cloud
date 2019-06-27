
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class DataReaderMapper : MapperBase
    {
        private Type ElementType;
        private readonly bool IsDataTable;
        private MethodInfo ToList;

        public DataReaderMapper(Type toType)
            : base(typeof(IDataReader), toType)
        {
            if (toType.IsArray)
                ElementType = toType.GetElementType();
            else if (toType.IsAssignableFrom(typeof(DataTable)))
                IsDataTable = true;
            else
                ElementType = toType.GetGenericArguments()[0];

            if (!IsDataTable)
                ToList = typeof(DataReaderExtensions).GetMethod("ToList", new Type[] { typeof(IDataReader) }).MakeGenericMethod(ElementType);
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;

            IDataReader reader = (IDataReader)from;

            if (IsDataTable)
                to = reader.ToDataTable();
            else
            {
                var tmp = ToList.FastFuncInvoke(null, reader);
                to = Mapper.Map(tmp, tmp.GetType(), Info.To);
            }
        }
    }
}
