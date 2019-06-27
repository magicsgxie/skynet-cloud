using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    internal class MsSql2000SqlBuilder : DbSqlBuilder
    {
        protected override void RegisterCastTypes()
        {
            RegisterCastType(DBType.Binary, "BINARY");
            RegisterCastType(DBType.Boolean, "BIT");
            RegisterCastType(DBType.Byte, "TINYINT");
            RegisterCastType(DBType.Char, "VARCHAR");
            RegisterCastType(DBType.Currency, "MONEY");
            RegisterCastType(DBType.DateTime, "DATETIME");
            RegisterCastType(DBType.Double, "FLOAT");
            RegisterCastType(DBType.Decimal, "DECIMAL(19,5)");
            RegisterCastType(DBType.Int16, "SMALLINT");
            RegisterCastType(DBType.Int32, "INT");
            RegisterCastType(DBType.Int64, "BIGINT");
            RegisterCastType(DBType.Guid, "UNIQUEIDENTIFIER");
            RegisterCastType(DBType.Image, "IMAGE");
            RegisterCastType(DBType.NVarChar, "NVARCHAR");
            RegisterCastType(DBType.NChar, "NVARCHAR");
            RegisterCastType(DBType.NText, "NText");
            RegisterCastType(DBType.Single, "REAL");
            RegisterCastType(DBType.Text, "TEXT");
            //RegisterCastType(DBType.Timestamp, "Timestamp");
            RegisterCastType(DBType.VarChar, "VARCHAR");
        }

        protected override void WriteTopClause(Expression expression)
        {
            this.Append("TOP ");
            this.Visit(expression);
            this.Append(" ");
        }
        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Power)
            {
                Append("POWER(");
                this.VisitValue(b.Left);
                Append(", ");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.Coalesce)
            {
                Append("COALESCE(");
                this.VisitValue(b.Left);
                Append(", ");
                Expression right = b.Right;
                while (right.NodeType == ExpressionType.Coalesce)
                {
                    BinaryExpression rb = (BinaryExpression)right;
                    this.VisitValue(rb.Left);
                    Append(", ");
                    right = rb.Right;
                }
                this.VisitValue(right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.LeftShift)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append(" * POWER(2, ");
                this.VisitValue(b.Right);
                Append("))");
                return b;
            }
            else if (b.NodeType == ExpressionType.RightShift)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append(" / POWER(2, ");
                this.VisitValue(b.Right);
                Append("))");
                return b;
            }
            return base.VisitBinary(b);
        }

        protected override Expression VisitValue(Expression expr)
        {
            if (IsPredicate(expr))
            {
                Append("CASE WHEN (");
                Visit(expr);
                Append(") THEN 1 ELSE 0 END");
                return expr;
            }
            return base.VisitValue(expr);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            if (this.IsPredicate(c.Test))
            {
                Append("(CASE WHEN ");
                this.VisitPredicate(c.Test);
                Append(" THEN ");
                this.VisitValue(c.IfTrue);
                Expression ifFalse = c.IfFalse;
                while (ifFalse != null && ifFalse.NodeType == ExpressionType.Conditional)
                {
                    ConditionalExpression fc = (ConditionalExpression)ifFalse;
                    Append(" WHEN ");
                    this.VisitPredicate(fc.Test);
                    Append(" THEN ");
                    this.VisitValue(fc.IfTrue);
                    ifFalse = fc.IfFalse;
                }
                if (ifFalse != null)
                {
                    Append(" ELSE ");
                    this.VisitValue(ifFalse);
                }
                Append(" END)");
            }
            else
            {
                Append("(CASE ");
                VisitValue(c.Test);
                Append(" WHEN 0 THEN ");
                this.VisitValue(c.IfFalse);
                Append(" ELSE ");
                this.VisitValue(c.IfTrue);
                Append(" END)");
            }
            return c;
        }

        protected override Expression VisitIf(IFCommand ifx)
        {
            if (!this.Dialect.SupportMultipleCommands)
            {
                return base.VisitIf(ifx);
            }
            this.Append("IF ");
            this.Visit(ifx.Check);
            this.AppendLine(Indentation.Same);
            this.Append("BEGIN");
            this.AppendLine(Indentation.Inner);
            this.VisitStatement(ifx.IfTrue);
            this.AppendLine(Indentation.Outer);
            if (ifx.IfFalse != null)
            {
                this.Append("END ELSE BEGIN");
                this.AppendLine(Indentation.Inner);
                this.VisitStatement(ifx.IfFalse);
                this.AppendLine(Indentation.Outer);
            }
            this.Append("END");
            return ifx;
        }

        protected override Expression VisitBlock(BlockCommand block)
        {
            if (!this.Dialect.SupportMultipleCommands)
            {
                return base.VisitBlock(block);
            }

            for (int i = 0, n = block.Commands.Count; i < n; i++)
            {
                if (i > 0)
                {
                    this.AppendLine(Indentation.Same);
                    this.AppendLine(Indentation.Same);
                }
                this.VisitStatement(block.Commands[i]);
            }
            return block;
        }

        TypeNames typeNames = null;
        string GetVariableDeclaration(SqlType sqlType, bool suppressSize, int? length)
        {
            string result = null;

            if (typeNames == null)
                typeNames = new UWay.Skynet.Cloud.Data.Schema.Script.Generator.SqlServerScriptGenerator().typeNames;

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


        protected override Expression VisitDeclaration(DeclarationCommand decl)
        {
            if (!this.Dialect.SupportMultipleCommands)
            {
                return base.VisitDeclaration(decl);
            }

            for (int i = 0, n = decl.Variables.Count; i < n; i++)
            {
                var v = decl.Variables[i];
                if (i > 0)
                    this.AppendLine(Indentation.Same);
                this.Append("DECLARE @");
                this.Append(v.Name);
                this.Append(" ");
                this.Append(this.GetVariableDeclaration(v.SqlType, false, null));
            }
            if (decl.Source != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("SELECT ");
                for (int i = 0, n = decl.Variables.Count; i < n; i++)
                {
                    if (i > 0)
                        this.Append(", ");
                    this.Append("@");
                    this.Append(decl.Variables[i].Name);
                    this.Append(" = ");
                    this.Visit(decl.Source.Columns[i].Expression);
                }
                if (decl.Source.From != null)
                {
                    this.AppendLine(Indentation.Same);
                    this.Append("FROM ");
                    this.VisitSource(decl.Source.From);
                }
                if (decl.Source.Where != null)
                {
                    this.AppendLine(Indentation.Same);
                    this.Append("WHERE ");
                    this.Visit(decl.Source.Where);
                }
            }
            else
            {
                for (int i = 0, n = decl.Variables.Count; i < n; i++)
                {
                    var v = decl.Variables[i];
                    if (v.Expression != null)
                    {
                        this.AppendLine(Indentation.Same);
                        this.Append("SET @");
                        this.Append(v.Name);
                        this.Append(" = ");
                        this.Visit(v.Expression);
                    }
                }
            }
            return decl;
        }

        private bool hasEnterBoolConvert;
        protected override void BuildConverterFunction(Expression from, Type fromType, Type toType)
        {
            if (toType == Types.Boolean && !hasEnterBoolConvert)
            {
                //把有符号数转换为无符号数，然后进行转换
                if (fromType == Types.SByte
                    || fromType == Types.Int16
                    || fromType == Types.Int32
                    || fromType == Types.Int64)
                {
                    hasEnterBoolConvert = true;
                    from = Expression.Call(typeof(Math).GetMethod("Abs", new Type[] { fromType }), from);
                    ConvertTo(from, toType);
                    hasEnterBoolConvert = false;
                    return;
                }
                if (fromType == Types.String)
                {
                    hasEnterBoolConvert = true;
                    from = Expression.Call(typeof(Math).GetMethod("Abs", new Type[] { Types.Int32 })
                        , Expression.Call(MethodRepository.GetConvertMethod(Types.String, Types.Int32), from));
                    ConvertTo(from, toType);
                    hasEnterBoolConvert = false;
                    return;
                }

            }
            if (toType == Types.DateTime)
            {
                sb.Append("CONVERT(DATETIME,");
                Visit(from);
                sb.Append(",20)");
                return;
            }
            if (toType == Types.String)
            {
                if (fromType == Types.DateTime)
                {
                    //参考：http://www.cnblogs.com/liushanshan/archive/2011/06/30/2094387.html
                    sb.Append("CONVERT(VARCHAR(100),");
                    Visit(from);
                    sb.Append(",20)");
                    return;
                }
                if (fromType == Types.Guid)
                {
                    sb.Append("CAST(");
                    Visit(from);
                    sb.Append(" AS VARCHAR(40))");
                    return;
                }
            }
            base.BuildConverterFunction(from, fromType, toType);
        }
    }
}
