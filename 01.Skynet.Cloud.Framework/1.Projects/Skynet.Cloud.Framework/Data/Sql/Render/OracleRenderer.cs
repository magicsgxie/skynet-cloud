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
using System.Text;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Data.Render
{
    /// <summary>
    /// Renderer for Oracle
    /// </summary>
    /// <remarks>
    /// Use OracleRenderer to render SQL statements for Oracle database.
    /// This version of Sql.Net has been tested with Oracle 9i.
    /// </remarks>
    public class OracleRenderer : SqlOmRenderer
	{

        protected override string PrefixNamed
        {
            get
            {
                return ":";
            }
        }

        /// <summary>
        /// Creates a new instance of OracleRenderer
        /// </summary>
        public OracleRenderer() : base('"', '"')
		{
			DateFormat = "dd-MMM-yy";
			DateTimeFormat = "dd-MMM-yy HH:mm:ss";
		}

		/// <summary>
		/// Renders IfNull SqlExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected override void IfNull(StringBuilder builder, SqlExpression expr)
		{
			builder.Append("nvl(");
			Expression(builder, expr.SubExpr1);
			builder.Append(", ");
			Expression(builder, expr.SubExpr2);
			builder.Append(")");
		}

		/// <summary>
		/// Returns true. 
		/// </summary>
		protected override bool UpperCaseIdentifiers
		{
			get { return true; }
		}

		/// <summary>
		/// Renders bitwise and
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected override void BitwiseAnd(StringBuilder builder, WhereTerm term)
		{
			builder.Append("BITAND(");
			Expression(builder, term.Expr1);
			builder.Append(", ");
			Expression(builder, term.Expr2);
			builder.Append(") > 0");
		}

		/// <summary>
		/// Renders a SELECT statement
		/// </summary>
		/// <param name="query">Query definition</param>
		/// <returns>Generated SQL statement</returns>
		public override string RenderSelect(SelectQuery query)
		{
			if (query.Top > -1 && query.OrderByTerms.Count > 0)
			{
				string baseSql = RenderSelect(query, -1);

				SelectQuery countQuery = new SelectQuery();
				SelectColumn col = new SelectColumn("*");
				countQuery.Columns.Add(col);
				countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
				return RenderSelect(countQuery, query.Top).Replace("\"", "").SimplifyBracket();
			}
			else
				return RenderSelect(query, query.Top).Replace("\"", "").SimplifyBracket();
		}

		string RenderSelect(SelectQuery query, int limitRows)
		{
			query.Validate();
			
			StringBuilder selectBuilder = new StringBuilder();
            //start the select
            this.Select(selectBuilder, query.IndexHints);

            //Render the Distinct statement
			this.Select(selectBuilder, query.Distinct);
			
			//Render select columns
			this.SelectColumns(selectBuilder, query.Columns);

			this.FromClause(selectBuilder, query.FromClause, query.TableSpace);

			WhereClause fullWhereClause = new WhereClause(FilterCompositionLogicalOperator.And);
            if(!query.WherePhrase.IsEmpty)
			    fullWhereClause.SubClauses.Add(query.WherePhrase);
			if (limitRows > -1)
				fullWhereClause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.PseudoField("rownum"), SqlExpression.Number(limitRows), FilterOperator.IsLessThanOrEqualTo));

			this.Where(selectBuilder, fullWhereClause);
			this.WhereClause(selectBuilder, fullWhereClause);

			this.GroupBy(selectBuilder, query.GroupByTerms);
			if (query.GroupByWithCube)
				selectBuilder.Append(" cube (");
			else if (query.GroupByWithRollup)
				selectBuilder.Append(" rollup (");
			this.GroupByTerms(selectBuilder, query.GroupByTerms);

			if (query.GroupByWithCube || query.GroupByWithRollup)
				selectBuilder.Append(" )");
			
			this.Having(selectBuilder, query.HavingPhrase) ;
			this.WhereClause(selectBuilder, query.HavingPhrase);

			this.OrderBy(selectBuilder, query.OrderByTerms);
			this.OrderByTerms(selectBuilder, query.OrderByTerms);

			return selectBuilder.ToString();
		}

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
			string baseSql = RenderSelect(query, -1);

			SelectQuery countQuery = new SelectQuery();
			SelectColumn col = new SelectColumn("*", null, "cnt", SqlAggregationFunction.Count);
			countQuery.Columns.Add(col);
			countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
			return RenderSelect(countQuery).Replace("\"", "").SimplifyBracket();
		}

        /// <summary>
        /// Renders a single ORDER BY term
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="term"></param>
        protected override void OrderByTerm(StringBuilder builder, OrderByTerm term)
        {
            string dir = (term.Direction == OrderByDirection.Descending) ? "desc nulls last" : "asc nulls last";
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

        public override string QueryTable()
        {
            
            return @"select dt.table_name tableName,
            dtc.comments tableComment,
            uo.created createTime
            from user_tables dt,
		    user_tab_comments dtc,
            user_objects uo
            where dt.table_name = dtc.table_name and dt.table_name = uo.object_name and uo.object_type = 'TABLE'";
        }

        public override string QueryTableByTableName()
        {
            return @"select dt.table_name tableName,
		        dtc.comments tableComment,
		        uo.created createTime
		        from user_tables dt,
		        user_tab_comments dtc,
		        user_objects uo
		        where dt.table_name = dtc.table_name and dt.table_name = uo.object_name and uo.object_type='TABLE'

                    and dt.table_name like concat(:tableName, '%')
                order by uo.CREATED desc";
        }

        public override string QueryTableColumns()
        {
            return @"select temp.column_name columnname,
              temp.data_type dataType,
              temp.comments columnComment,
               temp.nullable nullable,
                case temp.constraint_type when 'P' then 'PRI' when 'C' then 'UNI' else '' end COLUMNKEY,
                '' EXTRA
                from(
                select col.column_id,
                col.column_name,
                col.data_type,
                colc.comments,
                col.nullable,
                uc.constraint_type,
                --去重
                row_number() over(partition by col.column_name order by uc.constraint_type desc) as row_flg
                from user_tab_columns col
                left
                join user_col_comments colc

           on colc.table_name = col.table_name

           and colc.column_name = col.column_name
                left join user_cons_columns ucc
                on ucc.table_name = col.table_name
                and ucc.column_name = col.column_name
                left join user_constraints uc
                on uc.constraint_name = ucc.constraint_name
                where col.table_name =  :tableName
                ) temp
                where temp.row_flg = 1
                order by temp.column_id";
        }
    }
}
