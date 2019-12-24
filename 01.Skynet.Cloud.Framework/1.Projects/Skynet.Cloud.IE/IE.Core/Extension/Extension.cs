// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司.
//           All rights reserved.
//           filename : Extension.cs.
//           description :
//           created by magic.s.g.xie at  2019-09-11 13:51
//           QQ：279218456（编程交流）
// 
// ======================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace UWay.Skynet.Cloud.IE.Core.Extension
{
    public static class Extension
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        /// <summary>
        /// 将集合转成DataTable.
        /// </summary>
        /// <returns>DataTable.</returns>
        public static DataTable ToDataTable<T>(this ICollection<T> source)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p =>
                new DataColumn(p.PropertyType.GetAttribute<ExporterAttribute>()?.Name ?? p.GetDisplayName() ?? p.Name,
                    p.PropertyType)).ToArray());
            if (source.Count <= 0) return dt;

            for (var i = 0; i < source.Count; i++)
            {
                var tempList = new ArrayList();
                foreach (var obj in props.Select(pi => pi.GetValue(source.ElementAt(i), null))) tempList.Add(obj);
                var array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }

            return dt;
        }

    }
}