using System;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder
{
    class PostgresExpressionBuilder : DbExpressionBuilder
    {
        public override System.Linq.Expressions.Expression Translate(System.Linq.Expressions.Expression expression)
        {
            expression = OrderByRewriter.Rewrite(expression);

            expression = base.Translate(expression);

            // convert skip/take info into RowNumber pattern
            //expression = SkipToRowNumberRewriter.Rewrite(this.Language, expression);

            // fix up any order-by's we may have changed
            expression = OrderByRewriter.Rewrite(expression);

            return expression;
        }

        public override Expression GetGeneratedIdExpression(IMemberMapping member)
        {
            // the table name needs to be quoted, the column name does not.
            // http://archives.postgresql.org/pgsql-bugs/2007-01/msg00102.php
            //return new FunctionExpression(member.GetMemberType(), "CURRVAL",
            //    new FunctionExpression(typeof(object), "pg_get_serial_sequence",
            //        Expression.Constant(Quote(mapping.GetTableName(entity))),
            //        Expression.Constant(mapping.GetColumnName(entity, member))));
            throw new NotImplementedException();
        }

        /* NOTE: PGSql has no notion of the GeneratedIDExpression(), fortunately it is implemented 
      *          using the ADO rowcounts anyway
      */
        public override Expression GetRowsAffectedExpression(Expression command)
        {
            return base.GetRowsAffectedExpression(command);
        }
        public override bool IsRowsAffectedExpressions(Expression expression)
        {
            return base.IsRowsAffectedExpressions(expression);
        }
    }
}
