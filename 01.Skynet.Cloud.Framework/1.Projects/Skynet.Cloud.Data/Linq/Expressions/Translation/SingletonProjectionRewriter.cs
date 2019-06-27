// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Dialect;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    /// <summary>
    /// Rewrites nested singleton projection into server-side joins
    /// </summary>
    class SingletonProjectionRewriter : DbExpressionVisitor
    {
        IDbExpressionBuilder dialect;
        bool isTopLevel = true;
        SelectExpression currentSelect;

        private SingletonProjectionRewriter(IDbExpressionBuilder dialect)
        {
            this.dialect = dialect;
        }

        public static Expression Rewrite(IDbExpressionBuilder dialect, Expression expression)
        {
            return new SingletonProjectionRewriter(dialect).Visit(expression);
        }

        protected override Expression VisitClientJoin(ClientJoinExpression join)
        {
            // treat client joins as new top level
            var saveTop = this.isTopLevel;
            var saveSelect = this.currentSelect;
            this.isTopLevel = true;
            this.currentSelect = null;
            Expression result = base.VisitClientJoin(join);
            this.isTopLevel = saveTop;
            this.currentSelect = saveSelect;
            return result;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            if (isTopLevel)
            {
                isTopLevel = false;
                this.currentSelect = proj.Select;
                Expression projector = this.Visit(proj.Projector);
                if (projector != proj.Projector || this.currentSelect != proj.Select)
                {
                    return new ProjectionExpression(this.currentSelect, projector, proj.Aggregator);
                }
                return proj;
            }

            if (proj.IsSingleton && this.CanJoinOnServer(this.currentSelect))
            {
                TableAlias newAlias = new TableAlias();
                this.currentSelect = this.currentSelect.AddRedundantSelect(newAlias);

                // remap any references to the outer select to the new alias;
                SelectExpression source = (SelectExpression)ColumnMapper.Map(proj.Select, newAlias, this.currentSelect.Alias);

                // add outer-join test
                ProjectionExpression pex = this.dialect.AddOuterJoinTest(new ProjectionExpression(source, proj.Projector));

                var pc = ColumnProjector.ProjectColumns(pex.Projector, this.currentSelect.Columns, this.currentSelect.Alias, newAlias, proj.Select.Alias);

                JoinExpression join = new JoinExpression(JoinType.OuterApply, this.currentSelect.From, pex.Select, null);

                this.currentSelect = new SelectExpression(this.currentSelect.Alias, pc.Columns, join, null);
                return this.Visit(pc.Projector);
            }

            var saveTop = this.isTopLevel;
            var saveSelect = this.currentSelect;
            this.isTopLevel = true;
            this.currentSelect = null;
            Expression result = base.VisitProjection(proj);
            this.isTopLevel = saveTop;
            this.currentSelect = saveSelect;
            return result;
        }

        private bool CanJoinOnServer(SelectExpression select)
        {
            // can add singleton (1:0,1) join if no grouping/aggregates or distinct
            return !select.IsDistinct
                && (select.GroupBy == null || select.GroupBy.Count == 0)
                && !AggregateChecker.HasAggregates(select);
        }

        protected override Expression VisitSubquery(SubqueryExpression subquery)
        {
            return subquery;
        }

        protected override Expression VisitCommand(CommandExpression command)
        {
            this.isTopLevel = true;
            return base.VisitCommand(command);
        }
    }
}