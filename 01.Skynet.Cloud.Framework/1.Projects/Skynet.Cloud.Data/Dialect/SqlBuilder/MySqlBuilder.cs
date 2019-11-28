using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    class MySqlBuilder : DbSqlBuilder
    {

        //类型转换支持的力度很弱
        protected override void RegisterCastTypes()
        {
            RegisterCastType(DBType.VarChar, "CHAR");
            RegisterCastType(DBType.Char, "CHAR");
            RegisterCastType(DBType.NVarChar, "CHAR");
            RegisterCastType(DBType.NChar, "CHAR");
            RegisterCastType(DBType.Binary, "BINARY");
            RegisterCastType(DBType.Boolean, "UNSIGNED");
            RegisterCastType(DBType.Byte, "SIGNED");
            RegisterCastType(DBType.Single, "DECIMAL(19,5)");
            RegisterCastType(DBType.Double, "DECIMAL(19,5)");
            RegisterCastType(DBType.Decimal, "DECIMAL(19,5)");
            RegisterCastType(DBType.Int16, "SIGNED");
            RegisterCastType(DBType.Int32, "SIGNED");
            RegisterCastType(DBType.Int64, "SIGNED");
            RegisterCastType(DBType.Guid, "CHAR(40)");
            RegisterCastType(DBType.DateTime, "DATETIME");

            RegisterCastType(DBType.Text, "Text");
            RegisterCastType(DBType.NText, "Text");
            RegisterCastType(DBType.Currency, "DECIMAL(19,5)");
            RegisterCastType(DBType.Image, "BLOB");
            //RegisterCastType(DBType.Timestamp, "BLOB");

        }

        protected override void AppendParameterName(string name)
        {
            this.Append("?");
            this.Append(name);
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

        #region Expression Parse

        [System.Obsolete]
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
                VisitValue(b.Left);
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
                Append(" << ");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.RightShift)
            {
                Append("(");
                this.VisitValue(b.Left);
                Append(" >> ");
                this.VisitValue(b.Right);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.Add && b.Type == typeof(string))
            {
                Append("CONCAT(");
                int n = 0;
                this.VisitConcatArg(b.Left, ref n);
                this.VisitConcatArg(b.Right, ref n);
                Append(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.Divide && this.IsInteger(b.Type))
            {
                Append("TRUNCATE(");
                base.VisitBinary(b);
                Append(",0)");
                return b;
            }
            return base.VisitBinary(b);
        }

        private void VisitConcatArg(Expression e, ref int n)
        {
            if (e.NodeType == ExpressionType.Add && e.Type == typeof(string))
            {
                BinaryExpression b = (BinaryExpression)e;
                VisitConcatArg(b.Left, ref n);
                VisitConcatArg(b.Right, ref n);
            }
            else
            {
                if (n > 0)
                    Append(", ");
                Visit(e);
                n++;
            }
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
                this.VisitValue(c.Test);
                Append(" WHEN 0 THEN ");
                this.VisitValue(c.IfFalse);
                Append(" ELSE ");
                this.VisitValue(c.IfTrue);
                Append(" END)");
            }
            return c;
        }
        #endregion
    }
}
