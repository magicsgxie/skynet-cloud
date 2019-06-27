using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Linq
{
    [DebuggerDisplay("{Member.Name}={Expression}")]
    public class EntityAssignment
    {
        public MemberInfo Member { get; private set; }
        public IMemberMapping MemberMapping { get; private set; }
        public Expression Expression { get; private set; }
        public EntityAssignment(IMemberMapping mm, Expression expression)
        {
            this.MemberMapping = mm;
            this.Member = mm.Member;
            System.Diagnostics.Debug.Assert(expression != null);
            this.Expression = expression;
        }
        public EntityAssignment(MemberInfo member, Expression expression)
        {
            this.Member = member;
            System.Diagnostics.Debug.Assert(expression != null);
            this.Expression = expression;
        }


    }
}
