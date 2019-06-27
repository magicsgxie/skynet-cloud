using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud;
using System.Diagnostics;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    /// <summary>
    /// 类映射对象
    /// </summary>
    sealed class ClassMapper : MapperBase
    {
        private DefaultConstructorHandler Ctor;
        public ClassMapper(Type fromType, Type toType)
            : base(fromType, toType)
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
            if (from == null)
                return;

            var mappings = _Info.Mappings;
            var mappingCount = mappings.Count;

            if (_Info.memberMappings.Count == 0 && mappingCount == 0)
                return;

            if (to == null)
            {

                if (Ctor != null)
                    to = Ctor();
                //else
                //    to = ObjectCreator.Create(_Info.To);
            }

            for (int i = 0; i < mappingCount; i++)
            {
                var item = mappings[i];
                try
                {
                    object value = item.GetSourceMemberValue(ref from);
                    item.SetTargetMemberValue(ref to, ref value);
                }
                catch (Exception ex)
                {
                    var message = string.Format("{0}.{1} -> {2}.{3}"
                        , item.From.Member.DeclaringType.Name
                        , item.From.Id
                        , item.To.Member.DeclaringType.Name
                        , item.To.Id);

                    if (State != null)
                        State.AddError(message, ex.Message);

                    throw new InvalidCastException(message, ex);
                }
            }

            FilterMembers(ref from, ref to);
        }
    }
}
