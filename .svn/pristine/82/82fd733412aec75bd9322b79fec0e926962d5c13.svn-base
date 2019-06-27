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
using System.Runtime.Serialization;

namespace UWay.Skynet.Cloud.Data
{

		
	/// <summary>
	/// Encapsulates a SQL SELECT statement.
	/// </summary>
	/// <remarks>
	/// Use SelectQuery to define and modify a query. 
	/// When the query is ready to be executed it can be rendered to SQL using one of the renderers derived from <see cref="Render.ISqlOmRenderer">ISqlOMRenderer</see>
	/// and executed using standard .Net query execution methods.
	/// <para>SelectQuery can be useful for dynamic SQL generation for reports and filters.</para>
	/// <para>It can also be used to render SQL to retrieve pages of data from databases which do not directly support this feature (i.e. SqlServer).</para>
	/// </remarks>
	/// <example>
	/// The following example creates a select query which returns two columns from two inner joined tables and renders it to be executed on MySql
	/// <code>
	/// FromTerm tCustomers = FromTerm.Table("customers");
	/// FromTerm tProducts = FromTerm.Table("products", "p");
	/// FromTerm tOrders = FromTerm.Table("orders", "o");
	/// 
	/// SelectQuery query = new SelectQuery();
	/// query.Columns.Add(new SelectColumn("name", tCustomers));
	/// query.Columns.Add(new SelectColumn("name", tProducts));
	/// query.FromClause.BaseTable = tCustomers;
	/// query.FromClause.Join(JoinType.Inner, query.FromClause.BaseTable, tOrders, "customerId", "customerId");
	/// query.FromClause.Join(JoinType.Inner, tOrders, tProducts, "productId", "productId");
	/// 
	/// MySqlRenderer renderer = new MySqlRenderer();
	///	string sql = renderer.RenderSelect(query);
	///	...
	/// </code>
	///</example>
	/// <example>
	/// This example creates a select query which returns the second page of a result-set and renders it to be executed on SqlServer
	/// <code>
	/// 
	/// int totalRows = 50; //The total number of rows can be obtained using SelectQuery as well
	/// 
	/// SelectQuery query = new SelectQuery();
	/// 
	/// query.Columns.Add(new SelectColumn("name"));
	/// query.FromPhrase.BaseTable = FromClause.Table("customers");
	/// query.OrderByClauses.Add(new OrderByClause("name", null, OrderByDirection.Descending));
	/// query.OrderByClauses.Add(new OrderByClause("birthDate", null, OrderByDirection.Ascending));
	/// 
	/// SqlServerRenderer renderer = new SqlServerRenderer();
	///	sql = renderer.RenderPage(2, 10, totalRows, query);
	///	...
	/// </code>
    ///</example>
    [Serializable]
    [DataContract]
	public class SelectQuery : ICloneable
	{
		protected SelectColumnCollection columns = new SelectColumnCollection();
		protected WhereClause wherePhrase = new WhereClause();
		protected WhereClause havingPhrase = new WhereClause();
		protected FromClause fromClause = new FromClause();

		protected OrderByTermCollection orderByTerms = new OrderByTermCollection();
		protected GroupByTermCollection groupByTerms = new GroupByTermCollection();

		protected int top = -1;		
		protected bool groupByWithRollup = false;
		protected bool groupByWithCube = false;
		protected bool distinct = false;
		protected string tableSpace = null;
        protected string indexHints = null;

		/// <summary>
		/// Creates a new SelectQuery
		/// </summary>
		public SelectQuery()
		{
		}

        /// <summary>
        /// Gets the FROM definition for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual FromClause FromClause
		{
			get { return fromClause; }
		}

        /// <summary>
        /// Gets the GROUP BY definition for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual GroupByTermCollection GroupByTerms
		{
			get { return groupByTerms; }
            set { groupByTerms = value; }
		}

        /// <summary>
        /// Gets the ORDER BY definition for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual OrderByTermCollection OrderByTerms
		{
			get { return orderByTerms; }
            set { orderByTerms = value; }
		}

        /// <summary>
        /// Gets the WHERE conditions for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual WhereClause WherePhrase
		{
			get { return wherePhrase; }
            set { wherePhrase = value; }
		}

        /// <summary>
        /// Gets the WHERE conditions for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual WhereClause HavingPhrase
		{
			get { return havingPhrase; }
            set { havingPhrase = value; }
		}

        /// <summary>
        /// Gets the collection of columns for this SelectQuery
        /// </summary>
        [DataMember]
        public virtual SelectColumnCollection Columns 
		{
			get { return columns; }
            set { columns = value; }
		}

        /// <summary>
        /// Gets or sets the result-set row count limitation
        /// </summary>
        /// <remarks>
        /// When Top is less then zero, no limitation will apply on the result-set. To limit
        /// the number of rows returned by this query set Top to a positive integer or zero
        /// </remarks>
        [DataMember]
        public virtual int Top 
		{
			get { return top; }
			set { top = value; }
		}

        /// <summary>
        /// Gets or sets the group by with rollup option for the query
        /// </summary>
        /// <remarks>
        /// GroupByWithRollup property is only relevant for queries which perform group by and have aggregation columns.
        /// When GroupByWithRollup is true the result set will include additional rows with sub total information. Consult SQL documentation for more details.
        /// </remarks>
        [DataMember]
        public virtual bool GroupByWithRollup
		{
			get { return groupByWithRollup; }
			set { groupByWithRollup = value; }
		}

        /// <summary>
        /// Gets or sets the group by with cube option for the query. Not supported by all databases.
        /// </summary>
        /// <remarks>
        /// GroupByWithCube property is only relevant for queries which perform group by and have aggregation columns.
        /// When GroupByWithCube is true the result set will include additional rows with sub total information. GroupByWithCube even more data then <see cref="SelectQuery.GroupByWithRollup">GroupByWithRollup</see>. Consult SQL documentation for more details.
        /// <para>
        /// Important! Not all databases support this option.
        /// </para>
        /// </remarks>
        [DataMember]
        public virtual bool GroupByWithCube
		{
			get { return groupByWithCube; }
			set { groupByWithCube = value; }
		}

        /// <summary>
        /// Gets or sets wheather only distinct rows are to be returned.
        /// </summary>
        [DataMember]
        public virtual bool Distinct
		{
			get { return distinct; }
			set { distinct = value; }
		}

        /// <summary>
        /// Validates the SelectQuery
        /// </summary>
        /// <remarks>
        /// Sql.Net makes its best to validate a query before it is rendered or executed. 
        /// Still, some errors and inconsistancies can only be found on later stages.
        /// </remarks>
        public virtual void Validate()
		{
			if (columns.Count == 0)
				throw new InvalidQueryException("A select query must have at least one column");
			
			if (fromClause.BaseTable == null)
				throw new InvalidQueryException("A select query must have FromPhrase.BaseTable set");
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Clones the SelectQuery
		/// </summary>
		/// <returns>A new instance of SelectQuery which is exactly the same as the current one.</returns>
		public SelectQuery Clone()
		{
			SelectQuery newQuery = new SelectQuery();
			
			newQuery.columns = new SelectColumnCollection(columns);
			newQuery.orderByTerms = new OrderByTermCollection(orderByTerms);
			newQuery.groupByTerms = new GroupByTermCollection(groupByTerms);
			
			newQuery.wherePhrase = wherePhrase.Clone();
			newQuery.fromClause = fromClause.Clone();

			newQuery.top = top;		
			newQuery.groupByWithRollup = groupByWithRollup;
			newQuery.groupByWithCube = groupByWithCube;
			newQuery.distinct = distinct;
			newQuery.tableSpace = tableSpace;
            newQuery.indexHints = indexHints;

			return newQuery;
		}

        /// <summary>
        /// Gets or sets the common prefix for all tables in the query
        /// </summary>
        /// <remarks>
        /// You might want to use <see cref="TableSpace"/> property to utilize SQL Server 2000
        /// execution plan cache. For the cache to work in SQL Server 2000, all database objects in a query must be fully qualified.
        /// Setting <see cref="TableSpace"/> property might releive of the duty to fully qualify all table names in the query.
        /// </remarks>
        [DataMember]
        public virtual string TableSpace
		{
			get { return tableSpace; }
			set { tableSpace = value; }
		}

        /// <summary>
        /// Gets or sets IndexHints.
        /// </summary>
        [DataMember]
        public virtual string IndexHints
        {
            get { return indexHints; }
            set { indexHints = value; }
        }
	}
}
