using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public class FormulaInfo
    {
        public IEnumerable<BaseFormula> BaseFormulaCollection { get; set; }
        public Dictionary<int, GroupFormula> FormulaGroupCollection { get; set; }

        public IEnumerable<FormulaCounter> FormulaCounterCollection { get; set; }

        public IEnumerable<IndicatorDictItem> DictItemCollection { get; set; }
        public IEnumerable<RelNeLevelInfo> Ne_Level_RelationInfoCollection { get; set; }
        public IEnumerable<RelEnumInfo> Enum_Relation_Collection { get; set; }
        /// <summary>
        /// 载波列表
        /// </summary>
        public IEnumerable<IndicatorDictItem> CarrCollection { get; set; }
        /// <summary>
        /// 数据源列表
        /// </summary>
        public IEnumerable<CfgDataSource> DataSourceCollection { get; set; }

        public IEnumerable<BusyInfo> BusyInfoCollection { get; set; }
    }
}
