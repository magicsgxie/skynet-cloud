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

namespace UWay.Skynet.Cloud.Data
{

	/// <summary>
	/// Describes one expression of a <see cref="WhereTerm"/>
	/// </summary>
	/// <remarks>
	/// SqlExpression has no public constructor. Use one of the supplied static methods to create the type
	/// of the expression you need. 
	/// <para>
	/// <see cref="SqlExpression"/> can represent a database OtherData, or a static value. 
	/// To create a <see cref="SqlExpression"/> represnting a OtherData use the <seealso cref="SqlExpression.Field"/> method.
	/// To create a static value, use one of the methods <see cref="SqlExpression.String"/>, <see cref="SqlExpression.Date"/> or <see cref="SqlExpression.Number"/> accordingly to the type of the value.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// ...
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("John"), CompareOperator.Equal));
	/// ...
	/// </code>
    /// </example>
    [Serializable]
	public class SqlExpression
	{
		object val;
		SqlExpressionType type;
		FromTerm table = null;
		SqlAggregationFunction func = SqlAggregationFunction.None;

		CaseClause caseClause = new CaseClause();
		SqlExpression subExpr1, subExpr2;


        /// <summary>
        /// 构造函数
        /// </summary>
		private SqlExpression()
		{
		}

		/// <summary>
		/// Creates a SqlExpression which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a numeric value</returns>
		public static SqlExpression Number(double val)
		{
			return Constant(SqlConstant.Number(val));
		}

		/// <summary>
		/// Creates a SqlExpression which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a numeric value</returns>
		public static SqlExpression Number(int val)
		{
			return Constant(SqlConstant.Number(val));		
		}

		/// <summary>
		/// Creates a SqlExpression which represents a textual value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a textual value</returns>
		public static SqlExpression String(string val)
		{
			return Constant(SqlConstant.String(val));
		}


		/// <summary>
		/// Creates a SqlExpression which represents a date value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a date value</returns>
		public static SqlExpression Date(DateTime val)
		{
			return Constant(SqlConstant.Date(val));
		}
		
		/// <summary>
		/// Creates a SqlExpression which represents a constant typed value.
		/// </summary>
		/// <param name="val">SqlConstant instance</param>
		/// <returns>A SqlExpression which represents a date value</returns>
		public static SqlExpression Constant(SqlConstant val)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = val;
			expr.type = SqlExpressionType.Constant;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a constant typed value
		/// </summary>
		/// <param name="dataType">Value's data type</param>
		/// <param name="val">The value</param>
		/// <returns></returns>
		public static SqlExpression Constant(SqlDataType dataType, object val)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = new SqlConstant(dataType, val);
			expr.type = SqlExpressionType.Constant;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a OtherData in a database table.
		/// </summary>
		/// <param name="fieldName">Name of a OtherData</param>
		/// <param name="table">The table this OtherData belongs to</param>
		/// <returns></returns>
		public static SqlExpression Field(string fieldName, FromTerm table)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = fieldName;
			expr.table = table;
			expr.type = SqlExpressionType.Field;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with a CASE statement.
		/// </summary>
		/// <param name="caseClause"></param>
		/// <returns></returns>
		public static SqlExpression Case(CaseClause caseClause)
		{
			SqlExpression expr = new SqlExpression();
			expr.type = SqlExpressionType.Case;
			expr.caseClause = caseClause;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with IfNull function.
		/// </summary>
		/// <param name="test">Expression to be checked for being NULL</param>
		/// <param name="val">Substitution</param>
		/// <returns></returns>
		/// <remarks>
		/// Works as SQL Server's ISNULL() function.
		/// </remarks>
		public static SqlExpression IfNull(SqlExpression test, SqlExpression val)
		{
			SqlExpression expr = new SqlExpression();
			expr.type = SqlExpressionType.IfNull;
			expr.subExpr1 = test;
			expr.subExpr2 = val;
			return expr;
		}


        /// <summary>
        /// Creates a SqlExpression with Round function.
        /// </summary>
        /// <param name="test">Expression to be checked for being NULL</param>
        /// <param name="val">Substitution</param>
        /// <returns></returns>
        /// <remarks>
        /// Works as SQL Server's ISNULL() function.
        /// </remarks>
        public static SqlExpression Round(SqlExpression val, int RoundCount)
        {
            SqlExpression expr = new SqlExpression();
            expr.type = SqlExpressionType.IfNull;
            expr.subExpr1 = val;
            expr.val = RoundCount;
            return expr;
        }

		/// <summary>
		/// Creates a SqlExpression with an aggergation function
		/// </summary>
		/// <param name="func">Aggregation function to be applied on the supplied expression</param>
		/// <param name="param">Parameter of the aggregation function</param>
		/// <returns></returns>
		public static SqlExpression Function(SqlAggregationFunction func, SqlExpression param)
		{
			SqlExpression expr = new SqlExpression();
			expr.type = SqlExpressionType.Function;
			expr.subExpr1 = param;
			expr.func = func;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression representing a NULL value
		/// </summary>
		/// <returns></returns>
		public static SqlExpression Null()
		{
			SqlExpression expr = new SqlExpression();
			expr.type = SqlExpressionType.Null;
			return expr;
		}

		internal static SqlExpression PseudoField(string fieldName)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = fieldName;
			expr.type = SqlExpressionType.PseudoField;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a OtherData in a database table.
		/// </summary>
		/// <param name="fieldName">Name of a OtherData</param>
		/// <returns></returns>
		public static SqlExpression Field(string fieldName)
		{
			return Field(fieldName, null);
		}

		/// <summary>
		/// Creates a SqlExpression which represents a subquery.
		/// </summary>
		/// <param name="queryText">Text of the subquery.</param>
		/// <returns>A new SqlExpression</returns>
		/// <remarks>
		/// In many cases you can use an inner or outer JOIN instead of a subquery. 
		/// If you prefer using subqueries it is recomended that you construct the subquery
		/// using another instance of <see cref="SelectQuery"/>, render it using the correct 
		/// renderer and pass the resulting SQL statement to the <paramref name="queryText"/> parameter.
		/// </remarks>
		public static SqlExpression SubQuery(string queryText)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = queryText;
			expr.type = SqlExpressionType.SubQueryText;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a subquery.
		/// </summary>
		/// <param name="query">A SelectQuery object</param>
		/// <returns>A new SqlExpression</returns>
		public static SqlExpression SubQuery(SelectQuery query)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = query;
			expr.type = SqlExpressionType.SubQueryObject;
			return expr;
		}
		/// <summary>
		/// Create a parameter placeholder.
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns></returns>
		/// <remarks>
		/// Correct parameter name depends on your specifc data provider. OLEDB expects
		/// all parameters to be '?' and matches parameters to values based on their index.
		/// SQL Server Native driver matches parameters by names and expects to find "@paramName"
		/// parameter placeholder in the query.
		/// </remarks>
		public static SqlExpression Parameter(string paramName)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = paramName;
			expr.type = SqlExpressionType.Parameter;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with raw SQL
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static SqlExpression Raw(string sql)
		{
			SqlExpression expr = new SqlExpression();
			expr.val = sql;
			expr.type = SqlExpressionType.Raw;
			return expr;
		}

		internal string TableAlias
		{
			get { return (table == null) ? null : table.RefName; }
		}

		internal SqlExpressionType Type
        {
            get { return this.type; }
            set { this.type = value;}
		}

		internal object Value
		{
			get { return this.val; }
            set { this.val = value; }
		}

		internal SqlAggregationFunction AggFunction
		{
            get { return func; }
            set { this.func = value; }
		}

		internal CaseClause CaseClause
		{
            get { return caseClause; }
            set { this.caseClause = value; }
		}

		internal SqlExpression SubExpr1
		{
            get { return subExpr1; }
            set { this.subExpr1 = value; }
		}

		internal SqlExpression SubExpr2
		{
            get { return subExpr2; }
            set { this.subExpr2 = value; }
		}
	}

}
