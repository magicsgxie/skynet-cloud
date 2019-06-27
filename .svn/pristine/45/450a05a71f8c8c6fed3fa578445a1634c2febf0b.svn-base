/*----------------------------------------------------------------
// Copyright (C) 2010 深圳市优网科技有限公司
// 版权所有。 
//
// 文件名：
// 文件功能描述：
//
// 
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Render
{
	/// <summary>
	/// Provides common implementation for ISqlOmRenderer
	/// </summary>
	public abstract class SqlOmRenderer : ISqlOmRenderer //, IClauseRendererContext
	{
        protected virtual string PrefixNamed { get { return "@"; } }
        string dateFormat = "yyyy-MM-dd";
		string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
		char identifierOpeningQuote, identifierClosingQuote;
		
		/// <summary>
		/// Creates a new SqlOmRenderer
		/// </summary>
		public SqlOmRenderer(char identifierOpeningQuote , char identifierClosingQuote)
		{
            this.identifierOpeningQuote = identifierOpeningQuote;
			this.identifierClosingQuote = identifierClosingQuote;
		}

		/// <summary>
		/// Gets or sets a date format string
		/// </summary>
		/// <remarks>
		/// Use <see cref="DateFormat"/> to specify how date values should be formatted
		/// in order to be properly parsed by your database.
		/// Specific renderers set this property to the appliciable default value, so you
		/// only need to change this if your database is configured to use other then default date format.
		/// <para>
		/// DateFormat will be used to format <see cref="DateTime"/> values which have the Hour, Minute, Second and Milisecond properties set to 0.
		/// Otherwise, <see cref="DateTimeFormat"/> will be used.
		/// </para>
		/// </remarks>
		public string DateFormat
		{
			get { return dateFormat; }
			set { dateFormat = value; }
		}

		/// <summary>
		/// Gets or sets a date-time format string
		/// </summary>
		/// <remarks>
		/// Use <see cref="DateTimeFormat"/> to specify how timestamp values should be formatted
		/// in order to be properly parsed by your database.
		/// Specific renderers set this property to the appliciable default value, so you
		/// only need to change this if your database is configured to use other then default date format.
		/// </remarks>
		public string DateTimeFormat
		{
			get { return dateTimeFormat; }
			set { dateTimeFormat = value; }
		}

		/// <summary>
		/// Renders a SELECT statement
		/// </summary>
		/// <param name="query">Query definition</param>
		/// <returns>Generated SQL statement</returns>
		public abstract string RenderSelect(SelectQuery query);

		/// <summary>
		/// Renders a row count SELECT statement. 
		/// </summary>
		/// <param name="query">Query definition to count rows for</param>
		/// <returns>Generated SQL statement</returns>
		public abstract string RenderRowCount(SelectQuery query);

		/// <summary>
		/// Specifies weather all identifiers should be converted to upper case while rendering
		/// </summary>
		protected virtual bool UpperCaseIdentifiers { get {  return false; } }

        private NameValueCollection _namedParameters = new NameValueCollection();

        public NameValueCollection NamedParameters
        {
            get
            {
                return _namedParameters;
            }
        }

        //bool IClauseRendererContext.UpperCaseIdentifiers { get {  return this.UpperCaseIdentifiers; } }

        /// <summary>
        /// Renders an UPDATE statement
        /// </summary>
        /// <param name="query">UPDATE query definition</param>
        /// <returns>Generated SQL statement</returns>
        public virtual string RenderUpdate(UpdateQuery query)
		{
			return UpdateStatement(query);
		}

		/// <summary>
		/// Renders an INSERT statement
		/// </summary>
		/// <param name="query">INSERT query definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderInsert(InsertQuery query)
		{
			return InsertStatement(query);
		}

		/// <summary>
		/// Renders an DELETE statement
		/// </summary>
		/// <param name="query">DELETE query definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderDelete(DeleteQuery query)
		{
			return DeleteStatement(query);
		}

		
		/// <summary>
		/// Renders a UNION clause
		/// </summary>
		/// <param name="union">Union definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderUnion(SqlUnion union)
		{
			StringBuilder builder = new StringBuilder();
			foreach(SqlUnionItem item in union.Items)
			{
				if (item != union.Items[0])
					builder.AppendFormat(" union {0} ", (item.RepeatingAction == DistinctModifier.All) ? "all" : "");
				builder.Append(RenderSelect(item.Query));
			}
			return builder.ToString();
		}

		/// <summary>
		/// Renders a SELECT statement which a result-set page
		/// </summary>
		/// <param name="pageIndex">The zero based index of the page to be returned</param>
		/// <param name="pageSize">The size of a page</param>
		/// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
		/// <param name="query">Query definition to apply paging on</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// To generate pagination SQL you must supply <paramref name="totalRowCount"/>.
		/// To aquire the total number of rows use the <see cref="RenderRowCount"/> method.
		/// </remarks>
		public virtual string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
		{
			if (query.OrderByTerms.Count == 0)
				throw new InvalidQueryException("OrderBy must be specified for paging to work.");

			int currentPageSize = pageSize;
			if (pageSize * (pageIndex + 1) > totalRowCount)
				currentPageSize = totalRowCount - pageSize * pageIndex;
			if (currentPageSize < 0)
				currentPageSize = 0;
				
			SelectQuery baseQuery = query.Clone();
			
			baseQuery.Top = (pageIndex + 1) * pageSize;
			//baseQuery.Columns.Add(new SelectColumn("*"));
			foreach(OrderByTerm term in baseQuery.OrderByTerms)
				baseQuery.Columns.Add(new SelectColumn(term.Field, term.Table, FormatSortFieldName(term.Field), SqlAggregationFunction.None));

			string baseSql = RenderSelect(baseQuery);
			
			SelectQuery reverseQuery = new SelectQuery();
			reverseQuery.Columns.Add(new SelectColumn("*"));
			reverseQuery.Top = currentPageSize;
			reverseQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "r");
			ApplyOrderBy(baseQuery.OrderByTerms, reverseQuery, false, reverseQuery.FromClause.BaseTable);
			string reverseSql = RenderSelect(reverseQuery);

			SelectQuery forwardQuery = new SelectQuery();
			foreach(SelectColumn originalCol in query.Columns)
			{
				FromTerm forwardTable = FromTerm.TermRef("f");
				SqlExpression expr = null;
				if (originalCol.ColumnAlias != null)
					expr = SqlExpression.Field(originalCol.ColumnAlias, forwardTable);
				else if (	originalCol.Expression.Type == SqlExpressionType.Field || 
					originalCol.Expression.Type == SqlExpressionType.Constant)
				{
					expr = SqlExpression.Field((string)originalCol.Expression.Value, forwardTable);
				}

				if (expr != null)
					forwardQuery.Columns.Add(new SelectColumn(expr, originalCol.ColumnAlias));
			}

			forwardQuery.FromClause.BaseTable = FromTerm.SubQuery(reverseSql, "f");
			ApplyOrderBy(baseQuery.OrderByTerms, forwardQuery, true, forwardQuery.FromClause.BaseTable);

			return RenderSelect(forwardQuery).Replace("\"", "").SimplifyBracket();
		}

		string FormatSortFieldName(string fieldName)
		{
			return "sort_" + fieldName;
		}

		void ApplyOrderBy(OrderByTermCollection terms, SelectQuery orderQuery, bool forward, FromTerm table)
		{
			foreach(OrderByTerm expr in terms)
			{
				OrderByDirection dir = expr.Direction;
				
				//Reverse order direction if required
				if (!forward && dir == OrderByDirection.Ascending) 
					dir = OrderByDirection.Descending;
				else if (!forward && dir == OrderByDirection.Descending) 
					dir = OrderByDirection.Ascending;
					
				orderQuery.OrderByTerms.Add(new OrderByTerm(FormatSortFieldName(expr.Field.ToString()), table , dir));
			}
		}

		//protected abstract void SelectStatement(StringBuilder builder);

        /// <summary>
        /// Renders a the section of a SELECT clause
        /// </summary>
        /// <param name="builder">Select statement string builder</param>
        /// <param name="section"> SQL section</param>
        protected virtual void Select(StringBuilder builder, string section)
        {
            if (builder.Length == 0)
            {
                builder.Append("select ");
            }
            if (!string.IsNullOrEmpty(section))
                builder.Append(section);
        }

		/// <summary>
		/// Renders a the beginning of a SELECT clause with an optional DISTINCT setting
		/// </summary>
		/// <param name="builder">Select statement string builder</param>
		/// <param name="distinct">Turns on or off SQL distinct option</param>
		protected virtual void Select(StringBuilder builder, bool distinct)
		{
            if (builder.Length == 0)
            {
                builder.Append("select ");
            }
			if (distinct)
				builder.Append("distinct ");
		}
		
		/// <summary>
		/// Renders columns of SELECT clause
		/// </summary>
		protected virtual void SelectColumns(StringBuilder builder, SelectColumnCollection columns)
		{
			foreach(SelectColumn col in columns)
			{
				if (col != columns[0])
					Coma(builder);

				SelectColumn(builder, col);
			}
		}
		
		/// <summary>
		/// Renders a sinle select column
		/// </summary>
		protected virtual void SelectColumn(StringBuilder builder, SelectColumn col)
		{
			Expression(builder, col.Expression);
			if (col.ColumnAlias != null)
			{
				builder.Append(" ");
				Identifier(builder, col.ColumnAlias);
			}
		}
		
		/// <summary>
		/// Renders a separator between select columns
		/// </summary>
		protected virtual void Coma(StringBuilder builder)
		{
			builder.Append(", ");
		}

		/// <summary>
		/// Renders the begining of a FROM clause
		/// </summary>
		/// <param name="builder"></param>
		protected virtual void From(StringBuilder builder)
		{
			builder.Append(" from ");
		}

		/// <summary>
		/// Renders the terms of a from clause
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="fromClause"></param>
		/// <param name="tableSpace">Common prefix for all tables in the clause</param>
		protected virtual void FromClause(StringBuilder builder, FromClause fromClause, string tableSpace)
		{
			From(builder);
			RenderFromTerm(builder, fromClause.BaseTable, tableSpace);

			foreach(Join join in fromClause.Joins)
			{
				builder.AppendFormat(" {0} join ", join.Type.ToString().ToLower());
				RenderFromTerm(builder, join.RightTable, tableSpace);
			
				if (join.Type != JoinType.CrossJoin)
				{
					builder.AppendFormat(" on ");
					WhereClause(builder, join.Conditions);
				}
			}
		}
		
		/// <summary>
		/// Renders a single FROM term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="table"></param>
		/// <param name="tableSpace">Common prefix for all tables in the term</param>
		protected virtual void RenderFromTerm(StringBuilder builder, FromTerm table, string tableSpace)
		{
            if (table.Type == FromTermType.Table)
            {
                if (table.Ns1 != null)
                    TableNamespace(builder, table.Ns1);
                if (table.Ns2 != null)
                    TableNamespace(builder, table.Ns2);
                if (table.Ns1 == null && table.Ns2 == null && tableSpace != null)
                    TableNamespace(builder, tableSpace);
                Identifier(builder, (string)table.Expression);
            }
            else if (table.Type == FromTermType.SubQuery)
                builder.AppendFormat("( {0} )", table.Expression);
            else if (table.Type == FromTermType.SubQueryObj)
                builder.AppendFormat("( {0} )  {1} ", RenderSelect((SelectQuery)table.Expression), table.Alias);
            else if (table.Type == FromTermType.SqlUnion)
                builder.AppendFormat("({0}) {1} ", RenderUnion((SqlUnion)table.Expression), table.Alias);
            else
                throw new InvalidQueryException("Unknown FromExpressionType: " + table.Type.ToString());
			
			if (table.Alias != null)
			{
				builder.AppendFormat(" ");
				Identifier(builder, table.Alias);
			}
		}

		/// <summary>
		/// Renders the table namespace
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="ns"></param>
		protected virtual void TableNamespace(StringBuilder builder, string ns)
		{
			builder.AppendFormat("{0}.", ns);
		}

		/// <summary>
		/// Renders the begining of a WHERE statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void Where(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat(" where ");
		}

		/// <summary>
		/// Renders the begining of a HAVING statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void Having(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat(" having ");
		}

		/// <summary>
		/// Recursivly renders a WhereClause
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void WhereClause(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat("(");

            for (int i = 0; i < group.Terms.Count; i++)
            {
                WhereTerm term = (WhereTerm)group.Terms[i];

                WhereClause(builder, term);

                if (i < group.Terms.Count - 1 || group.SubClauses.Count > 0)
                {
                    RelationshipOperator(builder, term.Relationship);
                }
            } 

            for (int i = 0; i < group.SubClauses.Count; i++)
			{
                WhereClause childGroup =(WhereClause)group.SubClauses[i];

				if (childGroup.IsEmpty)
					continue; 

				WhereClause(builder, childGroup);

                if (i < group.SubClauses.Count - 1)
                {
                    RelationshipOperator(builder, childGroup.Relationship);
                }
			}

			builder.AppendFormat(")");
		}

		/// <summary>
		/// Renders bitwise and
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void BitwiseAnd(StringBuilder builder, WhereTerm term)
		{
			builder.Append("(");
			Expression(builder, term.Expr1);
			builder.Append(" & ");
			Expression(builder, term.Expr2);
			builder.Append(") > 0");
		}

		/// <summary>
		/// Renders a single WhereTerm
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void WhereClause(StringBuilder builder, WhereTerm term)
		{
			if (term.Type == WhereTermType.Compare)
			{
                //var expr1 = term.Expr1;
				Expression(builder, term.Expr1);
				builder.Append(" ");
				Operator(builder, term.Op);
				builder.Append(" ");
                //var expr2 = term.Expr2;
				Expression(builder, term.Expr2);
                //NamedParameters.Add(expr1., term.Expr1);
			}
			else if (term.Type == WhereTermType.In || term.Type == WhereTermType.NotIn || term.Type == WhereTermType.InSubQuery || term.Type == WhereTermType.NotInSubQuery)
			{
				Expression(builder, term.Expr1);
				if (term.Type == WhereTermType.NotIn || term.Type == WhereTermType.NotInSubQuery)
					builder.Append(" not");
				builder.Append(" in (");
				if (term.Type == WhereTermType.InSubQuery || term.Type == WhereTermType.NotInSubQuery)
					builder.Append((string)term.SubQuery);
				else
					ConstantList(builder, term.Values);
				builder.Append(")");
			}
			else if (term.Type == WhereTermType.Exists || term.Type == WhereTermType.NotExists)
			{
				if (term.Type == WhereTermType.NotExists)
					builder.Append(" not");
				builder.Append(" exists (");
				builder.Append((string)term.SubQuery);
				builder.Append(")");
            }
            else if (term.Type == WhereTermType.Between)
            {
                Expression(builder, term.Expr1);
                builder.AppendFormat(" between ");
                Expression(builder, term.Expr2);
                builder.AppendFormat(" and ");
                Expression(builder, term.Expr3);
            }
            else if (term.Type == WhereTermType.CustomSql)
            {
                builder.Append((string)term.SubQuery);
            }
			else if (term.Type == WhereTermType.IsNull || term.Type == WhereTermType.IsNotNull)
			{
				Expression(builder, term.Expr1);
				builder.Append(" is "); 
				if (term.Type == WhereTermType.IsNotNull)
					builder.Append("not ");
				builder.Append(" null ");
			}

		}

		/// <summary>
		/// Renders IfNull SqlExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected abstract void IfNull(StringBuilder builder, SqlExpression expr);



        /// <summary>
        /// Renders a SqlExpression of type Function 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <param name="param"></param>
        protected virtual void Round(StringBuilder builder, SqlExpression expr)
        {
            builder.Append("round(");
            Expression(builder, expr.SubExpr1);
            builder.Append(",");
            builder.Append(expr.Value);
            builder.Append(")");
        }

		/// <summary>
		/// Renders SqlExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected virtual void Expression(StringBuilder builder, SqlExpression expr)
		{
			SqlExpressionType type = expr.Type;
			if (type == SqlExpressionType.Field)
				QualifiedIdentifier(builder, expr.TableAlias, expr.Value.ToString());
			else if (type == SqlExpressionType.Function)
				Function(builder, expr.AggFunction, expr.SubExpr1);
			else if (type == SqlExpressionType.Constant)
				Constant(builder, (SqlConstant)expr.Value);
			else if (type == SqlExpressionType.SubQueryText)
				builder.AppendFormat("({0})", (string)expr.Value);
			else if (type == SqlExpressionType.SubQueryObject)
				builder.AppendFormat("({0})", RenderSelect((SelectQuery)expr.Value));
			else if (type == SqlExpressionType.PseudoField)
				builder.AppendFormat("{0}", (string)expr.Value);
			else if (type == SqlExpressionType.Parameter)
				builder.AppendFormat("{0}{1}", this.PrefixNamed, (string)expr.Value);
			else if (type == SqlExpressionType.Raw)
				builder.AppendFormat("{0}", (string)expr.Value);
			else if (type == SqlExpressionType.Case)
				CaseClause(builder, expr.CaseClause);
			else if (type == SqlExpressionType.IfNull)
				IfNull(builder, expr);
			else if (type == SqlExpressionType.Null)
				builder.Append("null");
			else if (type == SqlExpressionType.Round)
                Round(builder, expr);
			else
				throw new InvalidQueryException("Unkown expression type: " + type.ToString());
		}

		/// <summary>
		/// Renders a SqlExpression of type Function 
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="func"></param>
		/// <param name="param"></param>
		protected virtual void Function(StringBuilder builder, SqlAggregationFunction func, SqlExpression param)
		{
			builder.AppendFormat("{0}(", func.ToString());
			Expression(builder, param);
			builder.AppendFormat(")");
		}

		/// <summary>
		/// Renders a constant
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected virtual void Constant(StringBuilder builder, SqlConstant expr)
		{
			SqlDataType type = expr.Type;

			if (type == SqlDataType.Number)
				builder.Append(expr.Value.ToString());
			else if (type == SqlDataType.String)
				builder.AppendFormat("'{0}'", (expr.Value == null) ? "" : expr.Value.ToString());
			else if (type == SqlDataType.Date)
			{
				DateTime val = (DateTime)expr.Value;
				bool dateOnly = (val.Hour == 0 && val.Minute == 0 && val.Second == 0 && val.Millisecond == 0);
				string format = (dateOnly) ? dateFormat : dateTimeFormat;
				builder.AppendFormat("'{0}'", val.ToString(format));
			}
		}
		
		/// <summary>
		/// Renders a comaprison operator
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="op"></param>
		protected virtual void Operator(StringBuilder builder, FilterOperator op)
		{
			if (op == FilterOperator.IsEqualTo)
				builder.Append("=");
			else if (op == FilterOperator.IsNotEqualTo)
				builder.Append("<>");
			else if (op == FilterOperator.IsGreaterThan)
				builder.Append(">");
			else if (op == FilterOperator.IsLessThan)
				builder.Append("<");
			else if (op == FilterOperator.IsLessThanOrEqualTo)
				builder.Append("<=");
			else if (op == FilterOperator.IsGreaterThanOrEqualTo)
				builder.Append(">=");
			else if (op == FilterOperator.Contains)
				builder.Append("like");
            else if (op == FilterOperator.IsNull)
                builder.Append("is null");
            else if (op == FilterOperator.IsNotNull)
                builder.Append("is not null");
			else
				throw new InvalidQueryException("Unkown operator: " + op.ToString());
		}
		
		/// <summary>
		/// Renders a list of values
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		protected virtual void ConstantList(StringBuilder builder, SqlConstantCollection values)
		{
			for(int i = 0; i < values.Count; i++)
			{
				SqlConstant val = values[i];
				Constant(builder, val);
				if (i != values.Count - 1)
					Coma(builder);
			}
		}

		/// <summary>
		/// Encodes a textual string.
		/// </summary>
		/// <param name="val">Text to be encoded</param>
		/// <returns>Encoded text</returns>
		/// <remarks>All text string must be encoded before they are appended to a SQL statement.</remarks>
		public virtual string SqlEncode(string val)
		{
			return val.Replace("'", "''");
		}

		/// <summary>
		/// Renders a relationship operator
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="relationship"></param>
		protected virtual void RelationshipOperator(StringBuilder builder, FilterCompositionLogicalOperator relationship)
		{
			builder.AppendFormat(" {0} ", relationship.ToString().ToLower());
		}

		/// <summary>
		/// Renders the begining of a GROUP BY statement.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="groupByTerms"></param>
		/// <remarks>If <paramref name="groupByTerms"/> has no items, nothing will be appended.</remarks>
		protected virtual void GroupBy(StringBuilder builder, GroupByTermCollection groupByTerms)
		{
			if (groupByTerms.Count > 0)
				builder.Append(" group by ");
		}
		
		/// <summary>
		/// Renders GROUP BY terms 
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="groupByTerms"></param>
		protected virtual void GroupByTerms(StringBuilder builder, GroupByTermCollection groupByTerms)
		{
			foreach(GroupByTerm clause in groupByTerms)
			{
				if (clause != groupByTerms[0])
					builder.Append(", ");
				
				GroupByTerm(builder, clause);
			}
		}
		
		/// <summary>
		/// Renders a single GROUP BY term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void GroupByTerm(StringBuilder builder, GroupByTerm term)
		{
			QualifiedIdentifier(builder, term.TableAlias, term.Field);
		}

		/// <summary>
		/// Renders the begining of a ORDER BY statement.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="orderByTerms"></param>
		/// <remarks>If <paramref name="orderByTerms"/> has no items, nothing will be appended.</remarks>
		protected virtual void OrderBy(StringBuilder builder, OrderByTermCollection orderByTerms)
		{
			if (orderByTerms.Count > 0)
				builder.Append(" order by ");
		}

		/// <summary>
		/// Renders ORDER BY terms
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="orderByTerms"></param>
		protected virtual void OrderByTerms(StringBuilder builder, OrderByTermCollection orderByTerms)
		{
			for(int i = 0; i < orderByTerms.Count; i++)
			{
				OrderByTerm term = (OrderByTerm)orderByTerms[i];
				if (i > 0)
					builder.Append(", ");
				
				OrderByTerm(builder, term);
			}
		}
		
		/// <summary>
		/// Renders a single ORDER BY term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void OrderByTerm(StringBuilder builder, OrderByTerm term)
		{
			string dir = (term.Direction == OrderByDirection.Descending) ? "desc" : "asc";
            if (term.Exp != null)
            {
                Expression(builder, term.Exp);
            }
            else
            {
                QualifiedIdentifier(builder, term.TableAlias, term.Field);
            }
			builder.AppendFormat(" {0}", dir);
		}

		/// <summary>
		/// Renders an identifier name.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="name">Identifier name</param>
		protected virtual void Identifier(StringBuilder builder, string name)
		{
			if (name == "*")
				builder.Append(name);
			else
			{
				if (UpperCaseIdentifiers)
					name = name.ToUpper();
				builder.AppendFormat("{0}{1}{2}", identifierOpeningQuote, name, identifierClosingQuote);
			}
		}

		/// <summary>
		/// Renders a fully qualified identifer.
		/// </summary>
		/// <param name="builder">Select statement string builder</param>
		/// <param name="qnamespace">Identifier namespace</param>
		/// <param name="name">Identifier name</param>
		/// <remarks>
		/// <see cref="QualifiedIdentifier"/> is usually to render database fields with optional table alias prefixes.
		/// <paramref name="name"/> is a mandatory parameter while <paramref name="qnamespace"/> is optional.
		/// If <paramref name="qnamespace"/> is null, identifier will be rendered without a namespace (aka table alias)
		/// </remarks>
		protected virtual void QualifiedIdentifier(StringBuilder builder, string qnamespace, string name)
		{
			if (qnamespace != null)
			{
				Identifier(builder, qnamespace);
				builder.Append(".");
			}
				
			Identifier(builder, name);
		}

		/// <summary>
		/// Renders a the beginning of an UPDATE clause with the table name
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName">Name of the table to be updated</param>
		protected virtual void Update(StringBuilder builder, string tableName)
		{
			builder.Append("update ");
			Identifier(builder, tableName);
			builder.Append(" set ");
		}

		/// <summary>
		/// Renders update phrases
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void UpdateTerms(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm updateTerm in terms)
			{
				if (terms[0] != updateTerm)
					Coma(builder);
				UpdateTerm(builder, updateTerm);
			}
		}

		/// <summary>
		/// Render a single update phrase
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void UpdateTerm(StringBuilder builder, UpdateTerm term)
		{
			Identifier(builder, term.FieldName);
			builder.Append(" = ");
			Expression(builder, term.Value);
		}

		/// <summary>
		/// Renders the whole UPDATE statement using ANSI SQL standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL string</returns>
		public virtual string UpdateStatement(UpdateQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
			Update(builder, query.TableName);
			UpdateTerms(builder, query.Terms);
			Where(builder, query.WhereClause);
			WhereClause(builder, query.WhereClause);

			return builder.ToString();
		}

		/// <summary>
		/// Render the beginning of an INSERT statement with table name
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName"></param>
		protected virtual void Insert(StringBuilder builder, string tableName)
		{
			builder.Append("insert into ");
			Identifier(builder, tableName);
			builder.Append(" ");
		}

		/// <summary>
		/// Render the list of columns which are to be inserted.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void InsertColumns(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm term in terms)
			{
				if (terms[0] != term)
					Coma(builder);
				InsertColumn(builder, term);
			}
		}

		/// <summary>
		/// Render a single column name in an INSERT statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void InsertColumn(StringBuilder builder, UpdateTerm term)
		{
			Identifier(builder, term.FieldName);
		}

		/// <summary>
		/// Render the values of an INSERT statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void InsertValues(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm term in terms)
			{
				if (terms[0] != term)
					Coma(builder);
				InsertValue(builder, term);
			}
		}

		/// <summary>
		/// Render a single INSERT value
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void InsertValue(StringBuilder builder, UpdateTerm term)
		{
			Expression(builder, term.Value);
		}

		/// <summary>
		/// Render the whole INSERT statement in ANSI standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL INSERT statement</returns>
		public virtual string InsertStatement(InsertQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
			Insert(builder, query.TableName);

			builder.Append("(");
			InsertColumns(builder, query.Terms);
			builder.Append(") values (");
			InsertValues(builder, query.Terms);
			builder.Append(")");
			return builder.ToString();
		}

		/// <summary>
		/// Render the beginning of a DELETE statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName"></param>
		protected virtual void Delete(StringBuilder builder, string tableName)
		{
			builder.Append("delete from ");
			Identifier(builder, tableName);
			builder.Append(" ");
		}

		/// <summary>
		/// Render the whole DELETE statement in ANSI SQL standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL statement</returns>
		public virtual string DeleteStatement(DeleteQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
			Delete(builder, query.TableName);
			Where(builder, query.WhereClause);
			WhereClause(builder, query.WhereClause);
			return builder.ToString();
		}

		/// <summary>
		/// Renders a CaseCluase
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="clause"></param>
		protected virtual void CaseClause(StringBuilder builder, CaseClause clause)
		{
			builder.Append(" case ");
			foreach(CaseTerm term in clause.Terms)
				CaseTerm(builder, term);
			if (clause.ElseValue != null)
			{
				builder.Append(" else ");
				Expression(builder, clause.ElseValue);
			}

			builder.Append(" end ");
		}

		/// <summary>
		/// Renders a CaseTerm
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void CaseTerm(StringBuilder builder, CaseTerm term)
		{
			builder.Append(" when ");
			WhereClause(builder, term.Condition);
			builder.Append(" then ");
			Expression(builder, term.Value);
		}

/*		protected void SelectModifiers(StringBuilder builder, SelectQuery query)
		{
		}

		protected void GroupByModifiers(StringBuilder builder, SelectQuery query)
		{
		}

		//protected void SelectStatement(StringBuilder builder, bool distinct, int top, SelectColumnCollection columns, FromClause from, WhereClause where, GroupByTermCollection groupBy, bool withCube, bool withRollup, OrderByTermCollection orderBy, WhereClause having)
		protected void SelectStatement(StringBuilder builder, SelectQuery query)
		{
			query.Validate();
			
			StringBuilder selectBuilder = new StringBuilder();

			//Start the select statement
			Select(selectBuilder, query.Distinct);
			SelectModifiers(selectBuilder, query);
			SelectColumns(selectBuilder, query.Columns);

			this.FromClause(selectBuilder, query.FromClause, query.TableSpace);
			
			this.Where(selectBuilder, query.WherePhrase);
			this.WhereClause(selectBuilder, query.WherePhrase);

			GroupBy(selectBuilder, query.GroupByTerms);
			GroupByTerms(selectBuilder, query.GroupByTerms);
			GroupByModifiers(selectBuilder, query);
			
			Having(selectBuilder, query.HavingPhrase);
			WhereClause(selectBuilder, query.HavingPhrase);

			OrderBy(selectBuilder, query.OrderByTerms);
			OrderByTerms(selectBuilder, query.OrderByTerms);
		}
*/		
	}
}
