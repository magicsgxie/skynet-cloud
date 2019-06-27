using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Linq;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Internal;
using UWay.Skynet.Cloud.Data.Mapping;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Mapping;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder
{
    abstract class DbExpressionBuilder : IDbExpressionBuilder
    {

        private static readonly char[] dotSeparator = new char[] { '.' };
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase;

        Expression GetOuterJoinTest(SelectExpression select)
        {

            var aliases = TableAliasGatherer.Gather(select.From);
            var joinColumns = JoinColumnGatherer.Gather(aliases, select).ToList();
            if (joinColumns.Count > 0)
            {
                // prefer one that is already in the projection list.
                foreach (var jc in joinColumns)
                {
                    foreach (var col in select.Columns)
                    {
                        if (jc.Equals(col.Expression))
                        {
                            return jc;
                        }
                    }
                }
                return joinColumns[0];
            }

            // fall back to introducing a constant
            return Expression.Constant(1, typeof(int?));
        }

        public virtual ProjectionExpression AddOuterJoinTest(ProjectionExpression proj)
        {
            var test = this.GetOuterJoinTest(proj.Select);
            var select = proj.Select;
            ColumnExpression testCol = null;
            // look to see if test expression exists in columns already
            foreach (var col in select.Columns)
            {
                if (test.Equals(col.Expression))
                {
                    var colType = SqlType.Get(test.Type);
                    testCol = new ColumnExpression(test.Type, colType, select.Alias, col.Name);
                    break;
                }
            }
            if (testCol == null)
            {
                // add expression to projection
                testCol = test as ColumnExpression;
                string colName = (testCol != null) ? testCol.Name : "Test";
                colName = proj.Select.Columns.GetAvailableColumnName(colName);
                var colType = SqlType.Get(test.Type);
                select = select.AddColumn(new ColumnDeclaration(colName, test, colType));
                testCol = new ColumnExpression(test.Type, colType, select.Alias, colName);
            }
            var newProjector = new OuterJoinedExpression(testCol, proj.Projector);
            return new ProjectionExpression(select, newProjector, proj.Aggregator);
        }

        public abstract Expression GetGeneratedIdExpression(IMemberMapping member);

        public virtual Expression GetRowsAffectedExpression(Expression command)
        {
            return new FunctionExpression(typeof(int), "@@ROWCOUNT", null);
        }

        public virtual bool IsRowsAffectedExpressions(Expression expression)
        {
            FunctionExpression fex = expression as FunctionExpression;
            return fex != null && fex.Name == "@@ROWCOUNT";
        }

        public virtual ProjectionExpression GetQueryExpression(IEntityMapping mapping)
        {
            Expression projector;
            TableAlias selectAlias;
            ProjectedColumns pc;
            ProjectionExpression proj;

            var tableAlias = new TableAlias();
            selectAlias = new TableAlias();
            var table = new TableExpression(tableAlias, mapping);

            projector = this.GetEntityExpression(table, mapping);
            pc = ColumnProjector.ProjectColumns(projector, null, selectAlias, tableAlias);

            proj = new ProjectionExpression(
                new SelectExpression(selectAlias, pc.Columns, table, null),
                pc.Projector
                );

            return (ProjectionExpression)ApplyPolicy(proj, mapping.EntityType);

        }

        public Expression GetMemberExpression(Expression root, IEntityMapping mapping, MemberInfo member)
        {
            return GetMemberExpression(root, mapping, mapping.Get(member));
        }

        public virtual Expression GetMemberExpression(Expression root, IEntityMapping mapping, IMemberMapping mm)
        {
            var m = mm.Member;
            if (mm.IsRelationship)
            {
                IEntityMapping relatedEntity = mm.RelatedEntity;
                ProjectionExpression projection = this.GetQueryExpression(relatedEntity);

                // make where clause for joining back to 'root'
                var thisKeyMembers = mm.ThisKeyMembers;
                var otherKeyMembers = mm.OtherKeyMembers;

                Expression where = null;
                for (int i = 0, n = otherKeyMembers.Length; i < n; i++)
                {
                    Expression equal =
                        this.GetMemberExpression(projection.Projector, relatedEntity, otherKeyMembers[i]).Equal(
                            this.GetMemberExpression(root, mapping, thisKeyMembers[i])
                        );
                    where = (where != null) ? where.And(equal) : equal;
                }

                TableAlias newAlias = new TableAlias();
                var pc = ColumnProjector.ProjectColumns(projection.Projector, null, newAlias, projection.Select.Alias);

                LambdaExpression aggregator = Aggregator.GetAggregator(mm.MemberType, typeof(IEnumerable<>).MakeGenericType(pc.Projector.Type));
                var result = new ProjectionExpression(
                    new SelectExpression(newAlias, pc.Columns, projection.Select, where),
                    pc.Projector, aggregator
                    );

                return ApplyPolicy(result, m);
            }
            else
            {
                AliasedExpression aliasedRoot = root as AliasedExpression;
                if (aliasedRoot != null && mm.IsColumn)
                {
                    return new ColumnExpression(mm.MemberType, mm.SqlType, aliasedRoot.Alias, mm.ColumnName);
                }
                return QueryBinder.BindMember(root, m);
            }
        }

        public virtual Expression GetPrimaryKeyQuery(IEntityMapping mapping, Expression source, Expression[] keys)
        {
            ParameterExpression p = Expression.Parameter(mapping.EntityType, "p");
            Expression pred = null;

            if (mapping.PrimaryKeys.Length != keys.Length)
                throw new InvalidOperationException(string.Format(Res.LengthInvalid, " primary key values"));
            for (int i = 0, n = keys.Length; i < n; i++)
            {
                MemberInfo mem = mapping.PrimaryKeys[i].Member;
                Type memberType = mem.GetMemberType();
                if (keys[i] != null && TypeHelper.GetNonNullableType(keys[i].Type) != TypeHelper.GetNonNullableType(memberType))
                    throw new InvalidOperationException(string.Format(Res.TypeInvalid, "Primary key value"));
                Expression eq = Expression.MakeMemberAccess(p, mem).Equal(keys[i]);
                pred = (pred == null) ? eq : pred.And(eq);
            }
            var predLambda = Expression.Lambda(pred, p);

            return Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { mapping.EntityType }, source, predLambda);
        }

        protected virtual List<ColumnAssignment> GetInsertColumnAssignments(Expression table, Expression instance, IEntityMapping entity, Func<IMemberMapping, bool> fnIncludeColumn)
        {
            return GetColumnAssignments(table, instance, entity, fnIncludeColumn);
        }
        public virtual Expression GetInsertExpression(IEntityMapping mapping, Expression instance, LambdaExpression selector)
        {
            var tableAlias = new TableAlias();
            var table = new TableExpression(tableAlias, mapping);
            var assignments = this.GetInsertColumnAssignments(table, instance, mapping, m => !m.IsGenerated && !m.IsVersion).ToArray();

            object o = null;
            var c = instance as ConstantExpression;
            if (c != null)
                o = c.Value;

            if (selector != null)
            {
                return new BlockCommand(
                    new InsertCommand(table, assignments, o),
                    this.GetInsertResult(mapping, instance, selector, null)
                    );
            }

            return new InsertCommand(table, assignments, o);
        }

        public virtual Expression GetUpdateExpression(IEntityMapping mapping, Expression instance, LambdaExpression updateCheck, LambdaExpression selector, Expression @else)
        {
            var tableAlias = new TableAlias();
            var table = new TableExpression(tableAlias, mapping);

            var where = this.GetIdentityCheck(table, mapping, instance);
            if (updateCheck != null)
            {
                Expression typeProjector = this.GetEntityExpression(table, mapping);
                Expression pred = DbExpressionReplacer.Replace(updateCheck.Body, updateCheck.Parameters[0], typeProjector);
                where = where != null ? where.And(pred) : pred;
            }

            var assignments = this.GetColumnAssignments(table, instance, mapping, m => m.IsUpdatable && !m.IsVersion);

            var version = mapping.Version;
            bool supportsVersionCheck = false;


            if (version != null)
            {
                var versionValue = GetVersionValue(mapping, instance);
                var versionExp = Expression.Constant(versionValue, version.MemberType);
                var memberExpression = GetMemberExpression(table, mapping, mapping.Version);
                var versionCheck = memberExpression.Equal(versionExp);
                where = (where != null) ? where.And(versionCheck) : versionCheck;

                if (version.MemberType.IsNullable())
                {
                    var versionAssignment = new ColumnAssignment(
                           memberExpression as ColumnExpression,
                           versionValue == null ?
                                                (Expression)Expression.Constant(1, version.MemberType)
                                                : Expression.Add(memberExpression, Expression.Constant(1, version.MemberType))
                           );
                    assignments.Add(versionAssignment);
                    supportsVersionCheck = true;
                }
                else
                {
                    var versionAssignment = new ColumnAssignment(
                         memberExpression as ColumnExpression,
                          Expression.Add(memberExpression, Expression.Constant(1, version.MemberType))
                         );
                    assignments.Add(versionAssignment);
                    supportsVersionCheck = true;
                }
            }

            object o = null;
            var c = instance as ConstantExpression;
            if (c != null)
                o = c.Value;
            Expression update = new UpdateCommand(table, where, o, supportsVersionCheck, assignments);

            if (selector != null)
            {
                return new BlockCommand(
                    update,
                    new IFCommand(
                        this.GetRowsAffectedExpression(update).GreaterThan(Expression.Constant(0)),
                        this.GetUpdateResult(mapping, instance, selector),
                        @else
                        )
                    );
            }
            else if (@else != null)
            {
                return new BlockCommand(
                    update,
                    new IFCommand(
                        this.GetRowsAffectedExpression(update).LessThanOrEqual(Expression.Constant(0)),
                        @else,
                        null
                        )
                    );
            }
            else
            {
                return update;
            }
        }

        public virtual Expression GetDeleteExpression(IEntityMapping mapping, Expression instance, LambdaExpression deleteCheck)
        {
            TableExpression table = new TableExpression(new TableAlias(), mapping);
            Expression where = null;

            if (instance != null)
                where = this.GetIdentityCheck(table, mapping, instance);

            if (deleteCheck != null)
            {
                Expression row = this.GetEntityExpression(table, mapping);
                Expression pred = DbExpressionReplacer.Replace(deleteCheck.Body, deleteCheck.Parameters[0], row);
                where = (where != null) ? where.And(pred) : pred;
            }

            bool supportsVersionCheck = false;
            if (mapping.Version != null && instance != null)
            {

                //var versionValue = GetVersionValue(entity, instance);
                //var versionCheck = GetMemberExpression(table, entity, entity.Version).Equal(Expression.Constant(versionValue));
                //where = (where != null) ? where.And(versionCheck) : versionCheck;

                var version = mapping.Version;
                var versionValue = GetVersionValue(mapping, instance);
                var versionExp = Expression.Constant(versionValue, version.MemberType);
                var memberExpression = GetMemberExpression(table, mapping, mapping.Version);
                if (version.MemberType.IsNullable())
                {

                    var versionCheck = versionValue == null ? (Expression)memberExpression.Equal(Expression.Constant(null, version.MemberType))
                        : memberExpression.Equal(versionExp);
                    where = (where != null) ? where.And(versionCheck) : versionCheck;
                }
                else
                {
                    var versionCheck = memberExpression.Equal(versionExp);
                    where = (where != null) ? where.And(versionCheck) : versionCheck;
                }

                supportsVersionCheck = true;
            }

            object o = null;
            var c = instance as ConstantExpression;
            if (c != null)
                o = c.Value;
            return new DeleteCommand(table, where, o, supportsVersionCheck);
        }

        internal Expression ApplyPolicy(Expression expression, MemberInfo member)
        {
            List<LambdaExpression> ops;
            var dbContext = ExecuteContext.DbContext;
            if (dbContext != null && dbContext.Operations.TryGetValue(member, out ops))
            {
                var result = expression;
                foreach (var fnOp in ops)
                {
                    var pop = PartialEvaluator.Eval(fnOp, ExpressionHelper.CanBeEvaluatedLocally);
                    result = QueryBinder.Bind(this, dbContext, Expression.Invoke(pop, result));
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

        public virtual EntityExpression GetEntityExpression(Expression root, IEntityMapping mapping)
        {
            var assignments = new List<EntityAssignment>();
            foreach (var mi in mapping.Members)
            {
                if (!mi.IsRelationship)
                {
                    var me = this.GetMemberExpression(root, mapping, mi);
                    if (me != null)
                        assignments.Add(new EntityAssignment(mi.Member, me));
                }
            }

            return new EntityExpression(mapping, this.BuildEntityExpression(mapping, assignments));
        }

        public virtual Expression GetInsertResult(IEntityMapping mapping, Expression instance, LambdaExpression selector, Dictionary<MemberInfo, Expression> map)
        {
            var tableAlias = new TableAlias();
            var tex = new TableExpression(tableAlias, mapping);
            var aggregator = Aggregator.GetAggregator(selector.Body.Type, typeof(IEnumerable<>).MakeGenericType(selector.Body.Type));

            Expression where = null;
            DeclarationCommand genIdCommand = null;
            var generatedIds = mapping.PrimaryKeys.Where(m => m.IsPrimaryKey && m.IsGenerated).ToList();
            if (generatedIds.Count > 0)
            {
                if (map == null || !generatedIds.Any(m => map.ContainsKey(m.Member)))
                {
                    var localMap = new Dictionary<MemberInfo, Expression>();
                    genIdCommand = this.GetGeneratedIdCommand(mapping, generatedIds, localMap);
                    map = localMap;
                }

                // is this just a retrieval of one generated id member?
                var mex = selector.Body as MemberExpression;
                if (mex != null)
                {
                    var id = mapping.Get(mex.Member);
                    if (id != null && id.IsPrimaryKey && id.IsGenerated)
                    {
                        if (genIdCommand != null)
                        {
                            // just use the select from the genIdCommand
                            return new ProjectionExpression(
                                genIdCommand.Source,
                                new ColumnExpression(mex.Type, genIdCommand.Variables[0].SqlType, genIdCommand.Source.Alias, genIdCommand.Source.Columns[0].Name),
                                aggregator
                                );
                        }
                        else
                        {
                            TableAlias alias = new TableAlias();
                            var colType = id.SqlType;
                            return new ProjectionExpression(
                                new SelectExpression(alias, new[] { new ColumnDeclaration("", map[mex.Member], colType) }, null, null),
                                new ColumnExpression(mex.Member.GetMemberType(), colType, alias, ""),
                                aggregator
                                );
                        }
                    }

                    where = generatedIds.Select((m, i) =>
                        this.GetMemberExpression(tex, mapping, m.Member).Equal(map[m.Member])
                        ).Aggregate((x, y) => x.And(y));
                }
            }
            else
            {
                where = this.GetIdentityCheck(tex, mapping, instance);
            }

            Expression typeProjector = this.GetEntityExpression(tex, mapping);
            Expression selection = DbExpressionReplacer.Replace(selector.Body, selector.Parameters[0], typeProjector);
            TableAlias newAlias = new TableAlias();
            var pc = ColumnProjector.ProjectColumns(selection, null, newAlias, tableAlias);
            var pe = new ProjectionExpression(
                new SelectExpression(newAlias, pc.Columns, tex, where),
                pc.Projector,
                aggregator
                );

            if (genIdCommand != null)
            {
                return new BlockCommand(genIdCommand, pe);
            }
            return pe;
        }

        internal DeclarationCommand GetGeneratedIdCommand(IEntityMapping mapping, List<IMemberMapping> members, Dictionary<MemberInfo, Expression> map)
        {
            var columns = new List<ColumnDeclaration>();
            var decls = new List<VariableDeclaration>();
            var alias = new TableAlias();
            foreach (var member in members)
            {
                Expression genId = this.GetGeneratedIdExpression(member);
                var name = member.Member.Name;
                var colType = member.SqlType;
                columns.Add(new ColumnDeclaration(name, genId, colType));
                decls.Add(new VariableDeclaration(name, colType, new ColumnExpression(genId.Type, colType, alias, name)));
                if (map != null)
                {
                    var vex = new VariableExpression(name, member.MemberType, colType);
                    map.Add(member.Member, vex);
                }
            }
            var select = new SelectExpression(alias, columns, null, null);
            return new DeclarationCommand(decls, select);
        }

        internal Expression GetIdentityCheck(Expression root, IEntityMapping mapping, Expression instance)
        {
            var instanceType = instance.Type;
            if (instanceType == mapping.EntityType)
                return mapping.PrimaryKeys
                .Select(m => this.GetMemberExpression(root, mapping, m).Equal(Expression.MakeMemberAccess(instance, m.Member)))
                .Aggregate((x, y) => x.And(y));

            Expression r = null;
            if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
            {
                var dic = (instance as ConstantExpression).Value as IDictionary;
                var keys = dic.Keys.OfType<string>();

                foreach (var m in mapping.PrimaryKeys)
                {
                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                    if (k != null)
                    {
                        var c = this.GetMemberExpression(root, mapping, m.Member).Equal(Expression.Constant(Converter.Convert(dic[k], m.MemberType), m.MemberType));
                        if (r != null)
                            r = r.And(c);
                        else
                            r = c;
                    }
                }
                return r;

            }

            if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
            {
                var dic = (instance as ConstantExpression).Value;
                var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
                var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();

                foreach (var m in mapping.PrimaryKeys)
                {
                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                    if (k != null)
                    {
                        var c = this.GetMemberExpression(root, mapping, m.Member).Equal(Expression.Constant(Converter.Convert(getValueMethod(dic, k), m.MemberType), m.MemberType));
                        if (r != null)
                            r = r.And(c);
                        else
                            r = c;
                    }
                }
                return r;
            }

            if (instanceType == typeof(NameValueCollection))
            {
                var dic = (instance as ConstantExpression).Value as NameValueCollection;
                var keys = dic.AllKeys;

                foreach (var m in mapping.PrimaryKeys)
                {
                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                    if (k != null)
                    {
                        var c = this.GetMemberExpression(root, mapping, m.Member).Equal(Expression.Constant(Converter.Convert(dic[k], m.MemberType), m.MemberType));
                        if (r != null)
                            r = r.And(c);
                        else
                            r = c;
                    }
                }

                return r;
            }

            var ms = instanceType.GetMembers();
            var expressions = mapping.Members.Where(m => m.IsPrimaryKey)
                .Where(m => ms.Exists(p => p.Name == m.Member.Name))
                .Select(m => this.GetMemberExpression(root, mapping, m).Equal(Expression.MakeMemberAccess(instance, ms.FirstOrDefault(p => p.Name == m.Member.Name))))
                .ToArray();

            if (expressions.Length == 0)
                return null;
            return expressions.Aggregate((x, y) => x.And(y));
        }

        internal object GetVersionValue(IEntityMapping mapping, Expression instance)
        {
            var m = mapping.Version;
            var c = instance as ConstantExpression;
            if (c == null) return null;

            var instanceType = instance.Type;
            if (instanceType == mapping.EntityType)
                return m.GetValue(c.Value);

            if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
            {
                var dic = c.Value as IDictionary;
                var keys = dic.Keys.OfType<string>();

                var k = keys.FirstOrDefault(p => string.Equals(p, mapping.Version.Member.Name, StringComparison.OrdinalIgnoreCase));
                if (k != null)
                    return Converter.Convert(dic[k], m.MemberType);
            }

            if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
            {
                var dic = c.Value;
                var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
                var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();
                var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                if (k != null)
                    return Converter.Convert(getValueMethod(dic, k), m.MemberType);
            }

            if (instanceType == typeof(NameValueCollection))
            {
                var dic = c.Value as NameValueCollection;
                var keys = dic.AllKeys;
                var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                if (k != null)
                    return Converter.Convert(dic[k], m.MemberType);
            }

            var ms = instanceType.GetMembers();
            return ms.Where(p => p.Name == m.Member.Name)
                .Select(p => Converter.Convert(p.Get(c.Value), m.MemberType))
                .FirstOrDefault();
        }

        internal Expression GetEntityExistsTest(IEntityMapping entity, Expression instance)
        {
            ProjectionExpression tq = this.GetQueryExpression(entity);
            Expression where = this.GetIdentityCheck(tq.Select, entity, instance);
            return new ExistsExpression(new SelectExpression(new TableAlias(), null, tq.Select, where));
        }

        internal Expression GetEntityStateTest(IEntityMapping mapping, Expression instance, LambdaExpression updateCheck)
        {
            ProjectionExpression tq = this.GetQueryExpression(mapping);
            Expression where = this.GetIdentityCheck(tq.Select, mapping, instance);
            Expression check = DbExpressionReplacer.Replace(updateCheck.Body, updateCheck.Parameters[0], tq.Projector);
            where = where.And(check);
            return new ExistsExpression(new SelectExpression(new TableAlias(), null, tq.Select, where));
        }

        internal Expression GetUpdateResult(IEntityMapping mapping, Expression instance, LambdaExpression selector)
        {
            var tq = this.GetQueryExpression(mapping);
            Expression where = this.GetIdentityCheck(tq.Select, mapping, instance);
            Expression selection = DbExpressionReplacer.Replace(selector.Body, selector.Parameters[0], tq.Projector);
            TableAlias newAlias = new TableAlias();
            var pc = ColumnProjector.ProjectColumns(selection, null, newAlias, tq.Select.Alias);
            return new ProjectionExpression(
                new SelectExpression(newAlias, pc.Columns, tq.Select, where),
                pc.Projector,
                Aggregator.GetAggregator(selector.Body.Type, typeof(IEnumerable<>).MakeGenericType(selector.Body.Type))
                );
        }

        public virtual Expression BuildEntityExpression(IEntityMapping mapping, IList<EntityAssignment> assignments)
        {
            NewExpression newExpression;

            // handle cases where members are not directly assignable
            EntityAssignment[] readonlyMembers = assignments.Where(b => b.MemberMapping != null)
                .Where(b => (b.MemberMapping as MemberMapping).setter == null)
                .ToArray();
            ConstructorInfo[] cons = mapping.EntityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            bool hasNoArgConstructor = cons.Any(c => c.GetParameters().Length == 0);

            if (readonlyMembers.Length > 0 || !hasNoArgConstructor)
            {
                var consThatApply = cons
                                        .Select(c => this.BindConstructor(c, readonlyMembers))
                                        .Where(cbr => cbr != null && cbr.Remaining.Length == 0)
                                        .ToList();
                if (consThatApply.Count == 0)
                    throw new InvalidOperationException(string.Format(Res.ConstructTypeInvalid, mapping.EntityType));

                // just use the first one... (Note: need better algorithm. :-)
                if (readonlyMembers.Length == assignments.Count)
                    return consThatApply[0].Expression;
                var r = this.BindConstructor(consThatApply[0].Expression.Constructor, assignments);

                newExpression = r.Expression;
                assignments = r.Remaining;
            }
            else
                newExpression = Expression.New(mapping.EntityType);

            Expression result;
            if (assignments.Count > 0)
            {
                if (mapping.EntityType.IsInterface)
                    assignments = this.MapAssignments(assignments, mapping.EntityType).ToList();
                var memberBindings = new List<MemberBinding>();
                foreach (var a in assignments)
                {
                    try
                    {
                        memberBindings.Add(Expression.Bind(a.Member, a.Expression));
                    }
                    catch
                    {
                        throw;
                    }
                }
                result = Expression.MemberInit(newExpression, memberBindings.ToArray());
            }
            else
                result = newExpression;

            if (mapping.EntityType != mapping.EntityType)
                result = Expression.Convert(result, mapping.EntityType);
            return result;
        }

        internal ConstructorBindResult BindConstructor(ConstructorInfo cons, IList<EntityAssignment> assignments)
        {
            var ps = cons.GetParameters();
            var args = new Expression[ps.Length];
            var mis = new MemberInfo[ps.Length];
            HashSet<EntityAssignment> members = new HashSet<EntityAssignment>(assignments);
            HashSet<EntityAssignment> used = new HashSet<EntityAssignment>();

            for (int i = 0, n = ps.Length; i < n; i++)
            {
                ParameterInfo p = ps[i];
                var assignment = members.FirstOrDefault(a =>
                    p.Name == a.Member.Name
                    && p.ParameterType.IsAssignableFrom(a.Expression.Type));
                if (assignment == null)
                {
                    assignment = members.FirstOrDefault(a =>
                        string.Compare(p.Name, a.Member.Name, true) == 0
                        && p.ParameterType.IsAssignableFrom(a.Expression.Type));
                }
                if (assignment != null)
                {
                    args[i] = assignment.Expression;
                    if (mis != null)
                        mis[i] = assignment.Member;
                    used.Add(assignment);
                }
                else
                {
                    MemberInfo[] mems = cons.DeclaringType.GetMember(p.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (mems != null && mems.Length > 0)
                    {
                        args[i] = Expression.Constant(UWay.Skynet.Cloud.Reflection.TypeHelper.GetDefault(p.ParameterType), p.ParameterType);
                        mis[i] = mems[0];
                    }
                    else
                    {
                        // unknown parameter, does not match any member
                        return null;
                    }
                }
            }

            members.ExceptWith(used);

            return new ConstructorBindResult(Expression.New(cons, args, mis), members);
        }

        public virtual IEnumerable<EntityAssignment> GetAssignments(Expression newOrMemberInit)
        {
            var assignments = new List<EntityAssignment>();
            var minit = newOrMemberInit as MemberInitExpression;
            if (minit != null)
            {
                assignments.AddRange(minit.Bindings.OfType<MemberAssignment>().Select(a => new EntityAssignment(a.Member, a.Expression)));
                newOrMemberInit = minit.NewExpression;
            }
            var nex = newOrMemberInit as NewExpression;
            if (nex != null && nex.Members != null)
            {
                assignments.AddRange(
                    Enumerable.Range(0, nex.Arguments.Count)
                              .Where(i => nex.Members[i] != null)
                              .Select(i => new EntityAssignment(nex.Members[i], nex.Arguments[i]))
                              );
            }
            return assignments;
        }

        internal IEnumerable<EntityAssignment> MapAssignments(IEnumerable<EntityAssignment> assignments, Type entityType)
        {
            foreach (var assign in assignments)
            {
                MemberInfo[] members = entityType.GetMember(assign.Member.Name, BindingFlags.Instance | BindingFlags.Public);
                if (members != null && members.Length > 0)
                    yield return new EntityAssignment(members[0], assign.Expression);
                else
                    yield return assign;
            }
        }

        internal IEnumerable<ColumnAssignment> GetColumnAssignments(
           Expression table, Expression instance, IEntityMapping mapping,
           Func<IEntityMapping, MemberInfo, bool> fnIncludeColumn,
           Dictionary<MemberInfo, Expression> map)
        {
            foreach (var m in mapping.Members)
            {
                if (m.IsColumn && fnIncludeColumn(mapping, m.Member))
                {
                    yield return new ColumnAssignment(
                        (ColumnExpression)this.GetMemberExpression(table, mapping, m),
                        this.GetMemberAccess(instance, m.Member, map)
                        );
                }
            }
        }

        internal Expression GetMemberAccess(Expression instance, MemberInfo member, Dictionary<MemberInfo, Expression> map)
        {
            Expression exp;
            if (map == null || !map.TryGetValue(member, out exp))
            {
                exp = Expression.MakeMemberAccess(instance, member);
            }
            return exp;
        }

        internal void GetColumns(IEntityMapping mapping, Dictionary<string, TableAlias> aliases, List<ColumnDeclaration> columns)
        {
            foreach (var mi in mapping.Members)
            {
                if (!mi.IsRelationship && mi.IsColumn)
                {
                    string name = mi.ColumnName;
                    string aliasName = mi.AliasName;
                    TableAlias alias;
                    aliases.TryGetValue(aliasName, out alias);
                    var colType = mi.SqlType;
                    ColumnExpression ce = new ColumnExpression(mi.MemberType, colType, alias, name);
                    ColumnDeclaration cd = new ColumnDeclaration(name, ce, colType);
                    columns.Add(cd);
                }
            }
        }

        internal List<ColumnAssignment> GetColumnAssignments(Expression table, Expression instance, IEntityMapping mapping, Func<IMemberMapping, bool> fnIncludeColumn)
        {
            var items = new List<ColumnAssignment>();
            var instanceType = instance.Type;
            if (instanceType == mapping.EntityType)
            {
                foreach (var m in mapping.Members)
                {
                    if (m.IsColumn && fnIncludeColumn(m))
                    {
                        items.Add(new ColumnAssignment(
                            (ColumnExpression)this.GetMemberExpression(table, mapping, m.Member),
                            Expression.MakeMemberAccess(instance, m.Member)
                            ));
                    }
                }
            }
            else
            {
                if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
                {
                    var dic = (instance as ConstantExpression).Value as IDictionary;
                    var keys = dic.Keys.OfType<string>();

                    foreach (var m in mapping.Members)
                    {
                        var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                        if (k != null && m.IsColumn && fnIncludeColumn(m))
                        {
                            items.Add(new ColumnAssignment(
                                (ColumnExpression)this.GetMemberExpression(table, mapping, m.Member),
                                 Expression.Constant(Converter.Convert(dic[k], m.MemberType), m.MemberType)
                                ));
                        }
                    }

                }
                else if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
                {
                    var dic = (instance as ConstantExpression).Value;
                    var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
                    var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();
                    foreach (var m in mapping.Members)
                    {
                        var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                        if (k != null && m.IsColumn && fnIncludeColumn(m))
                        {
                            items.Add(new ColumnAssignment(
                                (ColumnExpression)this.GetMemberExpression(table, mapping, m.Member),
                                Expression.Constant(Converter.Convert(getValueMethod(dic, k), m.MemberType), m.MemberType)));
                        }
                    }
                }
                else if (instanceType == typeof(NameValueCollection))
                {
                    var dic = (instance as ConstantExpression).Value as NameValueCollection;
                    var keys = dic.AllKeys;

                    foreach (var m in mapping.Members)
                    {
                        var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                        if (k != null && m.IsColumn && fnIncludeColumn(m))
                        {
                            items.Add(new ColumnAssignment(
                                (ColumnExpression)this.GetMemberExpression(table, mapping, m.Member),
                                Expression.Constant(Converter.Convert(dic[k], m.MemberType), m.MemberType)
                                ));
                        }
                    }
                }
                else
                {
                    var members = instanceType.GetMembers();
                    foreach (var m in mapping.Members)
                    {
                        var m2 = members.FirstOrDefault(p => string.Equals(p.Name, m.Member.Name, StringComparison.OrdinalIgnoreCase));
                        if (m2 != null && m.IsColumn && fnIncludeColumn(m))
                        {
                            items.Add(new ColumnAssignment(
                                (ColumnExpression)this.GetMemberExpression(table, mapping, m.Member),
                                Expression.MakeMemberAccess(instance, m2)
                                ));
                        }
                    }
                }
            }

            return items;

        }


        public virtual Expression Translate(Expression expression)
        {
            expression = UnusedColumnRemover.Remove(expression);
            expression = RedundantColumnRemover.Remove(expression);
            expression = RedundantSubqueryRemover.Remove(expression);

            var rewritten = CrossApplyRewriter.Rewrite(expression);

            rewritten = CrossJoinRewriter.Rewrite(rewritten);

            if (rewritten != expression)
            {
                expression = rewritten;
                expression = UnusedColumnRemover.Remove(expression);
                expression = RedundantSubqueryRemover.Remove(expression);
                expression = RedundantJoinRemover.Remove(expression);
                expression = RedundantColumnRemover.Remove(expression);
            }

            return expression;
        }
    }
}
