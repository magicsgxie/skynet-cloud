using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Linq
{
    public class Expressor<T>
    {
        public static FieldInfo Field(Expression<Func<T, object>> func)
        {
            return (FieldInfo)((MemberExpression)((UnaryExpression)func.Body).Operand).Member;
        }

        public static PropertyInfo Property(Expression<Func<T, object>> func)
        {
            return ((PropertyInfo)((MemberExpression)func.Body).Member);
        }

        public static MethodInfo PropertyGet(Expression<Func<T, object>> func)
        {
            return ((PropertyInfo)((MemberExpression)func.Body).Member).GetGetMethod();
        }
        public static MethodInfo PropertySet(Expression<Func<T, object>> func)
        {
            return ((PropertyInfo)((MemberExpression)func.Body).Member).GetSetMethod();
        }

        public static MethodInfo Method(Expression<Func<T, object>> func)
        {
            return Expressor.Method<T>(func);
        }

        public static MethodInfo Method<T2>(Expression<Func<T, T2, object>> func)
        {
            return Expressor.Method<T, T2>(func);
        }

        public static MethodInfo Method<T2, T3>(Expression<Func<T, T2, T3, object>> func)
        {
            return Expressor.Method<T, T2, T3>(func);
        }

        public static MethodInfo Method<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> func)
        {
            return Expressor.Method<T, T2, T3, T4>(func);
        }

        public static MethodInfo Method<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> func)
        {
            return Expressor.Method<T, T2, T3, T4, T5>(func);
        }

        public static MethodInfo Method(Expression<Action<T>> func)
        {
            return Expressor.Method<T>(func);
        }

        public static MethodInfo Method<T2>(Expression<Action<T, T2>> func)
        {
            return Expressor.Method<T, T2>(func);
        }

        public static MethodInfo Method<T2, T3>(Expression<Action<T, T2, T3>> func)
        {
            return Expressor.Method<T, T2, T3>(func);
        }

        public static MethodInfo Method<T2, T3, T4>(Expression<Action<T, T2, T3, T4>> func)
        {
            return Expressor.Method<T, T2, T3, T4>(func);
        }

        public static MethodInfo Method<T2, T3, T4, T5>(Expression<Action<T, T2, T3, T4, T5>> func)
        {
            return Expressor.Method<T, T2, T3, T4, T5>(func);
        }

        public class Nullable<T> : Expressor<System.Nullable<T>> where T : struct
        {
            public static readonly ConstructorInfo Constructor = typeof(System.Nullable<T>).GetConstructor(Type.EmptyTypes);
            public static readonly PropertyInfo HasValue = Property(s => s.HasValue);
            public static readonly PropertyInfo Value = Property(s => s.Value);
            public static readonly MethodInfo GetValueOrDefault = Method(s => s.GetValueOrDefault());
            public static readonly MethodInfo GetValueOrDefault1 = Method<T>((t, s) => t.GetValueOrDefault(s));
        }
    }

    public static class Expressor
    {

        public static MethodInfo Method(Expression<Func<object>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T>(Expression<Func<T, object>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2>(Expression<Func<T, T2, object>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2, T3>(Expression<Func<T, T2, T3, object>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> func)
        {
            return Method(func as LambdaExpression);
        }

        public static MethodInfo Method<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> func)
        {
            return Method(func as LambdaExpression);
        }

        public static MethodInfo Method(Expression<Action> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T>(Expression<Action<T>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2>(Expression<Action<T, T2>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2, T3>(Expression<Action<T, T2, T3>> func)
        {
            return Method(func as LambdaExpression);
        }
        public static MethodInfo Method<T, T2, T3, T4>(Expression<Action<T, T2, T3, T4>> func)
        {
            return Method(func as LambdaExpression);
        }

        public static MethodInfo Method<T, T2, T3, T4, T5>(Expression<Action<T, T2, T3, T4, T5>> func)
        {
            return Method(func as LambdaExpression);
        }


        public static MemberInfo Member(Expression<Func<object>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T>(Expression<Func<T, object>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2>(Expression<Func<T, T2, object>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2, T3>(Expression<Func<T, T2, T3, object>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> func)
        {
            return Member(func as LambdaExpression);
        }

        public static MemberInfo Member<T, T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> func)
        {
            return Member(func as LambdaExpression);
        }

        public static MemberInfo Member(Expression<Action> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T>(Expression<Action<T>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2>(Expression<Action<T, T2>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2, T3>(Expression<Action<T, T2, T3>> func)
        {
            return Member(func as LambdaExpression);
        }
        public static MemberInfo Member<T, T2, T3, T4>(Expression<Action<T, T2, T3, T4>> func)
        {
            return Member(func as LambdaExpression);
        }

        public static MemberInfo Member<T, T2, T3, T4, T5>(Expression<Action<T, T2, T3, T4, T5>> func)
        {
            return Member(func as LambdaExpression);
        }

        internal static MethodInfo Method(LambdaExpression func)
        {
            var ex = func.Body;
            if (ex is UnaryExpression)
                ex = ((UnaryExpression)ex).Operand;
            return ((MethodCallExpression)ex).Method;
        }

        public static MemberInfo Member(LambdaExpression func)
        {
            var ex = func.Body;

            if (ex is UnaryExpression)
                ex = ((UnaryExpression)ex).Operand;
            return
                ex is MemberExpression ? ((MemberExpression)ex).Member :
                ex is MethodCallExpression ? ((MethodCallExpression)ex).Method :
                                 (MemberInfo)((NewExpression)ex).Constructor;
        }

        public class DataReader : Expressor<IDataReader>
        {
            public static MethodInfo GetValue = Method(rd => rd.GetValue(0));
            public static MethodInfo IsDBNull = Method(rd => rd.IsDBNull(0));
        }

        #region Convert

        static IEnumerable<T> Convert<T>(IEnumerable<T> source, Func<T, T> func)
            where T : class
        {
            var modified = false;
            var list = new List<T>();

            foreach (var item in source)
            {
                var e = func(item);
                list.Add(e);
                modified = modified || e != item;
            }

            return modified ? list : source;
        }

        static IEnumerable<T> Convert<T>(IEnumerable<T> source, Func<Expression, Expression> func)
            where T : Expression
        {
            var modified = false;
            var list = new List<T>();

            foreach (var item in source)
            {
                var e = Convert(item, func);
                list.Add((T)e);
                modified = modified || e != item;
            }

            return modified ? list : source;
        }

        public static Expression Convert(this Expression expr, Func<Expression, Expression> func)
        {
            if (expr == null)
                return null;

            switch (expr.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Assign:

                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = (BinaryExpression)expr;
                        var c = Convert(e.Conversion, func);
                        var l = Convert(e.Left, func);
                        var r = Convert(e.Right, func);

                        return c != e.Conversion || l != e.Left || r != e.Right ?
                            Expression.MakeBinary(expr.NodeType, l, r, e.IsLiftedToNull, e.Method, (LambdaExpression)c) :
                            expr;
                    }

                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as UnaryExpression;
                        var o = Convert(e.Operand, func);

                        return o != e.Operand ?
                            Expression.MakeUnary(expr.NodeType, o, e.Type, e.Method) :
                            expr;
                    }

                case ExpressionType.Call:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as MethodCallExpression;
                        var o = Convert(e.Object, func);
                        var a = Convert(e.Arguments, func);

                        return o != e.Object || a != e.Arguments ?
                            Expression.Call(o, e.Method, a) :
                            expr;
                    }

                case ExpressionType.Conditional:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as ConditionalExpression;
                        var s = Convert(e.Test, func);
                        var t = Convert(e.IfTrue, func);
                        var f = Convert(e.IfFalse, func);

                        return s != e.Test || t != e.IfTrue || f != e.IfFalse ?
                            Expression.Condition(s, t, f) :
                            expr;
                    }

                case ExpressionType.Invoke:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as InvocationExpression;
                        var ex = Convert(e.Expression, func);
                        var a = Convert(e.Arguments, func);

                        return ex != e.Expression || a != e.Arguments ? Expression.Invoke(ex, a) : expr;
                    }

                case ExpressionType.Lambda:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as LambdaExpression;
                        var b = Convert(e.Body, func);
                        var p = Convert(e.Parameters, func);

                        return b != e.Body || p != e.Parameters ? Expression.Lambda(ex.Type, b, p.ToArray()) : expr;
                    }

                case ExpressionType.ListInit:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as ListInitExpression;
                        var n = Convert(e.NewExpression, func);
                        var i = Convert(e.Initializers, p =>
                        {
                            var args = Convert(p.Arguments, func);
                            return args != p.Arguments ? Expression.ElementInit(p.AddMethod, args) : p;
                        });

                        return n != e.NewExpression || i != e.Initializers ?
                            Expression.ListInit((NewExpression)n, i) :
                            expr;
                    }

                case ExpressionType.MemberAccess:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as MemberExpression;
                        var ex = Convert(e.Expression, func);

                        return ex != e.Expression ? Expression.MakeMemberAccess(ex, e.Member) : expr;
                    }

                case ExpressionType.MemberInit:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        Func<MemberBinding, MemberBinding> modify = null; modify = b =>
                        {
                            switch (b.BindingType)
                            {
                                case MemberBindingType.Assignment:
                                    {
                                        var ma = (MemberAssignment)b;
                                        var ex = Convert(ma.Expression, func);

                                        if (ex != ma.Expression)
                                            ma = Expression.Bind(ma.Member, ex);

                                        return ma;
                                    }

                                case MemberBindingType.ListBinding:
                                    {
                                        var ml = (MemberListBinding)b;
                                        var i = Convert(ml.Initializers, p =>
                                        {
                                            var args = Convert(p.Arguments, func);
                                            return args != p.Arguments ? Expression.ElementInit(p.AddMethod, args) : p;
                                        });

                                        if (i != ml.Initializers)
                                            ml = Expression.ListBind(ml.Member, i);

                                        return ml;
                                    }

                                case MemberBindingType.MemberBinding:
                                    {
                                        var mm = (MemberMemberBinding)b;
                                        var bs = Convert(mm.Bindings, modify);

                                        if (bs != mm.Bindings)
                                            mm = Expression.MemberBind(mm.Member);

                                        return mm;
                                    }
                            }

                            return b;
                        };

                        var e = expr as MemberInitExpression;
                        var ne = Convert(e.NewExpression, func);
                        var bb = Convert(e.Bindings, modify);

                        return ne != e.NewExpression || bb != e.Bindings ?
                            Expression.MemberInit((NewExpression)ne, bb) :
                            expr;
                    }

                case ExpressionType.New:
                    {
                        var ex = func(expr);
                        if (ex != expr)
                            return ex;

                        var e = expr as NewExpression;
                        var a = Convert(e.Arguments, func);

                        return a != e.Arguments ?
                            e.Members == null ?
                                Expression.New(e.Constructor, a) :
                                Expression.New(e.Constructor, a, e.Members) :
                            expr;
                    }

                case ExpressionType.NewArrayBounds:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as NewArrayExpression;
                        var ex = Convert(e.Expressions, func);

                        return ex != e.Expressions ? Expression.NewArrayBounds(e.Type, ex) : expr;
                    }

                case ExpressionType.NewArrayInit:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as NewArrayExpression;
                        var ex = Convert(e.Expressions, func);

                        return ex != e.Expressions ?
                            Expression.NewArrayInit(e.Type.GetElementType(), ex) :
                            expr;
                    }

                case ExpressionType.TypeIs:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as TypeBinaryExpression;
                        var ex = Convert(e.Expression, func);

                        return ex != e.Expression ? Expression.TypeIs(ex, e.Type) : expr;
                    }
#if SDK4
                case ExpressionType.Block:
                    {
                        var exp = func(expr);
                        if (exp != expr)
                            return exp;

                        var e = expr as BlockExpression;
                        var ex = Convert(e.Expressions, func);
                        var v = Convert(e.Variables, func);

                        return ex != e.Expressions || v != e.Variables ? Expression.Block(e.Type, v, ex) : expr;
                    }
#endif
                case ExpressionType.Constant:
                case ExpressionType.Parameter: return func(expr);
            }

            throw new InvalidOperationException();
        }

        #endregion

        #region Helpers

        private static readonly MethodInfo ToStringMethod = Types.Object.GetMethod("ToString");
        public static Expression ToString(Expression t)
        {
            if (t.Type == Types.String)
                return t;
            var c = t as ConstantExpression;
            return c != null ?
                c.Value != null ? Expression.Constant(c.Value.ToString()) : Expression.Constant(null, Types.String)
                : (Expression)Expression.Call(t, ToStringMethod);
        }

        public static Expression Unwrap(this Expression ex)
        {
            if (ex == null)
                return null;

            switch (ex.NodeType)
            {
                case ExpressionType.Quote: return ((UnaryExpression)ex).Operand.Unwrap();
                case ExpressionType.ConvertChecked:
                case ExpressionType.Convert:
                    {
                        var ue = (UnaryExpression)ex;

                        if (!ue.Operand.Type.IsEnum)
                            return ue.Operand.Unwrap();

                        break;
                    }
            }

            return ex;
        }

        #endregion
    }
}
