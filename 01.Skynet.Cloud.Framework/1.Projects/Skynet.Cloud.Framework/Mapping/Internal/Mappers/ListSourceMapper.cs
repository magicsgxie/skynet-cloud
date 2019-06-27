using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    class ListSourceMapper : MapperBase
    {
        private bool isFromListSource;
        private bool isToListSource;

        public ListSourceMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            isFromListSource = Types.IListSource.IsAssignableFrom(fromType);
            isToListSource = Types.IListSource.IsAssignableFrom(toType);
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;

            if (to == null)
                to = ObjectCreator.Create(_Info.To);

            var newFrom = isFromListSource ? (from as IListSource).GetList() : from;
            var fromType = newFrom.GetType();
            var tmpTo = isToListSource ? (to as IListSource).GetList() as object : to;

            new CollectionMapper(_Info.From, tmpTo.GetType()).Map(ref from, ref tmpTo);
        }
    }
}
