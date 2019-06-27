using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    public static class ExpressionExtensions
    {
        public static string MemberWithoutInstance(this LambdaExpression expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }

        public static bool IsBindable(this LambdaExpression expression)
        {
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                case ExpressionType.Parameter:
                    return true;
            }

            return false;
        }

        public static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            return memberExpression;
        }


    }

    internal static class ExpressionHelper
    {
        // Methods
        public static string GetExpressionText(LambdaExpression expression)
        {
            Stack<string> source = new Stack<string>();
            Expression body = expression.Body;
            while (body != null)
            {
                if (body.NodeType == ExpressionType.Call)
                {
                    MethodCallExpression expression3 = (MethodCallExpression)body;
                    if (!IsSingleArgumentIndexer(expression3))
                    {
                        break;
                    }
                    source.Push(GetIndexerInvocation(expression3.Arguments.Single<Expression>(), expression.Parameters.ToArray<ParameterExpression>()));
                    body = expression3.Object;
                }
                else
                {
                    if (body.NodeType == ExpressionType.ArrayIndex)
                    {
                        BinaryExpression expression4 = (BinaryExpression)body;
                        source.Push(GetIndexerInvocation(expression4.Right, expression.Parameters.ToArray<ParameterExpression>()));
                        body = expression4.Left;
                        continue;
                    }
                    if (body.NodeType == ExpressionType.MemberAccess)
                    {
                        MemberExpression expression5 = (MemberExpression)body;
                        source.Push("." + expression5.Member.Name);
                        body = expression5.Expression;
                        continue;
                    }
                    if (body.NodeType != ExpressionType.Parameter)
                    {
                        break;
                    }
                    source.Push(string.Empty);
                    body = null;
                }
            }
            if ((source.Count > 0) && string.Equals(source.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
            {
                source.Pop();
            }
            if (source.Count <= 0)
            {
                return string.Empty;
            }
            return source.Aggregate<string>((left, right) => (left + right)).TrimStart(new char[] { '.' });
        }

        public static string GetExpressionText(string expression)
        {
            if (!string.Equals(expression, "model", StringComparison.OrdinalIgnoreCase))
            {
                return expression;
            }
            return string.Empty;
        }

        private static string GetIndexerInvocation(Expression expression, ParameterExpression[] parameters)
        {
            Func<object, object> func;
            Expression body = Expression.Convert(expression, typeof(object));
            ParameterExpression expression3 = Expression.Parameter(typeof(object), null);
            Expression<Func<object, object>> lambdaExpression = Expression.Lambda<Func<object, object>>(body, new ParameterExpression[] { expression3 });
            try
            {
                func = CachedExpressionCompiler.Process<object, object>(lambdaExpression);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Invalid Indexer Expression:{0}-{1}", new object[] { expression, parameters[0].Name }), exception);
            }
            return ("[" + Convert.ToString(func(null), CultureInfo.InvariantCulture) + "]");
        }

        internal static bool IsSingleArgumentIndexer(Expression expression)
        {
            MethodCallExpression methodExpression = expression as MethodCallExpression;
            return (((methodExpression != null) && (methodExpression.Arguments.Count == 1)) && methodExpression.Method.DeclaringType.GetDefaultMembers().OfType<PropertyInfo>().Any<PropertyInfo>(p => (p.GetGetMethod() == methodExpression.Method)));
        }
    }



}
