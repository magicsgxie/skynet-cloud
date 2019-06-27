using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Internal
{
    using System;
    using System.Linq.Expressions;

    internal class IdentityExpressionBuilder : ExpressionBuilderBase
    {
        public IdentityExpressionBuilder(Type itemType) : base(itemType)
        {
        }

        internal LambdaExpression CreateLambdaExpression()
        {
            return Expression.Lambda(this.ParameterExpression, this.ParameterExpression);
        }
    }
}
