using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Reflection;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataReaderExtensions
    {
        //private static readonly BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;
        /// <summary>
        /// 
        /// </summary>
        /// <modelExp name="reader"></modelExp>
        /// <returns></returns>
        public static DataTable ToDataTable(this IDataReader reader)
        {
            Guard.NotNull(reader, "reader");
            var tb = new DataTable("Table1");
            var fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
                tb.Columns.Add(reader.GetName(i).Replace("_", ""), reader.GetFieldType(i));

            using (reader)
            {
                while (reader.Read())
                {
                    var row = new object[fieldCount];
                    reader.GetValues(row);
                    tb.Rows.Add(row);
                }
            }

            return tb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <modelExp name="reader"></modelExp>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToList<TEntity>(this IDataReader reader)
        {
            Guard.NotNull(reader, "reader");
            var type = typeof(TEntity);

            var items = new List<TEntity>();
            if (Types.IDictionaryOfStringAndObject.IsAssignableFrom(type) || type == Types.Object)
            {
                var tb = new Table(reader);
                while (reader.Read())
                {
                    var row = new object[tb.Fields.Count];
                    reader.GetValues(row);
                    items.Add((TEntity)(object)new DynamicRow(tb, row));
                }
                return items;
            }


            var fun = RowMapper.GetRowMapper<TEntity>(reader);
            while (reader.Read())
                items.Add(fun(reader));
            return items;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <modelExp name="reader"></modelExp>
        ///// <returns></returns>
        //public static IEnumerable<TEntity> ToList<TEntity>(this DbDataReader reader)
        //{
        //    Guard.NotNull(reader, "reader");
        //    var type = typeof(TEntity);

        //    var items = new List<TEntity>();
        //    if (Types.IDictionaryOfStringAndObject.IsAssignableFrom(type) || type == Types.Object)
        //    {
        //        var tb = new Table(reader);
        //        while (reader.Read())
        //        {
        //            var row = new object[tb.Fields.Count];
        //            reader.GetValues(row);
        //            items.Add((TEntity)(object)new DynamicRow(tb, row));
        //        }
        //        return items;
        //    }


        //    var fun = RowMapper.GetRowMapper<TEntity>(reader);
        //    while (reader.Read())
        //        items.Add(fun(reader));
        //    return items;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <modelExp name="reader"></modelExp>
        ///// <returns></returns>
        //public static TEntity SingleOrDefault<TEntity>(this DbDataReader reader)
        //{
        //    Guard.NotNull(reader, "reader");
        //    var type = typeof(TEntity);

        //    var items = new List<TEntity>();
        //    if (Types.IDictionaryOfStringAndObject.IsAssignableFrom(type) || type == Types.Object)
        //    {
        //        var tb = new Table(reader);
        //        while (reader.Read())
        //        {
        //            var row = new object[tb.Fields.Count];
        //            reader.GetValues(row);
        //            items.Add((TEntity)(object)new DynamicRow(tb, row));
        //        }
        //        return items;
        //    }


        //    var fun = RowMapper.GetRowMapper<TEntity>(reader);
        //    while (reader.Read())
        //        items.Add(fun(reader));
        //    return items;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <modelExp name="reader"></modelExp>
        /// <modelExp name="creator"></modelExp>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToList<TEntity>(this IDataReader reader, Func<IDataReader, TEntity> creator)
        {
            Guard.NotNull(reader, "reader");
            Guard.NotNull(creator, "creator");

            var items = new List<TEntity>();
            while (reader.Read())
            {
                var item = creator(reader);
                if (item != null)
                    items.Add(item);
            }

            return items;
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <modelExp name="tb"></modelExp>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToList<TEntity>(this DataTable tb) where TEntity : new()
        {
            return DataReaderExtensions.ToList<TEntity>(tb.CreateDataReader());
        }


    }
}
