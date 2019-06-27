using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    class PostgresSqlBuilder : DbSqlBuilder
    {
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
                this.Visit(select.Take);
            }
            if (select.Skip != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("OFFSET ");
                this.Append(select.Skip);
            }
            return select;
        }

        protected override string GetOperator(BinaryExpression b)
        {
            if (b.Type == typeof(string))
            {
                switch (b.NodeType)
                {
                    case ExpressionType.Add:
                    case ExpressionType.AddChecked:
                        return "||";
                }
            }
            else
            {
                switch (b.NodeType)
                {
                    case ExpressionType.ExclusiveOr:
                        return "#";
                }
            }
            return base.GetOperator(b);
        }

        protected override Expression VisitValue(Expression expr)
        {
            if (IsPredicate(expr))
            {
                this.Append("CASE WHEN (");
                this.Visit(expr);
                this.Append(") THEN 1 ELSE 0 END");
                return expr;
            }
            return base.VisitValue(expr);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            if (this.IsPredicate(c.Test))
            {
                this.Append("(CASE WHEN ");
                this.VisitPredicate(c.Test);
                this.Append(" THEN ");
                this.VisitValue(c.IfTrue);
                Expression ifFalse = c.IfFalse;
                while (ifFalse != null && ifFalse.NodeType == ExpressionType.Conditional)
                {
                    ConditionalExpression fc = (ConditionalExpression)ifFalse;
                    this.Append(" WHEN ");
                    this.VisitPredicate(fc.Test);
                    this.Append(" THEN ");
                    this.VisitValue(fc.IfTrue);
                    ifFalse = fc.IfFalse;
                }
                if (ifFalse != null)
                {
                    this.Append(" ELSE ");
                    this.VisitValue(ifFalse);
                }
                this.Append(" END)");
            }
            else
            {
                this.Append("(CASE ");
                this.VisitValue(c.Test);
                this.Append(" WHEN 0 THEN ");
                this.VisitValue(c.IfFalse);
                this.Append(" ELSE ");
                this.VisitValue(c.IfTrue);
                this.Append(" END)");
            }
            return c;
        }
    }
}
