
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud;

namespace UWay.Skynet.Cloud.Mapping.Internal
{

    sealed class DataRowMapper : MapperBase
    {
        private DefaultConstructorHandler Ctor;
        public DataRowMapper(Type toType)
            : base(typeof(DataRow), toType)
        {
            if (!toType.IsAbstract)
            {
                if (toType.IsNullable())
                    Ctor = DynamicMethodFactory.GetDefaultCreator(Nullable.GetUnderlyingType(toType));
                else
                    Ctor = DynamicMethodFactory.GetDefaultCreator(toType);
            }
        }
        public override void Map(ref object from, ref object to)
        {
            var row = from as DataRow;
            if (row == null)
                return;

            if (to == null)
                to = Ctor();

            foreach (DataColumn col in row.Table.Columns)
                to.SetProperty(col.ColumnName, row[col]);
        }
    }
}
