using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    internal class AccessSqlBuilder : DbSqlBuilder
    {
        static Dictionary<DBType, IFunctionView> CastFunctions = new Dictionary<DBType, IFunctionView>();
        static AccessSqlBuilder()
        {
            CastFunctions[DBType.Boolean] = FunctionView.StandardSafe("CBool", 1);
            CastFunctions[DBType.Byte] = FunctionView.StandardSafe("CByte", 1);
            CastFunctions[DBType.Int16] = FunctionView.StandardSafe("CInt", 1);
            CastFunctions[DBType.Int32] = FunctionView.StandardSafe("CInt", 1);
            CastFunctions[DBType.Int64] = FunctionView.StandardSafe("CLng", 1);
            CastFunctions[DBType.Decimal] = FunctionView.StandardSafe("CCur", 1);//
            CastFunctions[DBType.Currency] = FunctionView.StandardSafe("CCur  ", 1);
            CastFunctions[DBType.Single] = FunctionView.StandardSafe("CSng ", 1);
            CastFunctions[DBType.Double] = FunctionView.StandardSafe("CDbl", 1);
            CastFunctions[DBType.DateTime] = FunctionView.StandardSafe("CDate", 1);
            CastFunctions[DBType.Guid] = FunctionView.StandardSafe("CStr", 1);
            CastFunctions[DBType.VarChar] = FunctionView.StandardSafe("CStr ", 1);
            CastFunctions[DBType.Char] = FunctionView.StandardSafe("CStr ", 1);
            CastFunctions[DBType.NVarChar] = FunctionView.StandardSafe("CStr ", 1);
            CastFunctions[DBType.NChar] = FunctionView.StandardSafe("CStr ", 1);
        }

        TypeNames typeNames = null;
        string GetVariableDeclaration(SqlType sqlType, bool suppressSize, int? length)
        {
            string result = null;

            if (typeNames == null)
                typeNames = new UWay.Skynet.Cloud.Data.Schema.Script.Generator.AccessScriptGenerator().typeNames;

            if (!suppressSize)
                result = typeNames.Get(sqlType.DbType);
            else if (sqlType.Length > 0 || sqlType.Precision > 0 || sqlType.Scale > 0)
                result = typeNames.Get(sqlType.DbType, sqlType.Length, sqlType.Precision, sqlType.Scale);
            else
                result = typeNames.Get(sqlType.DbType);

            if (result == null)
                throw new InvalidCastException(string.Format(Res.CastTypeInvalid, sqlType));

            return result;
        }

        protected internal virtual void FormatWithParameters(Expression expression)
        {
            var names = NamedValueGatherer.Gather(expression);
            if (names.Count > 0)
            {
                this.Append("PARAMETERS ");
                for (int i = 0, n = names.Count; i < n; i++)
                {
                    if (i > 0)
                        this.Append(", ");
                    this.AppendParameterName(names[i].Name);
                    this.Append(" ");
                    this.Append(GetVariableDeclaration(names[i].SqlType, true, null));
                }
                this.Append(";");
                this.AppendLine(Indentation.Same);
            }
        }

        protected override void AppendParameterName(string name)
        {
            this.Append(name);
        }


        protected override Expression VisitSelect(SelectExpression select)
        {
            if (select.Skip != null)
            {
                if (select.OrderBy == null && select.OrderBy.Count == 0)
                    throw new NotSupportedException(string.Format(Res.OperationNotSupported, "Access", "the 'skip'", " without explicit ordering"));
                else if (select.Take == null)
                    throw new NotSupportedException(string.Format(Res.OperationNotSupported, "Access", "the 'skip'", "without the 'take' operation"));
                else
                    throw new NotSupportedException(string.Format(Res.OperationNotSupported, "Access", "the 'skip'", " in this query"));

            }
            return base.VisitSelect(select);
        }

        protected override void WriteTopClause(Expression expression)
        {
            this.Append("TOP ");
            this.Append(expression);
            this.Append(" ");
        }

        protected override Expression VisitJoin(JoinExpression join)
        {
            if (join.Join == JoinType.CrossJoin)
            {
                this.VisitJoinLeft(join.Left);
                this.Append(", ");
                this.VisitJoinRight(join.Right);
                return join;
            }
            return base.VisitJoin(join);
        }

        protected override Expression VisitJoinLeft(Expression source)
        {
            if (source is JoinExpression)
            {
                this.Append("(");
                this.VisitSource(source);
                this.Append(")");
            }
            else
            {
                this.VisitSource(source);
            }
            return source;
        }

        protected override Expression VisitDeclaration(DeclarationCommand decl)
        {
            if (decl.Source != null)
            {
                this.Visit(decl.Source);
                return decl;
            }
            return base.VisitDeclaration(decl);
        }

        protected override void WriteColumns(System.Collections.ObjectModel.ReadOnlyCollection<ColumnDeclaration> columns)
        {
            if (columns.Count == 0)
                this.Append("0");
            else
                base.WriteColumns(columns);
        }
        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    string op = this.GetOperator(u);
                    if (IsBoolean(u.Operand.Type) || op.Length > 1)
                    {
                        return base.VisitUnary(u);
                    }
                    else
                    {
                        //NOT: -1-x
                        this.Append("(-1-");
                        this.VisitValue(u.Operand);
                        this.Append(")");
                    }
                    break;
                default:
                    return base.VisitUnary(u);
            }
            return u;
        }
        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Power)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append("^");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.Coalesce)
            {
                Append("IIF(");
                this.VisitValue(b.Left);
                Append(" IS NOT NULL, ");
                this.VisitValue(b.Left);
                Append(", ");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.LeftShift)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append(" * (2^");
                this.VisitValue(b.Right);
                Append("))");
                return b;
            }
            else if (b.NodeType == ExpressionType.RightShift)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append(@" \ (2^");
                this.VisitValue(b.Right);
                Append("))");
                return b;
            }
            else if (b.NodeType == ExpressionType.Divide) //changed:2013/1/11 Fix 除法的上下取整的Bug
            {
                Append("FIX(");
                this.VisitValue(b.Left);
                Append(" / ");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            return base.VisitBinary(b);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            Append("IIF(");
            VisitPredicate(c.Test);
            Append(", ");
            VisitValue(c.IfTrue);
            Append(", ");
            VisitValue(c.IfFalse);
            Append(")");
            return c;
        }

        protected override string GetOperator(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    if (b.Type == typeof(bool) || b.Type == typeof(bool?))
                        return "AND";
                    return "BAND";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Or:
                    if (b.Type == typeof(bool) || b.Type == typeof(bool?))
                        return "OR";
                    return "BOR";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Modulo:
                    return "MOD";
                case ExpressionType.ExclusiveOr:
                    return "XOR";
                case ExpressionType.Divide:
                    if (this.IsInteger(b.Type))
                        return "/"; // integer divide
                    goto default;
                default:
                    return base.GetOperator(b);
            }
        }

        protected override string GetOperator(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    return "NOT";
                default:
                    return base.GetOperator(u);
            }
        }

        private bool hasEnterBoolConvert;
        protected override void BuildConverterFunction(Expression from, Type fromType, Type toType)
        {
            if (fromType == Types.Boolean && !hasEnterBoolConvert)
            {
                //Access 的bool值的True=-1， False=0,所以需要首先把bool转化为整形然后取绝对值，这样bool的True才能转化为1
                hasEnterBoolConvert = true;
                from = Expression.Call(typeof(Math).GetMethod("Abs", new Type[] { Types.Int32 }), Expression.Call(MethodRepository.GetConvertMethod(Types.Boolean, Types.Int32), from));
                ConvertTo(from, toType);
                hasEnterBoolConvert = false;
                return;
            }

            if (toType == Types.String)
            {
                //if (fromType == Types.Guid)
                //{
                //    Visit(from);
                //    return;
                //}
                if (fromType == Types.DateTime)
                {
                    sb.Append("FORMAT(");
                    Visit(from);
                    sb.Append(",'yyyy-MM-dd HH:mm:ss')");
                    return;
                }
            }
            if (fromType == Types.String)
            {
                if (toType == Types.Guid)
                {
                    from = Expression.Call(MethodRepository.ToLower, from);
                    Visit(from);
                    return;
                }
            }

            DBType code;
            if (!SqlType.TypeMap.TryGetValue(toType.TypeHandle.Value.GetHashCode(), out code))
                throw new InvalidCastException(string.Format("Cann't convert from '{0}' to db type.", fromType.FullName));

            var fnView = CastFunctions[code];
            fnView.Render(this, from);
        }

    }
}
