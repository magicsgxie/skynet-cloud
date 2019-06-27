using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl
{
    using Reflection;
    using UWay.Skynet.Cloud.Linq.Impl.Internal;

    internal static class BindingHelper
    {
        internal static Type ExtractMemberTypeFromObject(object item, string memberName)
        {
            if (item.GetType().IsDynamicObject())
            {
                var lambda = ExpressionBuilder.Lambda<object>(memberName, true);
                var result = ((Func<object, object>)lambda.Compile()).Invoke(item);

                if (result != null)
                {
                    return result.GetType();
                }
                return null;
            }
            return new PropertyAccessExpressionBuilder(item.GetType(), memberName).CreateMemberAccessExpression().Type;
        }
    }
}
