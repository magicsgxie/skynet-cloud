using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Describes a column of a select clause
    /// </summary>
    [Serializable]
    public class SelectColumn
    {
        SqlExpression expr;
        string alias; 
        FromTerm _table;

        /// <summary>
        /// Creates a SelectColumn with a column name, no table, no column alias and no function
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        public SelectColumn(string columnName)
            : this(columnName, null)
        {
        }

        /// <summary>
        /// Creates a SelectColumn with a column name, table, no column alias and no function
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        /// <param name="table">The table this OtherData belongs to</param>
        public SelectColumn(string columnName, FromTerm table)
            : this(columnName, table, null)
        {
        }

        /// <summary>
        /// Creates a SelectColumn with a column name, table and column alias
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        /// <param name="table">The table this OtherData belongs to</param>
        /// <param name="columnAlias">Alias of the column</param>
        public SelectColumn(string columnName, FromTerm table, string columnAlias)
            : this(columnName, table, columnAlias, SqlAggregationFunction.None)
        {
        }

        /// <summary>
        /// Creates a SelectColumn with a column name, table, column alias and optional aggregation function
        /// </summary>
        /// <param name="columnName">Name of a column</param>
        /// <param name="table">The table this OtherData belongs to</param>
        /// <param name="columnAlias">Alias of the column</param>
        /// <param name="function">Aggregation function to be applied to the column. Use SqlAggregationFunction.None to specify that no function should be applied.</param>
        public SelectColumn(string columnName, FromTerm table, string columnAlias, SqlAggregationFunction function)
        {
            _table = table;
            if (function == SqlAggregationFunction.None)
                expr = SqlExpression.Field(columnName, table);
            else
                expr = SqlExpression.Function(function, SqlExpression.Field(columnName, table));
            this.alias = columnAlias;
        }

        /// <summary>
        /// Creates a SelectColumn
        /// </summary>
        /// <param name="expr">Expression</param>
        /// <param name="columnAlias">Column alias</param>
        public SelectColumn(SqlExpression expr, string columnAlias)
        {
            this.expr = expr;
            this.alias = columnAlias;
        }

        /// <summary>
        /// Gets the column alias for this SelectColumn
        /// </summary>
        public string ColumnAlias
        {
            get { return alias; }
        }

        /// <summary>
        /// Sql Expression
        /// </summary>
        internal SqlExpression Expression
        {
            get { return expr; }
        }

        /// <summary>
        /// ±í
        /// </summary>
        public FromTerm Table
        {
            get
            {
                return _table;
            }
        }
    }
}
