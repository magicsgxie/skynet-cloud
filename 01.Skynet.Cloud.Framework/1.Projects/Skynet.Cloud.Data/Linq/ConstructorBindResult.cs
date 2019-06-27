using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq
{
    public class ConstructorBindResult
    {
        public NewExpression Expression { get; private set; }
        public EntityAssignment[] Remaining { get; private set; }
        public ConstructorBindResult(NewExpression expression, IEnumerable<EntityAssignment> remaining)
        {
            this.Expression = expression;
            this.Remaining = remaining.ToArray();
        }
    }
}
