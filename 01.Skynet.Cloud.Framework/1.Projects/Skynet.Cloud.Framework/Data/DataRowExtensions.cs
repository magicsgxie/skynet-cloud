using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;

using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{

    /// <summary>
    /// This static class defines the DataRow extension methods.
    /// </summary>
    public static class DataRowExtensions
    {

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="columnName">The input column name specifying which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, string columnName)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[columnName]);
        }

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="column">The input DataColumn specifying which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, DataColumn column)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[column]);
        }

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="columnIndex">The input ordinal specifying which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, int columnIndex)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[columnIndex]);
        }

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="columnIndex">The input ordinal specifying which row value to retrieve.</param>
        /// <param name="version">The DataRow version for which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, int columnIndex, DataRowVersion version)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[columnIndex, version]);
        }

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="columnName">The input column name specifying which row value to retrieve.</param>
        /// <param name="version">The DataRow version for which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, string columnName, DataRowVersion version)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[columnName, version]);
        }

        /// <summary>
        /// This method provides access to the values in each of the columns in a given row. 
        /// This method makes casts unnecessary when accessing columns. 
        /// Additionally, Field supports nullable types and maps automatically between DBNull and 
        /// Nullable when the generic type is nullable. 
        /// </summary>
        /// <param name="row">The input DataRow</param>
        /// <param name="column">The input DataColumn specifying which row value to retrieve.</param>
        /// <param name="version">The DataRow version for which row value to retrieve.</param>
        /// <returns>The DataRow value for the column specified.</returns> 
        public static T Field<T>(this DataRow row, DataColumn column, DataRowVersion version)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            return UnboxT<T>.s_unbox(row[column, version]);
        }

        /// <summary>
        /// This method sets a new value for the specified column for the DataRow it�s called on. 
        /// </summary>
        /// <param name="row">The input DataRow.</param>
        /// <param name="columnIndex">The input ordinal specifying which row value to set.</param>
        /// <param name="value">The new row value for the specified column.</param>
        public static void SetField<T>(this DataRow row, int columnIndex, T value)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            row[columnIndex] = (object)value ?? DBNull.Value;
        }

        /// <summary>
        /// This method sets a new value for the specified column for the DataRow it�s called on. 
        /// </summary>
        /// <param name="row">The input DataRow.</param>
        /// <param name="columnName">The input column name specifying which row value to retrieve.</param>
        /// <param name="value">The new row value for the specified column.</param>
        public static void SetField<T>(this DataRow row, string columnName, T value)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            row[columnName] = (object)value ?? DBNull.Value;
        }

        /// <summary>
        /// This method sets a new value for the specified column for the DataRow it�s called on. 
        /// </summary>
        /// <param name="row">The input DataRow.</param>
        /// <param name="column">The input DataColumn specifying which row value to retrieve.</param>
        /// <param name="value">The new row value for the specified column.</param>
        public static void SetField<T>(this DataRow row, DataColumn column, T value)
        {
            DataSetUtil.CheckArgumentNull(row, nameof(row));
            row[column] = (object)value ?? DBNull.Value;
        }

        private static class UnboxT<T>
        {
            internal static readonly Converter<object, T> s_unbox = Create();

            private static Converter<object, T> Create()
            {
                if (default(T) == null)
                    return ReferenceOrNullableField;
                else
                    return ValueField;
            }

            private static T ReferenceOrNullableField(object value)
            {
                return ((DBNull.Value == value) ? default(T) : (T)value);
            }

            private static T ValueField(object value)
            {
                if (DBNull.Value == value)
                {
                    throw DataSetUtil.InvalidCast(string.Format("DataSetLinq_NonNullableCast:{0}", typeof(T).ToString()));
                }
                return (T)value;
            }
        }

        /// <summary>
        /// Read entity list by reader
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="reader">data reader</param>
        /// <returns>entity</returns>
        public static IEnumerable<T> ToList<T>(this DbDataReader reader) where T : new()
        {
            IList<T> list = new List<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    T inst = new T();
                    const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                    var items = from m in typeof(T)
                                   .GetProperties(bindingFlags)
                                   .Where(p => p.CanRead)
                                   .Where(p => p.CanWrite)
                                   .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                                   .Distinct()
                                select m;
                    foreach (var pi in items)
                    {
                        var obj = new object();
                        var columnName = pi.Name.ToUnderlineName();
                        try
                        {
                            obj = reader[columnName];
                        }
                        catch (Exception ex)
                        {
                            if (!reader.IsClosed)
                                reader.Close();
                            throw ex;
                            //continue;
                        }

                        if (obj == DBNull.Value || obj == null)
                            continue;
                        var si = pi.GetSetMethod();
                        if (si == null)
                            continue;
                        pi.SetValue(inst, obj, null);
                    }
                    list.Add(inst);
                }
            }
            if (!reader.IsClosed)
                reader.Close();
            return list;
        }

        /// <summary>
        /// Read entity list by reader
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="reader">data reader</param>
        /// <returns>entity</returns>
        public static T SingleOrDefault<T>(this DbDataReader reader) where T : new()
        {
            //IList<T> list = new List<T>();
       
            using (reader)
            {
                if (reader.Read())
                {
                    T inst = new T();
                    const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                    var items = from m in typeof(T)
                                   .GetProperties(bindingFlags)
                                   .Where(p => p.CanRead)
                                   .Where(p => p.CanWrite)
                                   .Where(p => !p.HasAttribute<IgnoreAttribute>(true))
                                   .Distinct()
                                select m;
                    foreach (var pi in items)
                    {
                        var obj = new object();
                        var columnName = pi.Name.ToUnderlineName();
                        try
                        {
                            obj = reader[columnName];
                        }
                        catch (Exception ex)
                        {
                            if (!reader.IsClosed)
                                reader.Close();
                            throw ex;
                        }

                        if (obj == DBNull.Value || obj == null)
                            continue;
                        var si = pi.GetSetMethod();
                        if (si == null)
                            continue;
                        pi.SetValue(inst, obj, null);
                    }
                  
                    return inst;
                    //list.Add(inst);
                } else
                {
                    return default(T);
                }
            }
            //if (!reader.IsClosed)
            //    reader.Close();
            //return list;
        }
    }
}
