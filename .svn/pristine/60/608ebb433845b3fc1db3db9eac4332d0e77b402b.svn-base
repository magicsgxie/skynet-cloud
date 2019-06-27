using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Collections;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Linq;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Internal;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Data.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{
    partial class InternalDbContext : IQueryPolicy
    {
        internal HashSet<MemberInfo> included = new HashSet<MemberInfo>();
        internal HashSet<MemberInfo> deferred = new HashSet<MemberInfo>();
        public IDictionary<MemberInfo, List<LambdaExpression>> Operations { get; private set; }
        //private readonly Dictionary<IEntityModel, IDbSet> tables;

        internal void Include(MemberInfo member, bool deferLoad)
        {
            var memberType = member.GetMemberType();
            var isOneToMany = memberType != Types.String
               && memberType != typeof(byte[])
               && Types.IEnumerable.IsAssignableFrom(memberType);

            if (isOneToMany)
                memberType = ReflectionHelper.GetElementType(memberType);
            if (memberType.FullName.Contains(ULinq.StrEntityRefType))
                memberType = memberType.GetGenericArguments()[0];

            if (!dbConfiguration.HasClass(memberType))
                throw new InvalidOperationException("Only include entity or entity collection.");
            this.included.Add(member);
            if (deferLoad)
                Defer(member);
        }

        private void Defer(MemberInfo member)
        {
            Type mType = member.GetMemberType();
            if (mType.IsGenericType)
            {
                var gType = mType.GetGenericTypeDefinition();
                if (gType != typeof(IEnumerable<>)
                    && gType != typeof(IList<>)
                    && !typeof(IDeferLoadable).IsAssignableFrom(mType))
                {
                    throw new InvalidOperationException(string.Format(Res.DeferInvalid, member));
                }
            }
            this.deferred.Add(member);
        }
        internal void Include(MemberInfo member)
        {
            Include(member, false);
        }

        internal void IncludeWith(LambdaExpression fnMember)
        {
            IncludeWith(fnMember, false);
        }

        internal void IncludeWith(LambdaExpression fnMember, bool deferLoad)
        {
            var rootMember = UWay.Skynet.Cloud.Linq.Expressor.Member(fnMember);// RootMemberFinder.Find(fnMember, fnMember.Parameters[0]);
            if (rootMember == null)
                throw new InvalidOperationException(Res.SubqueryInvalid);
            Include(rootMember, deferLoad);
            //if (rootMember != fnMember.Body)
            //{
            //    AssociateWith(fnMember);
            //}
        }

        internal void IncludeWith<TEntity>(Expression<Func<TEntity, object>> fnMember)
        {
            IncludeWith((LambdaExpression)fnMember, false);
        }

        internal void IncludeWith<TEntity>(Expression<Func<TEntity, object>> fnMember, bool deferLoad)
        {
            IncludeWith((LambdaExpression)fnMember, deferLoad);
        }

        internal void AssociateWith(LambdaExpression memberQuery)
        {
            var rootMember = RootMemberFinder.Find(memberQuery, memberQuery.Parameters[0]);
            if (rootMember == null)
                throw new InvalidOperationException(Res.SubqueryInvalid);
            if (rootMember != memberQuery.Body)
            {
                var memberParam = Expression.Parameter(rootMember.Type, "root");
                var newBody = ExpressionReplacer.Replace(memberQuery.Body, rootMember, memberParam);
                this.AddOperation(rootMember.Member, Expression.Lambda(newBody, memberParam));
            }
        }

        private void AddOperation(MemberInfo member, LambdaExpression operation)
        {
            List<LambdaExpression> memberOps;
            if (!this.Operations.TryGetValue(member, out memberOps))
            {
                memberOps = new List<LambdaExpression>();
                this.Operations.Add(member, memberOps);
            }
            memberOps.Add(operation);
        }

        internal void AssociateWith<TEntity>(Expression<Func<TEntity, IEnumerable>> memberQuery)
        {
            AssociateWith((LambdaExpression)memberQuery);
        }

        class RootMemberFinder : UWay.Skynet.Cloud.Data.Linq.ExpressionVisitor
        {
            MemberExpression found;
            ParameterExpression parameter;

            private RootMemberFinder(ParameterExpression parameter)
            {
                this.parameter = parameter;
            }

            public static MemberExpression Find(Expression query, ParameterExpression parameter)
            {
                var finder = new RootMemberFinder(parameter);
                finder.Visit(query);
                return finder.found;
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                if (m.Object != null)
                {
                    this.Visit(m.Object);
                }
                else if (m.Arguments.Count > 0)
                {
                    this.Visit(m.Arguments[0]);
                }
                return m;
            }

            protected override Expression VisitMemberAccess(MemberExpression m)
            {
                if (m.Expression == this.parameter)
                {
                    this.found = m;
                    return m;
                }
                else
                {
                    return base.VisitMemberAccess(m);
                }
            }
        }

        bool IQueryPolicy.IsIncluded(MemberInfo member)
        {
            return this.included.Contains(member);
        }

        bool IQueryPolicy.IsDeferLoaded(MemberInfo member)
        {
            return this.deferred.Contains(member);
        }


        internal Expression ApplyPolicy(Expression expression, MemberInfo member)
        {
            List<LambdaExpression> ops;
            if (Operations.TryGetValue(member, out ops))
            {
                var result = expression;
                foreach (var fnOp in ops)
                {
                    var pop = PartialEvaluator.Eval(fnOp, ExpressionHelper.CanBeEvaluatedLocally);
                    result = QueryBinder.Bind(ExpressionBuilder, this, Expression.Invoke(pop, result));
                }
                var projection = (ProjectionExpression)result;
                if (projection.Type != expression.Type)
                {
                    var fnAgg = Aggregator.GetAggregator(expression.Type, projection.Type);
                    projection = new ProjectionExpression(projection.Select, projection.Projector, fnAgg);
                }
                return projection;
            }
            return expression;
        }
        class RelationshipIncluder : DbExpressionVisitor
        {
            IDbExpressionBuilder dbExpressionBuilder;
            IQueryPolicy policy;
            ScopedDictionary<IMemberMapping, bool> includeScope = new ScopedDictionary<IMemberMapping, bool>(null, MemberModelComparer.Instance);

            class MemberModelComparer : IEqualityComparer<IMemberMapping>
            {
                public static readonly MemberModelComparer Instance = new MemberModelComparer();

                public bool Equals(IMemberMapping x, IMemberMapping y)
                {
                    return x.Member == y.Member;
                }

                public int GetHashCode(IMemberMapping obj)
                {
                    return obj.Member.GetHashCode();
                }
            }

            private RelationshipIncluder(IDbExpressionBuilder dbExpressionBuilder, InternalDbContext policy)
            {
                this.dbExpressionBuilder = dbExpressionBuilder;
                this.policy = policy;
            }

            public static Expression Include(IDbExpressionBuilder dbExpressionBuilder, InternalDbContext policy, Expression expression)
            {
                return new RelationshipIncluder(dbExpressionBuilder, policy).Visit(expression);
            }

            protected override Expression VisitProjection(ProjectionExpression proj)
            {
                Expression projector = this.Visit(proj.Projector);
                return this.UpdateProjection(proj, proj.Select, projector, proj.Aggregator);
            }

            bool HasIncludedMembers(EntityExpression entity)
            {
                foreach (var mi in entity.Entity.Members)
                {
                    if (policy.IsIncluded(mi.Member))
                        return true;
                }
                return false;
            }

            bool IsNullRelationshipAssignment(IEntityMapping entity, EntityAssignment assignment)
            {
                var mm = entity.Get(assignment.Member);
                if (mm != null && mm.IsRelationship)
                {
                    var cex = assignment.Expression as ConstantExpression;
                    if (cex != null && cex.Value == null)
                        return true;
                }
                return false;
            }

            EntityExpression IncludeMembers(EntityExpression entity, Func<IMemberMapping, bool> fnIsIncluded)
            {
                var assignments = this.dbExpressionBuilder.GetAssignments(entity.Expression).ToDictionary(ma => ma.Member.Name);
                bool anyAdded = false;
                foreach (var mi in entity.Entity.Members)
                {
                    EntityAssignment ea;
                    bool okayToInclude = !assignments.TryGetValue(mi.Member.Name, out ea) || IsNullRelationshipAssignment(entity.Entity, ea);
                    if (okayToInclude && fnIsIncluded(mi))
                    {
                        ea = new EntityAssignment(mi, dbExpressionBuilder.GetMemberExpression(entity.Expression, entity.Entity, mi));
                        assignments[mi.Member.Name] = ea;
                        anyAdded = true;
                    }
                }
                if (anyAdded)
                {
                    return new EntityExpression(entity.Entity, dbExpressionBuilder.BuildEntityExpression(entity.Entity, assignments.Values.ToList()));
                }
                return entity;
            }

            protected override Expression VisitEntity(EntityExpression entity)
            {
                var save = this.includeScope;
                this.includeScope = new ScopedDictionary<IMemberMapping, bool>(this.includeScope, MemberModelComparer.Instance);
                try
                {

                    if (HasIncludedMembers(entity))
                    {
                        entity = this.IncludeMembers(
                            entity,
                            m =>
                            {
                                if (this.includeScope.ContainsKey(m))
                                {
                                    return false;
                                }
                                if (this.policy.IsIncluded(m.Member))
                                {
                                    this.includeScope.Add(m, true);
                                    return true;
                                }
                                return false;
                            });
                    }
                    return base.VisitEntity(entity);
                }
                finally
                {
                    this.includeScope = save;
                }
            }
        }


    }
}
