// ======================================================================
// 
//           filename : ExcelBuilder.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-11 13:51
//           
//          
//           
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