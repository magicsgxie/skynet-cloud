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
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data
{
	/// <summary>
	/// Encapsulates a SQL DELETE statement
    /// 
	/// </summary>
	/// <remarks>
	/// Use DeleteQuery to delete a row in database table.
	/// Set <see cref="TableName"/> to the table you would like to delete rows from and use
	/// <see cref="WhereClause"/> to specify which rows are to be deleted.
	/// </remarks>
	/// <example>
	/// <code>
	/// DeleteQuery query = new DeleteQuery("products");
	/// query.WhereClause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productId"), SqlExpression.Number(999), CompareOperator.Equal));
	/// RenderDelete(query);
	/// </code>
    /// </example>
    [Serializable]
	public class DeleteQuery
	{
		string tableName;
		WhereClause whereClause = new WhereClause(FilterCompositionLogicalOperator.And);

		/// <summary>
		/// Creates a DeleteQuery
		/// </summary>
		public DeleteQuery(): this(null)
		{
		}

		/// <summary>
		/// Creates a DeleteQuery
		/// </summary>
		/// <param name="tableName">Name of the table records are to be deleted from</param>
		public DeleteQuery(string tableName)
		{
			this.tableName = tableName;
		}
        
		/// <summary>
		/// Specifies which rows are to be deleted
		/// </summary>
		public WhereClause WhereClause
		{
			get { return this.whereClause; }
		}

		/// <summary>
		/// Gets or set the name of a table records are to be deleted from
		/// </summary>
		public string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		/// <summary>
		/// Validates DeleteQuery
		/// </summary>
		public void Validate()
		{
			if (tableName == null)
				throw new InvalidQueryException("TableName is empty.");
			if (WhereClause.IsEmpty)
				throw new InvalidQueryException("DeleteQuery has no where condition.");
		}
	}
}
