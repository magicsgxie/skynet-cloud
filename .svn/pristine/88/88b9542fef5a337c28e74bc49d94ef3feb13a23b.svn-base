using System.Linq;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Common;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    class ExpressionHelper
    {
        internal static bool CanBeEvaluatedLocally(Expression expression)
        {
            ConstantExpression cex = expression as ConstantExpression;
            if (cex != null)
            {
                IQueryable query = cex.Value as IQueryable;
                if (query != null && typeof(IDbContext).IsAssignableFrom(query.Provider.GetType()))
                    return false;
            }
            MethodCallExpression m = expression as MethodCallExpression;
            if (m != null)
            {
                if ((m.Method.DeclaringType == typeof(Enumerable) ||
                 m.Method.DeclaringType == typeof(Queryable) ||
                 m.Method.DeclaringType == typeof(DbSet))
                 )
                    return false;
                if (MethodMapping.Mappings.ContainsKey(m.Method))
                    return false;
                if (m.Method.DeclaringType == typeof(SqlFunctions))
                    return false;
            }

            var member = expression as MemberExpression;
            if (member != null)
            {
                if (MethodMapping.Mappings.ContainsKey(member.Member))
                    return false;
                if (member.Member.DeclaringType == typeof(SqlFunctions))
                    return false;
            }


            if (expression.NodeType == ExpressionType.Convert &&
                expression.Type == typeof(object))
                return true;
            return expression.NodeType != ExpressionType.Parameter &&
                   expression.NodeType != ExpressionType.Lambda;
        }

    }
    //internal class EntityExpressionHelper
    //{
    //    internal Dialect.Dialect Dialect;
    //    internal IDbContext dbContext;
    //    DbConfiguration dbConfiguration;
    //    internal EntityExpressionHelper(IDbContext dbContext)
    //    {
    //        this.dbConfiguration = dbContext.DbConfiguration;
    //        this.Dialect = dbConfiguration.Dialect;
    //        this.dbContext = dbContext;
    //    }

    //    private static readonly char[] dotSeparator = new char[] { '.' };
    //    const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase;

    //    internal ProjectionExpression GetQueryExpression(IEntityModel entity)
    //    {
    //        Expression projector;
    //        TableAlias selectAlias;
    //        ProjectedColumns pc;
    //        ProjectionExpression proj;

    //        var tableAlias = new TableAlias();
    //        selectAlias = new TableAlias();
    //        var table = new TableExpression(tableAlias, entity, entity.TableName);

    //        projector = this.GetEntityExpression(table, entity);
    //        pc = ColumnProjector.ProjectColumns(this.Dialect, projector, null, selectAlias, tableAlias);

    //        proj = new ProjectionExpression(
    //            new SelectExpression(selectAlias, pc.Columns, table, null),
    //            pc.Projector
    //            );

    //        return (ProjectionExpression)ApplyPolicy(proj, entity.EntityType);

    //    }

    //    internal Expression GetMemberExpression(Expression root, IEntityModel entity, MemberInfo member)
    //    {
    //        return GetMemberExpression(root, entity, entity.Get(member));
    //    }

    //    internal Expression GetMemberExpression(Expression root, IEntityModel entity, IMemberModel mm)
    //    {
    //        var m = mm.Member;
    //        if (mm.IsRelationship)
    //        {
    //            IEntityModel relatedEntity = mm.RelatedEntity;
    //            ProjectionExpression projection = this.GetQueryExpression(relatedEntity);

    //            // make where clause for joining back to 'root'
    //            var thisKeyMembers = mm.ThisKeyMembers;
    //            var otherKeyMembers = mm.OtherKeyMembers;

    //            Expression where = null;
    //            for (int i = 0, n = otherKeyMembers.Length; i < n; i++)
    //            {
    //                Expression equal =
    //                    this.GetMemberExpression(projection.Projector, relatedEntity, otherKeyMembers[i]).Equal(
    //                        this.GetMemberExpression(root, entity, thisKeyMembers[i])
    //                    );
    //                where = (where != null) ? where.And(equal) : equal;
    //            }

    //            TableAlias newAlias = new TableAlias();
    //            var pc = ColumnProjector.ProjectColumns(Dialect, projection.Projector, null, newAlias, projection.Select.Alias);

    //            LambdaExpression aggregator = Aggregator.GetAggregator(mm.MemberType, typeof(IEnumerable<>).MakeGenericType(pc.Projector.Type));
    //            var result = new ProjectionExpression(
    //                new SelectExpression(newAlias, pc.Columns, projection.Select, where),
    //                pc.Projector, aggregator
    //                );

    //            return ApplyPolicy(result, m);
    //        }
    //        else
    //        {
    //            AliasedExpression aliasedRoot = root as AliasedExpression;
    //            if (aliasedRoot != null && mm.IsColumn)
    //            {
    //                return new ColumnExpression(mm.MemberType, mm.SqlType, aliasedRoot.Alias, mm.ColumnName);
    //            }
    //            return QueryBinder.BindMember(root, m);
    //        }
    //    }

    //     internal Expression GetPrimaryKeyQuery(IEntityModel entity, Expression source, Expression[] keys)
    //    {
    //        ParameterExpression p = Expression.Parameter(entity.EntityType, "p");
    //        Expression pred = null;

    //        if (entity.PrimaryKeys.Length != keys.Length)
    //            throw new InvalidOperationException("Incorrect number of primary key values");
    //        for (int i = 0, n = keys.Length; i < n; i++)
    //        {
    //            MemberInfo mem = entity.PrimaryKeys[i].Member;
    //            Type memberType = mem.GetMemberType();
    //            if (keys[i] != null && UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetNonNullableType(keys[i].Type) != UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetNonNullableType(memberType))
    //                throw new InvalidOperationException("Primary key value is wrong type");
    //            Expression eq = Expression.MakeMemberAccess(p, mem).Equal(keys[i]);
    //            pred = (pred == null) ? eq : pred.And(eq);
    //        }
    //        var predLambda = Expression.Lambda(pred, p);

    //        return Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { entity.EntityType }, source, predLambda);
    //    }

    //    internal Expression GetInsertExpression(IEntityModel entity, Expression instance, LambdaExpression selector)
    //    {
    //        var tableAlias = new TableAlias();
    //        var table = new TableExpression(tableAlias, entity, entity.TableName);
    //        var assignments = this.GetColumnAssignments(table, instance, entity, m => !m.IsGenerated && !m.IsVersion).ToArray();

    //        object o = null;
    //        var c = instance as ConstantExpression;
    //        if (c != null)
    //            o = c.Value;

    //        if (selector != null)
    //        {
    //            return new BlockCommand(
    //                new InsertCommand(table, assignments,o),
    //                this.GetInsertResult(entity, instance, selector, null)
    //                );
    //        }

    //        return new InsertCommand(table, assignments,o);
    //    }


    //    internal Expression GetUpdateExpression(IEntityModel entity, Expression instance, LambdaExpression updateCheck, LambdaExpression selector, Expression @else)
    //    {
    //        var tableAlias = new TableAlias();
    //        var table = new TableExpression(tableAlias, entity, entity.TableName);

    //        var where = this.GetIdentityCheck(table, entity, instance);
    //        if (updateCheck != null)
    //        {
    //            Expression typeProjector = this.GetEntityExpression(table, entity);
    //            Expression pred = DbExpressionReplacer.Replace(updateCheck.Body, updateCheck.Parameters[0], typeProjector);
    //            where = where != null ? where.And(pred) : pred;
    //        }

    //        var assignments = this.GetColumnAssignments(table, instance, entity, m => m.IsUpdatable && !m.IsVersion);

    //        var version = entity.Version;
    //        bool supportsVersionCheck = false;


    //        if (version != null)
    //        {
    //            var versionValue = GetVersionValue(entity, instance);
    //            if (versionValue != null)
    //            {
    //                var versionCheck = GetMemberExpression(table, entity, entity.Version).Equal(Expression.Constant(versionValue));
    //                where = (where != null) ? where.And(versionCheck) : versionCheck;

    //                var versionAssignment = new ColumnAssignment(
    //                       (ColumnExpression)this.GetMemberExpression(table, entity, version.Member),
    //                       Expression.Add(Expression.Constant(versionValue), Expression.Constant(1, version.MemberType))
    //                       );
    //                assignments.Add(versionAssignment);
    //                supportsVersionCheck = true;
    //            }
    //        }

    //        object o = null;
    //        var c = instance as ConstantExpression;
    //        if (c != null)
    //            o = c.Value;
    //        Expression update = new UpdateCommand(table, where,o, supportsVersionCheck, assignments);

    //        if (selector != null)
    //        {
    //            return new BlockCommand(
    //                update,
    //                new IFCommand(
    //                    this.Dialect.GetRowsAffectedExpression(update).GreaterThan(Expression.Constant(0)),
    //                    this.GetUpdateResult(entity, instance, selector),
    //                    @else
    //                    )
    //                );
    //        }
    //        else if (@else != null)
    //        {
    //            return new BlockCommand(
    //                update,
    //                new IFCommand(
    //                    this.Dialect.GetRowsAffectedExpression(update).LessThanOrEqual(Expression.Constant(0)),
    //                    @else,
    //                    null
    //                    )
    //                );
    //        }
    //        else
    //        {
    //            return update;
    //        }
    //    }

    //    internal Expression GetDeleteExpression(IEntityModel entity, Expression instance, LambdaExpression deleteCheck)
    //    {
    //        TableExpression table = new TableExpression(new TableAlias(), entity, entity.TableName);
    //        Expression where = null;

    //        if (instance != null)
    //            where = this.GetIdentityCheck(table, entity, instance);

    //        if (deleteCheck != null)
    //        {
    //            Expression row = this.GetEntityExpression(table, entity);
    //            Expression pred = DbExpressionReplacer.Replace(deleteCheck.Body, deleteCheck.Parameters[0], row);
    //            where = (where != null) ? where.And(pred) : pred;
    //        }

    //        bool supportsVersionCheck = false;
    //        if (entity.Version != null)
    //        {
    //            var versionValue = GetVersionValue(entity, instance);
    //            if (versionValue != null)
    //            {
    //                var versionCheck = GetMemberExpression(table, entity, entity.Version).Equal(Expression.Constant(versionValue));
    //                where = (where != null) ? where.And(versionCheck) : versionCheck;
    //                supportsVersionCheck = true;
    //            }

    //        }

    //        object o = null;
    //        var c = instance as ConstantExpression;
    //        if (c != null)
    //            o = c.Value;
    //        return new DeleteCommand(table, where, o, supportsVersionCheck);
    //    }


    //    Expression ApplyPolicy(Expression expression, MemberInfo member)
    //    {
    //        List<LambdaExpression> ops;
    //        if ((this.dbContext as DbContext) .Operations.TryGetValue(member, out ops))
    //        {
    //            var result = expression;
    //            foreach (var fnOp in ops)
    //            {
    //                var pop = PartialEvaluator.Eval(fnOp, ExpressionHelper.CanBeEvaluatedLocally);
    //                result = QueryBinder.Bind(this,dbContext, Expression.Invoke(pop, result));
    //            }
    //            var projection = (ProjectionExpression)result;
    //            if (projection.Type != expression.Type)
    //            {
    //                var fnAgg = Aggregator.GetAggregator(expression.Type, projection.Type);
    //                projection = new ProjectionExpression(projection.Select, projection.Projector, fnAgg);
    //            }
    //            return projection;
    //        }
    //        return expression;
    //    }


    //    EntityExpression GetEntityExpression(Expression root, IEntityModel entity)
    //    {
    //        var assignments = new List<EntityAssignment>();
    //        foreach (var mi in entity.Members)
    //        {
    //            if (!mi.IsRelationship)
    //            {
    //                var me = this.GetMemberExpression(root, entity, mi);
    //                if (me != null)
    //                    assignments.Add(new EntityAssignment(mi.Member, me));
    //            }
    //        }

    //        return new EntityExpression(entity, this.BuildEntityExpression(entity, assignments));
    //    }

    //    Expression GetInsertResult(IEntityModel entity, Expression instance, LambdaExpression selector, Dictionary<MemberInfo, Expression> map)
    //    {
    //        var tableAlias = new TableAlias();
    //        var tex = new TableExpression(tableAlias, entity, entity.TableName);
    //        var aggregator = Aggregator.GetAggregator(selector.Body.Type, typeof(IEnumerable<>).MakeGenericType(selector.Body.Type));

    //        Expression where = null;
    //        DeclarationCommand genIdCommand = null;
    //        var generatedIds = entity.PrimaryKeys.Where(m => m.IsPrimaryKey && m.IsGenerated).ToList();
    //        if (generatedIds.Count > 0)
    //        {
    //            if (map == null || !generatedIds.Any(m => map.ContainsKey(m.Member)))
    //            {
    //                var localMap = new Dictionary<MemberInfo, Expression>();
    //                genIdCommand = this.GetGeneratedIdCommand(entity, generatedIds, localMap);
    //                map = localMap;
    //            }

    //            // is this just a retrieval of one generated id member?
    //            var mex = selector.Body as MemberExpression;
    //            if (mex != null)
    //            {
    //                var id = entity.Get(mex.Member);
    //                if (id != null && id.IsPrimaryKey && id.IsGenerated)
    //                {
    //                    if (genIdCommand != null)
    //                    {
    //                        // just use the select from the genIdCommand
    //                        return new ProjectionExpression(
    //                            genIdCommand.Source,
    //                            new ColumnExpression(mex.Type, genIdCommand.Variables[0].SqlType, genIdCommand.Source.Alias, genIdCommand.Source.Columns[0].Name),
    //                            aggregator
    //                            );
    //                    }
    //                    else
    //                    {
    //                        TableAlias alias = new TableAlias();
    //                        var colType = id.SqlType;
    //                        return new ProjectionExpression(
    //                            new SelectExpression(alias, new[] { new ColumnDeclaration("", map[mex.Member], colType) }, null, null),
    //                            new ColumnExpression(UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetMemberType(mex.Member), colType, alias, ""),
    //                            aggregator
    //                            );
    //                    }
    //                }

    //                where = generatedIds.Select((m, i) =>
    //                    this.GetMemberExpression(tex, entity, m.Member).Equal(map[m.Member])
    //                    ).Aggregate((x, y) => x.And(y));
    //            }
    //        }
    //        else
    //        {
    //            where = this.GetIdentityCheck(tex, entity, instance);
    //        }

    //        Expression typeProjector = this.GetEntityExpression(tex, entity);
    //        Expression selection = DbExpressionReplacer.Replace(selector.Body, selector.Parameters[0], typeProjector);
    //        TableAlias newAlias = new TableAlias();
    //        var pc = ColumnProjector.ProjectColumns(this.Dialect, selection, null, newAlias, tableAlias);
    //        var pe = new ProjectionExpression(
    //            new SelectExpression(newAlias, pc.Columns, tex, where),
    //            pc.Projector,
    //            aggregator
    //            );

    //        if (genIdCommand != null)
    //        {
    //            return new BlockCommand(genIdCommand, pe);
    //        }
    //        return pe;
    //    }

    //    DeclarationCommand GetGeneratedIdCommand(IEntityModel entity, List<IMemberModel> members, Dictionary<MemberInfo, Expression> map)
    //    {
    //        var columns = new List<ColumnDeclaration>();
    //        var decls = new List<VariableDeclaration>();
    //        var alias = new TableAlias();
    //        foreach (var member in members)
    //        {
    //            Expression genId = this.Dialect.GetGeneratedIdExpression(member);
    //            var name = member.Member.Name;
    //            var colType = member.SqlType;
    //            columns.Add(new ColumnDeclaration(name, genId, colType));
    //            decls.Add(new VariableDeclaration(name, colType, new ColumnExpression(genId.Type, colType, alias, name)));
    //            if (map != null)
    //            {
    //                var vex = new VariableExpression(name, member.MemberType, colType);
    //                map.Add(member.Member, vex);
    //            }
    //        }
    //        var select = new SelectExpression(alias, columns, null, null);
    //        return new DeclarationCommand(decls, select);
    //    }

    //    Expression GetIdentityCheck(Expression root, IEntityModel entity, Expression instance)
    //    {
    //        var instanceType = instance.Type;
    //        if (instanceType == entity.EntityType)
    //            return entity.PrimaryKeys
    //            .Select(m => this.GetMemberExpression(root, entity, m).Equal(Expression.MakeMemberAccess(instance, m.Member)))
    //            .Aggregate((x, y) => x.And(y));

    //        Expression r = null;
    //        if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
    //        {
    //            var dic = (instance as ConstantExpression).Value as IDictionary;
    //            var keys = dic.Keys.OfType<string>();

    //            foreach (var m in entity.PrimaryKeys)
    //            {
    //                var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                if (k != null )
    //                {
    //                    var c = this.GetMemberExpression(root, entity, m.Member).Equal(Expression.Constant(PrimitiveMapper.Map(dic[k], m.MemberType), m.MemberType));
    //                    if (r != null)
    //                        r = r.And(c);
    //                    else
    //                        r = c;
    //                }
    //            }
    //            return r;

    //        }

    //        if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
    //        {
    //            var dic = (instance as ConstantExpression).Value;
    //            var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
    //            var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();

    //            foreach (var m in entity.PrimaryKeys)
    //            {
    //                var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                if (k != null)
    //                {
    //                    var c = this.GetMemberExpression(root, entity, m.Member).Equal(Expression.Constant(PrimitiveMapper.Map( getValueMethod(dic, k),m.MemberType),m.MemberType));
    //                    if (r != null)
    //                        r = r.And(c);
    //                    else
    //                        r = c;
    //                }
    //            }
    //            return r;
    //        }

    //        if (instanceType == typeof(NameValueCollection))
    //        {
    //            var dic = (instance as ConstantExpression).Value as NameValueCollection;
    //            var keys = dic.AllKeys;

    //            foreach (var m in entity.PrimaryKeys)
    //            {
    //                var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                if (k != null)
    //                {
    //                    var c = this.GetMemberExpression(root, entity, m.Member).Equal(Expression.Constant(PrimitiveMapper.Map( dic[k],m.MemberType),m.MemberType));
    //                    if (r != null)
    //                        r = r.And(c);
    //                    else
    //                        r = c;
    //                }
    //            }

    //            return r;
    //        }

    //        var ms = instanceType.GetMembers();
    //        var expressions = entity.Members.Where(m=>m.IsPrimaryKey)
    //            .Where(m=>ms.Exist(p=>p.Name == m.Member.Name))
    //            .Select(m => this.GetMemberExpression(root, entity, m).Equal(Expression.MakeMemberAccess(instance, ms.FirstOrDefault(p=>p.Name == m.Member.Name))))
    //            .ToArray();

    //        if(expressions.Length == 0)
    //            return null;
    //        return expressions.Aggregate((x, y) => x.And(y));
    //    }

    //    object GetVersionValue(IEntityModel entity, Expression instance)
    //    {
    //        var m = entity.Version;
    //        var c = instance as ConstantExpression;
    //        if (c == null) return null;

    //        var instanceType = instance.Type;
    //        if (instanceType == entity.EntityType)
    //            return m.GetValue(c.Value);

    //        if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
    //        {
    //            var dic = c.Value as IDictionary;
    //            var keys = dic.Keys.OfType<string>();

    //            var k = keys.FirstOrDefault(p => string.Equals(p, entity.Version.Member.Name, StringComparison.OrdinalIgnoreCase));
    //            if (k != null)
    //                return PrimitiveMapper.Map(dic[k], m.MemberType);
    //        }

    //        if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
    //        {
    //            var dic = c.Value;
    //            var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
    //            var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();
    //            var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //            if (k != null)
    //                return PrimitiveMapper.Map(getValueMethod(dic, k), m.MemberType);
    //        }

    //        if (instanceType == typeof(NameValueCollection))
    //        {
    //            var dic = c.Value as NameValueCollection;
    //            var keys = dic.AllKeys;
    //            var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //            if (k != null)
    //                return PrimitiveMapper.Map(dic[k], m.MemberType);
    //        }

    //        var ms = instanceType.GetMembers();
    //        return ms.Where(p => p.Name == m.Member.Name)
    //            .Select(p => PrimitiveMapper.Map(p.Get(c.Value), m.MemberType))
    //            .FirstOrDefault();
    //    }

    //    Expression GetEntityExistsTest(IEntityModel entity, Expression instance)
    //    {
    //        ProjectionExpression tq = this.GetQueryExpression(entity);
    //        Expression where = this.GetIdentityCheck(tq.Select, entity, instance);
    //        return new ExistsExpression(new SelectExpression(new TableAlias(), null, tq.Select, where));
    //    }

    //    Expression GetEntityStateTest(IEntityModel entity, Expression instance, LambdaExpression updateCheck)
    //    {
    //        ProjectionExpression tq = this.GetQueryExpression(entity);
    //        Expression where = this.GetIdentityCheck(tq.Select, entity, instance);
    //        Expression check = DbExpressionReplacer.Replace(updateCheck.Body, updateCheck.Parameters[0], tq.Projector);
    //        where = where.And(check);
    //        return new ExistsExpression(new SelectExpression(new TableAlias(), null, tq.Select, where));
    //    }

    //    Expression GetUpdateResult(IEntityModel entity, Expression instance, LambdaExpression selector)
    //    {
    //        var tq = this.GetQueryExpression(entity);
    //        Expression where = this.GetIdentityCheck(tq.Select, entity, instance);
    //        Expression selection = DbExpressionReplacer.Replace(selector.Body, selector.Parameters[0], tq.Projector);
    //        TableAlias newAlias = new TableAlias();
    //        var pc = ColumnProjector.ProjectColumns(this.Dialect, selection, null, newAlias, tq.Select.Alias);
    //        return new ProjectionExpression(
    //            new SelectExpression(newAlias, pc.Columns, tq.Select, where),
    //            pc.Projector,
    //            Aggregator.GetAggregator(selector.Body.Type, typeof(IEnumerable<>).MakeGenericType(selector.Body.Type))
    //            );
    //    }

    //    internal Expression BuildEntityExpression(IEntityModel entity, IList<EntityAssignment> assignments)
    //    {
    //        NewExpression newExpression;

    //        // handle cases where members are not directly assignable
    //        EntityAssignment[] readonlyMembers = assignments.Where(b => UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.IsReadOnly(b.Member)).ToArray();
    //        ConstructorInfo[] cons = entity.EntityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
    //        bool hasNoArgConstructor = cons.Any(c => c.GetParameters().Length == 0);

    //        if (readonlyMembers.Length > 0 || !hasNoArgConstructor)
    //        {
    //            var consThatApply = cons
    //                                    .Select(c => this.BindConstructor(c, readonlyMembers))
    //                                    .Where(cbr => cbr != null && cbr.Remaining.Length == 0)
    //                                    .ToList();
    //            if (consThatApply.Count == 0)
    //                throw new InvalidOperationException(string.Format("Cannot construct type '{0}' with all mapped includedMembers.", entity.EntityType));

    //            // just use the first one... (Note: need better algorithm. :-)
    //            if (readonlyMembers.Length == assignments.Count)
    //                return consThatApply[0].Expression;
    //            var r = this.BindConstructor(consThatApply[0].Expression.Constructor, assignments);

    //            newExpression = r.Expression;
    //            assignments = r.Remaining;
    //        }
    //        else
    //            newExpression = Expression.New(entity.EntityType);

    //        Expression result;
    //        if (assignments.Count > 0)
    //        {
    //            if (entity.EntityType.IsInterface)
    //                assignments = this.MapAssignments(assignments, entity.EntityType).ToList();
    //            result = Expression.MemberInit(newExpression, (MemberBinding[])assignments.Select(a => Expression.Bind(a.Member, a.Expression)).ToArray());
    //        }
    //        else
    //            result = newExpression;

    //        if (entity.EntityType != entity.EntityType)
    //            result = Expression.Convert(result, entity.EntityType);
    //        return result;
    //    }

    //    ConstructorBindResult BindConstructor(ConstructorInfo cons, IList<EntityAssignment> assignments)
    //    {
    //        var ps = cons.GetParameters();
    //        var args = new Expression[ps.Length];
    //        var mis = new MemberInfo[ps.Length];
    //        HashSet<EntityAssignment> members = new HashSet<EntityAssignment>(assignments);
    //        HashSet<EntityAssignment> used = new HashSet<EntityAssignment>();

    //        for (int i = 0, n = ps.Length; i < n; i++)
    //        {
    //            ParameterInfo p = ps[i];
    //            var assignment = members.FirstOrDefault(a =>
    //                p.Name == a.Member.Name
    //                && p.ParameterType.IsAssignableFrom(a.Expression.Type));
    //            if (assignment == null)
    //            {
    //                assignment = members.FirstOrDefault(a =>
    //                    string.Compare(p.Name, a.Member.Name, true) == 0
    //                    && p.ParameterType.IsAssignableFrom(a.Expression.Type));
    //            }
    //            if (assignment != null)
    //            {
    //                args[i] = assignment.Expression;
    //                if (mis != null)
    //                    mis[i] = assignment.Member;
    //                used.Add(assignment);
    //            }
    //            else
    //            {
    //                MemberInfo[] mems = cons.DeclaringType.GetMember(p.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
    //                if (mems != null && mems.Length > 0)
    //                {
    //                    args[i] = Expression.Constant(UWay.Skynet.Cloud.Data.Linq.Internal.ReflectionHelper.GetDefault(p.ParameterType), p.ParameterType);
    //                    mis[i] = mems[0];
    //                }
    //                else
    //                {
    //                    // unknown parameter, does not match any member
    //                    return null;
    //                }
    //            }
    //        }

    //        members.ExceptWith(used);

    //        return new ConstructorBindResult(Expression.New(cons, args, mis), members);
    //    }

    //    internal IEnumerable<EntityAssignment> GetAssignments(Expression newOrMemberInit)
    //    {
    //        var assignments = new List<EntityAssignment>();
    //        var minit = newOrMemberInit as MemberInitExpression;
    //        if (minit != null)
    //        {
    //            assignments.AddRange(minit.Bindings.OfType<MemberAssignment>().Select(a => new EntityAssignment(a.Member, a.Expression)));
    //            newOrMemberInit = minit.NewExpression;
    //        }
    //        var nex = newOrMemberInit as NewExpression;
    //        if (nex != null && nex.Members != null)
    //        {
    //            assignments.AddRange(
    //                Enumerable.Range(0, nex.Arguments.Count)
    //                          .Where(i => nex.Members[i] != null)
    //                          .Select(i => new EntityAssignment(nex.Members[i], nex.Arguments[i]))
    //                          );
    //        }
    //        return assignments;
    //    }

    //    IEnumerable<EntityAssignment> MapAssignments(IEnumerable<EntityAssignment> assignments, Type entityType)
    //    {
    //        foreach (var assign in assignments)
    //        {
    //            MemberInfo[] members = entityType.GetMember(assign.Member.Name, BindingFlags.Instance | BindingFlags.Public);
    //            if (members != null && members.Length > 0)
    //                yield return new EntityAssignment(members[0], assign.Expression);
    //            else
    //                yield return assign;
    //        }
    //    }

    //    IEnumerable<ColumnAssignment> GetColumnAssignments(
    //       Expression table, Expression instance, IEntityModel entity,
    //       Func<IEntityModel, MemberInfo, bool> fnIncludeColumn,
    //       Dictionary<MemberInfo, Expression> map)
    //    {
    //        foreach (var m in entity.Members)
    //        {
    //            if (m.IsColumn && fnIncludeColumn(entity, m.Member))
    //            {
    //                yield return new ColumnAssignment(
    //                    (ColumnExpression)this.GetMemberExpression(table, entity, m),
    //                    this.GetMemberAccess(instance, m.Member, map)
    //                    );
    //            }
    //        }
    //    }

    //    Expression GetMemberAccess(Expression instance, MemberInfo member, Dictionary<MemberInfo, Expression> map)
    //    {
    //        Expression exp;
    //        if (map == null || !map.TryGetValue(member, out exp))
    //        {
    //            exp = Expression.MakeMemberAccess(instance, member);
    //        }
    //        return exp;
    //    }

    //    void GetColumns(IEntityModel entity, Dictionary<string, TableAlias> aliases, List<ColumnDeclaration> columns)
    //    {
    //        foreach (var mi in entity.Members)
    //        {
    //            if (!mi.IsRelationship &&mi.IsColumn)
    //            {
    //                string name = mi.ColumnName;
    //                string aliasName = mi.AliasName;
    //                TableAlias alias;
    //                aliases.TryGetValue(aliasName, out alias);
    //                var colType = mi.SqlType;
    //                ColumnExpression ce = new ColumnExpression(mi.MemberType, colType, alias, name);
    //                ColumnDeclaration cd = new ColumnDeclaration(name, ce, colType);
    //                columns.Add(cd);
    //            }
    //        }
    //    }

    //    List<ColumnAssignment> GetColumnAssignments(Expression table, Expression instance, IEntityModel entity, Func<IMemberModel, bool> fnIncludeColumn)
    //    {
    //        var items = new List<ColumnAssignment>();
    //        var instanceType = instance.Type;
    //        if (instanceType == entity.EntityType)
    //        {
    //            foreach (var m in entity.Members)
    //            {
    //                if (m.IsColumn && fnIncludeColumn(m))
    //                {
    //                    items.Add( new ColumnAssignment(
    //                        (ColumnExpression)this.GetMemberExpression(table, entity, m.Member),
    //                        Expression.MakeMemberAccess(instance, m.Member)
    //                        ));
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (UWay.Skynet.Cloud.Reflection.TypeHelper.IsIDictionaryType(instanceType))
    //            {
    //                var dic = (instance as ConstantExpression).Value as IDictionary;
    //                var keys = dic.Keys.OfType<string>();

    //                foreach (var m in entity.Members)
    //                {
    //                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                    if (k != null && m.IsColumn && fnIncludeColumn(m))
    //                    {
    //                        items.Add(new ColumnAssignment(
    //                            (ColumnExpression)this.GetMemberExpression(table, entity, m.Member),
    //                             Expression.Constant(PrimitiveMapper.Map(dic[k], m.MemberType), m.MemberType)
    //                            ));
    //                    }
    //                }

    //            }
    //            else if(UWay.Skynet.Cloud.Reflection.TypeHelper.IsGenericDictionaryType(instanceType))
    //            {
    //                var dic = (instance as ConstantExpression).Value;
    //                var keys = (instanceType.GetProperty("Keys").GetGetter()(dic) as IEnumerable).Cast<string>();
    //                var getValueMethod = instanceType.GetMethod("get_Item").GetFunc();
    //                foreach (var m in entity.Members)
    //                {
    //                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                    if (k != null && m.IsColumn && fnIncludeColumn(m))
    //                    {
    //                        items.Add( new ColumnAssignment(
    //                            (ColumnExpression)this.GetMemberExpression(table, entity, m.Member),
    //                            Expression.Constant(PrimitiveMapper.Map(getValueMethod(dic,k),m.MemberType),m.MemberType)));
    //                    }
    //                }
    //            }
    //            else if (instanceType == typeof(NameValueCollection))
    //            {
    //                var dic = (instance as ConstantExpression).Value as NameValueCollection;
    //                var keys = dic.AllKeys;

    //                foreach (var m in entity.Members)
    //                {
    //                    var k = keys.FirstOrDefault(p => string.Equals(p, m.Member.Name, StringComparison.OrdinalIgnoreCase));
    //                    if (k != null && m.IsColumn && fnIncludeColumn(m))
    //                    {
    //                        items.Add(new ColumnAssignment(
    //                            (ColumnExpression)this.GetMemberExpression(table, entity, m.Member),
    //                            Expression.Constant(PrimitiveMapper.Map(dic[k], m.MemberType), m.MemberType)
    //                            ));
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                var members = instanceType.GetMembers();
    //                foreach (var m in entity.Members)
    //                {
    //                    var m2 = members.FirstOrDefault(p => p.Name == m.Member.Name);
    //                    if (m2 != null && m.IsColumn && fnIncludeColumn(m))
    //                    {
    //                        items.Add(new ColumnAssignment(
    //                            (ColumnExpression)this.GetMemberExpression(table, entity, m.Member),
    //                            Expression.MakeMemberAccess(instance, m2)
    //                            ));
    //                    }
    //                }
    //            }
    //        }

    //        return items;

    //    }
    //}
}
