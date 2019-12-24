// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : ExcelBuilder.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//          
//          
//           QQ：279218456（编程交流）
//           
// 
// ======================================================================

using System;

namespace UWay.Skynet.Cloud.IE.Excel.Builder
{
    public class ExcelBuilder
    {
        private ExcelBuilder()
        {
        }

        private Func<string, string> ColumnHeaderStringFunc { get; set; }

        /// <summary>
        ///     创建实例
        /// </summary>
        /// <returns></returns>
        public static ExcelBuilder Create()
        {
            return new ExcelBuilder();
        }

        /// <summary>
        ///     多语言处理
        /// </summary>
        /// <param name="columnHeaderStringFunc"></param>
        /// <returns></returns>
        public ExcelBuilder WithColumnHeaderStringFunc(Func<string, string>
            columnHeaderStringFunc)
        {
            ColumnHeaderStringFunc = columnHeaderStringFunc;
            return this;
        }

        /// <summary>
        ///     确定设置
        /// </summary>
        public void Build()
        {
            if (ColumnHeaderStringFunc != null)
                ExcelExporter.ColumnHeaderStringFunc = ColumnHeaderStringFunc;
        }
    }
}