using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public class FormulaCounter : NeField
    {

        /// <summary>
        /// 厂家
        /// </summary>
        public string Vendor
        {
            set;
            get;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            set;
            get;
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 原始字段名
        /// </summary>
        public string CltFieldName { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string CounterName { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string CltTableName { get; set; }
        /// <summary>
        /// 时间聚合方式
        /// </summary>
        public string TimeAggregation { get; set; }
        /// <summary>
        ///  网元聚合方式
        /// </summary>
        public string NeAggregation { get; set; }
        /// <summary>
        /// Key=Ne_Type_Ne_Level,Value =Table
        /// </summary>
        [Ignore]
        public Dictionary<string, string> TableName { get; set; }
        /// <summary>
        /// 其他特殊级别表
        /// </summary>
        public string TableNameSpecial { get; set; }
    }
}
