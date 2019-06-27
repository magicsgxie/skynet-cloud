using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    class SqlServer2005SqlBuilder : MsSql2000SqlBuilder
    {
        protected override void WriteTableName(Mapping.IEntityMapping mapping)
        {
            if (mapping.ServerName.HasValue())
                sb.Append(Dialect.Quote(mapping.ServerName)).Append(".");
            if (mapping.DatabaseName.HasValue())
                sb.Append(Dialect.Quote(mapping.DatabaseName)).Append(".");
            if (Dialect.SupportSchema && !string.IsNullOrEmpty(mapping.Schema))
            {
                sb.Append(Dialect.Quote(mapping.Schema));
                sb.Append(".");
            }
            this.AppendTableName(mapping.TableName);
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

    }


}
