// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.ObjectModel;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    /// <summary>
    /// Translates accesses to relationship members into projections or joins
    /// </summary>
    class RelationshipBinder : DbExpressionVisitor
    {
        IDbExpressionBuilder expressionBuilder;
        Expression currentFrom;

        private RelationshipBinder(IDbExpressionBuilder expressionBuilder)
        {
            this.expressionBuilder = expressionBuilder;
        }

        public static Expression Bind(IDbExpressionBuilder mapper, Expression expression)
        {
            return new RelationshipBinder(mapper).Visit(expression);
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            Expression saveCurrentFrom = this.currentFrom;
            this.currentFrom = this.VisitSource(select.From);
            try
            {
                Expression where = this.Visit(select.Where);
                ReadOnlyCollection<OrderExpression> orderBy = this.VisitOrderBy(select.OrderBy);
                ReadOnlyCollection<Expression> groupBy = this.VisitExpressionList(select.GroupBy);
                Expression skip = this.Visit(select.Skip);
                Expression take = this.Visit(select.Take);
                ReadOnlyCollection<ColumnDeclaration> columns = this.VisitColumnDeclarations(select.Columns);
                if (this.currentFrom != select.From
                    || where != select.Where
                    || orderBy != select.OrderBy
                    || groupBy != select.GroupBy
                    || take != select.Take
                    || skip != select.Skip
                    || columns != select.Columns
                    )
                {
                    return new SelectExpression(select.Alias, columns, this.currentFrom, where, orderBy, groupBy, select.IsDistinct, skip, take, select.IsReverse);
                }
                return select;
            }
            finally
            {
                this.currentFrom = saveCurrentFrom;
            }
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            Expression source = this.Visit(m.Expression);
            EntityExpression ex = source as EntityExpression;
            IMemberMapping mm = null;

            if (ex != null && (mm = ex.Entity.Get(m.Member)) != null && mm.IsRelationship)
            {
                ProjectionExpression projection = (ProjectionExpression)this.Visit(this.expressionBuilder.GetMemberExpression(source, ex.Entity, m.Member));
                if (this.currentFrom != null && mm.IsManyToOne)
                {
                    // convert singleton associations directly to OUTER APPLY
                    projection = this.expressionBuilder.AddOuterJoinTest(projection);
                    Expression newFrom = new JoinExpression(JoinType.OuterApply, this.currentFrom, projection.Select, null);
                    this.currentFrom = newFrom;
                    return projection.Projector;
                }
                return projection;
            }
            else
            {
                Expression result = QueryBinder.BindMember(source, m.Member);
                MemberExpression mex = result as MemberExpression;
                if (mex != null && mex.Member == m.Member && mex.Expression == m.Expression)
                {
                    return m;
                }
                return result;
            }
        }
    }
}
