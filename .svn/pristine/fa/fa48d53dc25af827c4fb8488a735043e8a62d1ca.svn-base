using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder
{
    class OracleExpressionBuilder : DbExpressionBuilder
    {
        public override System.Linq.Expressions.Expression Translate(System.Linq.Expressions.Expression expression)
        {
            expression = OrderByRewriter.Rewrite(expression);
            expression = base.Translate(expression);
            expression = SkipToRowNumberRewriter.Rewrite(expression);
            expression = OrderByRewriter.Rewrite(expression);

            return expression;
        }

        public override Expression GetGeneratedIdExpression(IMemberMapping member)
        {
            var sequenceName = string.IsNullOrEmpty(member.SequenceName) ? "NEXTID" : member.SequenceName;
            return new FunctionExpression(member.MemberType, sequenceName + ".CURRVAL", null);
        }

        protected override List<ColumnAssignment> GetInsertColumnAssignments(Expression table, Expression instance, IEntityMapping entity, Func<IMemberMapping, bool> fnIncludeColumn)
        {
            var items = base.GetInsertColumnAssignments(table, instance, entity, fnIncludeColumn);
            var pk = entity.PrimaryKeys.FirstOrDefault(p => p.IsGenerated);
            if (pk != null)
            {
                var sequenceName = string.IsNullOrEmpty(pk.SequenceName) ? "NEXTID" : pk.SequenceName;

                var ca = new ColumnAssignment(
                                (ColumnExpression)this.GetMemberExpression(table, entity, pk.Member),
                                 new FunctionExpression(pk.MemberType, sequenceName + ".NEXTVAL", null)
                                );
                items.Add(ca);
            }
            return items;
        }

    }
}
