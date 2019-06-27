using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    class FirebirdSqlBuilder : DbSqlBuilder
    {
        protected override void WriteAggregateName(string aggregateName)
        {
            if (aggregateName == "LongCount")
            {
                this.Append("COUNT_BIG");
            }
            else
            {
                base.WriteAggregateName(aggregateName);
            }
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            this.AddAliases(select.From);
            this.Append("SELECT ");
            if (select.IsDistinct)
            {
                this.Append("DISTINCT ");
            }
            if (select.Take != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("FIRST ");
                this.Visit(select.Take);
                this.Append(" SKIP ");
                if (select.Skip == null)
                    this.Append("0");
                else
                    this.Append(select.Skip);
                this.Append(" ");
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

            return select;
        }

        protected override Expression VisitRowNumber(RowNumberExpression rowNumber)
        {
            this.Append("ROW_NUMBER() OVER(");
            if (rowNumber.OrderBy != null && rowNumber.OrderBy.Count > 0)
            {
                this.Append("ORDER BY ");
                for (int i = 0, n = rowNumber.OrderBy.Count; i < n; i++)
                {
                    OrderExpression exp = rowNumber.OrderBy[i];
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
            this.Append(")");
            return rowNumber;
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
                typeNames = new UWay.Skynet.Cloud.Data.Schema.Script.Generator.FirebirdScriptGenerator().typeNames;

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
