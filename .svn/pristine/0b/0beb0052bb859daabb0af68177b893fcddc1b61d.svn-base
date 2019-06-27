/*----------------------------------------------------------------
// Copyright (C) 2010 �����������Ƽ����޹�˾
// ��Ȩ���С� 
//
// �ļ�����
// �ļ�����������
//
// 
// ������ʶ��
//
// �޸ı�ʶ��
// �޸�������
//
// �޸ı�ʶ��
// �޸�������
//----------------------------------------------------------------*/
using System;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data
{
	/// <summary>
	/// Encapsulates a SQL UPDATE statement
	/// </summary>
	/// <remarks>
	/// Use UpdateQuery to update data in a database table.
	/// Set <see cref="TableName"/> to the table you would like to update, populate 
	/// the <see cref="Terms"/> collection with column-value pairs and define which rows 
	/// should be affected using the <see cref="WhereClause"/>.
	/// </remarks>
	/// <example>
	/// <code>
	///	UpdateQuery query = new UpdateQuery("products");
	///	query.Terms.Add(new UpdateTerm("price", SqlExpression.Number(12.1)));
	///	query.Terms.Add(new UpdateTerm("quantaty", SqlExpression.Field("quantaty")));
	///	query.WhereClause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productId"), SqlExpression.Number(1), CompareOperator.Equal) );
	///	</code>
    /// </example>
    [Serializable]
	public class UpdateQuery
	{
		UpdateTermCollection terms = new UpdateTermCollection();
		WhereClause whereClause = new WhereClause(FilterCompositionLogicalOperator.And);
		string tableName;
		
		/// <summary>
		/// Creates a new UpdateQuery
		/// </summary>
		public UpdateQuery() : this(null)
		{
		}

		/// <summary>
		/// Creates a new UpdateQuery
		/// </summary>
		/// <param name="tableName"></param>
		public UpdateQuery(string tableName)
		{
			this.tableName = tableName;
		}

		/// <summary>
		/// Gets the terms collection for this UpdateQuery
		/// </summary>
		/// <remarks>
		/// Terms specify which columns should be updated and to what values.
		/// </remarks>
		public UpdateTermCollection Terms
		{
			get { return this.terms; }
		}

		/// <summary>
		/// Spicifies which rows are to be updated
		/// </summary>
		public WhereClause WhereClause
		{
			get { return this.whereClause; }
		}

		/// <summary>
		/// Gets or set the name of a table to be updated
		/// </summary>
		public string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		/// <summary>
		/// Validates UpdateQuery
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
