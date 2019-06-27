using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Data.Mapping;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{

    class FunctionBinder : DbExpressionVisitor
    {
        private static MethodInfo[] _enumerableMethods;
        public static MethodInfo[] EnumerableMethods
        {
            get { return _enumerableMethods ?? (_enumerableMethods = typeof(Enumerable).GetMethods()); }
        }

        private static MethodInfo[] _queryableMethods;
        public static MethodInfo[] QueryableMethods
        {
            get { return _queryableMethods ?? (_queryableMethods = typeof(Queryable).GetMethods()); }
        }

        static Type[] GetGenericArguments(Type type, Type baseType)
        {
            var baseTypeName = baseType.Name;

            for (var t = type; t != typeof(object) && t != null; t = t.BaseType)
            {
                if (t.IsGenericType)
                {
                    if (baseType.IsGenericTypeDefinition)
                    {
                        if (t.GetGenericTypeDefinition() == baseType)
                            return t.GetGenericArguments();
                    }
                    else if (baseTypeName == null || t.Name.Split('`')[0] == baseTypeName)
                    {
                        return t.GetGenericArguments();
                    }
                }
            }

            foreach (var t in type.GetInterfaces())
            {
                if (t.IsGenericType)
                {
                    if (baseType.IsGenericTypeDefinition)
                    {
                        if (t.GetGenericTypeDefinition() == baseType)
                            return t.GetGenericArguments();
                    }
                    else if (baseTypeName == null || t.Name.Split('`')[0] == baseTypeName)
                    {
                        return t.GetGenericArguments();
                    }
                }
            }

            return null;
        }
        static bool IsSameOrParent(Type parent, Type child)
        {
            if (parent == child ||
                child.IsEnum && Enum.GetUnderlyingType(child) == parent ||
                child.IsSubclassOf(parent))
            {
                return true;
            }

            if (parent.IsGenericTypeDefinition)
                for (var t = child; t != typeof(object) && t != null; t = t.BaseType)
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == parent)
                        return true;

            if (parent.IsInterface)
            {
                var interfaces = child.GetInterfaces();

                foreach (var t in interfaces)
                {
                    if (parent.IsGenericTypeDefinition)
                    {
                        if (t.IsGenericType && t.GetGenericTypeDefinition() == parent)
                            return true;
                    }
                    else if (t == parent)
                        return true;
                }
            }

            return false;
        }

        private FunctionBinder() { }
        private InternalDbContext DbContext;
        public static Expression Bind(InternalDbContext dbContext, Expression exp)
        {
            var instance = new FunctionBinder { DbContext = dbContext };
            return instance.Visit(exp);
        }

        public override Expression Visit(Expression exp)
        {
            exp = base.Visit(exp);
            if (exp != null && exp.NodeType == ExpressionType.ArrayLength)
            {
                var array = (exp as UnaryExpression).Operand;
                if (array.Type.GetElementType() == Types.Byte)
                    return Expression.Property(Expression.Call(MethodRepository.Len, array), "Value");
            }
            return exp;
        }

        static readonly MethodInfo DateDiffMethod = Expressor<object>.Method(
                           _ => SqlFunctions.DateDiff(DateParts.Day, DateTime.MinValue, DateTime.MinValue));

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            m = base.VisitMemberAccess(m) as MemberExpression;
            var expression = m.Expression;

            var l = MethodMapping.ConvertMember(m.Member);
            if (l != null)
            {
                var body = l.Body.Unwrap();
                var expr = body.Convert(wpi => wpi.NodeType == ExpressionType.Parameter ? expression : wpi);

                if (expr.Type != m.Type)
                    expr = Expression.Convert(expr, m.Type);

                return expr;
            }

            if (m.Member.DeclaringType == typeof(TimeSpan))
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.Subtract:
                    case ExpressionType.SubtractChecked:

                        DateParts datePart;

                        switch (m.Member.Name)
                        {
                            case "TotalMilliseconds":
                            case "TotalSeconds":
                            case "TotalMinutes":
                            case "TotalHours":
                            case "TotalDays":
                                throw new NotSupportedException(string.Format("The member access '{0}' is not supported", m.Member));
                            case "Milliseconds": datePart = DateParts.Millisecond; break;
                            case "Seconds": datePart = DateParts.Second; break;
                            case "Minutes": datePart = DateParts.Minute; break;
                            case "Hours": datePart = DateParts.Hour; break;
                            case "Days": datePart = DateParts.Day; break;
                            default: return m;
                        }

                        var ex = (BinaryExpression)expression;

                        var call = Expression.Call(
                                    DateDiffMethod,
                                    Expression.Constant(datePart),
                                     Expression.Convert(ex.Left, typeof(DateTime)),
                                    Expression.Convert(ex.Right, typeof(DateTime)));

                        return Visit(call);
                }
            }

            return m;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            u = base.VisitUnary(u) as UnaryExpression;
            switch (u.NodeType)
            {
                case ExpressionType.TypeAs:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    return Expression.Call(MethodRepository.GetConvertMethod(u.Operand.Type, u.Type), u.Operand);
            }
            return u;
        }

        protected override Expression VisitNew(NewExpression nex)
        {
            nex = base.VisitNew(nex) as NewExpression;
            var lambda = MethodMapping.ConvertMember(nex.Constructor);
            if (lambda != null)
            {
                var ef = lambda.Body.Unwrap();
                var parms = new Dictionary<string, ParameterMapping>(lambda.Parameters.Count);
                var pn = 0;

                foreach (var p in lambda.Parameters)
                    parms.Add(p.Name, new ParameterMapping(pn++, p.Type));

                return ef.Convert(wpi =>
                {
                    if (wpi.NodeType == ExpressionType.Parameter)
                    {
                        var pe = (ParameterExpression)wpi;
                        var n = parms[pe.Name];
                        var tmp = nex.Arguments[n.Index];
                        if (tmp.Type != n.Type)
                            return Expression.Convert(tmp, n.Type);
                        return tmp;
                    }

                    return wpi;
                });
            }

            return nex;
        }

        struct ParameterMapping
        {
            public ParameterMapping(int index, Type type)
            {
                Index = index;
                Type = type;
            }
            public int Index;
            public Type Type;
        }
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            m = base.VisitMethodCall(m) as MethodCallExpression;

            var lambda = MethodMapping.ConvertMember(m.Method);
            if (lambda != null)
            {
                var ef = lambda.Body.Unwrap();
                var parms = new Dictionary<string, ParameterMapping>(lambda.Parameters.Count);
                var pn = m.Method.IsStatic ? 0 : -1;

                foreach (var p in lambda.Parameters)
                    parms.Add(p.Name, new ParameterMapping(pn++, p.Type));

                var pie = ef.Convert(wpi =>
                {
                    if (wpi.NodeType == ExpressionType.Parameter)
                    {
                        ParameterMapping n;
                        if (parms.TryGetValue(((ParameterExpression)wpi).Name, out n))
                        {
                            var tmp = n.Index < 0 ? m.Object : m.Arguments[n.Index];
                            if (tmp.Type != n.Type)
                                return Expression.Convert(tmp, n.Type);
                            return tmp;
                        }
                    }

                    return wpi;
                });

                if (m.Method.ReturnType != pie.Type)
                    pie = Expression.Convert(pie, m.Method.ReturnType);

                return Visit(pie);
            }



            var methodName = m.Method.Name;
            var declaringType = m.Method.DeclaringType;

            if (declaringType == typeof(System.Convert))
            {
                Expression operand = null;
                Type toType = null;
                if (m.Method.Name.StartsWith("To"))
                {
                    toType = ClassLoader.Load("System." + m.Method.Name.Replace("To", ""));
                    operand = m.Arguments[0];
                }

                if (operand != null && toType != null && toType != Types.Object)
                    return Expression.Call(MethodRepository.GetConvertMethod(operand.Type, toType), operand);
            }

            if (typeof(TypeConverter).IsAssignableFrom(declaringType))
            {
                Expression operand = null;
                Type toType = null;

                if (methodName.StartsWith("ConvertFrom"))
                {
                    var c = m.Object as ConstantExpression;
                    Type converterType = null;
                    if (c != null)
                        converterType = (m.Object as ConstantExpression).Value.GetType();
                    else
                    {
                        var ma = m.Object as MemberExpression;
                        if (ma != null)
                        {
                            c = ma.Expression as ConstantExpression;
                            if (c == null)
                                throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));

                            converterType = ma.Member.GetGetter()(c.Value).GetType();
                        }
                    }
                    toType = ClassLoader.Load("System." + converterType.Name.Replace("Converter", ""));
                    if (toType == null)
                        throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));

                    if (methodName == "ConvertFrom" && m.Arguments.Count == 1)
                        operand = m.Arguments[0];
                    else if (methodName == "ConvertFromString" && m.Arguments.Count == 1)
                        operand = m.Arguments[0];
                    else if (methodName == "ConvertFromInvariantString" && m.Arguments.Count == 1)
                        operand = m.Arguments[0];

                }
                else if (methodName == "ConvertTo" && m.Arguments.Count == 2)
                {
                    operand = m.Arguments[0];
                    toType = (m.Arguments[1] as ConstantExpression).Value as Type;
                }
                else if (methodName == "ConvertToInvariantString" && m.Arguments.Count == 1)
                {
                    operand = m.Arguments[0];
                    toType = Types.String;
                }
                else if (methodName == "ConvertToString" && m.Arguments.Count == 1)
                {
                    operand = m.Arguments[0];
                    toType = Types.String;
                }

                if (operand != null && toType != null && toType != Types.Object)
                    return Expression.Call(MethodRepository.GetConvertMethod(operand.Type, toType), operand);
                throw new NotSupportedException(string.Format("The method '{0}' is not supported", methodName));
            }

            if (methodName == "Parse"
                   && m.Method.IsStatic
                   && (declaringType.IsValueType || declaringType.IsNullable())
                   && m.Arguments.Count == 1
                   && m.Method.ReturnType == m.Type)
            {
                Expression operand = m.Arguments[0];
                Type toType = declaringType.IsNullable() ? Nullable.GetUnderlyingType(declaringType) : declaringType;
                return Expression.Call(MethodRepository.GetConvertMethod(operand.Type, toType), operand);
            }
            if (declaringType == Types.String)
            {
                if (methodName == "Concat")
                    return BindConcat(m.Arguments.ToArray());
                if (methodName == "Join")
                    return BindJoin(m);
            }

            if (methodName == "ContainsValue" && IsSameOrParent(typeof(IDictionary<,>), declaringType))
            {
                var args = GetGenericArguments(declaringType, typeof(IDictionary<,>));
                var minf = EnumerableMethods
                    .First(s => s.Name == "Contains" && s.GetParameters().Length == 2)
                    .MakeGenericMethod(args[1]);

                return Expression.Call(
                    minf,
                    Expression.PropertyOrField(m.Object, "Values"),
                    m.Arguments[0]);
            }
            if (methodName == "ContainsKey" && IsSameOrParent(typeof(IDictionary<,>), declaringType))
            {
                var args = GetGenericArguments(declaringType, typeof(IDictionary<,>));
                var minf = EnumerableMethods
                    .First(s => s.Name == "Contains" && s.GetParameters().Length == 2)
                    .MakeGenericMethod(args[1]);

                return Expression.Call(
                    minf,
                    Expression.PropertyOrField(m.Object, "Keys"),
                    m.Arguments[0]);
            }


            if (declaringType.FullName == ULinq.StrSqlMethhodsType && declaringType.Assembly.GetName().Name == ULinq.StrAssemblyName)
            {
                ULinq.Init(declaringType.Assembly);
                return Visit(m);
            }
            //if (methodName == "Like" && declaringType == typeof(SqlFunctions) && m.Arguments.Count == 5)
            //{
            //    return BindLike(
            //        m.Arguments[0]
            //        , m.Arguments[1]
            //        , (bool)(m.Arguments[2] as ConstantExpression).Value
            //        , (bool)(m.Arguments[3] as ConstantExpression).Value
            //        , (bool)(m.Arguments[4] as ConstantExpression).Value);
            //}

            if (typeof(Queryable).IsAssignableFrom(declaringType) || typeof(Enumerable).IsAssignableFrom(declaringType))
            {
                var elementType = UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetElementType(m.Arguments[0].Type);
                switch (methodName)
                {
                    case "Contains":
                        EntityMapping mapping;
                        if (DbContext.dbConfiguration.mappings.TryGetValue(elementType.TypeHandle.Value, out mapping))
                        {
                            // Expression left = 
                        }
                        break;
                    //case "Aggregate":
                    //    {
                    //        var type = UWay.Skynet.Cloud.Reflection.TypeHelper.GetElementType(m.Arguments[0].Type);
                    //        if (type.IsNullable())
                    //            type = Nullable.GetUnderlyingType(type);
                    //        if (type.IsClass && type != Types.String)
                    //            throw new NotSupportedException("Not support 'Aggregate' function for complex type.");
                    //        break;
                    //    }
                    //case "ElementAt":
                    //    var index = m.Arguments[1];
                    //    var elementType = UWay.Skynet.Cloud.Reflection.TypeHelper.GetElementType(m.Arguments[0].Type);
                    //    var c = index as ConstantExpression;
                    //    if (c != null)
                    //    {
                    //        if((int)c.Value == 0)
                    //            return Expression.Call(typeof(Enumerable), "Take", new Type[] { elementType }, m.Arguments[0], Expression.Constant(1, Types.Int32));
                    //        index = Expression.Constant((int)c.Value + 1, Types.Int32);
                    //    }
                    //    else
                    //        index = Expression.Add(index, Expression.Constant(1, Types.Int32));
                    //    var s = Expression.Call(typeof(Enumerable), "Skip", new Type[] { elementType },m.Arguments[0], index);
                    //    return Expression.Call(typeof(Enumerable), "Take", new Type[] { elementType },s, Expression.Constant(1,Types.Int32));
                }
            }

            if (IsSameOrParent(typeof(IEnumerable<>), declaringType)
                && !declaringType.IsArray
                && m.Object != null
                && m.Object.NodeType == ExpressionType.Constant
                && !typeof(IQueryable).IsAssignableFrom(declaringType))
            {
                switch (methodName)
                {
                    case "Contains":
                        var elementType = UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetElementType(declaringType);
                        if (!DbContext.dbConfiguration.HasClass(elementType) && elementType != Types.Char)
                        {
                            var lst = (m.Object as ConstantExpression).Value as IEnumerable;
                            if (lst != null)
                            {
                                var items = lst.Cast<object>().ToArray();
                                var arry = Array.CreateInstance(elementType, items.Length);
                                for (int i = 0; i < items.Length; i++)
                                    arry.SetValue(items[i], i);
                                Expression array = Expression.Constant(arry);

                                var containsMethod = EnumerableMethods
                                    .First(s => s.Name == "Contains" && s.GetParameters().Length == 2)
                                    .MakeGenericMethod(elementType);
                                m = Expression.Call(containsMethod, array, m.Arguments[0]);
                            }
                        }
                        break;
                }


            }
            return m;
        }

        //private static Expression BindLike(Expression o, Expression a, bool hasStart, bool hasEnd, bool hasEscape)
        //{

        //    Expression arg = a;
        //    if (hasEscape)
        //    {
        //        //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("%"), Expression.Constant("~~"));
        //        //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("_"), Expression.Constant("~~"));
        //        //arg = new FunctionExpression(Types.String, FunctionNames.String.Replace, a, Expression.Constant("~"), Expression.Constant("~~"));
        //    }
        //    var c = a as ConstantExpression;
        //    if (c!= null && c.Value == null)
        //        a = Expression.Constant("NULL", Types.String);
        //    if (hasStart && hasEnd)
        //        return Expression.Call(MethodRepository.Like, o, Expression.Call(MethodRepository.Concat,Expression.NewArrayInit(Types.String, Expression.Constant("%"), a, Expression.Constant("%"))));
        //    if(hasStart)
        //        return Expression.Call(MethodRepository.Like, o, Expression.Call(MethodRepository.Concat,Expression.NewArrayInit(Types.String, a, Expression.Constant("%"))));
        //    if(hasEnd)
        //        return Expression.Call(MethodRepository.Like, o, Expression.Call(MethodRepository.Concat,Expression.NewArrayInit(Types.String, Expression.Constant("%"), a)));
        //    return Expression.Call(MethodRepository.Like, o, a);
        //}

        static string EscapeLikeText(string text)
        {
            if (text.IndexOfAny(new[] { '%', '_' }) < 0)
                return text;

            var builder = new StringBuilder(text.Length);

            foreach (var ch in text)
            {
                switch (ch)
                {
                    case '%':
                    case '_':
                    case '~':
                        builder.Append('~');
                        break;
                }

                builder.Append(ch);
            }

            return builder.ToString();
        }
        protected override Expression VisitBinary(BinaryExpression b)
        {
            b = base.VisitBinary(b) as BinaryExpression;

            switch (b.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    if (b.Left.Type.Equals(typeof(String)))
                        return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, b.Left, Expressor.ToString(b.Right)));
                    if (b.Right.Type.Equals(typeof(String)))
                        return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, Expressor.ToString(b.Left), b.Right));
                    return b;
                case ExpressionType.ArrayIndex:
                    if (b.Type == Types.Byte)
                    {

                    }
                    return b;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                default:
                    return b;
            }
        }

        private Expression BindJoin(MethodCallExpression m)
        {
            var separator = m.Arguments[0];
            var args = new List<Expression>();
            if (m.Arguments[1].NodeType == ExpressionType.NewArrayInit)
            {
                var items = ((NewArrayExpression)m.Arguments[1]).Expressions;
                for (int i = 0; i < items.Count; i++)
                {
                    args.Add(Expressor.ToString(items[i]));
                    if (i != items.Count - 1)
                        args.Add(separator);
                }
            }
            else if (m.Arguments[1].NodeType == ExpressionType.Constant)
            {
                var c = (m.Arguments[1] as ConstantExpression);
                var tmpItems = (c.Value as IEnumerable);
                if (Types.IEnumerable.IsAssignableFrom(c.Type) && c.Type != Types.String)
                {
                    if (tmpItems != null)
                    {
                        var items = tmpItems.Cast<object>().ToList();
                        for (int i = 0; i < items.Count; i++)
                        {
                            var value = items[i];
                            if (value != null)
                                args.Add(Expression.Constant(value.ToString()));
                            else
                                args.Add(Expression.Constant(null, Types.String));
                            if (i != items.Count - 1)
                                args.Add(separator);
                        }
                    }

                    if (args.Count == 0)
                        args.Add(Expression.Constant(null, Types.String));
                }
            }

            return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, args.ToArray()));
        }

        private Expression BindConcat(Expression[] args)
        {
            if (args.Length == 1)
            {
                var arg = args[0];
                if (arg.NodeType == ExpressionType.NewArrayInit)
                    args = ((NewArrayExpression)arg).Expressions.ToArray();
                else if (arg.NodeType == ExpressionType.MemberAccess)
                {
                    var m = arg as MemberExpression;
                    var c = m.Expression as ConstantExpression;
                    if (c != null && c.Value != null)
                    {
                        var v = m.Member.GetGetter()(c.Value);
                        if (v != null)
                        {
                            if (v is string)
                                args[0] = Expression.Constant(v, Types.String);
                            else if (v is IEnumerable)
                            {
                                var result = new List<Expression>();
                                foreach (var item in v as IEnumerable)
                                {
                                    if (item != null)
                                        result.Add(Expression.Constant(item.ToString(), Types.String));
                                    else
                                        result.Add(Expression.Constant(null, Types.String));
                                }
                                args = result.ToArray();
                            }
                            else
                                args[0] = Expression.Constant(v.ToString(), Types.String);
                        }
                    }
                }
            }
            var length = args.Length;

            for (int i = 0; i < length; i++)
                args[i] = Expressor.ToString(args[i]);
            return Expression.Call(MethodRepository.Concat, Expression.NewArrayInit(Types.String, args));
        }
    }
}
