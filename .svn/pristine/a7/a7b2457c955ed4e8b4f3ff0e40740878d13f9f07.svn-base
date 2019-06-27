using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Represents a typed constant value.
    /// </summary>
    [Serializable]
    public class SqlConstant
    {
        SqlDataType type;
        object val;

        /// <summary>
        /// Creates a new SqlConstant instance
        /// </summary>
        /// <param name="type">Constant's date type</param>
        /// <param name="val">Constant's value</param>
        public SqlConstant(SqlDataType type, object val)
        {
            this.type = type;
            this.val = val;
        }

        /// <summary>
        /// Creates a SqlConstant which represents a numeric value.
        /// </summary>
        /// <param name="val">Value of the expression</param>
        /// <returns>A SqlConstant which represents a floating point value</returns>
        public static SqlConstant Number(double val)
        {
            return new SqlConstant(SqlDataType.Number, val);
        }

        /// <summary>
        /// Creates a SqlConstant which represents a numeric value.
        /// </summary>
        /// <param name="val">Value of the expression</param>
        /// <returns>A SqlConstant which represents a decimal value</returns>
        public static SqlConstant Number(decimal val)
        {
            return new SqlConstant(SqlDataType.Number, val);
        }

        /// <summary>
        /// Creates a SqlConstant which represents a numeric value.
        /// </summary>
        /// <param name="val">Value of the expression</param>
        /// <returns>A SqlConstant which represents a numeric value</returns>
        public static SqlConstant Number(int val)
        {
            return new SqlConstant(SqlDataType.Number, val);
        }

        /// <summary>
        /// Creates a SqlConstant which represents a textual value.
        /// </summary>
        /// <param name="val">Value of the expression</param>
        /// <returns>A SqlConstant which represents a textual value</returns>
        public static SqlConstant String(string val)
        {
            return new SqlConstant(SqlDataType.String, val);
        }


        /// <summary>
        /// Creates a SqlConstant which represents a date value.
        /// </summary>
        /// <param name="val">Value of the expression</param>
        /// <returns>A SqlConstant which represents a date value</returns>
        public static SqlConstant Date(DateTime val)
        {
            return new SqlConstant(SqlDataType.Date, val);
        }

        internal SqlDataType Type
        {
            get { return this.type; }
        }

        internal object Value
        {
            get { return this.val; }
        }
    }
}
