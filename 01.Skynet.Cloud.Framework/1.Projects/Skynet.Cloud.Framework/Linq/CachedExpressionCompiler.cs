using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal delegate TValue Hoisted<TModel, TValue>(TModel model, List<object> capturedConstants);
    internal static class CachedExpressionCompiler
    {
        // Methods
        public static Func<TModel, TValue> Process<TModel, TValue>(Expression<Func<TModel, TValue>> lambdaExpression)
        {
            return Compiler<TModel, TValue>.Compile(lambdaExpression);
        }

        // Nested Types
        private static class Compiler<TIn, TOut>
        {
            // Fields
            private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> _constMemberAccessDict;
            private static readonly ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>> _fingerprintedCache;
            private static Func<TIn, TOut> _identityFunc;
            private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> _simpleMemberAccessDict;

            // Methods
            static Compiler()
            {
                CachedExpressionCompiler.Compiler<TIn, TOut>._simpleMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();
                CachedExpressionCompiler.Compiler<TIn, TOut>._constMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();
                CachedExpressionCompiler.Compiler<TIn, TOut>._fingerprintedCache = new ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>();
            }

            public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
            {
                return (CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromIdentityFunc(expr) ?? (CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromConstLookup(expr) ?? (CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromMemberAccess(expr) ?? (CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromFingerprint(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileSlow(expr)))));
            }

            private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
            {
                ConstantExpression body = expr.Body as ConstantExpression;
                if (body != null)
                {
                    TOut constantValue = (TOut)body.Value;
                    return _ => constantValue;
                }
                return null;
            }

            private static Func<TIn, TOut> CompileFromFingerprint(Expression<Func<TIn, TOut>> expr)
            {
                Func<ExpressionFingerprintChain, Hoisted<TIn, TOut>> valueFactory = null;
                List<object> capturedConstants;
                ExpressionFingerprintChain fingerprintChain = FingerprintingExpressionVisitor.GetFingerprintChain(expr, out capturedConstants);
                if (fingerprintChain == null)
                {
                    return null;
                }
                if (valueFactory == null)
                {
                    valueFactory = _ => HoistingExpressionVisitor<TIn, TOut>.Hoist(expr).Compile();
                }
                Hoisted<TIn, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._fingerprintedCache.GetOrAdd(fingerprintChain, valueFactory);
                return model => del(model, capturedConstants);
            }

            private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
            {
                if (expr.Body != expr.Parameters[0])
                {
                    return null;
                }
                if (CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc == null)
                {
                    CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc = expr.Compile();
                }
                return CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc;
            }

            private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
            {
                Func<MemberInfo, Func<TIn, TOut>> valueFactory = null;
                Func<MemberInfo, Func<object, TOut>> func2 = null;
                MemberExpression memberExpr = expr.Body as MemberExpression;
                if (memberExpr != null)
                {
                    if ((memberExpr.Expression == expr.Parameters[0]) || (memberExpr.Expression == null))
                    {
                        if (valueFactory == null)
                        {
                            valueFactory = _ => expr.Compile();
                        }
                        return CachedExpressionCompiler.Compiler<TIn, TOut>._simpleMemberAccessDict.GetOrAdd(memberExpr.Member, valueFactory);
                    }
                    ConstantExpression expression = memberExpr.Expression as ConstantExpression;
                    if (expression != null)
                    {
                        if (func2 == null)
                        {
                            func2 = delegate (MemberInfo _) {
                                ParameterExpression expression1;
                                UnaryExpression expression2 = Expression.Convert(expression1 = Expression.Parameter(typeof(object), "capturedLocal"), memberExpr.Member.DeclaringType);
                                return Expression.Lambda<Func<object, TOut>>(memberExpr.Update(expression2), new ParameterExpression[] { expression1 }).Compile();
                            };
                        }
                        Func<object, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._constMemberAccessDict.GetOrAdd(memberExpr.Member, func2);
                        object capturedLocal = expression.Value;
                        return _ => del(capturedLocal);
                    }
                }
                return null;
            }

            private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
            {
                return expr.Compile();
            }
        }
    }


}
