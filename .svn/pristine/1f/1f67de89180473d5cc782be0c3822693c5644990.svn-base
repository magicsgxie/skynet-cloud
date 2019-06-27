using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{

    class SqlCeBuilder : DbSqlBuilder
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

        protected override void WriteAggregateName(string aggregateName)
        {
            if (aggregateName == "LongCount")
            {
                this.Append("COUNT");
            }
            else
            {
                base.WriteAggregateName(aggregateName);
            }
        }

        protected override void WriteTopClause(Expression expression)
        {
            this.Append("TOP (");
            this.Visit(expression);
            this.Append(") ");
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            if (select.Skip != null) // this should have been rewritten
                throw new InvalidOperationException(Res.SQLCESupportSkip);
            return base.VisitSelect(select);
        }

        #region Expression Parse

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

        #endregion


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
                    sb.Append("CONVERT(NVARCHAR(200),");
                    Visit(from);
                    sb.Append(",20)");
                    return;
                }
                if (fromType == Types.Guid)
                {
                    sb.Append("CAST(");
                    Visit(from);
                    sb.Append(" AS NVARCHAR(72))");
                    return;
                }
            }
            base.BuildConverterFunction(from, fromType, toType);
        }
    }

}
