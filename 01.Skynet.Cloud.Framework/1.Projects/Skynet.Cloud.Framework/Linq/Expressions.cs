using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Expressions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <param name="ifTrue"></param>
        /// <param name="ifFalse"></param>
        /// <returns></returns>
        public static ConditionalExpression Condition(this Expression test, Expression ifTrue, Expression ifFalse)
        {
            return Expression.Condition(test, ifTrue, ifFalse);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static MemberExpression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static NewExpression New(this Type type)
        {
            return Expression.New(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UnaryExpression TypeAs(this Expression expression, Type type)
        {
            return Expression.TypeAs(expression, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UnaryExpression Convert(this Expression expression, Type type)
        {
            return Expression.Convert(expression, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression Equal(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.Equal(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression NotEqual(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.NotEqual(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression GreaterThan(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.GreaterThan(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression GreaterThanOrEqual(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.GreaterThanOrEqual(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression LessThan(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.LessThan(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression LessThanOrEqual(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.LessThanOrEqual(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression And(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.And(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression Or(this Expression expression1, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.Or(expression1, expression2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="op"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static Expression Binary(this Expression expression1, ExpressionType op, Expression expression2)
        {
            ConvertExpressions(ref expression1, ref expression2);
            return Expression.MakeBinary(op, expression1, expression2);
        }

        private static void ConvertExpressions(ref Expression expression1, ref Expression expression2)
        {
            var leftType = expression1.Type;
            var rightType = expression2.Type;
            if (leftType != rightType)
            {
                var isNullable1 = leftType.IsNullable();
                var isNullable2 = rightType.IsNullable();
                if (isNullable1 || isNullable2)
                {
                    var type1 = isNullable1 ? Nullable.GetUnderlyingType(leftType) : leftType;
                    var type2 = isNullable2 ? Nullable.GetUnderlyingType(rightType) : rightType;

                    if (type1 == type2)
                    {
                        if (!isNullable1)
                            expression1 = Expression.Convert(expression1, rightType);
                        else if (!isNullable2)
                            expression2 = Expression.Convert(expression2, leftType);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="binarySeparators"></param>
        /// <returns></returns>
        public static Expression[] Split(this Expression expression, params ExpressionType[] binarySeparators)
        {
            var list = new List<Expression>();
            Split(expression, list, binarySeparators);
            return list.ToArray();
        }

        private static void Split(Expression expression, List<Expression> list, ExpressionType[] binarySeparators)
        {
            if (expression != null)
            {
                if (binarySeparators.Contains(expression.NodeType))
                {
                    var bex = expression as BinaryExpression;
                    if (bex != null)
                    {
                        Split(bex.Left, list, binarySeparators);
                        Split(bex.Right, list, binarySeparators);
                    }
                }
                else
                {
                    list.Add(expression);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="binarySeparator"></param>
        /// <returns></returns>
        public static Expression Join(this IEnumerable<Expression> list, ExpressionType binarySeparator)
        {
            if (list != null)
            {
                var array = list.ToArray();
                if (array.Length > 0)
                {
                    return array.Aggregate((x1, x2) => Expression.MakeBinary(binarySeparator, x1, x2));
                }
            }
            return null;
        }
    }
}
