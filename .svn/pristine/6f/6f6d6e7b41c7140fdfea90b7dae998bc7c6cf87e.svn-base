// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    using UWay.Skynet.Cloud.Data.Dialect;
    using UWay.Skynet.Cloud.Linq;

    class ComparisonRewriter : DbExpressionVisitor
    {
        IDbExpressionBuilder expressionBuilder;

        private ComparisonRewriter(IDbExpressionBuilder mapping)
        {
            this.expressionBuilder = mapping;
        }

        public static Expression Rewrite(IDbExpressionBuilder mapping, Expression expression)
        {
            return new ComparisonRewriter(mapping).Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    Expression result = this.Compare(b);
                    if (result == b)
                        goto default;
                    return this.Visit(result);
                default:
                    return base.VisitBinary(b);
            }
        }

        protected Expression SkipConvert(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }

        protected Expression Compare(BinaryExpression bop)
        {
            var e1 = this.SkipConvert(bop.Left);
            var e2 = this.SkipConvert(bop.Right);
            EntityExpression entity1 = e1 as EntityExpression;
            EntityExpression entity2 = e2 as EntityExpression;

            if (entity1 == null && e1 is OuterJoinedExpression)
            {
                entity1 = ((OuterJoinedExpression)e1).Expression as EntityExpression;
            }

            if (entity2 == null && e2 is OuterJoinedExpression)
            {
                entity2 = ((OuterJoinedExpression)e2).Expression as EntityExpression;
            }

            bool negate = bop.NodeType == ExpressionType.NotEqual;
            if (entity1 != null)
            {
                return this.MakePredicate(e1, e2, entity1.Entity.PrimaryKeys.Select(p => p.Member), negate);
            }
            else if (entity2 != null)
            {
                return this.MakePredicate(e1, e2, entity2.Entity.PrimaryKeys.Select(p => p.Member), negate);
            }

            var dm1 = this.GetDefinedMembers(e1);
            var dm2 = this.GetDefinedMembers(e2);

            if (dm1 == null && dm2 == null)
            {
                // neither are constructed types
                return bop;
            }

            if (dm1 != null && dm2 != null)
            {
                // both are constructed types, so they'd better have the same members declared
                HashSet<string> names1 = new HashSet<string>(dm1.Select(m => m.Name), StringComparer.Ordinal);
                HashSet<string> names2 = new HashSet<string>(dm2.Select(m => m.Name), StringComparer.Ordinal);
                if (names1.IsSubsetOf(names2) && names2.IsSubsetOf(names1))
                {
                    return MakePredicate(e1, e2, dm1, negate);
                }
            }
            else if (dm1 != null)
            {
                return MakePredicate(e1, e2, dm1, negate);
            }
            else if (dm2 != null)
            {
                return MakePredicate(e1, e2, dm2, negate);
            }

            throw new InvalidOperationException(Res.InvalidOperationCompareException);
        }

        protected Expression MakePredicate(Expression e1, Expression e2, IEnumerable<MemberInfo> members, bool negate)
        {
            var pred = members.Select(m =>
                QueryBinder.BindMember(e1, m).Equal(QueryBinder.BindMember(e2, m))
                ).Join(ExpressionType.And);
            if (negate)
                pred = Expression.Not(pred);
            return pred;
        }

        private IEnumerable<MemberInfo> GetDefinedMembers(Expression expr)
        {
            MemberInitExpression mini = expr as MemberInitExpression;
            if (mini != null)
            {
                var members = mini.Bindings.Select(b => FixMember(b.Member));
                if (mini.NewExpression.Members != null)
                {
                    members.Concat(mini.NewExpression.Members.Select(m => FixMember(m)));
                }
                return members;
            }
            else
            {
                NewExpression nex = expr as NewExpression;
                if (nex != null && nex.Members != null)
                {
                    return nex.Members.Select(m => FixMember(m));
                }
            }
            return null;
        }

        private static MemberInfo FixMember(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Method && member.Name.StartsWith("get_"))
            {
                return member.DeclaringType.GetProperty(member.Name.Substring(4));
            }
            return member;
        }
    }
}
