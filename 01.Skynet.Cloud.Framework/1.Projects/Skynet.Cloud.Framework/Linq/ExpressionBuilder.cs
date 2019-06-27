using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Impl.Internal;

    public static class ExpressionBuilder
    {
        public static Expression<Func<TModel, TValue>> Expression<TModel, TValue>(string memberName)
        {
            return (Expression<Func<TModel, TValue>>)Lambda<TModel>(memberName);
        }

        public static LambdaExpression Lambda<T>(string memberName)
        {
            return Lambda<T>(memberName, false);
        }

        public static LambdaExpression Lambda<T>(Type memberType, string memberName, bool checkForNull)
        {
            MemberAccessExpressionBuilderBase expressionBuilder = ExpressionBuilderFactory.MemberAccess(typeof(T), memberType, memberName, checkForNull);

            return expressionBuilder.CreateLambdaExpression();
        }

        public static LambdaExpression Lambda<T>(string memberName, bool checkForNull)
        {
            MemberAccessExpressionBuilderBase expressionBuilder = ExpressionBuilderFactory.MemberAccess(typeof(T), memberName, checkForNull);

            return expressionBuilder.CreateLambdaExpression();
        }

        public static Expression<Func<T, bool>> Expression<T>(IList<IFilterDescriptor> filterDescriptors)
        {
            return Expression<T>(filterDescriptors, true);
        }

        public static Expression<Func<T, bool>> Expression<T>(IList<IFilterDescriptor> filterDescriptors, bool checkForNull)
        {
            ParameterExpression parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(T), "item");

            var builder = new FilterDescriptorCollectionExpressionBuilder(parameterExpression, filterDescriptors);

            builder.Options.LiftMemberAccessToNull = checkForNull;

            return (Expression<Func<T, bool>>)builder.CreateFilterExpression();
        }
    }
}
