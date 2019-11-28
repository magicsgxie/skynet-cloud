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
using System.Text;

namespace UWay.Skynet.Cloud.Data.Render
{
	/// <summary>
	/// Renderer for MySql
	/// </summary>
	/// <remarks>
	/// Use MySqlRenderer to render SQL statements for MySql database.
	/// This version of Sql.Net has been tested with MySql 4
	/// </remarks>
	public class MySqlRenderer : SqlOmRenderer
	{
        /// <summary>
        /// 前缀
        /// </summary>
        protected override string PrefixNamed
        {
            get
            {
                return "?";
            }
        }

        /// <summary>
        /// Creates a new MySqlRenderer
        /// </summary>
        public MySqlRenderer() : base('`', '`')
		{
		}

		/// <summary>
		/// Renders IfNull SqlExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected override void IfNull(StringBuilder builder, SqlExpression expr)
		{
			builder.Append("ifnull(");
			Expression(builder, expr.SubExpr1);
			builder.Append(", ");
			Expression(builder, expr.SubExpr2);
			builder.Append(")");
		}
	
		/// <summary>
		/// Renders a SELECT statement
		/// </summary>
		/// <param name="query">Query definition</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>MySql 4.1 does not support GroupByWithCube option. If a query has <see cref="SelectQuery.GroupByWithCube"/> set an <see cref="InvalidQueryException"/> exception will be thrown. </remarks>
		public override string RenderSelect(SelectQuery query)
		{
			return RenderSelect(query, false, 0, query.Top).Replace("\"", "").SimplifyBracket();
		}


		string RenderSelect(SelectQuery query, bool forRowCount, int offset, int limitRows)
		{
			query.Validate();
			
			StringBuilder selectBuilder = new StringBuilder();

			//Start the select statement
			this.Select(selectBuilder, query.Distinct);
			
			//Render select columns
			if (forRowCount)
				this.SelectColumn(selectBuilder, new SelectColumn("*", null, "cnt", SqlAggregationFunction.Count));
			else
				this.SelectColumns(selectBuilder, query.Columns);

			this.FromClause(selectBuilder, query.FromClause, query.TableSpace);
			
			this.Where(selectBuilder, query.WherePhrase);
			this.WhereClause(selectBuilder, query.WherePhrase);

			this.GroupBy(selectBuilder, query.GroupByTerms);
			this.GroupByTerms(selectBuilder, query.GroupByTerms);

			if (query.GroupByWithCube)
				throw new InvalidQueryException("MySql does not support WITH CUBE modifier.");
			
			if (query.GroupByWithRollup)
				selectBuilder.Append(" with rollup");
			
			this.Having(selectBuilder, query.HavingPhrase) ;
			this.WhereClause(selectBuilder, query.HavingPhrase);

			this.OrderBy(selectBuilder, query.OrderByTerms);
			this.OrderByTerms(selectBuilder, query.OrderByTerms);

			if (limitRows > -1)
				selectBuilder.AppendFormat(" limit {0}, {1}", offset, limitRows);

			return selectBuilder.ToString();
		}
		/*
		void RenderFromPhrase(StringBuilder builder, FromClause fromClause)
		{
			this.From(builder);
			
			this.FromTerm(builder, fromClause.BaseTable);

			foreach(Join join in fromClause.Joins)
			{
				builder.AppendFormat(" {0} join ", join.Type.ToString().ToLower());
				this.FromTerm(builder, join.RightTable);
			
				if (join.Type != JoinType.Cross)
				{
					builder.AppendFormat(" on ");
					this.QualifiedIdentifier(builder, join.LeftTable.RefName, join.LeftField);
					builder.AppendFormat(" = ");
					this.QualifiedIdentifier(builder, join.RightTable.RefName, join.RightField);
				}
			}
		}
*/
		/// <summary>
		/// Renders a row count SELECT statement. 
		/// </summary>
		/// <param name="query">Query definition to count rows for</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// Renders a SQL statement which returns a result set with one row and one cell which contains the number of rows <paramref name="query"/> can generate. 
		/// The generated statement will work nicely with <see cref="System.Data.IDbCommand.ExecuteScalar"/> method.
		/// </remarks>
		public override string RenderRowCount(SelectQuery query)
		{
			string baseSql = RenderSelect(query);

			SelectQuery countQuery = new SelectQuery();
			SelectColumn col = new SelectColumn("*", null, "cnt", SqlAggregationFunction.Count);
			countQuery.Columns.Add(col);
			countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
			return RenderSelect(countQuery).Replace("\"", "").SimplifyBracket();
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
		/// Parameter <paramref name="totalRowCount"/> is ignored.
		/// </remarks>
		public override string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
		{
			return RenderSelect(query, false, pageIndex * pageSize, pageSize).Replace("\"", "").SimplifyBracket();		
		}

        /// <summary>
        /// 查询所有表信息
        /// </summary>
        /// <returns></returns>
        public override string QueryTable()
        {
            return @"select table_name tableName, engine, table_comment tableComment, create_time createTime from information_schema.tables
		where table_schema = (select database()) order by create_time desc";
        }

        /// <summary>
        /// 单个查询表结构
        /// </summary>
        /// <returns></returns>
        public override string QueryTableByTableName()
        {
            return @"select table_name tableName, engine, table_comment tableComment, create_time createTime from information_schema.tables

        where table_schema = (select database()) and table_name like concat('%',  :tableName, '%') order by create_time desc";
        }

        /// <summary>
        /// 查询表结构
        /// </summary>
        /// <returns></returns>
        public override string QueryTableColumns()
        {
            return @"select column_name columnName, data_type dataType, column_comment columnComment, column_key columnKey, extra from information_schema.columns
 			where table_name = :tableName and table_schema = (select database()) order by ordinal_position";
        }
    }
}
