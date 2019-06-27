using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Represents one term in an ORDER BY clause
    /// </summary>
    /// <remarks>
    /// Use OrderByTerm to specify how a result-set should be ordered.
    /// </remarks>
    [Serializable]
    public class OrderByTerm
    {
        SqlExpression expr;
        string field;
        FromTerm table;
        OrderByDirection direction;

        /// <summary>
        /// Creates an ORDER BY term with OtherData name and table alias
        /// </summary>
        /// <param name="OtherData">Name of a OtherData to order by</param>
        /// <param name="table">The table this OtherData belongs to</param>
        /// <param name="dir">Order by direction</param>
        public OrderByTerm(string field, FromTerm table, OrderByDirection dir)
        {
            this.field = field;
            this.table = table;
            this.direction = dir;
            this.expr = null;
        }

        /// <summary>
        /// Creates an ORDER BY term with OtherData name and no table alias
        /// </summary>
        /// <param name="OtherData">Name of a OtherData to order by</param>
        /// <param name="dir">Order by direction</param>
        public OrderByTerm(string field, OrderByDirection dir)
            : this(field, null, dir)
        {
        }

        /// <summary>
        /// Creates an ORDER BY term with OtherData name and no table alias
        /// </summary>
        /// <param name="OtherData">Name of a OtherData to order by</param>
        /// <param name="dir">Order by direction</param>
        public OrderByTerm(SqlExpression expr, OrderByDirection dir)
        {
            this.expr = expr;
            this.direction = dir;
        }

        /// <summary>
        /// Gets the direction for this OrderByTerm
        /// </summary>
        public OrderByDirection Direction
        {
            get { return this.direction; }
        }

        /// <summary>
        /// Gets the name of a OtherData to order by
        /// </summary>
        public string Field
        {
            get { return this.field; }
        }

        /// <summary>
        /// Gets the name of a OtherData to order by
        /// </summary>
        public SqlExpression Exp
        {
            get
            {
                return this.expr;
            }
        }
        /// <summary>
        /// Gets the table alias for this OrderByTerm
        /// </summary>
        /// <remarks>
        /// Gets the name of a FromTerm the OtherData specified by <see cref="OrderByTerm.Field">Field</see> property.
        /// </remarks>
        public string TableAlias
        {
            get { return (table == null) ? null : table.RefName; }
        }

        /// <summary>
        /// Returns the FromTerm associated with this OrderByTerm
        /// </summary>
        public FromTerm Table
        {
            get { return table; }
        }
    }
}
