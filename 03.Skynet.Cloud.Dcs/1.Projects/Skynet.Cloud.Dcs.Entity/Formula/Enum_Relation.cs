using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Cfgs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public class Enum_Relation 
    {
        public string Source_Type { get; set; }
        public string Dict_Code { get; set; }
        public string Dest_Type { get; set; }
        public string Dest_Value { get; set; }
        public string Desp { get; set; }

        public string RedundanceFieldsString { get; set; }

        /// <summary>
        /// 冗余字段信息集合
        /// </summary>
        [Ignore]
        public List<DictItem> Redundance_Fields { get; set; }

        public string HideFields { get; set; }
        /// <summary>
        /// 隐藏字段列表
        /// </summary>
        [Ignore]
        public List<string> Hide_Fields { get; set; }

        /// <summary>
        /// 主键字段
        /// </summary>
        public string KeyField { get; set; }

        public string ExtensionFieldsString { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [Ignore]
        public List<string> ExtensionFields { get; set; }

        /// <summary>
        /// 扩展视图、表、SQL
        /// </summary>
        public string ExtensionTableView { get; set; }

        public string ExtensionRelationFieldsString { get; set; }

        /// <summary>
        /// 扩展视图、表关联字段
        /// </summary>
        [Ignore]
        public List<string> ExtensionRelationFields { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
    }
}
