using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using UWay.Skynet.Cloud.Data.Linq.Expressions;
using UWay.Skynet.Cloud.Data.Mapping;

namespace UWay.Skynet.Cloud.Data.Dialect.SqlBuilder
{
    abstract partial class DbSqlBuilder : DbExpressionVisitor, ISqlBuilder
    {
        internal IDialect Dialect;
        internal IFunctionRegistry FuncRegistry;
        protected internal StringBuilder sb = new StringBuilder(512);
        Dictionary<TableAlias, string> aliases = new Dictionary<TableAlias, string>(TableAlias.Comparer);

        public DbSqlBuilder()
        {
            RegisterCastTypes();
        }

        int indent = 2;
        int depth;
        bool hideColumnAliases;
        bool hideTableAliases;
        bool isNested;

        static readonly char[] _hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        internal static string ByteArrayToBinaryString(Byte[] binaryArray)
        {
            var sb = new StringBuilder(binaryArray.Length * 2);
            for (var i = 0; i < binaryArray.Length; i++)
            {
                sb.Append(_hexDigits[(binaryArray[i] & 0xF0) >> 4]).Append(_hexDigits[binaryArray[i] & 0x0F]);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return this.sb.ToString();
        }

        #region 注册类型转换
        internal readonly TypeNames castTypeNames = new TypeNames();
        protected void RegisterCastType(DBType code, string name)
        {
            castTypeNames.Put(code, name);
        }
        protected virtual void RegisterCastTypes() { }
        #endregion

        protected bool HideColumnAliases
        {
            get { return this.hideColumnAliases; }
            set { this.hideColumnAliases = value; }
        }

        protected bool HideTableAliases
        {
            get { return this.hideTableAliases; }
            set { this.hideTableAliases = value; }
        }

        protected bool IsNested
        {
            get { return this.isNested; }
            set { this.isNested = value; }
        }



        public int IndentationWidth
        {
            get { return this.indent; }
            set { this.indent = value; }
        }

        public ISqlBuilder Append(object value)
        {
            this.sb.Append(value);
            return this;
        }

        public void AppendFormat(string fmt, params object[] args)
        {
            sb.AppendFormat(fmt, args);
        }

        public ISqlBuilder Do(Action handler)
        {
            if (handler != null) handler();
            return this;
        }

        public void VisitEnumerable(Expression[] items)
        {
            const string separator = ", ";
            VisitEnumerable(items, separator);
        }

        public void VisitEnumerable(Expression[] items, string separetor)
        {
            var length = items.Length;
            for (int i = 0; i < length; i++)
            {
                Visit(items[i]);
                if (i < length - 1)
                    Append(separetor);
            }
        }

        protected virtual void AppendParameterName(string name)
        {
            this.Append("@" + name);
        }

        protected virtual void AppendVariableName(string name)
        {
            this.AppendParameterName(name);
        }

        protected virtual void AppendAsAliasName(string aliasName)
        {
            this.Append("AS ");
            this.AppendAliasName(aliasName);
        }

        protected virtual void AppendAliasName(string aliasName)
        {
            this.Append(aliasName);
        }

        protected virtual void AppendAsColumnName(string columnName)
        {
            this.Append("AS ");
            this.AppendColumnName(columnName);
        }

        protected virtual void AppendColumnName(string columnName)
        {
            string name = (this.Dialect != null) ? this.Dialect.Quote(columnName) : columnName;
            this.Append(name);
        }



        protected virtual void AppendTableName(string tableName)
        {
            string name = (this.Dialect != null) ? this.Dialect.Quote(tableName) : tableName;
            this.Append(name);
        }

        public void AppendLine(Indentation style)
        {
            sb.AppendLine();
            this.Indent(style);
            for (int i = 0, n = this.depth * this.indent; i < n; i++)
                this.Append(" ");
        }

        public void AppendIndentation()
        {
            Indent(Indentation.Inner);
        }
        public void AppendOutdentation()
        {
            Indent(Indentation.Outer);
        }

        protected void Indent(Indentation style)
        {
            if (style == Indentation.Inner)
            {
                this.depth++;
            }
            else if (style == Indentation.Outer)
            {
                this.depth--;
                System.Diagnostics.Debug.Assert(this.depth >= 0);
            }
        }

        protected virtual string GetAliasName(TableAlias alias)
        {
            string name;
            if (!this.aliases.TryGetValue(alias, out name))
            {
                name = "ut" + this.aliases.Count;
                this.aliases.Add(alias, name);
            }
            return name;
        }

        protected void AddAlias(TableAlias alias)
        {
            string name;
            if (!this.aliases.TryGetValue(alias, out name))
            {
                name = "t" + this.aliases.Count;
                this.aliases.Add(alias, name);
            }
        }

        protected virtual void AddAliases(Expression expr)
        {
            AliasedExpression ax = expr as AliasedExpression;
            if (ax != null)
            {
                this.AddAlias(ax.Alias);
            }
            else
            {
                JoinExpression jx = expr as JoinExpression;
                if (jx != null)
                {
                    this.AddAliases(jx.Left);
                    this.AddAliases(jx.Right);
                }
            }
        }

        private static ConditionalExpression CreateCompareExpression(Expression left, Expression right)
        {
            return Expression.Condition(
                Expression.Equal(left, right),
                Expression.Constant(0),
                Expression.Condition(
                    Expression.GreaterThan(left, right),
                    Expression.Constant(1),
                    Expression.Constant(-1)));
        }

        private static IEnumerable<Expression> PopulateArguments(MethodCallExpression m)
        {
            IEnumerable<Expression> args;
            if (m.Method.IsStatic)
                args = m.Arguments;
            else
            {
                var items = new List<Expression>();
                items.Add(m.Object);
                if (m.Arguments.Count > 0)
                    items.AddRange(m.Arguments);
                args = items;
            }
            return args;
        }



        public override Expression Visit(Expression exp)
        {
            if (exp == null) return null;

            // check for supported node types first 
            // non-supported ones should not be visited (as they would produce bad SQL)
            switch (exp.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Power:
                case ExpressionType.Conditional:
                case ExpressionType.Constant:
                case ExpressionType.MemberAccess:
                case ExpressionType.Call:
                case ExpressionType.New:
                case (ExpressionType)DbExpressionType.Table:
                case (ExpressionType)DbExpressionType.Column:
                case (ExpressionType)DbExpressionType.Select:
                case (ExpressionType)DbExpressionType.Join:
                case (ExpressionType)DbExpressionType.Aggregate:
                case (ExpressionType)DbExpressionType.Scalar:
                case (ExpressionType)DbExpressionType.Exists:
                case (ExpressionType)DbExpressionType.In:
                case (ExpressionType)DbExpressionType.AggregateSubquery:
                case (ExpressionType)DbExpressionType.IsNull:
                case (ExpressionType)DbExpressionType.Between:
                case (ExpressionType)DbExpressionType.RowCount:
                case (ExpressionType)DbExpressionType.Projection:
                case (ExpressionType)DbExpressionType.NamedValue:
                case (ExpressionType)DbExpressionType.Insert:
                case (ExpressionType)DbExpressionType.Update:
                case (ExpressionType)DbExpressionType.Delete:
                case (ExpressionType)DbExpressionType.Block:
                case (ExpressionType)DbExpressionType.If:
                case (ExpressionType)DbExpressionType.Declaration:
                case (ExpressionType)DbExpressionType.Variable:
                case (ExpressionType)DbExpressionType.Function:
                case ExpressionType.NewArrayInit:
                case ExpressionType.TypeAs:
                    return base.Visit(exp);
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                //case ExpressionType.TypeAs:
                case ExpressionType.ArrayIndex:
                case ExpressionType.TypeIs:
                case ExpressionType.Parameter:
                case ExpressionType.Lambda:
                case ExpressionType.NewArrayBounds:
                case ExpressionType.Invoke:
                case ExpressionType.MemberInit:
                case ExpressionType.ListInit:
                default:
                    throw new NotSupportedException(string.Format(Res.NotSupported, "The LINQ expression node of type " + exp.NodeType, ""));

            }
        }


        protected override Expression VisitColumn(ColumnExpression column)
        {
            if (column.Alias != null && !this.HideColumnAliases)
            {
                this.AppendAliasName(GetAliasName(column.Alias));
                this.Append(".");
            }
            this.AppendColumnName(column.Name);
            return column;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            // treat these like scalar subqueries
            if ((proj.Projector is ColumnExpression))
            {
                this.Append("(");
                this.AppendLine(Indentation.Inner);
                this.Visit(proj.Select);
                this.Append(")");
                this.Indent(Indentation.Outer);
            }
            else
                throw new NotSupportedException("Non-scalar projections cannot be translated to SQL.");
            return proj;
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            this.AddAliases(select.From);
            this.Append("SELECT ");
            if (select.IsDistinct)
                this.Append("DISTINCT ");
            if (select.Take != null)
                this.WriteTopClause(select.Take);
            this.WriteColumns(select.Columns);
            if (select.From != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("FROM ");
                this.VisitSource(select.From);
            }
            if (select.Where != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("WHERE ");
                this.VisitPredicate(select.Where);
            }
            if (select.GroupBy != null && select.GroupBy.Count > 0)
            {
                this.AppendLine(Indentation.Same);
                this.Append("GROUP BY ");
                for (int i = 0, n = select.GroupBy.Count; i < n; i++)
                {
                    if (i > 0)
                        this.Append(", ");
                    this.VisitValue(select.GroupBy[i]);
                }
            }
            if (select.OrderBy != null && select.OrderBy.Count > 0)
            {
                this.AppendLine(Indentation.Same);
                this.Append("ORDER BY ");
                for (int i = 0, n = select.OrderBy.Count; i < n; i++)
                {
                    OrderExpression exp = select.OrderBy[i];
                    if (i > 0)
                        this.Append(", ");
                    this.VisitValue(exp.Expression);
                    if (exp.OrderType != OrderType.Ascending)
                        this.Append(" DESC");
                }
            }
            return select;
        }

        protected virtual void WriteTopClause(Expression expression)
        {
            this.Append("TOP ");
            this.Visit(expression);
            this.Append(" ");
        }

        protected virtual void WriteColumns(ReadOnlyCollection<ColumnDeclaration> columns)
        {
            if (columns.Count > 0)
            {
                for (int i = 0, n = columns.Count; i < n; i++)
                {
                    ColumnDeclaration column = columns[i];
                    if (i > 0)
                    {
                        this.Append(", ");
                    }
                    ColumnExpression c = this.VisitValue(column.Expression) as ColumnExpression;
                    if (!string.IsNullOrEmpty(column.Name) && (c == null || c.Name != column.Name))
                    {
                        this.Append(" ");
                        this.AppendAsColumnName(column.Name);
                    }
                }
            }
            else
            {
                this.Append("NULL ");
                if (this.isNested)
                {
                    this.AppendAsColumnName("tmp");
                    this.Append(" ");
                }
            }
        }

        protected virtual void WriteTableName(IEntityMapping mapping)
        {
            if (Dialect.SupportSchema && !string.IsNullOrEmpty(mapping.Schema))
            {
                sb.Append(Dialect.Quote(mapping.Schema));
                sb.Append(".");
            }
            this.AppendTableName(mapping.TableName);
        }

        protected override Expression VisitSource(Expression source)
        {
            bool saveIsNested = this.isNested;
            this.isNested = true;
            switch ((DbExpressionType)source.NodeType)
            {
                case DbExpressionType.Table:
                    TableExpression table = (TableExpression)source;
                    WriteTableName(table.Mapping);
                    if (!this.HideTableAliases)
                    {
                        this.Append(" ");
                        this.AppendAsAliasName(GetAliasName(table.Alias));
                    }
                    break;
                case DbExpressionType.Select:
                    SelectExpression select = (SelectExpression)source;
                    this.Append("(");
                    this.AppendLine(Indentation.Inner);
                    this.Visit(select);
                    this.AppendLine(Indentation.Same);
                    this.Append(") ");
                    this.AppendAsAliasName(GetAliasName(select.Alias));
                    this.Indent(Indentation.Outer);
                    break;
                case DbExpressionType.Join:
                    this.VisitJoin((JoinExpression)source);
                    break;
                default:
                    throw new InvalidOperationException("Select source is not valid type");
            }
            this.isNested = saveIsNested;
            return source;
        }

        protected override Expression VisitJoin(JoinExpression join)
        {
            this.VisitJoinLeft(join.Left);
            this.AppendLine(Indentation.Same);
            switch (join.Join)
            {
                case JoinType.CrossJoin:
                    this.Append("CROSS JOIN ");
                    break;
                case JoinType.InnerJoin:
                    this.Append("INNER JOIN ");
                    break;
                case JoinType.CrossApply:
                    this.Append("CROSS APPLY ");
                    break;
                case JoinType.OuterApply:
                    this.Append("OUTER APPLY ");
                    break;
                case JoinType.LeftOuter:
                case JoinType.SingletonLeftOuter:
                    this.Append("LEFT OUTER JOIN ");
                    break;
            }
            this.VisitJoinRight(join.Right);
            if (join.Condition != null)
            {
                this.AppendLine(Indentation.Inner);
                this.Append("ON ");
                this.VisitPredicate(join.Condition);
                this.Indent(Indentation.Outer);
            }
            return join;
        }

        protected virtual Expression VisitJoinLeft(Expression source)
        {
            return this.VisitSource(source);
        }

        protected virtual Expression VisitJoinRight(Expression source)
        {
            return this.VisitSource(source);
        }

        protected virtual void WriteAggregateName(string aggregateName)
        {
            switch (aggregateName)
            {
                case "Average":
                    this.Append("AVG");
                    break;
                case "LongCount":
                    this.Append("COUNT");
                    break;
                default:
                    this.Append(aggregateName.ToUpper());
                    break;
            }
        }

        protected virtual bool RequiresAsteriskWhenNoArgument(string aggregateName)
        {
            return aggregateName == "Count" || aggregateName == "LongCount";
        }

        protected override Expression VisitAggregate(AggregateExpression aggregate)
        {
            this.WriteAggregateName(aggregate.AggregateName);
            this.Append("(");
            if (aggregate.IsDistinct)
                this.Append("DISTINCT ");
            if (aggregate.Argument != null)
                this.VisitValue(aggregate.Argument);
            else if (RequiresAsteriskWhenNoArgument(aggregate.AggregateName))
                this.Append("*");
            this.Append(")");
            return aggregate;
        }

        protected override Expression VisitIsNull(IsNullExpression isnull)
        {
            this.VisitValue(isnull.Expression);
            if (isnull.IsNot)
                sb.Append(" IS NOT NULL");
            else
                sb.Append(" IS NULL");
            return isnull;
        }

        protected override Expression VisitBetween(BetweenExpression between)
        {
            this.VisitValue(between.Expression);
            this.Append(" BETWEEN ");
            this.VisitValue(between.Lower);
            this.Append(" AND ");
            this.VisitValue(between.Upper);
            return between;
        }

        protected override Expression VisitRowNumber(RowNumberExpression rowNumber)
        {
            throw new NotSupportedException();
        }

        protected override Expression VisitScalar(ScalarExpression subquery)
        {
            this.Append("(");
            this.AppendLine(Indentation.Inner);
            this.Visit(subquery.Select);
            this.AppendLine(Indentation.Same);
            this.Append(")");
            this.Indent(Indentation.Outer);
            return subquery;
        }

        protected override Expression VisitExists(ExistsExpression exists)
        {
            this.Append("EXISTS(");
            this.AppendLine(Indentation.Inner);
            this.Visit(exists.Select);
            this.AppendLine(Indentation.Same);
            this.Append(")");
            this.Indent(Indentation.Outer);
            return exists;
        }

        protected override Expression VisitIn(InExpression @in)
        {
            if (@in.Values != null)
            {
                if (@in.Values.Count == 0)
                    this.Append("0 <> 0");
                if (@in.Values.Count > 1000)
                {
                    var inExpression = new StringBuilder();
                    this.Append("(");
                    this.VisitValue(@in.Expression);
                    this.Append(" IN (");
                    for (int i = 0, n = @in.Values.Count; i < n; i++)
                    {
                        if ((i+1)% 995 == 0)
                        {
                            this.Append(" ) ");
                            this.Append(" OR ");
                            this.VisitValue(@in.Expression);
                            this.Append(" IN (");
                        }
                        if (i > 0 && ((i + 1) % 995 != 0) ) this.Append(", ");
                        this.VisitValue(@in.Values[i]);
                    }
                    this.Append(" ) ");
                    this.Append(") ");
                }
                else
                {
                    this.VisitValue(@in.Expression);
                    this.Append(" IN (");
                    for (int i = 0, n = @in.Values.Count; i < n; i++)
                    {
                        if (i > 0) this.Append(", ");
                        this.VisitValue(@in.Values[i]);
                    }
                    this.Append(")");
                }
            }
            else
            {
                this.VisitValue(@in.Expression);
                this.Append(" IN (");
                this.AppendLine(Indentation.Inner);
                this.Visit(@in.Select);
                this.AppendLine(Indentation.Same);
                this.Append(")");
                this.Indent(Indentation.Outer);
            }
            return @in;
        }

        protected override Expression VisitNamedValue(NamedValueExpression value)
        {
            this.AppendParameterName(value.Name);
            return value;
        }

        protected override Expression VisitInsert(InsertCommand insert)
        {
            this.Append("INSERT INTO ");
            //if (Dialect.SupportSchema && !string.IsNullOrEmpty(insert.Table.Mapping.Schema))
            //{
            //    sb.Append(Dialect.Quote(insert.Table.Mapping.Schema));
            //    sb.Append(".");
            //}
            //this.AppendTableName(insert.Table.Name);
            WriteTableName(insert.Table.Mapping);
            this.Append("(");
            for (int i = 0, n = insert.Assignments.Count; i < n; i++)
            {
                ColumnAssignment ca = insert.Assignments[i];
                if (i > 0) this.Append(", ");
                this.AppendColumnName(ca.Column.Name);
            }
            this.Append(")");
            this.AppendLine(Indentation.Same);
            this.Append("VALUES (");
            for (int i = 0, n = insert.Assignments.Count; i < n; i++)
            {
                ColumnAssignment ca = insert.Assignments[i];
                if (i > 0) this.Append(", ");
                this.Visit(ca.Expression);
            }
            this.Append(")");
            return insert;
        }

        protected override Expression VisitUpdate(UpdateCommand update)
        {
            this.Append("UPDATE ");
            WriteTableName(update.Table.Mapping);
            //if (Dialect.SupportSchema && !string.IsNullOrEmpty(update.Table.Mapping.Schema))
            //{
            //    sb.Append(Dialect.Quote(update.Table.Mapping.Schema));
            //    sb.Append(".");
            //}
            //this.AppendTableName(update.Table.Name);
            this.AppendLine(Indentation.Same);
            bool saveHide = this.HideColumnAliases;
            this.HideColumnAliases = true;
            this.Append("SET ");
            for (int i = 0, n = update.Assignments.Count; i < n; i++)
            {
                ColumnAssignment ca = update.Assignments[i];
                if (i > 0) this.Append(", ");
                this.Visit(ca.Column);
                this.Append(" = ");
                this.Visit(ca.Expression);
            }
            if (update.Where != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("WHERE ");
                this.VisitPredicate(update.Where);
            }
            this.HideColumnAliases = saveHide;
            return update;
        }

        protected override Expression VisitDelete(DeleteCommand delete)
        {
            this.Append("DELETE FROM ");
            bool saveHideTable = this.HideTableAliases;
            bool saveHideColumn = this.HideColumnAliases;
            this.HideTableAliases = true;
            this.HideColumnAliases = true;
            this.VisitSource(delete.Table);
            if (delete.Where != null)
            {
                this.AppendLine(Indentation.Same);
                this.Append("WHERE ");
                this.VisitPredicate(delete.Where);
            }
            this.HideTableAliases = saveHideTable;
            this.HideColumnAliases = saveHideColumn;
            return delete;
        }

        protected override Expression VisitIf(IFCommand ifx)
        {
            throw new NotSupportedException();
        }

        protected override Expression VisitBlock(BlockCommand block)
        {
            for (int i = 0, n = block.Commands.Count; i < n; i++)
            {
                if (i > 0)
                {
                    this.AppendLine(Indentation.Same);
                    this.AppendLine(Indentation.Same);
                }
                this.VisitStatement(block.Commands[i]);
            }
            return block;
            //throw new NotSupportedException();
        }

        protected override Expression VisitDeclaration(DeclarationCommand decl)
        {
            throw new NotSupportedException();
        }

        protected override Expression VisitVariable(VariableExpression vex)
        {
            this.AppendVariableName(vex.Name);
            return vex;
        }

        protected virtual void VisitStatement(Expression expression)
        {
            var p = expression as ProjectionExpression;
            if (p != null)
                this.Visit(p.Select);
            else
                this.Visit(expression);
        }

        protected override Expression VisitFunction(FunctionExpression func)
        {
            this.Append(func.Name);
            if (func.Arguments.Count > 0)
            {
                this.Append("(");
                for (int i = 0, n = func.Arguments.Count; i < n; i++)
                {
                    if (i > 0) this.Append(", ");
                    this.Visit(func.Arguments[i]);
                }
                this.Append(")");
            }
            return func;
        }
    }
}
