// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Linq.Internal;
using UWay.Skynet.Cloud.Data.Mapping;
//using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data.Linq.Translation
{

    class ExecutionBuilder : DbExpressionVisitor
    {
        IQueryPolicy policy;
        InternalDbContext dbContext;
        IDialect Dialect;
        Expression executor;
        Scope scope;
        bool isTop = true;
        MemberInfo receivingMember;
        int nReaders = 0;
        List<ParameterExpression> variables = new List<ParameterExpression>();
        List<Expression> initializers = new List<Expression>();
        Dictionary<string, Expression> variableMap = new Dictionary<string, Expression>(StringComparer.Ordinal);

        private ExecutionBuilder(IDialect dialect, InternalDbContext policy, Expression executor)
        {
            this.Dialect = dialect;
            dbContext = policy;
            this.policy = policy;
            this.executor = executor;
        }


        public static Expression Build(IDialect dialect, InternalDbContext policy, Expression expression, Expression provider)
        {
            var executor = Expression.Parameter(typeof(ExecutionService), "executionService");
            var builder = new ExecutionBuilder(dialect, policy, executor);
            var dbContext = Expression.Convert(provider, typeof(InternalDbContext));
            builder.variables.Add(executor);
            builder.initializers.Add(Expression.New(MethodRepository.ExecutorCtor, dbContext));
            var result = builder.Build(expression);
            return result;
        }

        private Expression Build(Expression expression)
        {
            expression = this.Visit(expression);
            expression = this.AddVariables(expression);
            return expression;
        }

        private Expression AddVariables(Expression expression)
        {
            if (this.variables.Count > 0)
            {
                List<Expression> exprs = new List<Expression>();
                for (int i = 0, n = this.variables.Count; i < n; i++)
                    exprs.Add(MakeAssign(this.variables[i], this.initializers[i]));
                exprs.Add(expression);

                expression = MakeSequence(exprs);  // yields last expression value

                var nulls = this.variables.Select(v => Expression.Constant(null, v.Type)).ToArray();
                expression = Expression.Invoke(Expression.Lambda(expression, this.variables.ToArray()), nulls);
            }

            return expression;
        }

        private static Expression MakeSequence(IList<Expression> expressions)
        {
            Expression last = expressions[expressions.Count - 1];
            expressions = expressions.Select(e => e.Type.IsValueType ? Expression.Convert(e, typeof(object)) : e).ToList();
            return Expression.Convert(Expression.Call(typeof(ExecutionBuilder), "Sequence", null, Expression.NewArrayInit(typeof(object), expressions)), last.Type);
        }

        public static object Sequence(params object[] values)
        {
            return values[values.Length - 1];
        }

        public static IEnumerable<R> Batch<T, R>(IEnumerable<T> items, Func<T, R> selector)
        {
            var result = items.Select(selector);
            return result.ToList();
        }

        private static Expression MakeAssign(ParameterExpression variable, Expression value)
        {
            return Expression.Call(typeof(ExecutionBuilder), "Assign", new Type[] { variable.Type }, variable, value);
        }

        public static T Assign<T>(ref T variable, T value)
        {
            variable = value;
            return value;
        }

        private Expression BuildInner(Expression expression)
        {
            var eb = new ExecutionBuilder(this.Dialect, this.dbContext, this.executor);
            eb.scope = this.scope;
            eb.receivingMember = this.receivingMember;
            eb.nReaders = this.nReaders;
            eb.nLookup = this.nLookup;
            eb.variableMap = this.variableMap;
            return eb.Build(expression);
        }

        protected override MemberBinding VisitBinding(MemberBinding binding)
        {
            var save = this.receivingMember;
            this.receivingMember = binding.Member;
            var result = base.VisitBinding(binding);
            this.receivingMember = save;
            return result;
        }

        int nLookup = 0;

        private Expression MakeJoinKey(IList<Expression> key)
        {
            if (key.Count == 1)
            {
                return key[0];
            }
            else
            {
                return Expression.New(
                    typeof(CompoundKey).GetConstructors()[0],
                    Expression.NewArrayInit(typeof(object), key.Select(k => (Expression)Expression.Convert(k, typeof(object))))
                    );
            }
        }

        [Obsolete]
        protected override Expression VisitClientJoin(ClientJoinExpression join)
        {
            // convert client join into a up-front lookup table builder & replace client-join in tree with lookup accessor

            // 1) lookup = query.Select(e => new KVP(key: inner, value: e)).ToLookup(kvp => kvp.Key, kvp => kvp.Value)
            Expression innerKey = MakeJoinKey(join.InnerKey);
            Expression outerKey = MakeJoinKey(join.OuterKey);

            ConstructorInfo kvpConstructor = typeof(KeyValuePair<,>).MakeGenericType(innerKey.Type, join.Projection.Projector.Type).GetConstructor(new Type[] { innerKey.Type, join.Projection.Projector.Type });
            Expression constructKVPair = Expression.New(kvpConstructor, innerKey, join.Projection.Projector);
            ProjectionExpression newProjection = new ProjectionExpression(join.Projection.Select, constructKVPair);

            int iLookup = ++nLookup;
            Expression execution = this.ExecuteProjection(newProjection, false);

            ParameterExpression kvp = Expression.Parameter(constructKVPair.Type, "kvp");

            // filter out nulls
            if (join.Projection.Projector.NodeType == (ExpressionType)DbExpressionType.OuterJoined)
            {
                LambdaExpression pred = Expression.Lambda(
                    Expression.PropertyOrField(kvp, "Value").NotEqual(ReflectionHelper.GetNullConstant(join.Projection.Projector.Type)),
                    kvp
                    );
                execution = Expression.Call(typeof(Enumerable), "Where", new Type[] { kvp.Type }, execution, pred);
            }

            // make lookup
            LambdaExpression keySelector = Expression.Lambda(Expression.PropertyOrField(kvp, "Key"), kvp);
            LambdaExpression elementSelector = Expression.Lambda(Expression.PropertyOrField(kvp, "Value"), kvp);
            Expression toLookup = Expression.Call(typeof(Enumerable), "ToLookup", new Type[] { kvp.Type, outerKey.Type, join.Projection.Projector.Type }, execution, keySelector, elementSelector);

            // 2) agg(lookup[outer])
            ParameterExpression lookup = Expression.Parameter(toLookup.Type, "lookup" + iLookup);
            PropertyInfo property = lookup.Type.GetProperty("Item");
            Expression access = Expression.Call(lookup, property.GetGetMethod(), this.Visit(outerKey));
            if (join.Projection.Aggregator != null)
            {
                // apply aggregator
                access = DbExpressionReplacer.Replace(join.Projection.Aggregator.Body, join.Projection.Aggregator.Parameters[0], access);
            }

            this.variables.Add(lookup);
            this.initializers.Add(toLookup);

            return access;
        }

        protected override Expression VisitProjection(ProjectionExpression projection)
        {
            if (this.isTop)
            {
                this.isTop = false;
                return this.ExecuteProjection(projection, this.scope != null);
            }
            else
            {
                return this.BuildInner(projection);
            }
        }

        protected virtual Expression Parameterize(Expression expression)
        {
            if (this.variableMap.Count > 0)
            {
                expression = VariableSubstitutor.Substitute(this.variableMap, expression);
            }
            expression = UWay.Skynet.Cloud.Data.Linq.Expressions.Parameterizer.Parameterize(this.Dialect, expression);
            //expression = StringLikeFunctionRewriter.Rewrite(expression);
            //expression = StringTrimFunctionChecker.Check(expression);
            return expression;
        }

        private Expression ExecuteProjection(ProjectionExpression projection, bool okayToDefer)
        {
            projection = (ProjectionExpression)this.Parameterize(projection);

            if (this.scope != null)
            {
                // also convert references to outer alias to named values!  these become SQL parameters too
                projection = (ProjectionExpression)OuterParameterizer.Parameterize(this.scope.Alias, projection);
            }

            string commandText = this.dbContext.BuildSql(projection.Select);

            ReadOnlyCollection<NamedValueExpression> namedValues = NamedValueGatherer.Gather(projection.Select);
            Expression[] values = namedValues.Select(v => Expression.Convert(this.Visit(v.Value), typeof(object))).ToArray();

            return this.ExecuteProjection(projection, okayToDefer, commandText, namedValues.Select(v => new NamedParameter(v.Name, v.Type, v.SqlType)).ToArray(), values);
        }

        private Expression ExecuteProjection(ProjectionExpression projection, bool okayToDefer, string commandText, NamedParameter[] parameters, Expression[] values)
        {
            okayToDefer &= this.receivingMember != null && this.policy.IsDeferLoaded(this.receivingMember);

            var saveScope = this.scope;
            ParameterExpression reader = Expression.Parameter(typeof(FieldReader), "r" + nReaders++);
            this.scope = new Scope(this.scope, reader, projection.Select.Alias, projection.Select.Columns);
            LambdaExpression projector = Expression.Lambda(this.Visit(projection.Projector), reader);
            this.scope = saveScope;

            var queryContext = Expression.New(typeof(UWay.Skynet.Cloud.Data.Common.QueryContext<>).MakeGenericType(projector.Body.Type).GetConstructors().FirstOrDefault(),
                Expression.Constant(commandText),
                Expression.Constant(parameters),
                Expression.NewArrayInit(typeof(object), values),
                projector);

            Expression result = Expression.Call(this.executor, "Query", new Type[] { projector.Body.Type }, queryContext);

            if (projection.Aggregator != null)
            {
                // apply aggregator
                result = DbExpressionReplacer.Replace(projection.Aggregator.Body, projection.Aggregator.Parameters[0], result);
            }
            return result;
        }

        protected override Expression VisitBatch(BatchExpression batch)
        {
            if (this.Dialect.SupportMultipleCommands || !IsMultipleCommands(batch.Operation.Body as CommandExpression))
            {
                return this.BuildExecuteBatch(batch);
            }
            else
            {
                var source = this.Visit(batch.Input);
                var op = this.Visit(batch.Operation.Body);
                var fn = Expression.Lambda(op, batch.Operation.Parameters[1]);
                return Expression.Call(this.GetType(), "Batch", new Type[] { ReflectionHelper.GetElementType(source.Type), batch.Operation.Body.Type }, source, fn);
            }
        }

        protected virtual Expression BuildExecuteBatch(BatchExpression batch)
        {
            Expression operation = this.Parameterize(batch.Operation.Body);

            var cdu = batch.Operation.Body as CDUCommandExpression;
            Type entityType = null;

            if (cdu != null)
                entityType = cdu.Table.Mapping.EntityType;

            string commandText = this.dbContext.BuildSql(operation);
            var namedValues = NamedValueGatherer.Gather(operation);
            var parameters = namedValues.Select(v => new NamedParameter(v.Name, v.Type, v.SqlType)).ToArray();

            Expression[] values = namedValues.Select(v => Expression.Convert(this.Visit(v.Value), typeof(object))).ToArray();

            Expression paramSets = Expression.Call(typeof(Enumerable), "Select", new Type[] { batch.Operation.Parameters[1].Type, typeof(object[]) },
                batch.Input,
                Expression.Lambda(Expression.NewArrayInit(typeof(object), values), new[] { batch.Operation.Parameters[1] })
                );

            Expression plan = null;

            ProjectionExpression projection = ProjectionFinder.FindProjection(operation);
            if (projection != null)
            {
                var saveScope = this.scope;
                ParameterExpression reader = Expression.Parameter(typeof(FieldReader), "r" + nReaders++);
                this.scope = new Scope(this.scope, reader, projection.Select.Alias, projection.Select.Columns);
                LambdaExpression projector = Expression.Lambda(this.Visit(projection.Projector), reader);
                this.scope = saveScope;

                // var entity = EntityFinder.Find(projection.Projector);

                var batchContext = Expression.New(typeof(BatchContext<>).MakeGenericType(projector.Body.Type).GetConstructors().FirstOrDefault(),
                     Expression.Constant(commandText),
                     Expression.Constant(parameters),
                     paramSets,
                     projector);
                plan = Expression.Call(this.executor, "Batch", new Type[] { projector.Body.Type }, batchContext);
            }
            else
            {
                var batchContext = Expression.New(typeof(BatchContext).GetConstructors().FirstOrDefault(),
                     Expression.Constant(commandText),
                     Expression.Constant(parameters),
                     paramSets,
                     Expression.Constant(entityType));

                //plan = Expression.Call(this.executor, "Batch", null,
                //    Expression.Constant(commandText),
                //    Expression.Constant(parameters),
                //    paramSets
                //    );
                plan = Expression.Call(this.executor, "Batch", null, batchContext);
            }

            return plan;
        }

        protected override Expression VisitCommand(CommandExpression command)
        {
            if (this.Dialect.SupportMultipleCommands || !IsMultipleCommands(command))
                return this.BuildExecuteCommand(command);
            else
                return base.VisitCommand(command);
        }

        protected virtual bool IsMultipleCommands(CommandExpression command)
        {
            if (command == null)
                return false;
            switch ((DbExpressionType)command.NodeType)
            {
                case DbExpressionType.Insert:
                case DbExpressionType.Delete:
                case DbExpressionType.Update:
                    return false;
                default:
                    return true;
            }
        }

        protected override Expression VisitInsert(InsertCommand insert)
        {
            return this.BuildExecuteCommand(insert);
        }

        protected override Expression VisitUpdate(UpdateCommand update)
        {
            return this.BuildExecuteCommand(update);
        }

        protected override Expression VisitDelete(DeleteCommand delete)
        {
            return this.BuildExecuteCommand(delete);
        }

        protected override Expression VisitBlock(BlockCommand block)
        {
            return MakeSequence(this.VisitExpressionList(block.Commands));
        }

        protected override Expression VisitIf(IFCommand ifx)
        {
            var test =
                Expression.Condition(
                    ifx.Check,
                    ifx.IfTrue,
                    ifx.IfFalse != null
                        ? ifx.IfFalse
                        : ifx.IfTrue.Type == typeof(int)
                            ? (Expression)Expression.Property(this.executor, "RowsAffected")
                            : (Expression)Expression.Constant(UWay.Skynet.Cloud.Reflection.TypeHelper.GetDefault(ifx.IfTrue.Type), ifx.IfTrue.Type)
                            );
            return this.Visit(test);
        }

        protected override Expression VisitFunction(FunctionExpression func)
        {
            if (this.dbContext.ExpressionBuilder.IsRowsAffectedExpressions(func))
                return Expression.Property(this.executor, "RowsAffected");
            return base.VisitFunction(func);
        }

        [Obsolete]
        protected override Expression VisitExists(ExistsExpression exists)
        {
            // how did we get here? Translate exists into count query
            var colType = SqlType.Int32;
            var newSelect = exists.Select.SetColumns(
                new[] { new ColumnDeclaration("value", new AggregateExpression(typeof(int), "Count", null, false), colType) }
                );

            var projection =
                new ProjectionExpression(
                    newSelect,
                    new ColumnExpression(typeof(int), colType, newSelect.Alias, "value"),
                    Aggregator.GetAggregator(typeof(int), typeof(IEnumerable<int>))
                    );

            var expression = projection.GreaterThan(Expression.Constant(0));

            return this.Visit(expression);
        }

        [Obsolete]
        protected override Expression VisitDeclaration(DeclarationCommand decl)
        {
            if (decl.Source != null)
            {
                // make query that returns all these declared values as an object[]
                var projection = new ProjectionExpression(
                    decl.Source,
                    Expression.NewArrayInit(
                        typeof(object),
                        decl.Variables.Select(v => v.Expression.Type.IsValueType
                            ? Expression.Convert(v.Expression, typeof(object))
                            : v.Expression).ToArray()
                        ),
                    Aggregator.GetAggregator(typeof(object[]), typeof(IEnumerable<object[]>))
                    );

                // create execution variable to hold the array of declared variables
                var vars = Expression.Parameter(typeof(object[]), "vars");
                this.variables.Add(vars);
                this.initializers.Add(Expression.Constant(null, typeof(object[])));

                // create subsitution for each variable (so it will find the variable value in the new vars array)
                for (int i = 0, n = decl.Variables.Count; i < n; i++)
                {
                    var v = decl.Variables[i];
                    NamedValueExpression nv = new NamedValueExpression(
                        v.Name, v.SqlType,
                        Expression.Convert(Expression.ArrayIndex(vars, Expression.Constant(i)), v.Expression.Type)
                        );
                    this.variableMap.Add(v.Name, nv);
                }

                // make sure the execution of the select stuffs the results into the new vars array
                return MakeAssign(vars, this.Visit(projection));
            }

            // probably bad if we get here since we must not allow mulitple commands
            throw new InvalidOperationException(Res.DeclarationQueryInvalid);
        }



        protected virtual Expression BuildExecuteCommand(CommandExpression command)
        {
            // parameterize query
            var expression = this.Parameterize(command);

            var cdu = (command as CDUCommandExpression);

            string commandText = this.dbContext.BuildSql(expression);
            ReadOnlyCollection<NamedValueExpression> namedValues = NamedValueGatherer.Gather(expression);
            Expression[] values = namedValues.Select(v => Expression.Convert(this.Visit(v.Value), typeof(object))).ToArray();
            var parameters = namedValues.Select(v => new NamedParameter(v.Name, v.Type, v.SqlType)).ToArray();

            ProjectionExpression projection = ProjectionFinder.FindProjection(expression);
            if (projection != null)
            {
                return this.ExecuteProjection(projection, false, commandText, parameters, values);
            }

            bool supportVersionCheck = false;
            var delete = cdu as DeleteCommand;
            if (delete != null)
                supportVersionCheck = delete.SupportsVersionCheck;
            var update = cdu as UpdateCommand;
            if (update != null)
                supportVersionCheck = update.SupportsVersionCheck;

            var commandContext = Expression.New(
                MethodRepository.CommandContext.New,
                Expression.Constant(commandText),
                Expression.Constant(parameters),
                Expression.NewArrayInit(typeof(object), values),
                Expression.Constant(cdu.Table.Mapping.EntityType, Types.Type),
                Expression.Constant((OperationType)cdu.DbNodeType),
                Expression.Constant(supportVersionCheck),
                Expression.Constant(cdu.Instance))
                ;

            //Expression plan = Expression.Call(this.executor, "ExecuteNonQuery", new Type[] { (command as CDUCommandExpression).Table.Entity.EntityType },
            //    Expression.Constant(commandText),
            //     Expression.Constant(parameters),
            //    Expression.NewArrayInit(typeof(object), values)
            //    );

            Expression plan = Expression.Call(this.executor, "ExecuteNonQuery", null, commandContext);
            return plan;
        }

        private Type entityType;
        protected override Expression VisitEntity(EntityExpression entity)
        {
            entityType = entity.Type;
            return this.Visit(entity.Expression);
        }

        protected override Expression VisitOuterJoined(OuterJoinedExpression outer)
        {
            Expression expr = this.Visit(outer.Expression);
            ColumnExpression column = (ColumnExpression)outer.Test;
            ParameterExpression reader;
            int iOrdinal;
            if (this.scope.TryGetValue(column, out reader, out iOrdinal))
            {
                return Expression.Condition(
                    Expression.Call(reader, "IsDbNull", null, Expression.Constant(iOrdinal)),
                    Expression.Constant(UWay.Skynet.Cloud.Reflection.TypeHelper.GetDefault(outer.Type), outer.Type),
                    expr
                    );
            }
            return expr;
        }

        private IDictionary<int, Type> columnTypes = new Dictionary<int, Type>();
        protected override Expression VisitColumn(ColumnExpression column)
        {
            ParameterExpression fieldReader;
            int index;
            if (this.scope != null && this.scope.TryGetValue(column, out fieldReader, out index))
            {
                MethodInfo method = FieldReader.GetReaderMethod(column.Type);
                columnTypes[index] = column.Type;
                return Expression.Call(fieldReader, method, Expression.Constant(index));
            }
            else
            {
                System.Diagnostics.Debug.Fail(string.Format("column not in scope: {0}", column));
            }
            return column;
        }

        class Scope
        {
            Scope outer;
            ParameterExpression fieldReader;
            internal TableAlias Alias { get; private set; }
            Dictionary<string, int> nameMap;

            internal Scope(Scope outer, ParameterExpression fieldReader, TableAlias alias, IEnumerable<ColumnDeclaration> columns)
            {
                this.outer = outer;
                this.fieldReader = fieldReader;
                this.Alias = alias;
                this.nameMap = columns.Select((c, i) => new { c, i }).ToDictionary(x => x.c.Name, x => x.i);
            }

            internal bool TryGetValue(ColumnExpression column, out ParameterExpression fieldReader, out int ordinal)
            {
                for (Scope s = this; s != null; s = s.outer)
                {
                    if (column.Alias == s.Alias && this.nameMap.TryGetValue(column.Name, out ordinal))
                    {
                        fieldReader = this.fieldReader;
                        return true;
                    }
                }
                fieldReader = null;
                ordinal = 0;
                return false;
            }
        }

        /// <summary>
        /// columns referencing the outer alias are turned into special named-value parameters
        /// </summary>
        class OuterParameterizer : DbExpressionVisitor
        {
            int iParam;
            TableAlias outerAlias;
            Dictionary<ColumnExpression, NamedValueExpression> map = new Dictionary<ColumnExpression, NamedValueExpression>();

            internal static Expression Parameterize(TableAlias outerAlias, Expression expr)
            {
                OuterParameterizer op = new OuterParameterizer();
                op.outerAlias = outerAlias;
                return op.Visit(expr);
            }

            [Obsolete]
            protected override Expression VisitProjection(ProjectionExpression proj)
            {
                SelectExpression select = (SelectExpression)this.Visit(proj.Select);
                return this.UpdateProjection(proj, select, proj.Projector, proj.Aggregator);
            }

            [Obsolete]
            protected override Expression VisitColumn(ColumnExpression column)
            {
                if (column.Alias == this.outerAlias)
                {
                    NamedValueExpression nv;
                    if (!this.map.TryGetValue(column, out nv))
                    {
                        nv = new NamedValueExpression("n" + (iParam++), column.SqlType, column);
                        this.map.Add(column, nv);
                    }
                    return nv;
                }
                return column;
            }
        }

        class ProjectionFinder : DbExpressionVisitor
        {
            ProjectionExpression found = null;

            internal static ProjectionExpression FindProjection(Expression expression)
            {
                var finder = new ProjectionFinder();
                finder.Visit(expression);
                return finder.found;
            }

            protected override Expression VisitProjection(ProjectionExpression proj)
            {
                this.found = proj;
                return proj;
            }
        }

        class VariableSubstitutor : DbExpressionVisitor
        {
            Dictionary<string, Expression> map;

            private VariableSubstitutor(Dictionary<string, Expression> map)
            {
                this.map = map;
            }

            public static Expression Substitute(Dictionary<string, Expression> map, Expression expression)
            {
                return new VariableSubstitutor(map).Visit(expression);
            }

            protected override Expression VisitVariable(VariableExpression vex)
            {
                Expression sub;
                if (this.map.TryGetValue(vex.Name, out sub))
                {
                    return sub;
                }
                return vex;
            }
        }

        class EntityFinder : DbExpressionVisitor
        {
            IEntityMapping entity;

            public static IEntityMapping Find(Expression expression)
            {
                var finder = new EntityFinder();
                finder.Visit(expression);
                return finder.entity;
            }

            public override Expression Visit(Expression exp)
            {
                if (entity == null)
                    return base.Visit(exp);
                return exp;
            }

            protected override Expression VisitEntity(EntityExpression entity)
            {
                if (this.entity == null)
                    this.entity = entity.Entity;
                return entity;
            }


        }

    }
}