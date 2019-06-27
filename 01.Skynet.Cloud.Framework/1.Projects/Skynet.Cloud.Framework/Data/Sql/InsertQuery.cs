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
	/// Encapsulates a SQL INSERT statement
	/// </summary>
	/// <remarks>
	/// Use InsertQuery to insert a new row into a database table.
	/// Set <see cref="TableName"/> to the table you would like to insert into and use
	/// the <see cref="Terms"/> collection to specify values to be inserted.
	/// </remarks>
	/// <example>
	/// <code>
	/// InsertQuery query = new InsertQuery("products");
	/// query.Terms.Add(new UpdateTerm("productId", SqlExpression.Number(999)));
	/// query.Terms.Add(new UpdateTerm("name", SqlExpression.String("Temporary Test Product")));
	/// query.Terms.Add(new UpdateTerm("price", SqlExpression.Number(123.45)));
	/// query.Terms.Add(new UpdateTerm("quantaty", SqlExpression.Number(97)));
	/// RenderInsert(query);
	/// </code>
    /// </example>
    [Serializable]
	public class InsertQuery
	{
		UpdateTermCollection terms = new UpdateTermCollection();
		string tableName;


		/// <summary>
		/// Create an InsertQuery
		/// </summary>
		public InsertQuery() : this(null)
		{
		}

		/// <summary>
		/// Create an InsertQuery
		/// </summary>
		/// <param name="tableName">The name of the table to be inseserted into</param>
		public InsertQuery(string tableName)
		{
			this.tableName = tableName;
		}

		/// <summary>
		/// Gets the collection if column-value pairs
		/// </summary>
		/// <remarks>
		/// Terms specify which values should be inserted into the table.
		/// </remarks>
		public UpdateTermCollection Terms
		{
			get { return this.terms; }
		}

		/// <summary>
		/// Gets or set the name of a table to be inserted into
		/// </summary>
		public string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		/// <summary>
		/// Validates InsertQuery
		/// </summary>
		public void Validate()
		{
			if (tableName == null)
				throw new InvalidQueryException("TableName is empty.");
			if (terms.Count == 0)
				throw new InvalidQueryException("Terms collection is empty.");
		}
	}
}
