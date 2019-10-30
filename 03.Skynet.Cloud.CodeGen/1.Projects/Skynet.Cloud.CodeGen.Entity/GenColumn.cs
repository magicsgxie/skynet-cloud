using System;

namespace UWay.Skynet.Cloud.CodeGen.Entity
{
    public class GenColumn
    {
        /// <summary>
        /// 列表
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }


        /// <summary>
        /// 驼峰属性
        /// </summary>
        public string CaseAttrName { get; set; }

        /// <summary>
        /// 普通属性
        /// </summary>
        public string LowerAttrName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrType { get; set; }

        /// <summary>
        /// 其他信息
        /// </summary>
        public string Extra { get; set; }
    }
}
