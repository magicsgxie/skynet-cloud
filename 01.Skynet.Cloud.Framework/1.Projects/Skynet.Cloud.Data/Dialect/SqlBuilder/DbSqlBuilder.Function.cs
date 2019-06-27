using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    partial class DbSqlBuilder
    {
        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            var type = m.Member.DeclaringType;
            if (type.IsNullable())
            {
                return Visit(m.Expression);
                //return m;
            }
            var member = /*type.IsNullable() ? (m.Expression as MemberExpression).Member :*/ m.Member;
            var memberName = member.Name;

            var key = member.Name;
            IFunctionView fn;
            if (FuncRegistry.TryGetFunction(key, out fn))
            {
                fn.Render(this, m.Expression);
                return m;
            }

            throw new NotSupportedException(string.Format("The member access '{0}' is not supported", m.Member));
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            var methodName = m.Method.Name;
            var declaringType = m.Method.DeclaringType;

            if (declaringType == typeof(SqlFunctions) && methodName == "Between")
            {
                var between = new BetweenExpression(m.Arguments[0], m.Arguments[1], m.Arguments[2]);
                return VisitBetween(between);
            }

            var key = methodName;
            IFunctionView fn;
            if (FuncRegistry.TryGetFunction(key, out fn))
            {
                if (key == "Concat")
                    fn.Render(this, (m.Arguments[0] as NewArrayExpression).Expressions.ToArray());
                else
                {
                    if (m.Method.IsStatic)
                        fn.Render(this, m.Arguments.ToArray());
                    else
                    {
                        var args = new List<Expression>();
                        args.Add(m.Object);
                        args.AddRange(m.Arguments);
                        fn.Render(this, args.ToArray());
                    }
                }
                return m;
            }

            if (methodName == "CompareString"
                       && declaringType.FullName == "Microsoft.VisualBasic.CompilerServices.Operators")
            {
                FuncRegistry.SqlFunctions[FunctionType.Compare].Render(this, m.Arguments.ToArray());
                return m;
            }

            if (methodName == "Compare" && m.Arguments.Count > 1
                && m.Method.ReturnType == typeof(int))
            {
                FuncRegistry.SqlFunctions[FunctionType.Compare].Render(this, m.Arguments.ToArray());
                return m;
            }
            if (methodName == "CompareTo" && m.Arguments.Count == 1
                     && m.Method.ReturnType == typeof(int))
            {
                FuncRegistry.SqlFunctions[FunctionType.Compare].Render(this, m.Object, m.Arguments[0]);
                return m;
            }

            if (methodName == "Equals")
            {
                var a = m.Arguments[0];
                var b = m.Method.IsStatic ? m.Arguments[1] : m.Object;

                a = a.NodeType == ExpressionType.Convert ? (a as UnaryExpression).Operand : a;
                b = b.NodeType == ExpressionType.Convert ? (b as UnaryExpression).Operand : b;

                if (a.NodeType == ExpressionType.Constant)
                    FunctionView.Equal.Render(this, b, a);
                else
                    FunctionView.Equal.Render(this, a, b);
                return m;
            }
            if (methodName == "ToString" && !m.Method.IsStatic && m.Arguments.Count == 0)
            {
                ConvertTo(m.Object, Types.String);
                return m;
            }
            if (methodName == "Convert" && declaringType == typeof(SqlFunctions))
            {
                if (m.Method.IsGenericMethod)
                    ConvertTo(m.Arguments[0], m.Type);
                else
                    ConvertTo(m.Arguments[0], (m.Arguments[1] as ConstantExpression).Value as Type);
                return m;
            }
            if (declaringType == Types.Decimal)
            {
                switch (methodName)
                {
                    case "Add":
                        sb.Append("(");
                        Visit(m.Arguments[0]);
                        sb.Append("+");
                        Visit(m.Arguments[1]);
                        sb.Append(")");
                        return m;
                    case "Subtract":
                        sb.Append("(");
                        Visit(m.Arguments[0]);
                        sb.Append("-");
                        Visit(m.Arguments[1]);
                        sb.Append(")");
                        return m;
                    case "Multiply":
                        sb.Append("(");
                        Visit(m.Arguments[0]);
                        sb.Append("*");
                        Visit(m.Arguments[1]);
                        sb.Append(")");
                        return m;
                    case "Divide":
                        sb.Append("(");
                        Visit(m.Arguments[0]);
                        sb.Append("/");
                        Visit(m.Arguments[1]);
                        sb.Append(")");
                        return m;
                    case "Remainder":
                        FuncRegistry.SqlFunctions["Decimal." + methodName].Render(this, m.Arguments.ToArray());
                        return m;
                    case "Negate":
                        sb.Append("-");
                        Visit(m.Arguments[0]);
                        return m;
                }
            }
            if (declaringType == Types.DateTime)
            {
                switch (methodName)
                {
                    case "Add":
                        FuncRegistry.SqlFunctions[FunctionType.DateTime.DateAdd].Render(this, Expression.Constant(DateParts.TimeSpan), m.Object, m.Arguments[0]);
                        return m;
                    case "Subtract":
                        if (m.Arguments[0].Type == Types.TimeSpan)
                        {
                            FuncRegistry.SqlFunctions[FunctionType.DateTime.DateAdd].Render(this, Expression.Constant(DateParts.TimeSpan), m.Object, m.Arguments[0], Expression.Constant(false));
                            return m;
                        }
                        break;
                }
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m));

        }

        //bug: Char 类型处理  http://iqtoolkit.codeplex.com/discussions/208764
        protected virtual void ConvertTo(Expression from, Type toType)
        {
            //解决字符串Reverse函数解析
            if (from.NodeType == ExpressionType.Call && Types.IEnumerableofT.MakeGenericType(Types.Char).IsAssignableFrom(from.Type))
            {
                Visit(from);
                return;
            }

            while (ExpressionType.Convert == from.NodeType
                  && typeof(object) == from.Type)
                from = ((UnaryExpression)from).Operand;

            if (toType == Types.Object)
                toType = from.Type;

            var fType = from.Type;
            fType = fType.IsNullable() ? Nullable.GetUnderlyingType(fType) : fType;
            toType = toType.IsNullable() ? Nullable.GetUnderlyingType(toType) : toType;

            if (fType == toType)
            {
                Visit(from);
                return;
            }

            bool fIsInterger = false, tIsInterger = false, fIsFloat = false, tIsFloat = false;
            var fCode = Type.GetTypeCode(fType);
            var tCode = Type.GetTypeCode(toType);

            switch (fCode)
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    fIsInterger = true;
                    break;
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    fIsInterger = true;
                    fIsFloat = true;
                    break;
            }

            switch (tCode)
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    tIsInterger = true;
                    break;
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    tIsInterger = true;
                    tIsFloat = true;
                    break;
            }

            if (fIsInterger && tIsInterger)
            {
                if (!fIsFloat)
                {
                    if (tIsFloat)
                        Visit(from);//TODO: int->float
                    else
                        Visit(from);
                }
                else
                {
                    if (tIsFloat)
                        Visit(from);
                    else
                        FuncRegistry.SqlFunctions[FunctionType.Math.Floor].Render(this, from);
                }
                return;
            }


            if (toType == Types.String)
            {
                if (fType == Types.Char)
                {
                    Visit(from);
                    return;
                }
                if (typeof(IEnumerable<char>).IsAssignableFrom(fType) && from.NodeType == ExpressionType.Convert)
                {
                    var reverse = (from as UnaryExpression).Operand as MethodCallExpression;
                    if (reverse != null)
                    {
                        FuncRegistry.SqlFunctions[FunctionType.String.Reverse].Render(this, reverse.Arguments[0]);
                        return;
                    }
                }
            }
            if (fType == Types.String && toType == Types.Boolean
                && from.NodeType == ExpressionType.Constant)
            {
                string v = (from as ConstantExpression).Value as string;
                if (v != null)
                {
                    v = v.ToLower();
                    switch (v)
                    {
                        case "true":
                        case "t":
                        case "yes":
                        case "y":
                            from = Expression.Constant("1", Types.String);
                            break;
                        case "false":
                        case "f":
                        case "n":
                            from = Expression.Constant("0", Types.String);
                            break;
                    }
                }
            }
            BuildConverterFunction(from, fType, toType);
        }

        protected virtual void BuildConverterFunction(Expression from, Type fromType, Type toType)
        {
            var sqlType = GetDbTypeName(toType);

            Append("CAST(");
            Visit(from);
            Append(" AS ");
            Append(sqlType.ToString());
            Append(")");
        }

        protected virtual string GetDbTypeName(Type type)
        {
            DBType code;
            var t = type.IsNullable() ? Nullable.GetUnderlyingType(type) : type;
            if (!SqlType.TypeMap.TryGetValue(t.TypeHandle.Value.GetHashCode(), out code))
                throw new InvalidCastException(string.Format("Cann't convert from '{0}' to db type.", t.FullName));
            var dbType = castTypeNames.Get(code);
            if (dbType.IsNullOrEmpty())
                throw new InvalidCastException(string.Format("Cann't convert from '{0}' to db type.", t.FullName));
            return dbType;
        }

        protected virtual bool IsInteger(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        protected override Expression VisitNew(NewExpression m)
        {
            //var key = m.Constructor.DeclaringType.Name + ".New";
            //IFunctionView fn;
            //if (Dialect.TryGetFunction(key, out fn))
            //{
            //    fn.Render(this, m.Arguments.ToArray());
            //    return m;
            //}
            throw new NotSupportedException(string.Format("The construtor for '{0}' is not supported", m.Constructor.DeclaringType));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            string op = this.GetOperator(u);
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    if (IsBoolean(u.Operand.Type) || op.Length > 1)
                    {
                        if (u.Operand.NodeType == (ExpressionType)DbExpressionType.IsNull)
                        {
                            var isNull = u.Operand as IsNullExpression;
                            isNull.IsNot = true;
                            VisitIsNull(isNull);
                            return u;
                        }

                        var m = u.Operand as MethodCallExpression;
                        if (m != null)
                        {
                            if (m.Method.Name == "IsNullOrEmpty")
                            {
                                FunctionView.IsNullOrEmpty.Render(this, m.Arguments[0], Expression.Constant(true));
                                return u;
                            }
                            if (m.Method.Name == "IsNullOrWhiteSpace")
                            {
                                FunctionView.IsNullOrEmpty.Render(this, m.Arguments[0], Expression.Constant(true));
                                return u;
                            }
                        }
                        Append(op);
                        Append(" ");
                        VisitPredicate(u.Operand);
                    }
                    else
                    {
                        Append(op);
                        VisitValue(u.Operand);
                    }
                    break;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    Append(op);
                    VisitValue(u.Operand);
                    break;
                case ExpressionType.UnaryPlus:
                    VisitValue(u.Operand);
                    break;
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                //case ExpressionType.Unbox:
                case ExpressionType.TypeAs://:TypeAs 运算符支持引用类型不支持简单类型，因此忽略解析
                    ConvertTo(u.Operand, u.Type);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            string op = this.GetOperator(b);
            Expression left = b.Left;
            Expression right = b.Right;
            var leftType = left.Type;
            ConstantExpression c = b.Right as ConstantExpression;
            if (leftType == Types.Boolean || leftType == typeof(bool?)) // 确保左边是布尔表达式
            {
                if (c != null)// 右边是常量表达式
                {
                    if (c.Value == null)// 常量值是Null
                        return Visit(new IsNullExpression(left));

                    // 非Null
                    var value = c.Value;
                    var vType = value.GetType();

                    if (vType == Types.Boolean)
                        return (bool)value ? Visit(left) : Visit(Expression.Not(left));

                    if (vType == typeof(bool?))
                    {
                        var v = value as bool?;
                        if (v.HasValue)
                            return v.Value ? Visit(left) : Visit(Expression.Not(left));
                        return Visit(new IsNullExpression(left));
                    }
                }
            }


            Append("(");
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    if (this.IsBoolean(left.Type))
                    {
                        this.VisitPredicate(left);
                        Append(" ");
                        Append(op);
                        Append(" ");
                        VisitPredicate(right);
                    }
                    else
                    {
                        VisitValue(left);
                        Append(" ");
                        Append(op);
                        Append(" ");
                        VisitValue(right);
                    }
                    break;
                case ExpressionType.Equal:
                    if (right.NodeType == ExpressionType.Constant || left.NodeType == ExpressionType.Constant)
                    {
                        FunctionView.Equal.Render(this, left, right);
                        break;
                    }
                    goto case ExpressionType.LessThan;
                case ExpressionType.NotEqual:
                    if (right.NodeType == ExpressionType.Constant || left.NodeType == ExpressionType.Constant)
                    {
                        FunctionView.NotEqual.Render(this, left, right);
                        break;
                    }
                    goto case ExpressionType.LessThan;
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    if (right.NodeType == ExpressionType.Constant)
                    {
                        if (left.NodeType == ExpressionType.Call)
                        {
                            MethodCallExpression mc = (MethodCallExpression)left;
                            ConstantExpression ce = (ConstantExpression)right;
                            if (ce.Value != null && ce.Value.GetType() == typeof(int) && ((int)ce.Value) == 0)
                            {
                                if (mc.Method.Name == "CompareTo" && !mc.Method.IsStatic && mc.Arguments.Count == 1)
                                {
                                    left = mc.Object;
                                    right = mc.Arguments[0];
                                }
                                else if ((mc.Method.DeclaringType == typeof(string) || mc.Method.DeclaringType == typeof(decimal))
                                      && mc.Method.Name == "Compare" && mc.Method.IsStatic && mc.Arguments.Count == 2)
                                {
                                    left = mc.Arguments[0];
                                    right = mc.Arguments[1];
                                }
                            }
                        }
                    }
                    goto case ExpressionType.Add;
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                    this.VisitValue(left);
                    Append(" ");
                    Append(op);
                    Append(" ");
                    this.VisitValue(right);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            Append(")");
            return b;
        }

        protected virtual string GetOperator(string methodName)
        {
            switch (methodName)
            {
                case "Add": return "+";
                case "Subtract": return "-";
                case "Multiply": return "*";
                case "Divide": return "/";
                case "Negate": return "-";
                case "Remainder": return "%";
                default: return null;
            }
        }

        protected virtual string GetOperator(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return "-";
                case ExpressionType.UnaryPlus:
                    return "+";
                case ExpressionType.Not:
                    return IsBoolean(u.Operand.Type) ? "NOT" : "~";
                default:
                    return "";
            }
        }

        protected virtual string GetOperator(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return (IsBoolean(b.Left.Type)) ? "AND" : "&";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return (IsBoolean(b.Left.Type) ? "OR" : "|");
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.LeftShift:
                    return "<<";
                case ExpressionType.RightShift:
                    return ">>";
                default:
                    return "";
            }
        }

        protected virtual bool IsBoolean(Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }

        protected virtual bool IsPredicate(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return IsBoolean(((BinaryExpression)expr).Type);
                case ExpressionType.Not:
                    return IsBoolean(((UnaryExpression)expr).Type);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case (ExpressionType)DbExpressionType.IsNull:
                case (ExpressionType)DbExpressionType.Between:
                case (ExpressionType)DbExpressionType.Exists:
                case (ExpressionType)DbExpressionType.In:
                    return true;
                case ExpressionType.Call:
                    return IsBoolean(((MethodCallExpression)expr).Type);
                case ExpressionType.MemberAccess:
                    var m = expr as MemberExpression;
                    // var member = m.Member.DeclaringType.IsNullable() ? (m.Expression as MemberExpression).Member : m.Member;
                    var memberType = m.Member.DeclaringType;
                    if (memberType.IsNullable())
                    {
                        if (m.Member.Name == "HasValue")
                            return true;
                        memberType = Nullable.GetUnderlyingType(memberType);
                        return memberType == Types.Boolean;
                    }
                    return false;
                default:
                    return false;
            }
        }

        protected virtual Expression VisitPredicate(Expression expr)
        {
            var c = expr as ConstantExpression;
            if (c != null && c.Value != null)
            {
                if ((bool)c.Value)
                    Append(" 1=1 ");
                else
                    Append(" 1<>1 ");
                return expr;
            }
            var expr2 = this.Visit(expr);
            if (!IsPredicate(expr2))
                this.Append(" <> 0");
            switch (expr2.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.TypeAs:
                    this.Append(" <> 0");
                    return expr2;
                case ExpressionType.Call:
                    var m = expr2 as MethodCallExpression;
                    if (m.Method.Name == "Convert" && m.Method.DeclaringType == typeof(SqlFunctions))
                        this.Append(" <> 0");
                    break;
            }
            return expr2;
        }

        protected virtual Expression VisitValue(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            throw new NotSupportedException(string.Format("Conditional expressions not supported"));
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            WriteValue(c.Value);
            return c;
        }

        protected virtual void WriteValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                Append("NULL");
                return;
            }

            var type = value.GetType();
            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
                value = value.GetProperty("Value");
            }
            if (type.IsEnum)
            {
                value = Convert.ChangeType(value, Enum.GetUnderlyingType(type));
                type = value.GetType();
            }

            var code = Type.GetTypeCode(value.GetType());
            if (code == TypeCode.Object)
                throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", value));

            switch (code)
            {
                case TypeCode.Boolean:
                    WriteBoolean(value);
                    break;
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.SByte:
                    sb.Append(value);
                    break;
                case TypeCode.Single:
                case TypeCode.Decimal:
                case TypeCode.Double:
                    WriteNumberic(value);
                    break;
                case TypeCode.Char:
                case TypeCode.String:
                    sb.Append('\'')
                    .Append(value.ToString().Replace("'", "''"))
                    .Append('\'');
                    break;
                case TypeCode.DateTime:
                    WriteDateTime(value);
                    break;
                default:
                    if (value is Guid)
                        WriteGuid(value);
                    else if (value is byte[])
                        sb.Append(ByteArrayToBinaryString(value as byte[]));
                    else if (value is DateTimeOffset)
                        WriteDateTime(value);
                    else
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", value));
                    break;
            }
        }

        #region NumberFormate
        static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo
        {
            CurrencyDecimalDigits = NumberFormatInfo.InvariantInfo.CurrencyDecimalDigits,
            CurrencyDecimalSeparator = NumberFormatInfo.InvariantInfo.CurrencyDecimalSeparator,
            CurrencyGroupSeparator = NumberFormatInfo.InvariantInfo.CurrencyGroupSeparator,
            CurrencyGroupSizes = NumberFormatInfo.InvariantInfo.CurrencyGroupSizes,
            CurrencyNegativePattern = NumberFormatInfo.InvariantInfo.CurrencyNegativePattern,
            CurrencyPositivePattern = NumberFormatInfo.InvariantInfo.CurrencyPositivePattern,
            CurrencySymbol = NumberFormatInfo.InvariantInfo.CurrencySymbol,
            NaNSymbol = NumberFormatInfo.InvariantInfo.NaNSymbol,
            NegativeInfinitySymbol = NumberFormatInfo.InvariantInfo.NegativeInfinitySymbol,
            NegativeSign = NumberFormatInfo.InvariantInfo.NegativeSign,
            NumberDecimalDigits = NumberFormatInfo.InvariantInfo.NumberDecimalDigits,
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = NumberFormatInfo.InvariantInfo.NumberGroupSeparator,
            NumberGroupSizes = NumberFormatInfo.InvariantInfo.NumberGroupSizes,
            NumberNegativePattern = NumberFormatInfo.InvariantInfo.NumberNegativePattern,
            PercentDecimalDigits = NumberFormatInfo.InvariantInfo.PercentDecimalDigits,
            PercentDecimalSeparator = ".",
            PercentGroupSeparator = NumberFormatInfo.InvariantInfo.PercentGroupSeparator,
            PercentGroupSizes = NumberFormatInfo.InvariantInfo.PercentGroupSizes,
            PercentNegativePattern = NumberFormatInfo.InvariantInfo.PercentNegativePattern,
            PercentPositivePattern = NumberFormatInfo.InvariantInfo.PercentPositivePattern,
            PercentSymbol = NumberFormatInfo.InvariantInfo.PercentSymbol,
            PerMilleSymbol = NumberFormatInfo.InvariantInfo.PerMilleSymbol,
            PositiveInfinitySymbol = NumberFormatInfo.InvariantInfo.PositiveInfinitySymbol,
            PositiveSign = NumberFormatInfo.InvariantInfo.PositiveSign,
        };
        #endregion

        protected virtual void WriteNumberic(object value)
        {
            var str = value.ToString().ToString(NumberFormat);
            if (!str.Contains('.'))
                str += ".0";
            sb.Append(str);
        }

        protected virtual void WriteBoolean(object value)
        {
            var b = (bool)value;
            sb.Append(b ? "1" : "0");
        }

        protected virtual void WriteDateTime(object value)
        {
            sb.Append(string.Format("'{0:yyyy-MM-dd HH:mm:ss.fff}'", value));
        }
        protected virtual void WriteGuid(object value)
        {
            sb.Append('\'')
                   .Append(value)
                   .Append('\'');
        }

    }
}
