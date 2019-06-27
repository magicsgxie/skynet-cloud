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
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data
{
    internal enum WhereTermType { Compare, Between, In, NotIn, InSubQuery, NotInSubQuery, IsNull, IsNotNull, Exists, NotExists, CustomSql }
	
	/// <summary>
	/// Represents one term in a WHERE clause
	/// </summary>
	/// <remarks>
	/// <see cref="WhereTerm"/> usually consists of one or more <see cref="SqlExpression"/> objects and an a conditional operator which applies to those expressions.
	/// <see cref="WhereTerm"/> has no public constructor. Use one of the supplied static methods to create a term. 
	/// <para>
	/// Use <see cref="WhereTerm.CreateCompare"/> to create a comparison term. A comparison term can apply one of <see cref="CompareOperator"/> operators on the supplied expressions.
	/// Use <see cref="WhereTerm.CreateIn"/> to create a term which checks wheather an expression exists in a list of supplied values.
	/// Use <see cref="WhereTerm.CreateBetween"/> to create a term which checks wheather an expression value is between a supplied lower and upper bounds.
	/// </para>
    /// </remarks>
    [Serializable]
	public class WhereTerm : ICloneable
	{
		SqlExpression expr1, expr2, expr3;
        FilterOperator op;
		WhereTermType type;
		SqlConstantCollection values;
        string subQuery;
        FilterCompositionLogicalOperator _Relationship = FilterCompositionLogicalOperator.And;
        NameValueCollection namedParameters = new NameValueCollection();

		private WhereTerm()
		{
		}

		/// <summary>
		/// Creates a comparison WhereTerm.
		/// </summary>
		/// <param name="expr1">Expression on the left side of the operator</param>
		/// <param name="expr2">Expression on the right side of the operator</param>
		/// <param name="op">Conditional operator to be applied on the expressions</param>
		/// <returns>A new conditional WhereTerm</returns>
		/// <remarks>
		/// A comparison term compares two expression on the basis of their values. Expressions can be of any type but their results must be of comparible types. 
		/// For instance, you can not compare a database OtherData of type 'date' and a static value of type 'int'.
		/// </remarks>
		/// <example>
		/// <code>
		/// ...
		/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("J%"), CompareOperator.Like));
		/// </code>
		/// </example>
		public static WhereTerm CreateCompare(SqlExpression expr1, SqlExpression expr2, FilterOperator op)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr1;
			term.expr2 = expr2;
			term.op = op;
			
			term.type = WhereTermType.Compare;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which represents SQL IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="sql">Sub query</param>
		/// <returns></returns>
		public static WhereTerm CreateIn(SqlExpression expr, string sql)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.subQuery = sql;

			term.type = WhereTermType.InSubQuery;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which represents SQL IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="values">List of values</param>
		/// <returns></returns>
		public static WhereTerm CreateIn(SqlExpression expr, SqlConstantCollection values)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.values = values;

			term.type = WhereTermType.In;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which represents SQL NOT IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="sql">Sub query</param>
		/// <returns></returns>
		public static WhereTerm CreateNotIn(SqlExpression expr, string sql)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.subQuery = sql;

			term.type = WhereTermType.NotInSubQuery;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which represents SQL NOT IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static WhereTerm CreateNotIn(SqlExpression expr, SqlConstantCollection values)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.values = values;

			term.type = WhereTermType.NotIn;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which returns TRUE if an expression is NULL
		/// </summary>
		/// <param name="expr">Expression to be evaluated</param>
		/// <returns></returns>
		public static WhereTerm CreateIsNull(SqlExpression expr)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.type = WhereTermType.IsNull;
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which returns TRUE if an expression is NOT NULL
		/// </summary>
		/// <param name="expr"></param>
		/// <returns></returns>
		public static WhereTerm CreateIsNotNull(SqlExpression expr)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.type = WhereTermType.IsNotNull;
			return term;
		}

        /// <summary>
        /// Creates a WhereTerm which encapsulates SQL EXISTS clause
        /// </summary>
        /// <param name="sql">Sub query for the EXISTS clause</param>
        /// <returns></returns>
        public static WhereTerm CreateExists(string sql)
        {
            WhereTerm term = new WhereTerm();
            term.subQuery = sql;
            term.type = WhereTermType.Exists;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which encapsulates SQL EXISTS clause
        /// </summary>
        /// <param name="sql">Sub query for the EXISTS clause</param>
        /// <returns></returns>
        public static WhereTerm CustomSql(string sql)
        {
            WhereTerm term = new WhereTerm();
            term.subQuery = sql;
            term.type = WhereTermType.CustomSql;
            return term;
        }

		/// <summary>
		/// Creates a WhereTerm which encapsulates SQL NOT EXISTS clause
		/// </summary>
		/// <param name="sql">Sub query for the NOT EXISTS clause</param>
		/// <returns></returns>
		public static WhereTerm CreateNotExists(string sql)
		{
			WhereTerm term = new WhereTerm();
			term.subQuery = sql;
			term.type = WhereTermType.NotExists;
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which checks weather a value is in a specifed range.
		/// </summary>
		/// <param name="expr">Expression which yeilds the value to be checked</param>
		/// <param name="lowBound">Expression which yeilds the low bound of the range</param>
		/// <param name="highBound">Expression which yeilds the high bound of the range</param>
		/// <returns>A new WhereTerm</returns>
		/// <remarks>
		/// CreateBetween only accepts expressions which yeild a 'Date' or 'Number' values.
		/// All expressions must be of compatible types.
		/// </remarks>
		public static WhereTerm CreateBetween(SqlExpression expr, SqlExpression lowBound, SqlExpression highBound)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.expr2 = lowBound;
			term.expr3 = highBound;
			
			term.type = WhereTermType.Between;			
			
			return term;
		}

















        /// <summary>
        /// Creates a comparison WhereTerm.
        /// </summary>
        /// <param name="expr1">Expression on the left side of the operator</param>
        /// <param name="expr2">Expression on the right side of the operator</param>
        /// <param name="op">Conditional operator to be applied on the expressions</param>
        /// <returns>A new conditional WhereTerm</returns>
        /// <remarks>
        /// A comparison term compares two expression on the basis of their values. Expressions can be of any type but their results must be of comparible types. 
        /// For instance, you can not compare a database OtherData of type 'date' and a static value of type 'int'.
        /// </remarks>
        /// <example>
        /// <code>
        /// ...
        /// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("J%"), CompareOperator.Like));
        /// </code>
        /// </example>
        public static WhereTerm CreateCompare(FilterCompositionLogicalOperator relationship,SqlExpression expr1, SqlExpression expr2, FilterOperator op)
        {
            WhereTerm term = CreateCompare(expr1, expr2, op);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which represents SQL IN clause
        /// </summary>
        /// <param name="expr">Expression to be looked up</param>
        /// <param name="sql">Sub query</param>
        /// <returns></returns>
        public static WhereTerm CreateIn(FilterCompositionLogicalOperator relationship, SqlExpression expr, string sql)
        {
            WhereTerm term = CreateIn(expr, sql);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which represents SQL IN clause
        /// </summary>
        /// <param name="expr">Expression to be looked up</param>
        /// <param name="values">List of values</param>
        /// <returns></returns>
        public static WhereTerm CreateIn(FilterCompositionLogicalOperator relationship, SqlExpression expr, SqlConstantCollection values)
        {
            WhereTerm term = CreateIn(expr, values);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which represents SQL NOT IN clause
        /// </summary>
        /// <param name="expr">Expression to be looked up</param>
        /// <param name="sql">Sub query</param>
        /// <returns></returns>
        public static WhereTerm CreateNotIn(FilterCompositionLogicalOperator relationship, SqlExpression expr, string sql)
        {
            WhereTerm term = CreateNotIn(expr, sql);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which represents SQL NOT IN clause
        /// </summary>
        /// <param name="expr">Expression to be looked up</param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static WhereTerm CreateNotIn(FilterCompositionLogicalOperator relationship, SqlExpression expr, SqlConstantCollection values)
        {
            WhereTerm term = CreateNotIn(expr, values);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which returns TRUE if an expression is NULL
        /// </summary>
        /// <param name="expr">Expression to be evaluated</param>
        /// <returns></returns>
        public static WhereTerm CreateIsNull(FilterCompositionLogicalOperator relationship, SqlExpression expr)
        {
            WhereTerm term = CreateIsNull(expr);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which returns TRUE if an expression is NOT NULL
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static WhereTerm CreateIsNotNull(FilterCompositionLogicalOperator relationship, SqlExpression expr)
        {
            WhereTerm term = CreateIsNotNull(expr);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which encapsulates SQL EXISTS clause
        /// </summary>
        /// <param name="sql">Sub query for the EXISTS clause</param>
        /// <returns></returns>
        public static WhereTerm CreateExists(FilterCompositionLogicalOperator relationship, string sql)
        {
            WhereTerm term = CreateExists(sql);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which encapsulates SQL NOT EXISTS clause
        /// </summary>
        /// <param name="sql">Sub query for the NOT EXISTS clause</param>
        /// <returns></returns>
        public static WhereTerm CreateNotExists(FilterCompositionLogicalOperator relationship, string sql)
        {
            WhereTerm term = CreateNotExists(sql);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Creates a WhereTerm which checks weather a value is in a specifed range.
        /// </summary>
        /// <param name="expr">Expression which yeilds the value to be checked</param>
        /// <param name="lowBound">Expression which yeilds the low bound of the range</param>
        /// <param name="highBound">Expression which yeilds the high bound of the range</param>
        /// <returns>A new WhereTerm</returns>
        /// <remarks>
        /// CreateBetween only accepts expressions which yeild a 'Date' or 'Number' values.
        /// All expressions must be of compatible types.
        /// </remarks>
        public static WhereTerm CreateBetween(FilterCompositionLogicalOperator relationship, SqlExpression expr, SqlExpression lowBound, SqlExpression highBound)
        {
            WhereTerm term = CreateBetween(expr,lowBound,highBound);
            term._Relationship = relationship;
            return term;
        }

        /// <summary>
        /// Gets the relationship for this clause
        /// </summary>
        /// <remarks>
        /// Where clause relationship defines what kind of logical condition exists between all terms and sub clauses of this WhereClause
        /// </remarks>
        internal FilterCompositionLogicalOperator Relationship
        {
            get { return _Relationship; }
            set
            {
                _Relationship = value;
            }
        }

		internal SqlExpression Expr1
		{
			get { return expr1; }
		}
		
		internal SqlExpression Expr2
		{
			get { return expr2; }
		}
		
		internal SqlExpression Expr3
		{
			get { return expr3;	}
		}
		
		internal FilterOperator Op
		{
			get { return op; }
		}
		
		internal WhereTermType Type
		{
			get { return type; }
		}
		
		internal SqlConstantCollection Values
		{
			get { return values; }
		}

		internal string SubQuery
		{
			get { return this.subQuery; }
		}
		
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Creates a copy of this WhereTerm
		/// </summary>
		/// <returns>A new WhereTerm which exactly the same as the current one.</returns>
		public WhereTerm Clone()
		{
			WhereTerm a = new WhereTerm();

			a.expr1 = expr1;
			a.expr2 = expr2;
			a.expr3 = expr3;
			a.op = op;
			a.type = type;
            a.Relationship = Relationship;
			a.subQuery = subQuery;
			a.values = new SqlConstantCollection(values);
			
			return a;
		}
	}
}
