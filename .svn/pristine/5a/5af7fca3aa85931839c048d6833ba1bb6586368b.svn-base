using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.Function.Default
{
    class ContainsFunctionView: IFunctionView
    {
        private static Expression FormatValue(Expression value)
        {

            Expression arg = value;
            //if (hasEscape)
            //{
            //    //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("%"), Expression.Constant("~~"));
            //    //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("_"), Expression.Constant("~~"));
            //    //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("~"), Expression.Constant("~~"));
            //}
            var c = value as ConstantExpression;
            if (c != null && c.Value == null)
                value = Expression.Constant("NULL", Types.String);
            //if (hasStart && hasEnd)
                return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, Expression.Constant("%"), value, Expression.Constant("%")));

            //if (hasStart)
            //    return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, value, Expression.Constant("%")));

            //if (hasEnd)
            //    return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, Expression.Constant("%"), value));
            //return value;
        }

        public void Render(ISqlBuilder builder, params Expression[] args)
        {
            var value = args[1];
            value = FormatValue(value);

            builder.Append("(");
            builder.Visit(args[0]);
            builder.Append(" LIKE ");
            builder.Visit(value);
            builder.Append(")");
        }
    }
}
