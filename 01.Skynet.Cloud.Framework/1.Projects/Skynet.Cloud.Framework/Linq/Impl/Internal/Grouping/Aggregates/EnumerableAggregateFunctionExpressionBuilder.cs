

namespace UWay.Skynet.Cloud.Linq.Impl.Internal.Grouping.Aggregates
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;

    internal class EnumerableAggregateFunctionExpressionBuilder : AggregateFunctionExpressionBuilderBase
    {
        protected new EnumerableAggregateFunction Function
        {
            get
            {
                return (EnumerableAggregateFunction)base.Function;
            }
        }

        /// <exception cref="ArgumentException">
        /// Provided <paramref name="enumerableExpression"/>'s <see cref="Expression.Type"/> is not <see cref="IEnumerable{T}"/>
        /// </exception>
        public EnumerableAggregateFunctionExpressionBuilder(Expression enumerableExpression, EnumerableAggregateFunction function)
            : base(enumerableExpression, function)
        {
        }

        public override Expression CreateAggregateExpression()
        {
            return Expression.Call(
                this.Function.ExtensionMethodsType,
                this.Function.AggregateMethodName,
                new[] { this.ItemType },
                this.EnumerableExpression);
        }
    }
}
