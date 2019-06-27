using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Dialect
{
    /// <summary>
    /// SQLCE doesn't understand scalar subqueries (???) but it does understand cross/outer apply.
    /// Convert scalar subqueries into OUTER APPLY
    /// </summary>
    class ScalarSubqueryRewriter : DbExpressionVisitor
    {
        Expression currentFrom;


        public static Expression Rewrite(Expression expression)
        {
            return new ScalarSubqueryRewriter().Visit(expression);
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            var saveFrom = this.currentFrom;

            var from = this.VisitSource(select.From);
            this.currentFrom = from;

            var where = this.Visit(select.Where);
            var orderBy = this.VisitOrderBy(select.OrderBy);
            var groupBy = this.VisitExpressionList(select.GroupBy);
            var skip = this.Visit(select.Skip);
            var take = this.Visit(select.Take);
            var columns = this.VisitColumnDeclarations(select.Columns);

            from = this.currentFrom;
            this.currentFrom = saveFrom;

            return this.UpdateSelect(select, from, where, orderBy, groupBy, skip, take, select.IsDistinct, select.IsReverse, columns);
        }

        protected override Expression VisitScalar(ScalarExpression scalar)
        {
            var select = scalar.Select;
            var colType = SqlType.Get(scalar.Type);
            if (string.IsNullOrEmpty(select.Columns[0].Name))
            {
                var name = select.Columns.GetAvailableColumnName("scalar");
                select = select.SetColumns(new[] { new ColumnDeclaration(name, select.Columns[0].Expression, colType) });
            }
            this.currentFrom = new JoinExpression(JoinType.OuterApply, this.currentFrom, select, null);
            return new ColumnExpression(scalar.Type, colType, scalar.Select.Alias, select.Columns[0].Name);
        }
    }
}
