// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    using UWay.Skynet.Cloud.Linq;
    /// <summary>
    /// Attempt to rewrite cross joins as inner joins
    /// </summary>
    class CrossJoinRewriter : DbExpressionVisitor
    {
        private Expression currentWhere;

        public static Expression Rewrite(Expression expression)
        {
            return new CrossJoinRewriter().Visit(expression);
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            var saveWhere = this.currentWhere;
            try
            {
                this.currentWhere = select.Where;
                var result = (SelectExpression)base.VisitSelect(select);
                if (this.currentWhere != result.Where)
                {
                    return result.SetWhere(this.currentWhere);
                }
                return result;
            }
            finally
            {
                this.currentWhere = saveWhere;
            }
        }

        protected override Expression VisitJoin(JoinExpression join)
        {
            join = (JoinExpression)base.VisitJoin(join);
            if (join.Join == JoinType.CrossJoin && this.currentWhere != null)
            {
                // try to figure out which parts of the current where expression can be used for a join condition
                var declaredLeft = TableAliasGatherer.Gather(join.Left);
                var declaredRight = TableAliasGatherer.Gather(join.Right);
                var declared = new HashSet<TableAlias>(declaredLeft.Union(declaredRight), TableAlias.Comparer);
                var exprs = this.currentWhere.Split(ExpressionType.And, ExpressionType.AndAlso);
                var good = exprs.Where(e => CanBeJoinCondition(e, declaredLeft, declaredRight, declared)).ToList();
                if (good.Count > 0)
                {
                    var condition = good.Join(ExpressionType.And);
                    join = this.UpdateJoin(join, JoinType.InnerJoin, join.Left, join.Right, condition);
                    var newWhere = exprs.Where(e => !good.Contains(e)).Join(ExpressionType.And);
                    this.currentWhere = newWhere;
                }
            }
            return join;
        }

        private bool CanBeJoinCondition(Expression expression, HashSet<TableAlias> left, HashSet<TableAlias> right, HashSet<TableAlias> all)
        {
            // an expression is good if it has at least one reference to an alias from both left & right sets and does
            // not have any additional references that are not in both left & right sets
            var referenced = ReferencedAliasGatherer.Gather(expression);

            bool hasLeftKey = false, hasRightKey = false;

            foreach (var item in referenced)
            {
                foreach (var l in left)
                {
                    if (item == l)
                    {
                        hasLeftKey = true;
                        break;
                    }
                }
                foreach (var r in right)
                {
                    if (item == r)
                    {
                        hasRightKey = true;
                        break;
                    }
                }


                if (!all.Contains(item))
                    return false;
            }

            return hasLeftKey && hasRightKey;
        }
    }
}