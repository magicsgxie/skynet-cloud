// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace UWay.Skynet.Cloud.Data.Linq.Expressions
{
    using System.Diagnostics;
    using UWay.Skynet.Cloud.Data.Common;
    using UWay.Skynet.Cloud.Data.Linq.Internal;
    using UWay.Skynet.Cloud.Data.Mapping;

    /// <summary>
    /// Extended node types for custom expressions
    /// </summary>
    public enum DbExpressionType
    {
        Table = 1000, // make sure these don't overlap with ExpressionType
        ClientJoin = 1001,
        Column = 1002,
        Select = 1003,
        Projection = 1004,
        Entity = 1005,
        Join = 1006,
        Aggregate = 1007,
        Scalar = 1008,
        Exists = 1009,
        In = 1010,
        Grouping = 1011,
        AggregateSubquery = 1012,
        IsNull = 1013,
        Between = 1014,
        RowCount = 1015,
        NamedValue = 1016,
        OuterJoined = 1017,
        Insert = 1018,
        Update = 1019,
        Delete = 1020,
        Batch = 1021,
        Function = 1022,
        Block = 1023,
        If = 1024,
        Declaration = 1025,
        Variable = 1026,
    }

    public static class DbExpressionTypeExtensions
    {
        public static bool IsDbExpression(this ExpressionType et)
        {
            return ((int)et) >= 1000;
        }
    }

    public abstract class DbExpression : Expression
    {
        public readonly DbExpressionType DbNodeType;
        protected DbExpression(DbExpressionType nodeType, Type type)
            : base((ExpressionType)nodeType, type)
        {
            DbNodeType = nodeType;
        }

        public override string ToString()
        {
            return DbExpressionWriter.WriteToString(this);
        }
    }

    /// <summary>
    /// A base class for expressions that declare table aliases.
    /// </summary>
    public abstract class AliasedExpression : DbExpression
    {
        private readonly TableAlias alias;

        protected AliasedExpression(DbExpressionType nodeType, Type type, TableAlias alias)
            : base(nodeType, type)
        {
            this.alias = alias;
        }

        public TableAlias Alias
        {
            get { return this.alias; }
        }
    }

    /// <summary>
    /// A custom expression node that represents a table reference in a SQL query
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},Name={name}")]
    public class TableExpression : AliasedExpression
    {
        private readonly IEntityMapping entity;

        public TableExpression(TableAlias alias, IEntityMapping entity)
            : base(DbExpressionType.Table, typeof(void), alias)
        {
            this.entity = entity;
        }


        public IEntityMapping Mapping
        {
            get { return this.entity; }
        }

        public string Name
        {
            get { return this.entity.TableName; }
        }

        public override string ToString()
        {
            return "T(" + this.Name + ")";
        }
    }

    /// <summary>
    /// An expression node that introduces an entity mapping.
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},Name={entity},Expression={expression}")]
    public class EntityExpression : DbExpression
    {
        private readonly IEntityMapping entity;
        private readonly Expression expression;

        public EntityExpression(IEntityMapping entity, Expression expression)
            : base(DbExpressionType.Entity, expression.Type)
        {
            this.entity = entity;
            this.expression = expression;
        }


        public IEntityMapping Entity
        {
            get { return this.entity; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }
    }

    /// <summary>
    /// A custom expression node that represents a reference to a column in a SQL query
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},Name={name},Alias={alias}")]
    public class ColumnExpression : DbExpression, IEquatable<ColumnExpression>
    {
        TableAlias alias;
        string name;
        SqlType sqlType;
        int hashCode;

        public ColumnExpression(Type type, SqlType sqlType, TableAlias alias, string name)
            : base(DbExpressionType.Column, type)
        {
            if (sqlType == null)
                throw new ArgumentNullException("queryType");
            if (name == null)
                throw new ArgumentNullException("name");
            this.alias = alias;
            this.name = name;
            this.sqlType = sqlType;
            hashCode = (alias + name).GetHashCode();
        }

        public TableAlias Alias
        {
            get { return this.alias; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public SqlType SqlType
        {
            get { return this.sqlType; }
        }

        public override string ToString()
        {
            return this.Alias.ToString() + ".C(" + this.name + ")";
        }

        public override int GetHashCode()
        {
            //return alias.GetHashCode() + name.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ColumnExpression);
        }

        public bool Equals(ColumnExpression other)
        {
            return other != null
                && ((object)this) == (object)other
                 || (alias == other.alias && name == other.Name);
        }
    }


    /// <summary>
    /// An alias for a table.
    /// </summary>
    public class TableAlias
    {
        class TableAliasComparer : IEqualityComparer<TableAlias>
        {
            public int Compare(TableAlias x, TableAlias y)
            {
                if (x.hashCode < y.hashCode)
                    return -1;
                if (x.hashCode > y.hashCode)
                    return 1;
                return 0;
            }

            public bool Equals(TableAlias x, TableAlias y)
            {
                return x.hashCode == y.hashCode;
            }

            public int GetHashCode(TableAlias obj)
            {
                return obj.hashCode;
            }
        }

        internal static readonly IEqualityComparer<TableAlias> Comparer = new TableAliasComparer();

        [ThreadStatic]
        static int seed;

        int hashCode;
        public TableAlias()
        {
            hashCode = ++seed;
        }

        public override string ToString()
        {
            return "A:" + hashCode;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public bool Equals(TableAlias other)
        {
            return other.hashCode == hashCode;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as TableAlias);
        }


    }

    /// <summary>
    /// A declaration of a column in a SQL SELECT expression
    /// </summary>
    [DebuggerDisplay("name={name},type={sqlType},expression={expression}")]
    public class ColumnDeclaration
    {
        string name;
        Expression expression;
        SqlType sqlType;

        public ColumnDeclaration(string name, Expression expression, SqlType sqlType)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (sqlType == null)
                throw new ArgumentNullException("sqlType");
            this.name = name;
            this.expression = expression;
            this.sqlType = sqlType;
        }

        public string Name
        {
            get { return this.name; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public SqlType SqlType
        {
            get { return this.sqlType; }
        }
    }

    /// <summary>
    /// An SQL OrderBy order type 
    /// </summary>
    public enum OrderType
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// A pairing of an expression and an order type for use in a SQL Order By clause
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},OrderType={orderType},Expression={expression}")]
    public class OrderExpression
    {
        private readonly OrderType orderType;
        private readonly Expression expression;

        public OrderExpression(OrderType orderType, Expression expression)
        {
            this.orderType = orderType;
            this.expression = expression;
        }

        public OrderType OrderType
        {
            get { return this.orderType; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }
    }

    /// <summary>
    /// A custom expression node used to represent a SQL SELECT expression
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},from={from},where={where},columns={columns},orderBy={orderBy},groupBy={groupBy}, take={take},skip={skip}, reverse={reverse}")]
    public class SelectExpression : AliasedExpression
    {
        private readonly ReadOnlyCollection<ColumnDeclaration> columns;
        private readonly bool isDistinct;
        private readonly Expression from;
        private readonly Expression where;
        private readonly ReadOnlyCollection<OrderExpression> orderBy;
        private readonly ReadOnlyCollection<Expression> groupBy;
        private readonly Expression take;
        private readonly Expression skip;
        private readonly bool reverse;

        public SelectExpression(
            TableAlias alias,
            IEnumerable<ColumnDeclaration> columns,
            Expression from,
            Expression where,
            IEnumerable<OrderExpression> orderBy,
            IEnumerable<Expression> groupBy,
            bool isDistinct,
            Expression skip,
            Expression take,
            bool reverse
            )
            : base(DbExpressionType.Select, typeof(void), alias)
        {
            this.columns = columns.ToReadOnly();
            this.isDistinct = isDistinct;
            this.from = from;
            this.where = where;
            this.orderBy = orderBy.ToReadOnly();
            this.groupBy = groupBy.ToReadOnly();
            this.take = take;
            this.skip = skip;
            this.reverse = reverse;
        }

        public SelectExpression(
            TableAlias alias,
            IEnumerable<ColumnDeclaration> columns,
            Expression from,
            Expression where,
            IEnumerable<OrderExpression> orderBy,
            IEnumerable<Expression> groupBy
            )
            : this(alias, columns, from, where, orderBy, groupBy, false, null, null, false)
        {
        }

        public SelectExpression(
            TableAlias alias, IEnumerable<ColumnDeclaration> columns,
            Expression from, Expression where
            )
            : this(alias, columns, from, where, null, null)
        {
        }

        public ReadOnlyCollection<ColumnDeclaration> Columns
        {
            get { return this.columns; }
        }

        public Expression From
        {
            get { return this.from; }
        }

        public Expression Where
        {
            get { return this.where; }
        }

        public ReadOnlyCollection<OrderExpression> OrderBy
        {
            get { return this.orderBy; }
        }

        public ReadOnlyCollection<Expression> GroupBy
        {
            get { return this.groupBy; }
        }

        public bool IsDistinct
        {
            get { return this.isDistinct; }
        }

        public Expression Skip
        {
            get { return this.skip; }
        }

        public Expression Take
        {
            get { return this.take; }
        }

        public bool IsReverse
        {
            get { return this.reverse; }
        }
    }



    /// <summary>
    /// A SQL join clause expression
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},JoinType={joinType},left={left},right={right},condition={condition}")]
    public class JoinExpression : DbExpression
    {
        private readonly JoinType joinType;
        private readonly Expression left;
        private readonly Expression right;
        private readonly Expression condition;

        public JoinExpression(JoinType joinType, Expression left, Expression right, Expression condition)
            : base(DbExpressionType.Join, typeof(void))
        {
            this.joinType = joinType;
            this.left = left;
            this.right = right;
            this.condition = condition;
        }

        public JoinType Join
        {
            get { return this.joinType; }
        }

        public Expression Left
        {
            get { return this.left; }
        }

        public Expression Right
        {
            get { return this.right; }
        }

        public new Expression Condition
        {
            get { return this.condition; }
        }
    }

    /// <summary>
    /// A wrapper around and expression that is part of an outer joined projection
    /// including a test expression to determine if the expression ought to be considered null.
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},test={test},expression={expression}")]
    public class OuterJoinedExpression : DbExpression
    {
        private readonly Expression test;
        private readonly Expression expression;

        public OuterJoinedExpression(Expression test, Expression expression)
            : base(DbExpressionType.OuterJoined, expression.Type)
        {
            this.test = test;
            this.expression = expression;
        }

        public Expression Test
        {
            get { return this.test; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }
    }

    /// <summary>
    /// An base class for SQL subqueries.
    /// </summary>
    public abstract class SubqueryExpression : DbExpression
    {
        private readonly SelectExpression select;

        protected SubqueryExpression(DbExpressionType nodeType, Type type, SelectExpression select)
            : base(nodeType, type)
        {
            this.select = select;
        }

        public SelectExpression Select
        {
            get { return this.select; }
        }
    }

    /// <summary>
    /// A SQL scalar subquery expression:
    ///   exists(select x from y where z)
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},Select={Select}")]
    public class ScalarExpression : SubqueryExpression
    {
        public ScalarExpression(Type type, SelectExpression select)
            : base(DbExpressionType.Scalar, type, select)
        {
        }
    }

    /// <summary>
    /// A SQL Exists subquery expression.
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},Select={Select}")]
    public class ExistsExpression : SubqueryExpression
    {
        public ExistsExpression(SelectExpression select)
            : base(DbExpressionType.Exists, typeof(bool), select)
        {
        }

    }

    /// <summary>
    /// A SQL 'In' subquery:
    ///   expr in (select x from y where z)
    ///   expr in (a, b, c)
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},expression={expression} in(values), Select={Select}")]
    public class InExpression : SubqueryExpression
    {
        private readonly Expression expression;
        private readonly ReadOnlyCollection<Expression> values;  // either select or expressions are assigned

        public InExpression(Expression expression, SelectExpression select)
            : base(DbExpressionType.In, typeof(bool), select)
        {
            this.expression = expression;
        }

        public InExpression(Expression expression, IEnumerable<Expression> values)
            : base(DbExpressionType.In, typeof(bool), null)
        {
            this.expression = expression;
            this.values = values.ToReadOnly();
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public ReadOnlyCollection<Expression> Values
        {
            get { return this.values; }
        }
    }

    /// <summary>
    /// An SQL Aggregate expression:
    ///     MIN, MAX, AVG, COUNT
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},aggregateName={aggregateName} argument={argument}, isDistinct={isDistinct}")]
    public class AggregateExpression : DbExpression
    {
        private readonly string aggregateName;
        private readonly Expression argument;
        private readonly bool isDistinct;

        public AggregateExpression(Type type, string aggregateName, Expression argument, bool isDistinct)
            : base(DbExpressionType.Aggregate, type)
        {
            this.aggregateName = aggregateName;
            this.argument = argument;
            this.isDistinct = isDistinct;
        }

        public string AggregateName
        {
            get { return this.aggregateName; }
        }

        public Expression Argument
        {
            get { return this.argument; }
        }

        public bool IsDistinct
        {
            get { return this.isDistinct; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},aggregateInGroupSelect={aggregateInGroupSelect}, aggregateAsSubquery={aggregateAsSubquery}, groupByAlias={groupByAlias}")]
    public class AggregateSubqueryExpression : DbExpression
    {
        private readonly TableAlias groupByAlias;
        private readonly Expression aggregateInGroupSelect;
        private readonly ScalarExpression aggregateAsSubquery;

        public AggregateSubqueryExpression(TableAlias groupByAlias, Expression aggregateInGroupSelect, ScalarExpression aggregateAsSubquery)
            : base(DbExpressionType.AggregateSubquery, aggregateAsSubquery.Type)
        {
            this.aggregateInGroupSelect = aggregateInGroupSelect;
            this.groupByAlias = groupByAlias;
            this.aggregateAsSubquery = aggregateAsSubquery;
        }

        public TableAlias GroupByAlias
        {
            get { return this.groupByAlias; }
        }

        public Expression AggregateInGroupSelect
        {
            get { return this.aggregateInGroupSelect; }
        }

        public ScalarExpression AggregateAsSubquery
        {
            get { return this.aggregateAsSubquery; }
        }
    }

    /// <summary>
    /// Allows is-null tests against value-types like int and float
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},expression={expression}, Not={Not}")]
    public class IsNullExpression : DbExpression
    {
        private readonly Expression expression;

        internal  bool IsNot;

        public IsNullExpression(Expression expression)
            : base(DbExpressionType.IsNull, typeof(bool))
        {
            this.expression = expression;
        }

        public Expression Expression
        {
            get { return this.expression; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},expression between {lower} and {upper}")]
    public class BetweenExpression : DbExpression
    {
        private readonly Expression expression;
        private readonly Expression lower;
        private readonly Expression upper;

        public BetweenExpression(Expression expression, Expression lower, Expression upper)
            : base(DbExpressionType.Between, expression.Type)
        {
            this.expression = expression;
            this.lower = lower;
            this.upper = upper;
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public Expression Lower
        {
            get { return this.lower; }
        }

        public Expression Upper
        {
            get { return this.upper; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},orderBy={orderBy}, Not={Not}")]
    public class RowNumberExpression : DbExpression
    {
        private readonly ReadOnlyCollection<OrderExpression> orderBy;

        public RowNumberExpression(IEnumerable<OrderExpression> orderBy)
            : base(DbExpressionType.RowCount, typeof(int))
        {
            this.orderBy = orderBy.ToReadOnly();
        }

        public ReadOnlyCollection<OrderExpression> OrderBy
        {
            get { return this.orderBy; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},Name={name},Value={value},SqlType={sqlType}")]
    public class NamedValueExpression : DbExpression
    {
        private readonly string name;
        private readonly SqlType sqlType;
        private readonly Expression value;

        public NamedValueExpression(string name, SqlType sqlType, Expression value)
            : base(DbExpressionType.NamedValue, value.Type)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");
            this.name = name;
            this.sqlType = sqlType;
            this.value = value;
        }

        public string Name
        {
            get { return this.name; }
        }

        public SqlType SqlType
        {
            get { return this.sqlType; }
        }

        public Expression Value
        {
            get { return this.value; }
        }
    }

    /// <summary>
    /// A custom expression representing the construction of one or more result objects from a 
    /// SQL select expression
    /// </summary>
    [DebuggerDisplay("DbNodeType={DbNodeType},projector={projector}, aggregator={aggregator},select={select}")]
    public class ProjectionExpression : DbExpression
    {
        private readonly SelectExpression select;
        private readonly Expression projector;
        private readonly LambdaExpression aggregator;

        public ProjectionExpression(SelectExpression source, Expression projector)
            : this(source, projector, null)
        {
        }

        public ProjectionExpression(SelectExpression source, Expression projector, LambdaExpression aggregator)
            : base(DbExpressionType.Projection, aggregator != null ? aggregator.Body.Type : typeof(IEnumerable<>).MakeGenericType(projector.Type))
        {
            this.select = source;
            this.projector = projector;
            this.aggregator = aggregator;
        }

        public SelectExpression Select
        {
            get { return this.select; }
        }

        public Expression Projector
        {
            get { return this.projector; }
        }

        public LambdaExpression Aggregator
        {
            get { return this.aggregator; }
        }

        public bool IsSingleton
        {
            get { return this.aggregator != null && this.aggregator.Body.Type == projector.Type; }
        }

        public override string ToString()
        {
            //return DbExpressionWriter.WriteToString(this);
            var dbContext = ExecuteContext.DbContext;
            return dbContext.BuildSql(this);
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},innerKey={innerKey},outerKey={outerKey},projection={projection}")]
    public class ClientJoinExpression : DbExpression
    {
        private readonly ReadOnlyCollection<Expression> outerKey;
        private readonly ReadOnlyCollection<Expression> innerKey;
        private readonly ProjectionExpression projection;

        public ClientJoinExpression(ProjectionExpression projection, IEnumerable<Expression> outerKey, IEnumerable<Expression> innerKey)
            : base(DbExpressionType.ClientJoin, projection.Type)
        {
            this.outerKey = outerKey.ToReadOnly();
            this.innerKey = innerKey.ToReadOnly();
            this.projection = projection;
        }

        public ReadOnlyCollection<Expression> OuterKey
        {
            get { return this.outerKey; }
        }

        public ReadOnlyCollection<Expression> InnerKey
        {
            get { return this.innerKey; }
        }

        public ProjectionExpression Projection
        {
            get { return this.projection; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},input={input},operation={operation}")]
    public class BatchExpression : DbExpression
    {
        private readonly Expression input;
        private readonly LambdaExpression operation;

        public BatchExpression(Expression input, LambdaExpression operation)
            : base(DbExpressionType.Batch, typeof(IEnumerable<>).MakeGenericType(operation.Body.Type))
        {
            this.input = input;
            this.operation = operation;
        }

        public Expression Input
        {
            get { return this.input; }
        }

        public LambdaExpression Operation
        {
            get { return this.operation; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},name={name},arguments={arguments}")]
    public class FunctionExpression : DbExpression
    {
        private readonly string name;
        private readonly ReadOnlyCollection<Expression> arguments;

        public FunctionExpression(Type type, string name, params Expression[] arguments)
            : base(DbExpressionType.Function, type)
        {
            this.name = name;
            this.arguments = arguments.ToReadOnly();
        }

        public string Name
        {
            get { return this.name; }
        }

        public ReadOnlyCollection<Expression> Arguments
        {
            get { return this.arguments; }
        }
    }

    public abstract class CommandExpression : DbExpression
    {
        protected CommandExpression(DbExpressionType nodeType, Type type)
            : base(nodeType, type)
        {
        }

    }

    public abstract class CDUCommandExpression : CommandExpression
    {
        private readonly TableExpression table;
        private object instance;
        protected CDUCommandExpression(TableExpression table, object instance, DbExpressionType nodeType, Type type)
            : base(nodeType, type)
        {
            this.table = table;
            this.instance = instance;
        }

        public object Instance
        {
            get { return instance; }
        }
        public TableExpression Table
        {
            get { return this.table; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},Table={Table},Instance={Instance},assignments={assignments}")]
    public class InsertCommand : CDUCommandExpression
    {

        private readonly ReadOnlyCollection<ColumnAssignment> assignments;


        public InsertCommand(TableExpression table, IEnumerable<ColumnAssignment> assignments, object instance)
            : base(table, instance, DbExpressionType.Insert, typeof(int))
        {
            this.assignments = assignments.ToReadOnly();
        }


        public ReadOnlyCollection<ColumnAssignment> Assignments
        {
            get { return this.assignments; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},{column}={expression}")]
    public class ColumnAssignment
    {
        private readonly ColumnExpression column;
        private readonly Expression expression;

        public ColumnAssignment(ColumnExpression column, Expression expression)
        {
            this.column = column;
            this.expression = expression;
        }

        public ColumnExpression Column
        {
            get { return this.column; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public override string ToString()
        {
            return column.ToString() + " = " + (expression != null ? expression.ToString() : "NULL");
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},Table={Table},Instance={Instance},assignments={assignments},where={where},SupportsVersionCheck={SupportsVersionCheck}")]
    public class UpdateCommand : CDUCommandExpression
    {
        private readonly Expression where;
        private readonly ReadOnlyCollection<ColumnAssignment> assignments;

        public UpdateCommand(TableExpression table, Expression where, object instance, bool supportsVersionCheck, IEnumerable<ColumnAssignment> assignments)
            : base(table, instance, DbExpressionType.Update, typeof(int))
        {
            this.where = where;
            SupportsVersionCheck = supportsVersionCheck;
            this.assignments = assignments.ToReadOnly();
        }

        public bool SupportsVersionCheck { get; private set; }

        public Expression Where
        {
            get { return this.where; }
        }

        public ReadOnlyCollection<ColumnAssignment> Assignments
        {
            get { return this.assignments; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},Table={Table},Instance={Instance},assignments={assignments},where={where},SupportsVersionCheck={SupportsVersionCheck}")]
    public class DeleteCommand : CDUCommandExpression
    {
        private readonly Expression where;

        public DeleteCommand(TableExpression table, Expression where, object instance, bool supportsVersionCheck)
            : base(table, instance, DbExpressionType.Delete, typeof(int))
        {
            this.where = where;
            SupportsVersionCheck = supportsVersionCheck;
        }

        public bool SupportsVersionCheck { get; private set; }

        public Expression Where
        {
            get { return this.where; }
        }
    }


    [DebuggerDisplay("DbNodeType={DbNodeType},{check}?{ifTrue}:{ifFalse}")]
    public class IFCommand : CommandExpression
    {
        private readonly Expression check;
        private readonly Expression ifTrue;
        private readonly Expression ifFalse;

        public IFCommand(Expression check, Expression ifTrue, Expression ifFalse)
            : base(DbExpressionType.If, ifTrue.Type)
        {
            this.check = check;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }

        public Expression Check
        {
            get { return this.check; }
        }

        public Expression IfTrue
        {
            get { return this.ifTrue; }
        }

        public Expression IfFalse
        {
            get { return this.ifFalse; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},commands={commands}")]
    public class BlockCommand : CommandExpression
    {
        private readonly ReadOnlyCollection<Expression> commands;

        public BlockCommand(IList<Expression> commands)
            : base(DbExpressionType.Block, commands[commands.Count - 1].Type)
        {
            this.commands = commands.ToReadOnly();
        }

        public BlockCommand(params Expression[] commands)
            : this((IList<Expression>)commands)
        {
        }

        public ReadOnlyCollection<Expression> Commands
        {
            get { return this.commands; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},variables={variables},source={source}")]
    public class DeclarationCommand : CommandExpression
    {
        private readonly ReadOnlyCollection<VariableDeclaration> variables;
        private readonly SelectExpression source;

        public DeclarationCommand(IEnumerable<VariableDeclaration> variables, SelectExpression source)
            : base(DbExpressionType.Declaration, typeof(void))
        {
            this.variables = variables.ToReadOnly();
            this.source = source;
        }

        public ReadOnlyCollection<VariableDeclaration> Variables
        {
            get { return this.variables; }
        }

        public SelectExpression Source
        {
            get { return this.source; }
        }
    }

    [DebuggerDisplay("name={name},type={type},expression={expression}")]
    public class VariableDeclaration
    {
        private readonly string name;
        private readonly SqlType type;
        private readonly Expression expression;

        public VariableDeclaration(string name, SqlType type, Expression expression)
        {
            this.name = name;
            this.type = type;
            this.expression = expression;
        }

        public string Name
        {
            get { return this.name; }
        }

        public SqlType SqlType
        {
            get { return this.type; }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }
    }

    [DebuggerDisplay("DbNodeType={DbNodeType},name={name},sqlType={sqlType}")]
    public class VariableExpression : Expression
    {
        private readonly string name;
        private readonly SqlType sqlType;

        [Obsolete]
        public VariableExpression(string name, Type type, SqlType sqlType)
            : base((ExpressionType)DbExpressionType.Variable, type)
        {
            this.name = name;
            this.sqlType = sqlType;
        }

        public string Name
        {
            get { return this.name; }
        }

        public SqlType SqlType
        {
            get { return this.sqlType; }
        }
    }
}
