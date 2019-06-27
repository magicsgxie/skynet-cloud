// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    using UWay.Skynet.Cloud.Data.Dialect;
    using UWay.Skynet.Cloud.Linq;


    /// <summary>
    /// rewrites nested projections into client-side joins
    /// </summary>
    class ClientJoinedProjectionRewriter : DbExpressionVisitor
    {
        IQueryPolicy policy;
        IDbExpressionBuilder dbExpressionBuilder;
        bool isTopLevel = true;
        SelectExpression currentSelect;
        MemberInfo currentMember;
        bool canJoinOnClient = true;

        private ClientJoinedProjectionRewriter(InternalDbContext dbContext)
        {
            this.policy = dbContext;
            this.dbExpressionBuilder = dbContext.ExpressionBuilder;
        }

        public static Expression Rewrite(InternalDbContext dbContext, Expression expression)
        {
            return new ClientJoinedProjectionRewriter(dbContext).Visit(expression);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            var saveMember = this.currentMember;
            this.currentMember = assignment.Member;
            Expression e = this.Visit(assignment.Expression);
            this.currentMember = saveMember;
            return this.UpdateMemberAssignment(assignment, assignment.Member, e);
        }

        protected override Expression VisitMemberAndExpression(MemberInfo member, Expression expression)
        {
            var saveMember = this.currentMember;
            this.currentMember = member;
            Expression e = this.Visit(expression);
            this.currentMember = saveMember;
            return e;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            SelectExpression save = this.currentSelect;
            this.currentSelect = proj.Select;
            try
            {
                if (!this.isTopLevel)
                {
                    if (this.CanJoinOnClient(this.currentSelect))
                    {
                        // make a query that combines all the constraints from the outer queries into a single select
                        SelectExpression newOuterSelect = (SelectExpression)QueryDuplicator.Duplicate(save);

                        // remap any references to the outer select to the new alias;
                        SelectExpression newInnerSelect = (SelectExpression)ColumnMapper.Map(proj.Select, newOuterSelect.Alias, save.Alias);
                        // add outer-join test
                        ProjectionExpression newInnerProjection = this.dbExpressionBuilder.AddOuterJoinTest(new ProjectionExpression(newInnerSelect, proj.Projector));
                        newInnerSelect = newInnerProjection.Select;
                        Expression newProjector = newInnerProjection.Projector;

                        TableAlias newAlias = new TableAlias();
                        var pc = ColumnProjector.ProjectColumns(newProjector, null, newAlias, newOuterSelect.Alias, newInnerSelect.Alias);

                        JoinExpression join = new JoinExpression(JoinType.OuterApply, newOuterSelect, newInnerSelect, null);
                        SelectExpression joinedSelect = new SelectExpression(newAlias, pc.Columns, join, null, null, null, proj.IsSingleton, null, null, false);

                        // apply client-join treatment recursively
                        this.currentSelect = joinedSelect;
                        newProjector = this.Visit(pc.Projector);

                        // compute keys (this only works if join condition was a single column comparison)
                        List<Expression> outerKeys = new List<Expression>();
                        List<Expression> innerKeys = new List<Expression>();
                        if (this.GetEquiJoinKeyExpressions(newInnerSelect.Where, newOuterSelect.Alias, outerKeys, innerKeys))
                        {
                            // outerKey needs to refer to the outer-scope's alias
                            var outerKey = outerKeys.Select(k => ColumnMapper.Map(k, save.Alias, newOuterSelect.Alias));
                            // innerKey needs to refer to the new alias for the select with the new join
                            var innerKey = innerKeys.Select(k => ColumnMapper.Map(k, joinedSelect.Alias, ((ColumnExpression)k).Alias));
                            ProjectionExpression newProjection = new ProjectionExpression(joinedSelect, newProjector, proj.Aggregator);
                            return new ClientJoinExpression(newProjection, outerKey, innerKey);
                        }
                    }
                    else
                    {
                        bool saveJoin = this.canJoinOnClient;
                        this.canJoinOnClient = false;
                        var result = base.VisitProjection(proj);
                        this.canJoinOnClient = saveJoin;
                        return result;
                    }
                }
                else
                {
                    this.isTopLevel = false;
                }
                return base.VisitProjection(proj);
            }
            finally
            {
                this.currentSelect = save;
            }
        }

        private bool CanJoinOnClient(SelectExpression select)
        {
            // can add singleton (1:0,1) join if no grouping/aggregates or distinct
            return
                this.canJoinOnClient
                && this.currentMember != null
                && !this.policy.IsDeferLoaded(this.currentMember)
                && !select.IsDistinct
                && (select.GroupBy == null || select.GroupBy.Count == 0)
                && !AggregateChecker.HasAggregates(select);
        }

        private bool GetEquiJoinKeyExpressions(Expression predicate, TableAlias outerAlias, List<Expression> outerExpressions, List<Expression> innerExpressions)
        {
            if (predicate.NodeType == ExpressionType.Equal)
            {
                var b = (BinaryExpression)predicate;
                ColumnExpression leftCol = this.GetColumnExpression(b.Left);
                ColumnExpression rightCol = this.GetColumnExpression(b.Right);
                if (leftCol != null && rightCol != null)
                {
                    if (leftCol.Alias == outerAlias)
                    {
                        outerExpressions.Add(b.Left);
                        innerExpressions.Add(b.Right);
                        return true;
                    }
                    else if (rightCol.Alias == outerAlias)
                    {
                        innerExpressions.Add(b.Left);
                        outerExpressions.Add(b.Right);
                        return true;
                    }
                }
            }

            bool hadKey = false;
            var parts = predicate.Split(ExpressionType.And, ExpressionType.AndAlso);
            if (parts.Length > 1)
            {
                foreach (var part in parts)
                {
                    bool hasOuterAliasReference = ReferencedAliasGatherer.Gather(part).Contains(outerAlias);
                    if (hasOuterAliasReference)
                    {
                        if (!GetEquiJoinKeyExpressions(part, outerAlias, outerExpressions, innerExpressions))
                            return false;
                        hadKey = true;
                    }
                }
            }

            return hadKey;
        }

        private ColumnExpression GetColumnExpression(Expression expression)
        {
            // ignore converions 
            while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression as ColumnExpression;
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

        /// <summary>
        /// Duplicate the query expression by making a copy with new table aliases
        /// </summary>
        class QueryDuplicator : DbExpressionVisitor
        {
            Dictionary<TableAlias, TableAlias> map = new Dictionary<TableAlias, TableAlias>(TableAlias.Comparer);

            public static Expression Duplicate(Expression expression)
            {
                return new QueryDuplicator().Visit(expression);
            }

            protected override Expression VisitTable(TableExpression table)
            {
                TableAlias newAlias = new TableAlias();
                this.map[table.Alias] = newAlias;
                return new TableExpression(newAlias, table.Mapping);
            }

            protected override Expression VisitSelect(SelectExpression select)
            {
                TableAlias newAlias = new TableAlias();
                this.map[select.Alias] = newAlias;
                select = (SelectExpression)base.VisitSelect(select);
                return new SelectExpression(newAlias, select.Columns, select.From, select.Where, select.OrderBy, select.GroupBy, select.IsDistinct, select.Skip, select.Take, select.IsReverse);
            }

            protected override Expression VisitColumn(ColumnExpression column)
            {
                TableAlias newAlias;
                if (this.map.TryGetValue(column.Alias, out newAlias))
                {
                    return new ColumnExpression(column.Type, column.SqlType, newAlias, column.Name);
                }
                return column;
            }
        }
    }
}