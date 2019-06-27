using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
namespace UWay.Skynet.Cloud.Data.Linq
{
    interface IQueryPolicy
    {
        bool IsDeferLoaded(MemberInfo member);
        bool IsIncluded(MemberInfo member);
        IDictionary<MemberInfo, List<LambdaExpression>> Operations { get; }
    }
}
