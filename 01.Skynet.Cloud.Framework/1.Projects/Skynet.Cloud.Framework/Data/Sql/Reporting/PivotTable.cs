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
using System.Text;
using System.Data;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Reporting
{
	/// <summary>
	/// Facilates Pivot Table (Cross-Tab) transformation from relational data
	/// </summary>
	/// <remarks>
	/// Use <see cref="PivotTable"/> class to create Cross-Tab reports.
	/// <para>
	/// Set <see cref="BaseQuery"/> or <see cref="BaseSql"/> property to SQL 
	/// which produces data that needs to be pivoted. Set <see cref="RowField"/> to specify
	/// how data should be grouped. Set <see cref="ValueField"/> and <see cref="Function"/> to 
	/// specify how the values in the cross tab should be collected. Create at least one 
	/// PivotColumn which specifies the data column to pivot on and the which Cross-Tab 
	/// columns are to be created.
	/// </para>
	/// </remarks>
	public class PivotTable
	{
		string rowField, valueField;
		SqlAggregationFunction valueFuntion;
		PivotColumnCollection columns = new PivotColumnCollection();
		bool withIsTotal = true;
		bool withTotal = true;
		string isTotalColumnName = "IsTotal";
		SelectQuery baseQuery;
		string baseSql;

		const string isRowSubTotalField = "pivot_rowSubTotal";

		/// <summary>
		/// Creates a new PivotTable instance
		/// </summary>
		public PivotTable()
		{
		}

		void Validate()
		{
			if (columns.Count == 0)
				throw new PivotTableException("PivotTable must have at least one PivotColumn.");
			if (valueField == null)
				throw new PivotTableException("ValueField must be set.");
			if (baseQuery == null && baseSql == null)
				throw new PivotTableException("BaseQuery or BaseSql property must be set.");
			if (Function == SqlAggregationFunction.None)
				throw new PivotTableException("Function property must be set.");
			if (RowField == null)
				throw new PivotTableException("RowField property must be set.");
		}

		/// <summary>
		/// Creates a <see cref="SelectQuery"/> which produces the defined cross tab
		/// </summary>
		/// <returns>A <see cref="SelectQuery"/> instance</returns>
		public SelectQuery BuildPivotSql()
		{
			Validate();

			SelectQuery query = new SelectQuery();
			query.Columns.Add(new SelectColumn(rowField));
			if (withIsTotal && withTotal)
				query.Columns.Add(new SelectColumn(rowField, null, isTotalColumnName, SqlAggregationFunction.Grouping));
			foreach(PivotColumn column in columns)
				AddCrossTabColumns(query, column);

			query.FromClause.BaseTable = GetBaseFromTerm();

			query.GroupByTerms.Add(new GroupByTerm(rowField));	
			query.GroupByWithRollup = withTotal;
			return query;
		}

		/// <summary>
		/// Creates a <see cref="SelectQuery"/> which produces drill-down results
		/// </summary>
		/// <param name="crossTabRowKey">Value identifying cross-tab's row</param>
		/// <param name="crossTabColumnName">Name of a cross-tab column</param>
		/// <returns>A <see cref="SelectQuery"/> which produces drill-down results of the specified cross-tab cell</returns>
		public SelectQuery BuildDrillDownSql(SqlConstant crossTabRowKey, string crossTabColumnName)
		{
			Validate();
            SelectQuery query = new SelectQuery();
			query.Columns.Add(new SelectColumn("*"));
			query.FromClause.BaseTable = GetBaseFromTerm();

			PivotColumn pivotCol;
			PivotColumnValue pivotVal;
			if (!FindPivotColumnValue(crossTabColumnName, out pivotCol, out pivotVal))
				throw new PivotTableException(string.Format("Cross-Tab column '{0}' could not be found in pivot transformation definition.", crossTabColumnName));
			query.WherePhrase.Terms.Add(CreateColumnValueCondition(pivotCol, pivotVal));

			if (crossTabRowKey != null)
				query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field(rowField), SqlExpression.Constant(crossTabRowKey), FilterOperator.IsEqualTo));

            return query;
		}

		bool FindPivotColumnValue(string crossTabColumnName, out PivotColumn pivotCol, out PivotColumnValue pivotVal)
		{
			pivotCol = null;
			pivotVal = null;
			foreach(PivotColumn col in columns)
			{
				foreach(PivotColumnValue val in col.Values)
				{
					if (string.Compare(val.CrossTabColumnName, crossTabColumnName, true) == 0)
					{
						pivotCol = col;
						pivotVal = val;
						return true;
					}
				}
			}
			return false;
		}

		FromTerm GetBaseFromTerm()
		{
			return (baseQuery != null) ? FromTerm.SubQuery(baseQuery, "b") : FromTerm.SubQuery(baseSql, "b");
		}


		void AddCrossTabColumns(SelectQuery query, PivotColumn column)
		{
			foreach(PivotColumnValue val in column.Values) 
				query.Columns.Add(new SelectColumn(SqlExpression.Function(valueFuntion, PivotCaseExpression(column, val)), val.CrossTabColumnName));
		}

		SqlExpression PivotCaseExpression(PivotColumn col, PivotColumnValue val)
		{
			CaseClause caseClause = new CaseClause();
			caseClause.ElseValue = SqlExpression.Null();

			CaseTerm term = new CaseTerm(PivotCaseCondition(col, val), SqlExpression.Field(valueField));
			caseClause.Terms.Add(term);
			return SqlExpression.Case(caseClause);
		}

		WhereClause PivotCaseCondition(PivotColumn col, PivotColumnValue val)
		{
			WhereClause clause = new WhereClause(FilterCompositionLogicalOperator.And);
			clause.Terms.Add(CreateColumnValueCondition(col, val));
			return clause;
		}

		WhereTerm CreateColumnValueCondition(PivotColumn col, PivotColumnValue val)
		{
			if (val.ValueType == PivotColumnValueType.Scalar)
				return WhereTerm.CreateCompare(SqlExpression.Field(col.ColumnField), SqlExpression.Constant(new SqlConstant(col.DataType, val.Value)), FilterOperator.IsEqualTo);
			else
				return CreateRangeTerm(col, val);
		}

		WhereTerm CreateRangeTerm(PivotColumn pivotCol, PivotColumnValue pivotColValue)
		{
			Range step = pivotColValue.Range;
			SqlExpression fieldExpr = SqlExpression.Field(pivotCol.ColumnField);
			if (step.HighBound == null && step.LowBound == null)
				throw new PivotTableException("At least one bound of a Range must be set.");

			SqlExpression lowBoundExpr = (step.LowBound != null) ? SqlExpression.Constant(pivotCol.DataType, pivotColValue.Range.LowBound) : null;
			SqlExpression highBoundExpr = (step.HighBound != null) ? SqlExpression.Constant(pivotCol.DataType, pivotColValue.Range.HighBound) : null;

			WhereTerm term;
			if (step.HighBound == null)
				term = WhereTerm.CreateCompare(fieldExpr, lowBoundExpr, FilterOperator.IsGreaterThanOrEqualTo);
			else if (step.LowBound == null)
				term = WhereTerm.CreateCompare(fieldExpr, highBoundExpr, FilterOperator.IsLessThan);
			else
				term = WhereTerm.CreateBetween(fieldExpr, lowBoundExpr, highBoundExpr);

			return term;
		}

		/// <summary>
		/// Gets or sets the ValueField
		/// </summary>
		public string ValueField
		{
			get { return valueField; }
			set { valueField = value; }
		}

		/// <summary>
		/// Gets or sets the RowField name
		/// </summary>
		public string RowField
		{
			get { return rowField; }
			set { rowField = value; }
		}

		/// <summary>
		/// Gets or sets the function to be performed on ValueField
		/// </summary>
		public SqlAggregationFunction Function
		{
			get { return valueFuntion; }
			set { valueFuntion = value; }
		}

		/// <summary>
		/// Gets the collection of PivotColumn objects
		/// </summary>
		public PivotColumnCollection Columns
		{
			get { return columns; }
		}

		/// <summary>
		/// Gets or sets wheather IsTotal column should be added
		/// </summary>
		/// <remarks>
		/// When <see cref="WithIsTotal"/> is true, an additional column called "IsTotal"
		/// will be added to the result set. Value of 1 in the column indicates that the row
		/// is a total row.
		/// </remarks>
		public bool WithIsTotal
		{
			get { return withIsTotal; }
			set { withIsTotal = value; }
		}

		/// <summary>
		/// Gets or sets wheather totals should be calculated.
		/// </summary>
		public bool WithTotal
		{
			get { return withTotal; }
			set { withTotal = value; }
		}

		/// <summary>
		/// Gets or sets the name of the column which indicates weather the row is a total summary row.
		/// </summary>
		public string IsTotalColumnName
		{
			get { return isTotalColumnName; }
			set { isTotalColumnName = value; }
		}

		/// <summary>
		/// Gets or sets the base query as a SelectQuery object
		/// </summary>
		/// <remarks>
		/// The <see cref="BaseQuery"/> and <see cref="BaseSql"/> are mutually exclusive.
		/// </remarks>
		public SelectQuery BaseQuery
		{
			get { return baseQuery; }
			set { baseQuery = value; baseSql = null; }
		}

		/// <summary>
		/// Gets or sets the base query as a SQL string
		/// </summary>
		public string BaseSql
		{
			get { return baseSql; }
			set { baseSql = value; baseQuery = null;}
		}
	}
}
