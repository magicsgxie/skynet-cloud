﻿// ======================================================================
// 
//           filename : ExcelImporterAttribute.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//          
//           
//           
// 
// ======================================================================

using UWay.Skynet.Cloud.IE.Core;
using System;

namespace UWay.Skynet.Cloud.IE.Excel
{

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public class ExcelImporterAttribute : ImporterAttribute
    {
        /// <summary>
        ///     指定Sheet名称(获取指定Sheet名称)
        ///     为空则自动获取第一个
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        ///     截止读取的列数（从1开始，如果已设置，则将支持空行以及特殊列）
        /// </summary>
        public int? EndColumnCount { get; set; }

        /// <summary>
        ///     是否标注错误（默认为true）
        /// </summary>
        public bool IsLabelingError { get; set; } = true;
    }
}