using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    public class FormulaGroup
    {
        /// <summary>
        /// 分组编号
        /// </summary>
        public long GroupID { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public long Parent_ID { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        [Ignore]
        public int HasChild { get; set; }

        /// <summary>
        /// 分组下的公式
        /// </summary>
        [Ignore]
        public List<BaseFormula> Formulas { get; set; }

    }
}
