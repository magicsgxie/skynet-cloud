using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.CodeGen.Entity
{
    public class GenTable
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        ///  主键
        /// </summary>
        public GenColumn PrimaryKey { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public IList<GenColumn> Columns { get; set; }

        /// <summary>
        /// 驼峰类型
        /// </summary>
        public string CaseClassName { get; set; }

        /// <summary>
        /// 普通类型
        /// </summary>
        public string LowerClassName { get; set; }

    }
}
