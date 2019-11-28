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
	/// Describes the WHERE clause of a SELECT statement
	/// </summary>
	/// <remarks>
	/// Using WhereClause you can abstractly define most common SQL conditional expressions.
	/// A WhereClause consists of individual <see cref="WhereTerm">terms</see> and <see cref="WhereClause">sub clauses</see>.
	/// Between all terms and sub clauses of the same clause exists a single logical <see cref="FilterCompositionLogicalOperator">relationship</see>. 
	/// To create a group of terms with a different relationship, create a sub clause with the desired relationship and add it to the relevant <see cref="WhereClause.SubClauses"/>collection.
	/// Terms of a where clause are represnted by the <see cref="WhereTerm"/> class while the sub clauses are represnted by the same <see cref="WhereClause"/> class.
	/// </remarks>
	/// <example>
	/// The following example attempts to demonstrate some of the most common usages of WhereClause and WhereTerm classes
	/// <code>
	/// FromTerm tCustomers = FromTerm.Table("customers", "c");
	/// FromTerm tProducts = FromTerm.Table("products", "p");
	/// FromTerm tOrders = FromTerm.Table("orders", "o");
	/// 
	/// SelectQuery query = new SelectQuery();
	/// 
	/// query.Columns.Add(new SelectColumn("name", tCustomers));
	/// query.Columns.Add(new SelectColumn("name", tProducts));
	/// query.Columns.Add(new SelectColumn("price", tProducts));
	/// 
	/// query.FromClause.BaseTable = tCustomers;
	/// query.FromClause.Join(JoinType.Left, tCustomers, tOrders, "customerId", "customerId");
	/// query.FromClause.Join(JoinType.Inner, tOrders, tProducts, "productId", "productId");
	///			
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("John"), CompareOperator.Equal));
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("J%"), CompareOperator.Like));
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Date(DateTime.Now), SqlExpression.Field("date", tOrders), CompareOperator.Greater));
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Number(1), SqlExpression.Number(1), CompareOperator.BitwiseAnd));
	/// 		
	/// WhereClause group = new WhereClause(WhereClauseRelationship.Or);
	/// 		
	/// group.Terms.Add(WhereTerm.CreateBetween(SqlExpression.Field("price", tProducts), SqlExpression.Number(1), SqlExpression.Number(10)));
	/// group.Terms.Add(WhereTerm.CreateIn(SqlExpression.Field("name", tProducts), new string[] {"Nail", "Hamer", "Skrewdriver"}));
	/// 		
	/// query.WherePhrase.SubClauses.Add(group);
	/// 
	/// ...
	/// </code>
    /// </example>
    [Serializable]
	public class WhereClause : ICloneable
	{
		FilterCompositionLogicalOperator relationship = FilterCompositionLogicalOperator.And;
		WhereTermCollection whereTerms = new WhereTermCollection();
		WhereClauseCollection clauses = new WhereClauseCollection();

        /// <summary>
        /// 
        /// </summary>
		public WhereClause()
		{
		}

		/// <summary>
		/// Creates a new WhereClause
		/// </summary>
		/// <param name="relationship">Relationship between all the terms and sub clauses of this clause</param>
		/// <example>
		/// <code>
		/// SelectQuery query = new SelectQuery();
		/// ...
		/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("John"), CompareOperator.Equal));
		/// WhereClause group = new WhereClause(WhereClauseRelationship.Or);
		/// group.Terms.Add(WhereTerm.CreateBetween(SqlExpression.Field("price", tProducts), SqlExpression.Number(1), SqlExpression.Number(10)));
		/// group.Terms.Add(WhereTerm.CreateIn(SqlExpression.Field("name", tProducts), new string[] {"Nail", "Hamer", "Skrewdriver"}));
		/// query.WherePhrase.SubClauses.Add(group);
		/// </code>
		/// </example>
		public WhereClause(FilterCompositionLogicalOperator relationship)
		{
			this.relationship = relationship;
		}

		/// <summary>
		/// Gets the relationship for this clause
		/// </summary>
		/// <remarks>
		/// Where clause relationship defines what kind of logical condition exists between all terms and sub clauses of this WhereClause
		/// </remarks>
		public FilterCompositionLogicalOperator Relationship
		{
			get { return relationship; }
		}


        /// <summary>
        /// Gets the terms collection for this WherePhrase
        /// </summary>
        public WhereTermCollection Terms
		{
			get { return whereTerms; }
		}
		
		/// <summary>
		/// Gets the sub clauses collection for this WherePhrase
		/// </summary>
		public WhereClauseCollection SubClauses
		{
			get { return clauses; }
		}
		
		/// <summary>
		/// Returns true if this WhereClause and its descendant sub clauses have no terms
		/// </summary>
		public bool IsEmpty
		{
			get 
			{ 
				foreach(WhereClause group in clauses)
				{
					if (!group.IsEmpty)
						return false;
				}
				
				return whereTerms.Count == 0; 
			}
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Creates a copy of this WhereClause
		/// </summary>
		/// <returns>A new WhereClause which is exactly the same as the current one</returns>
		public WhereClause Clone()
		{
			WhereClause a = new WhereClause();
			
			a.relationship = relationship;
			a.whereTerms = new WhereTermCollection(whereTerms);
			foreach(WhereClause group in clauses)
				a.clauses.Add(group.Clone());
			return a;
		}

	}
}
