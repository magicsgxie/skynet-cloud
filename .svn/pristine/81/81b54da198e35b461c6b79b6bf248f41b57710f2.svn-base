using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    class SQLiteSqlBuilder : DbSqlBuilder
    {
        protected override void RegisterCastTypes()
        {
            RegisterCastType(DBType.VarChar, "TEXT");
            RegisterCastType(DBType.Char, "TEXT");
            RegisterCastType(DBType.NVarChar, "TEXT");
            RegisterCastType(DBType.NChar, "TEXT");
            RegisterCastType(DBType.Binary, "BLOB");
            RegisterCastType(DBType.Boolean, "NUMBER");
            RegisterCastType(DBType.Byte, "INTEGER");
            RegisterCastType(DBType.Single, "NUMERIC");
            RegisterCastType(DBType.Double, "NUMERIC");
            RegisterCastType(DBType.Decimal, "NUMBER");
            RegisterCastType(DBType.Int16, "INTEGER");
            RegisterCastType(DBType.Int32, "INTEGER");
            RegisterCastType(DBType.Int64, "INTEGER");
            RegisterCastType(DBType.Guid, "UNIQUEIDENTIFIER");
            RegisterCastType(DBType.DateTime, "DATETIME");
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            this.AddAliases(select.From);
            this.Append("SELECT ");
            if (select.IsDistinct)
            {
                this.Append("DISTINCT ");
            }
            this.WriteColumns(select.Columns);
            if (select.From != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("FROM ");
                this.VisitSource(select.From);
            }
            if (select.Where != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("WHERE ");
                this.VisitPredicate(select.Where);
            }
            if (select.GroupBy != null && select.GroupBy.Count > 0)
            {
                this.AppendLine(Indentation.Same);
                this.Append("GROUP BY ");
                for (int i = 0, n = select.GroupBy.Count; i < n; i++)
                {
                    if (i > 0)
                    {
                        this.Append(", ");
                    }
                    this.VisitValue(select.GroupBy[i]);
                }
            }
            if (select.OrderBy != null && select.OrderBy.Count > 0)
            {
                this.AppendLine(Indentation.Same);
                this.Append("ORDER BY ");
                for (int i = 0, n = select.OrderBy.Count; i < n; i++)
                {
                    OrderExpression exp = select.OrderBy[i];
                    if (i > 0)
                    {
                        this.Append(", ");
                    }
                    this.VisitValue(exp.Expression);
                    if (exp.OrderType != OrderType.Ascending)
                    {
                        this.Append(" DESC");
                    }
                }
            }
            if (select.Take != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("LIMIT ");
                if (select.Skip == null)
                {
                    this.Append("0");
                }
                else
                {
                    this.Append(select.Skip);
                }
                this.Append(", ");
                this.Visit(select.Take);
            }
            return select;
        }

        protected override void WriteColumns(System.Collections.ObjectModel.ReadOnlyCollection<ColumnDeclaration> columns)
        {
            if (columns.Count == 0)
            {
                this.Append("0");
                if (this.IsNested)
                {
                    this.Append(" AS ");
                    this.AppendColumnName("tmp");
                    this.Append(" ");
                }
            }
            else
            {
                base.WriteColumns(columns);
            }
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
            else if (b.NodeType == ExpressionType.ExclusiveOr)
            {
                // SQLite does not have XOR (^).. Use translation:  ((A & ~B) | (~A & B))
                Append("((");
                this.VisitValue(b.Left);
                Append(" & ~");
                this.VisitValue(b.Right);
                Append(") | (~");
                this.VisitValue(b.Left);
                Append(" & ");
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

        protected override void WriteDateTime(object value)
        {
            sb
             .Append(string.Format("'{0:yyyy-MM-dd HH:mm:ss.fff}", value).TrimEnd('0'))
             .Append('\'');
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            if (this.IsPredicate(c.Test))
            {
                Append("(CASE WHEN ");
                VisitPredicate(c.Test);
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
                this.VisitValue(c.Test);
                Append(" WHEN 0 THEN ");
                this.VisitValue(c.IfFalse);
                Append(" ELSE ");
                this.VisitValue(c.IfTrue);
                Append(" END)");
            }
            return c;
        }

        protected override void BuildConverterFunction(Expression from, Type fromType, Type toType)
        {
            if (fromType == Types.String)
            {
                if (toType == Types.DateTime)
                {
                    from = Expression.Call(MethodRepository.Replace, from, Expression.Constant("/"), Expression.Constant("-"));
                    Visit(from);
                    return;
                }
                if (toType == Types.Guid)
                {
                    from = Expression.Call(MethodRepository.ToLower, from);
                    Visit(from);
                    return;
                }
            }
            base.BuildConverterFunction(from, fromType, toType);
        }
    }
}
