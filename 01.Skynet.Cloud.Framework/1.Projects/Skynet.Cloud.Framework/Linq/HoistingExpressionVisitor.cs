using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class HoistingExpressionVisitor<TIn, TOut> : ExpressionVisitor
    {
        // Fields 
        private static readonly
            ParameterExpression _hoistedConstantsParamExpr;
        private int _numConstantsProcessed;
        // Methods 
        static HoistingExpressionVisitor() {
            HoistingExpressionVisitor<TIn, TOut>._hoistedConstantsParamExpr =
                Expression.Parameter(typeof(List<object>), "hoistedConstants");
        }
        private HoistingExpressionVisitor()
        { }


        public static Expression<Hoisted<TIn, TOut>> Hoist(Expression<Func<TIn, TOut>> expr)
        {
            HoistingExpressionVisitor<TIn, TOut> visitor = new HoistingExpressionVisitor<TIn, TOut>();
            return Expression.Lambda<
                Hoisted<TIn, TOut>>(
                    visitor.Visit(expr.Body),
                new ParameterExpression[] {
                    expr.Parameters[0],
                    HoistingExpressionVisitor<TIn, TOut>._hoistedConstantsParamExpr
                });
        }
        protected override Expression VisitConstant(ConstantExpression node) {
            return Expression.Convert(
                Expression.Property(HoistingExpressionVisitor<TIn, TOut>._hoistedConstantsParamExpr,
                "Item", new Expression[] { Expression.Constant(this._numConstantsProcessed++) }),
                node.Type);

        }
    }
}
